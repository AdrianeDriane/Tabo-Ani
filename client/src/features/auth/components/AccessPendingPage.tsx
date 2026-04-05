import { Link, useLocation } from "react-router-dom";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../../store";
import { logoutUser } from "../authSlice";

type AccessRouteState = {
  returnTo?: string;
  requiredRoles?: string[];
};

export function AccessPendingPage() {
  const dispatch = useDispatch<AppDispatch>();
  const location = useLocation();
  const routeState = (location.state ?? null) as AccessRouteState | null;

  return (
    <div className="flex min-h-screen items-center justify-center bg-agri-light px-6 py-12">
      <div className="w-full max-w-2xl rounded-3xl border border-slate-100 bg-white p-8 shadow-2xl shadow-agri-accent/5 sm:p-10">
        <p className="text-sm font-semibold tracking-[0.2em] text-agri-leaf uppercase">
          Access Pending
        </p>
        <h1 className="mt-4 font-display text-4xl font-extrabold text-slate-900">
          Your account is active, but your workspace is not ready yet.
        </h1>
        <p className="mt-4 text-base leading-7 text-slate-600">
          You successfully signed in, but there are no active role grants
          available for this account yet. Once the operations team approves the
          required access, your dashboard will become available automatically.
        </p>
        {routeState?.requiredRoles?.length ? (
          <div className="mt-6 rounded-2xl border border-amber-200 bg-amber-50 px-4 py-3 text-sm text-amber-800">
            Requested access: {routeState.requiredRoles.join(", ")}
          </div>
        ) : null}
        {routeState?.returnTo ? (
          <p className="mt-4 text-sm text-slate-500">
            Attempted route: <span className="font-semibold">{routeState.returnTo}</span>
          </p>
        ) : null}
        <div className="mt-8 flex flex-col gap-3 sm:flex-row">
          <Link
            to="/"
            className="inline-flex items-center justify-center rounded-full bg-agri-accent px-6 py-3 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition hover:bg-agri-accent/90"
          >
            Back to Home
          </Link>
          <button
            type="button"
            onClick={() => void dispatch(logoutUser())}
            className="inline-flex items-center justify-center rounded-full border border-slate-200 px-6 py-3 text-sm font-semibold text-slate-700 transition hover:bg-slate-50"
          >
            Log Out
          </button>
        </div>
      </div>
    </div>
  );
}
