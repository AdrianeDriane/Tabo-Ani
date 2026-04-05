import { type PropsWithChildren, useEffect, useRef } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../../../store";
import { bootstrapSession } from "../authSlice";

export function AuthBootstrap({ children }: PropsWithChildren) {
  const dispatch = useDispatch<AppDispatch>();
  const hasBootstrapped = useRef(false);
  const bootstrapStatus = useSelector(
    (state: RootState) => state.auth.bootstrapStatus
  );

  useEffect(() => {
    if (hasBootstrapped.current) {
      return;
    }

    hasBootstrapped.current = true;
    void dispatch(bootstrapSession());
  }, [dispatch]);

  if (bootstrapStatus !== "ready") {
    return (
      <div className="flex min-h-screen items-center justify-center bg-agri-light px-6">
        <div className="w-full max-w-md rounded-3xl border border-slate-100 bg-white px-8 py-10 text-center shadow-2xl shadow-agri-accent/5">
          <p className="text-sm font-semibold tracking-[0.2em] text-agri-leaf uppercase">
            Tabo-Ani
          </p>
          <h1 className="mt-4 font-display text-3xl font-extrabold text-slate-900">
            Restoring Session
          </h1>
          <p className="mt-3 text-sm text-slate-500">
            Checking your authentication status.
          </p>
        </div>
      </div>
    );
  }

  return children;
}
