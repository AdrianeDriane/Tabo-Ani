import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import type { AppDispatch, RootState } from "../../../store";
import { bootstrapSession } from "../authSlice";
import type { SessionRoleCode } from "../types/auth.types";
import { AuthLoadingSpinner } from "./AuthLoadingSpinner";
import { resolveRoleMismatchPath } from "../utils/redirects";

type ProtectedRouteProps = {
  allowedRoles?: SessionRoleCode[];
};

export function ProtectedRoute({ allowedRoles }: ProtectedRouteProps) {
  const dispatch = useDispatch<AppDispatch>();
  const location = useLocation();
  const auth = useSelector((state: RootState) => state.auth);
  const currentRoles = auth.user?.roles ?? [];
  const requestedPath = `${location.pathname}${location.search}${location.hash}`;
  const isNetworkUnavailable = auth.lastIssue?.code === "network_unavailable";
  const isAccessTokenExpired =
    auth.sessionStatus === "authenticated" &&
    Boolean(auth.accessTokenExpiresAt) &&
    new Date(auth.accessTokenExpiresAt ?? "").getTime() <= Date.now();

  useEffect(() => {
    if (
      !isAccessTokenExpired ||
      auth.bootstrapStatus !== "ready" ||
      isNetworkUnavailable
    ) {
      return;
    }

    void dispatch(bootstrapSession());
  }, [auth.bootstrapStatus, dispatch, isAccessTokenExpired, isNetworkUnavailable]);

  if (auth.bootstrapStatus !== "ready") {
    return <AuthLoadingSpinner />;
  }

  if (isNetworkUnavailable && (!auth.user || isAccessTokenExpired)) {
    return (
      <div className="flex min-h-screen items-center justify-center bg-agri-light px-6 py-12">
        <div className="w-full max-w-lg rounded-3xl border border-slate-100 bg-white p-8 text-center shadow-2xl shadow-agri-accent/5">
          <h1 className="font-display text-3xl font-extrabold text-slate-900">
            We couldn&apos;t verify your access right now.
          </h1>
          <p className="mt-4 text-sm leading-7 text-slate-600">
            Check your connection and retry before continuing.
          </p>
          <button
            type="button"
            onClick={() => void dispatch(bootstrapSession())}
            className="mt-6 inline-flex items-center justify-center rounded-full bg-agri-accent px-6 py-3 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition hover:bg-agri-accent/90"
          >
            Retry
          </button>
        </div>
      </div>
    );
  }

  if (isAccessTokenExpired) {
    return <AuthLoadingSpinner />;
  }

  if (auth.sessionStatus !== "authenticated") {
    return (
      <Navigate
        replace
        to="/login"
        state={{ returnTo: requestedPath }}
      />
    );
  }

  if (
    allowedRoles &&
    allowedRoles.length > 0 &&
    !allowedRoles.some((role) => currentRoles.includes(role))
  ) {
    return (
      <Navigate
        replace
        to={resolveRoleMismatchPath(currentRoles)}
        state={{ returnTo: requestedPath, requiredRoles: allowedRoles }}
      />
    );
  }

  return <Outlet />;
}
