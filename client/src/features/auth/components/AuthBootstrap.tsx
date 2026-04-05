import { type PropsWithChildren, useEffect, useRef } from "react";
import { useDispatch } from "react-redux";
import type { AppDispatch } from "../../../store";
import { bootstrapSession } from "../authSlice";

export function AuthBootstrap({ children }: PropsWithChildren) {
  const dispatch = useDispatch<AppDispatch>();
  const hasBootstrapped = useRef(false);

  useEffect(() => {
    if (hasBootstrapped.current) {
      return;
    }

    hasBootstrapped.current = true;
    void dispatch(bootstrapSession());
  }, [dispatch]);

  return children;
}
