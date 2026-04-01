using Microsoft.AspNetCore.Mvc;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Exceptions;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateOrder(
        [FromBody] InitialOrderRequestDto orderRequestDto,
        CancellationToken cancellationToken)
    {
        if (orderRequestDto.BuyerUserId == Guid.Empty)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order creation failed.",
                Errors = ["Buyer user ID is required."]
            });
        }

        try
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderRequestDto, cancellationToken);

            return CreatedAtAction(
                nameof(GetOrderById),
                new { orderId = createdOrder.OrderId },
                new ApiResponseDto<OrderResponseDto>
                {
                    Success = true,
                    Message = "Order created successfully.",
                    Data = createdOrder
                });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order creation failed.",
                Errors = [exception.Message]
            });
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(new ErrorResponseDto
            {
                Success = false,
                Message = "Order creation failed.",
                Errors = [exception.Message]
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
            {
                Success = false,
                Message = "An unexpected error occurred while creating the order.",
                Errors = ["Please try again later."]
            });
        }
    }

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest(CreateErrorResponse("Order lookup failed.", "Order ID is required."));
        }

        return await ExecuteOrderActionAsync(
            () => _orderService.GetOrderByIdAsync(orderId, cancellationToken),
            order => Ok(CreateSuccessResponse("Order retrieved successfully.", order)),
            "Order lookup failed.",
            "An unexpected error occurred while retrieving the order.");
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(ApiResponseDto<IEnumerable<OrderResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrdersByUserId(Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest(CreateErrorResponse("Order lookup failed.", "User ID is required."));
        }

        return await ExecuteOrderActionAsync(
            () => _orderService.GetOrdersByUserIdAsync(userId, cancellationToken),
            orders => Ok(CreateSuccessResponse("Orders retrieved successfully.", orders)),
            "Order lookup failed.",
            "An unexpected error occurred while retrieving orders.");
    }

    [HttpPatch("{orderId:guid}/downpayment")]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PayDownpayment(
        Guid orderId,
        [FromBody] OrderPaymentRequestDto request,
        CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest(CreateErrorResponse("Downpayment failed.", "Order ID is required."));
        }

        return await ExecuteOrderActionAsync(
            () => _orderService.PayDownpaymentAsync(orderId, request.Amount, cancellationToken),
            order => Ok(CreateSuccessResponse("Downpayment applied successfully.", order)),
            "Downpayment failed.",
            "An unexpected error occurred while applying the downpayment.");
    }

    [HttpPatch("{orderId:guid}/final-payment")]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PayFinalPayment(
        Guid orderId,
        [FromBody] OrderPaymentRequestDto request,
        CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest(CreateErrorResponse("Final payment failed.", "Order ID is required."));
        }

        return await ExecuteOrderActionAsync(
            () => _orderService.PayFinalPaymentAsync(orderId, request.Amount, cancellationToken),
            order => Ok(CreateSuccessResponse("Final payment applied successfully.", order)),
            "Final payment failed.",
            "An unexpected error occurred while applying the final payment.");
    }

    [HttpPatch("{orderId:guid}/complete")]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CompleteOrder(Guid orderId, CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest(CreateErrorResponse("Order completion failed.", "Order ID is required."));
        }

        return await ExecuteOrderActionAsync(
            () => _orderService.CompleteOrderAsync(orderId, cancellationToken),
            order => Ok(CreateSuccessResponse("Order completed successfully.", order)),
            "Order completion failed.",
            "An unexpected error occurred while completing the order.");
    }

    [HttpPatch("{orderId:guid}/cancel")]
    [ProducesResponseType(typeof(ApiResponseDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CancelOrder(Guid orderId, CancellationToken cancellationToken)
    {
        if (orderId == Guid.Empty)
        {
            return BadRequest(CreateErrorResponse("Order cancellation failed.", "Order ID is required."));
        }

        return await ExecuteOrderActionAsync(
            () => _orderService.CancelOrderAsync(orderId, cancellationToken),
            order => Ok(CreateSuccessResponse("Order cancelled successfully.", order)),
            "Order cancellation failed.",
            "An unexpected error occurred while cancelling the order.");
    }

    private async Task<IActionResult> ExecuteOrderActionAsync<T>(
        Func<Task<T>> action,
        Func<T, IActionResult> onSuccess,
        string failureMessage,
        string unexpectedMessage)
    {
        try
        {
            var result = await action();
            return onSuccess(result);
        }
        catch (OrderOperationException exception)
        {
            return CreateOrderOperationErrorResult(exception, failureMessage);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(CreateErrorResponse(failureMessage, exception.Message));
        }
        catch
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                CreateErrorResponse(unexpectedMessage, "Please try again later."));
        }
    }

    private IActionResult CreateOrderOperationErrorResult(OrderOperationException exception, string failureMessage)
    {
        var errorResponse = CreateErrorResponse(failureMessage, exception.Message);

        return exception.FailureType switch
        {
            OrderOperationFailureType.NotFound => NotFound(errorResponse),
            OrderOperationFailureType.Conflict => Conflict(errorResponse),
            _ => BadRequest(errorResponse)
        };
    }

    private static ErrorResponseDto CreateErrorResponse(string message, params string[] errors)
    {
        return new ErrorResponseDto
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }

    private static ApiResponseDto<T> CreateSuccessResponse<T>(string message, T data)
    {
        return new ApiResponseDto<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
}
