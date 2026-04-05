// TODO(auth-check): Remove this temporary feature flag once the /auth-check QA route is deleted.
export const isAuthAccessCheckRouteEnabled =
  import.meta.env.DEV ||
  import.meta.env.VITE_ENABLE_AUTH_ACCESS_CHECK_ROUTE === "true";
