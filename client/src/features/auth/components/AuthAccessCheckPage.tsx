import { useState } from "react";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { isApiRequestError } from "../../../api/request";
import type { ApiResponse } from "../../../types/api.types";
import type { RootState } from "../../../store";
import {
  checkAdminListingsAccess,
  checkBuyerCartAccess,
  checkBuyerOrdersAccess,
} from "../api/authAccessCheck.api";
import type { SessionRoleCode } from "../types/auth.types";

type ApiCheckKey = "admin-listings" | "buyer-orders" | "buyer-cart";

type ApiCheckStatus =
  | {
      state: "idle";
    }
  | {
      state: "pending";
    }
  | {
      state: "success";
      message: string;
    }
  | {
      state: "reachable";
      message: string;
    }
  | {
      state: "blocked";
      message: string;
    };

type ApiCheckDefinition = {
  key: ApiCheckKey;
  label: string;
  requiredRole: SessionRoleCode;
  description: string;
  run: (userId: string) => Promise<ApiResponse<unknown>>;
};

const API_CHECKS: ApiCheckDefinition[] = [
  {
    key: "admin-listings",
    label: "Admin marketplace controller",
    requiredRole: "ADMIN",
    description:
      "Calls the real admin listings endpoint protected by backend RBAC.",
    run: () => checkAdminListingsAccess(),
  },
  {
    key: "buyer-orders",
    label: "Buyer orders controller",
    requiredRole: "BUYER",
    description:
      "Calls the real buyer orders endpoint protected by RBAC and ownership checks.",
    run: (userId) => checkBuyerOrdersAccess(userId),
  },
  {
    key: "buyer-cart",
    label: "Buyer cart controller",
    requiredRole: "BUYER",
    description:
      "Calls the real buyer cart endpoint protected by RBAC and ownership checks.",
    run: (userId) => checkBuyerCartAccess(userId),
  },
];

const ROUTE_CHECKS: Array<{
  label: string;
  path: string;
  requiredRole?: SessionRoleCode;
}> = [
  { label: "Admin dashboard route", path: "/admin", requiredRole: "ADMIN" },
  {
    label: "Buyer dashboard route",
    path: "/buyers/dashboard",
    requiredRole: "BUYER",
  },
  {
    label: "Farmer dashboard route",
    path: "/farmer/dashboard",
    requiredRole: "FARMER",
  },
  {
    label: "Distributor dashboard route",
    path: "/distributor/dashboard",
    requiredRole: "DISTRIBUTOR",
  },
];

const INITIAL_CHECK_STATE: Record<ApiCheckKey, ApiCheckStatus> = {
  "admin-listings": { state: "idle" },
  "buyer-orders": { state: "idle" },
  "buyer-cart": { state: "idle" },
};

