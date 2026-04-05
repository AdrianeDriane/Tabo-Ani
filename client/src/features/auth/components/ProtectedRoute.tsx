import { Navigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";
import type { RootState } from "../../../store";
import type { SessionRoleCode } from "../types/auth.types";

type ProtectedRouteProps = {
  allowedRoles?: SessionRoleCode[];
};

export function ProtectedRoute({ allowedRoles }: ProtectedRouteProps) {
  const auth = useSelector((state: RootState) => state.auth);
  const currentRoles = auth.user?.roles ?? [];

  if (auth.bootstrapStatus !== "ready") {
    return null;
  }

  if (auth.sessionStatus !== "authenticated") {
    return <Navigate replace to="/login" />;
  }

  if (
    allowedRoles &&
    allowedRoles.length > 0 &&
    !allowedRoles.some((role) => currentRoles.includes(role))
  ) {
    return <Navigate replace to="/" />;
  }

  return <Outlet />;
}
