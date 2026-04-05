import { requestAuthenticatedJson } from "../../../api/authenticatedRequest";
import type { ApiResponse } from "../../../types/api.types";

// TODO(auth-check): Remove these temporary QA/UAT access-check helpers before production cleanup.
export async function checkAdminListingsAccess() {
  return requestAuthenticatedJson<ApiResponse<unknown>>(
    "/api/v1/admin/marketplace/listings?Page=1&PageSize=1",
    {
      method: "GET",
    }
  );
}

export async function checkBuyerOrdersAccess(userId: string) {
  return requestAuthenticatedJson<ApiResponse<unknown>>(
    `/api/v1/orders/user/${userId}`,
    {
      method: "GET",
    }
  );
}

export async function checkBuyerCartAccess(userId: string) {
  return requestAuthenticatedJson<ApiResponse<unknown>>(
    `/api/v1/users/${userId}/cart`,
    {
      method: "GET",
    }
  );
}