// TODO(auth-check): Remove this temporary verification page after auth QA/UAT sign-off.
export function AuthAccessCheckPage() {
  const auth = useSelector((state: RootState) => state.auth);
  const [checkStates, setCheckStates] = useState(INITIAL_CHECK_STATE);

  const userId = auth.user?.userId ?? "";

  async function runApiCheck(check: ApiCheckDefinition) {
    setCheckStates((current) => ({
      ...current,
      [check.key]: { state: "pending" },
    }));

    try {
      const response = await check.run(userId);
      setCheckStates((current) => ({
        ...current,
        [check.key]: {
          state: "success",
          message: response.message,
        },
      }));
    } catch (error) {
      if (isApiRequestError(error)) {
        const message = error.message || "Request failed.";

        setCheckStates((current) => ({
          ...current,
          [check.key]:
            error.status === 401 || error.status === 403
              ? {
                  state: "blocked",
                  message: `${error.status}: ${message}`,
                }
              : {
                  state: "reachable",
                  message: `${error.status}: ${message}`,
                },
        }));
        return;
      }

      setCheckStates((current) => ({
        ...current,
        [check.key]: {
          state: "reachable",
          message: "The request reached the API, but the result could not be classified cleanly.",
        },
      }));
    }
  }

  return (
    <div className="min-h-screen bg-agri-light px-6 py-12">
      <div className="mx-auto max-w-5xl space-y-6">
        <div className="rounded-3xl border border-slate-100 bg-white p-8 shadow-2xl shadow-agri-accent/5">
          <p className="text-sm font-semibold tracking-[0.2em] text-agri-leaf uppercase">
            Auth Access Check
          </p>
          <h1 className="mt-4 font-display text-4xl font-extrabold text-slate-900">
            Frontend route and backend RBAC verification
          </h1>
          <p className="mt-4 max-w-3xl text-base leading-7 text-slate-600">
            This temporary page uses the real route guards and the real protected
            controllers. It does not rely on fake backend auth probe endpoints.
          </p>
          <div className="mt-6 grid gap-4 md:grid-cols-2">
            <InfoCard label="Current user" value={auth.user?.email ?? "Unknown"} />
            <InfoCard
              label="Active roles"
              value={auth.user?.roles.join(", ") || "None"}
            />
            <InfoCard
              label="User ID"
              value={auth.user?.userId ?? "Unavailable"}
            />
            <InfoCard
              label="Session expiry"
              value={auth.accessTokenExpiresAt ?? "Unavailable"}
            />
          </div>
        </div>

        <div className="rounded-3xl border border-slate-100 bg-white p-8 shadow-xl shadow-agri-accent/5">
          <p className="text-sm font-semibold tracking-[0.2em] text-slate-500 uppercase">
            Route Guard Checks
          </p>
          <div className="mt-4 grid gap-3 md:grid-cols-2">
            {ROUTE_CHECKS.map((route) => (
              <Link
                key={route.path}
                to={route.path}
                className="rounded-2xl border border-slate-200 px-4 py-4 text-sm text-slate-700 transition hover:border-agri-accent hover:bg-agri-light"
              >
                <p className="font-semibold text-slate-900">{route.label}</p>
                <p className="mt-1 text-xs uppercase tracking-wide text-slate-500">
                  {route.requiredRole ?? "Authenticated user"} route
                </p>
                <p className="mt-2 break-all font-mono text-xs text-slate-500">
                  {route.path}
                </p>
              </Link>
            ))}
          </div>
        </div>

        <div className="rounded-3xl border border-slate-100 bg-white p-8 shadow-xl shadow-agri-accent/5">
          <p className="text-sm font-semibold tracking-[0.2em] text-slate-500 uppercase">
            Real Controller Checks
          </p>
          <div className="mt-4 space-y-4">
            {API_CHECKS.map((check) => {
              const checkState = checkStates[check.key];
              const canRun = Boolean(userId);

              return (
                <div
                  key={check.key}
                  className="rounded-2xl border border-slate-200 p-5"
                >
                  <div className="flex flex-col gap-4 lg:flex-row lg:items-center lg:justify-between">
                    <div className="space-y-2">
                      <p className="text-lg font-semibold text-slate-900">
                        {check.label}
                      </p>
                      <p className="text-sm text-slate-600">
                        {check.description}
                      </p>
                      <p className="text-xs font-semibold tracking-wide text-slate-500 uppercase">
                        Required role: {check.requiredRole}
                      </p>
                    </div>
                    <button
                      type="button"
                      disabled={!canRun || checkState.state === "pending"}
                      onClick={() => void runApiCheck(check)}
                      className="inline-flex items-center justify-center rounded-full bg-agri-accent px-5 py-3 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition hover:bg-agri-accent/90 disabled:cursor-not-allowed disabled:opacity-60"
                    >
                      {checkState.state === "pending" ? "Checking..." : "Run check"}
                    </button>
                  </div>
                  <CheckStatusBanner status={checkState} />
                </div>
              );
            })}
          </div>
          <div className="mt-6 rounded-2xl border border-amber-200 bg-amber-50 px-4 py-3 text-sm text-amber-800">
            Farmer and distributor are not using fake controller probes here.
            The current API surface has real buyer/admin checks available from a
            generic page, while farmer routes require a real `farmerProfileId`
            and there is currently no distributor-specific protected controller
            to call.
          </div>
          <p className="mt-4 text-xs font-semibold tracking-wide text-slate-400 uppercase">
            TODO(auth-check): Remove this temporary page after auth QA/UAT sign-off.
          </p>
        </div>
      </div>
    </div>
  );
}

type InfoCardProps = {
  label: string;
  value: string;
};

function InfoCard({ label, value }: InfoCardProps) {
  return (
    <div className="rounded-2xl border border-slate-200 bg-slate-50 px-4 py-3">
      <dt className="text-xs font-semibold tracking-[0.18em] text-slate-400 uppercase">
        {label}
      </dt>
      <dd className="mt-2 break-all text-sm font-semibold text-slate-800">
        {value}
      </dd>
    </div>
  );
}

type CheckStatusBannerProps = {
  status: ApiCheckStatus;
};

function CheckStatusBanner({ status }: CheckStatusBannerProps) {
  if (status.state === "idle" || status.state === "pending") {
    return null;
  }

  const toneClassName =
    status.state === "success"
      ? "border-green-200 bg-green-50 text-green-800"
      : status.state === "blocked"
        ? "border-red-200 bg-red-50 text-red-800"
        : "border-amber-200 bg-amber-50 text-amber-800";

  return (
    <div className={`mt-4 rounded-2xl border px-4 py-3 text-sm ${toneClassName}`}>
      {status.message}
    </div>
  );
}
