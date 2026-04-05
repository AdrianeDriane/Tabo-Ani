# Auth Login and RBAC Guide

This project already has ASP.NET Core authentication middleware and authorization middleware in place.

Backend middleware wiring:
- `server/TaboAni.Api/Program.cs`
- `AddAuthentication(...).AddJwtBearer(...)`
- `AddAuthorization(...)`
- `app.UseAuthentication()`
- `app.UseAuthorization()`

Frontend auth bootstrap and route protection:
- `client/src/features/auth/components/AuthBootstrap.tsx`
- `client/src/features/auth/components/ProtectedRoute.tsx`

## What Exists Now

Backend:
- JWT bearer access-token authentication
- Refresh-token cookie flow
- Rate-limited login and refresh endpoints
- Origin validation for cookie-backed session endpoints
- Reusable role attribute: `RequireRoles`

Frontend:
- Startup session bootstrap through `GET /api/v1/auth/session`
- Centralized authenticated request helper with one refresh retry on `401`
- Protected routes with `allowedRoles`
- Access-pending and access-denied pages
- Production-style auth loading spinner instead of debug copy
- Temporary browser-visible auth access-check route for QA/UAT

## Backend RBAC Standard

Use `RoleCodes` and `RequireRolesAttribute` for controller or action protection.

Files:
- `server/TaboAni.Api/Application/Configuration/RoleCodes.cs`
- `server/TaboAni.Api/Api/Authorization/RequireRolesAttribute.cs`

Examples:

```csharp
[RequireRoles(RoleCodes.Admin)]
public sealed class AdminMarketplaceController : ControllerBase
{
}
```

```csharp
[RequireRoles(RoleCodes.Buyer, RoleCodes.Admin)]
[HttpGet("example")]
public IActionResult Example()
{
    return Ok();
}
```

Notes:
- `RequireRoles` is built on top of ASP.NET Core authorization. It does not replace the existing middleware.
- Keep ownership checks separate from RBAC. Role membership answers "can this role access this area?" Ownership guards answer "does this authenticated user own this resource?"
- Current real protected controllers:
  - `AdminMarketplaceController` -> `ADMIN`
  - `CartController` -> `BUYER` plus ownership
  - `OrdersController` -> `BUYER` plus ownership
  - `FarmerListingsController` -> `FARMER` plus ownership
- Current gap:
  - there is no distributor-specific protected controller yet

## Frontend Route Guard Standard

Use `ProtectedRoute` with `allowedRoles`.

File:
- `client/src/features/auth/components/ProtectedRoute.tsx`

Examples:

```tsx
{
  path: "admin",
  element: <ProtectedRoute allowedRoles={["ADMIN"]} />,
  children: [{ index: true, Component: AdminDashboard }],
}
```

```tsx
{
  element: <ProtectedRoute allowedRoles={["BUYER", "ADMIN"]} />,
  children: [{ path: "example", Component: ExamplePage }],
}
```

Behavior:
- Unauthenticated users are redirected to `/login`
- The attempted route is preserved in `returnTo`
- Authenticated users without the required role go to `/access-denied`
- Authenticated users with no usable role land on `/access-pending`
- Protected routes show the auth spinner while access is still being resolved

## Current Session Flow

Frontend bootstrap now calls:
- `GET /api/v1/auth/session`

Why this exists:
- A missing refresh cookie is treated as a normal anonymous state
- Expired or invalid refresh tokens are converted into a user-facing session-expired state
- Blocked accounts are converted into an account-blocked state
- Public routes no longer wait behind a full-screen auth status page

Relevant files:
- `server/TaboAni.Api/Api/Controllers/V1/AuthController.cs`
- `client/src/features/auth/authSlice.ts`
- `client/src/api/authenticatedRequest.ts`

## Temporary Frontend Access-Check Route

This route exists only to make auth and RBAC easy to verify in QA/UAT, without adding fake backend auth controllers.

Frontend access-check route:
- `/auth-check`

Relevant files:
- `client/src/features/auth/components/AuthAccessCheckPage.tsx`
- `client/src/features/auth/api/authAccessCheck.api.ts`
- `client/src/router.tsx`

This temporary route is intentionally:
- hidden from main navigation
- config-gated
- marked with `TODO(auth-check)` removal notes

What it verifies:
- real frontend route guards by linking to existing protected pages
- real backend RBAC by calling existing protected controllers

Current real API checks used by the page:
- `GET /api/v1/admin/marketplace/listings?Page=1&PageSize=1`
- `GET /api/v1/orders/user/{userId}`
- `GET /api/v1/users/{userId}/cart`

Why there is no farmer/distributor controller probe on this page:
- farmer endpoints require a real `farmerProfileId`, so there is no safe generic request from a shared frontend checker
- there is currently no distributor-specific protected controller in the API surface

## Access-Check Route Configuration

Frontend:
- `VITE_ENABLE_AUTH_ACCESS_CHECK_ROUTE=true`

Defaults:
- frontend access-check route is enabled in Vite dev mode, or when `VITE_ENABLE_AUTH_ACCESS_CHECK_ROUTE=true`

## QA / UAT Verification Steps

1. Sign in with a user that has at least one active role.
2. Visit `/auth-check`.
3. Visit the role-specific route that matches the signed-in user.
4. Use the real controller check buttons and confirm authorized roles get either:
   - a success response, or
   - a non-auth domain response such as `404`, which still proves RBAC allowed the request through
5. Use a role-mismatched account and confirm the same checks return `401` or `403`.
6. Visit a route for a role the user does not have and confirm the app redirects to `/access-denied`.
7. Sign in with an active account that has no active roles and confirm the app lands on `/access-pending`.
8. Remove or expire the session and confirm protected routes redirect to `/login`.

## Security / Hardening Notes

Implemented in this pass:
- login rate limiting
- refresh rate limiting
- origin checks on cookie-backed session endpoints
- centralized refresh-on-401 handling for authenticated frontend requests
- user-friendly login and session issue messaging

Still environment-dependent:
- production frontend origin configuration
- secure cookie settings
- live browser verification against the deployed API and Supabase-backed environment

## Removal Checklist for Temporary Access-Check Route

Search for:
- `TODO(auth-check)`

Remove:
- frontend `/auth-check` route entry in `client/src/router.tsx`
- `client/src/features/auth/components/AuthAccessCheckPage.tsx`
- `client/src/pages/auth/AuthAccessCheck.tsx`
- `client/src/features/auth/api/authAccessCheck.api.ts`
- `client/src/features/auth/utils/authAccessCheck.ts`

After removal:
- rerun frontend typecheck
- rerun backend build
