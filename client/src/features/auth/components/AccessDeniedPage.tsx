import { Link, useLocation } from "react-router-dom";
import { useSelector } from "react-redux";
import type { RootState } from "../../../store";
import { resolvePostLoginPath } from "../utils/redirects";

type AccessRouteState = {
  returnTo?: string;
  requiredRoles?: string[];
};

export function AccessDeniedPage() {
  const auth = useSelector((state: RootState) => state.auth);
  const location = useLocation();
  const routeState = (location.state ?? null) as AccessRouteState | null;

  return (
    <div className="flex min-h-screen items-center justify-center bg-agri-light px-6 py-12">
      <div className="w-full max-w-2xl rounded-3xl border border-slate-100 bg-white p-8 shadow-2xl shadow-agri-accent/5 sm:p-10">
        <p className="text-sm font-semibold tracking-[0.2em] text-agri-leaf uppercase">
          Access Denied
        </p>
        <h1 className="mt-4 font-display text-4xl font-extrabold text-slate-900">
          This page is outside your permitted workspace.
        </h1>
        <p className="mt-4 text-base leading-7 text-slate-600">
          You are signed in, but your current role assignment does not allow
          access to this route.
        </p>
        {routeState?.requiredRoles?.length ? (
          <div className="mt-6 rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-800">
            Required role: {routeState.requiredRoles.join(", ")}
          </div>
        ) : null}
        {auth.user?.roles?.length ? (
          <div className="mt-4 rounded-2xl border border-slate-200 bg-slate-50 px-4 py-3 text-sm text-slate-700">
            Your active roles: {auth.user.roles.join(", ")}
          </div>
        ) : null}
        {routeState?.returnTo ? (
          <p className="mt-4 text-sm text-slate-500">
            Attempted route: <span className="font-semibold">{routeState.returnTo}</span>
          </p>
        ) : null}
        <div className="mt-8 flex flex-col gap-3 sm:flex-row">
          <Link
            to={resolvePostLoginPath(auth.user?.roles)}
            className="inline-flex items-center justify-center rounded-full bg-agri-accent px-6 py-3 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition hover:bg-agri-accent/90"
          >
            Go to My Workspace
          </Link>
          <Link
            to="/"
            className="inline-flex items-center justify-center rounded-full border border-slate-200 px-6 py-3 text-sm font-semibold text-slate-700 transition hover:bg-slate-50"
          >
            Back to Home
          </Link>
        </div>
      </div>
    </div>
  );
}
