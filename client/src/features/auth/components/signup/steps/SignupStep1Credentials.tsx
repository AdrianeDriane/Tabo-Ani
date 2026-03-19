import { useState } from "react";
import type { FormEvent } from "react";
import type { SignupStepComponentProps } from "../../../types/signup.types";

export function SignupStep1Credentials({
  step,
  onContinue,
}: SignupStepComponentProps) {
  const [isPasswordVisible, setIsPasswordVisible] = useState(false);
  const [isConfirmPasswordVisible, setIsConfirmPasswordVisible] = useState(false);

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onContinue();
  }

  return (
    <div className="space-y-5 sm:space-y-6">
      <button
        type="button"
        className="flex w-full items-center justify-center gap-3 rounded-xl border border-slate-200 px-4 py-2.5 text-sm font-semibold text-slate-700 transition-colors hover:bg-slate-50 sm:py-3 sm:text-base"
      >
        <svg viewBox="0 0 24 24" className="size-5" aria-hidden="true">
          <path
            d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
            fill="#4285F4"
          />
          <path
            d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
            fill="#34A853"
          />
          <path
            d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l3.66-2.84z"
            fill="#FBBC05"
          />
          <path
            d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
            fill="#EA4335"
          />
        </svg>
        Continue with Google
      </button>

      <div className="relative flex items-center py-2">
        <div className="grow border-t border-slate-200" />
        <span className="mx-4 shrink-0 text-sm text-slate-400">
          or register with email
        </span>
        <div className="grow border-t border-slate-200" />
      </div>

      <form className="space-y-4 sm:space-y-5" onSubmit={handleSubmit}>
        <div>
          <label
            htmlFor="signup-email"
            className="mb-1 block text-sm font-semibold text-slate-700"
          >
            Email Address
          </label>
          <input
            id="signup-email"
            type="email"
            placeholder="juandelacruz@email.com"
            className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
          />
        </div>

        <div>
          <label
            htmlFor="signup-mobile"
            className="mb-1 block text-sm font-semibold text-slate-700"
          >
            Mobile Number
          </label>
          <div className="flex flex-col gap-2 sm:flex-row">
            <div className="relative flex-1">
              <span className="absolute top-1/2 left-4 -translate-y-1/2 font-medium text-slate-500">
                +63
              </span>
              <input
                id="signup-mobile"
                type="tel"
                placeholder="912 345 6789"
                className="w-full rounded-xl border border-slate-200 py-3 pr-4 pl-12 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
              />
            </div>
            <button
              type="button"
              className="rounded-xl border border-agri-leaf/30 px-4 py-2 text-xs font-bold text-agri-leaf transition-colors hover:bg-agri-leaf/5 sm:text-sm"
            >
              Send OTP
            </button>
          </div>
        </div>

        <div>
          <label
            htmlFor="signup-password"
            className="mb-1 block text-sm font-semibold text-slate-700"
          >
            Password
          </label>
          <div className="relative">
            <input
              id="signup-password"
              type={isPasswordVisible ? "text" : "password"}
              placeholder="********"
              className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
            />
            <button
              type="button"
              aria-label={isPasswordVisible ? "Hide password" : "Show password"}
              className="absolute top-1/2 right-4 -translate-y-1/2 text-slate-400"
              onClick={() => setIsPasswordVisible((current) => !current)}
            >
              <span className="material-symbols-outlined">
                {isPasswordVisible ? "visibility_off" : "visibility"}
              </span>
            </button>
          </div>
        </div>

        <div>
          <label
            htmlFor="signup-confirm-password"
            className="mb-1 block text-sm font-semibold text-slate-700"
          >
            Confirm Password
          </label>
          <div className="relative">
            <input
              id="signup-confirm-password"
              type={isConfirmPasswordVisible ? "text" : "password"}
              placeholder="********"
              className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
            />
            <button
              type="button"
              aria-label={
                isConfirmPasswordVisible
                  ? "Hide confirm password"
                  : "Show confirm password"
              }
              className="absolute top-1/2 right-4 -translate-y-1/2 text-slate-400"
              onClick={() => setIsConfirmPasswordVisible((current) => !current)}
            >
              <span className="material-symbols-outlined">
                {isConfirmPasswordVisible ? "visibility_off" : "visibility"}
              </span>
            </button>
          </div>
        </div>

        <div className="pt-3 sm:pt-4">
          <button
            type="submit"
            className="w-full rounded-full bg-agri-accent py-3.5 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition-all hover:scale-[1.02] hover:bg-agri-accent/90 active:scale-[0.98] sm:py-4 sm:text-base"
          >
            {step.ctaLabel}
          </button>
        </div>
      </form>
    </div>
  );
}
