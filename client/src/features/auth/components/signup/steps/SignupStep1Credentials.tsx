import { useState } from "react";
import type { FormEvent } from "react";
import type { SignupStepComponentProps } from "../../../types/signup.types";

function resolveFieldError(
  errors: SignupStepComponentProps["errors"],
  field: "email" | "mobileNumber" | "password" | "confirmPassword" | "firstName" | "lastName" | "displayName",
) {
  return errors[field];
}

export function SignupStep1Credentials({
  step,
  form,
  errors,
  onContinue,
  onAccountFieldChange,
}: SignupStepComponentProps) {
  const [isPasswordVisible, setIsPasswordVisible] = useState(false);
  const [isConfirmPasswordVisible, setIsConfirmPasswordVisible] = useState(false);

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onContinue();
  }

  return (
    <form className="space-y-4 sm:space-y-5" onSubmit={handleSubmit}>
      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
        <div>
          <label
            htmlFor="signup-first-name"
            className="mb-1 block text-sm font-semibold text-slate-700"
          >
            First Name
          </label>
          <input
            id="signup-first-name"
            type="text"
            value={form.firstName}
            onChange={(event) => onAccountFieldChange("firstName", event.target.value)}
            placeholder="Juan"
            className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
          />
          {resolveFieldError(errors, "firstName") ? (
            <p className="mt-1 text-xs text-red-600">{errors.firstName}</p>
          ) : null}
        </div>

        <div>
          <label
            htmlFor="signup-last-name"
            className="mb-1 block text-sm font-semibold text-slate-700"
          >
            Last Name
          </label>
          <input
            id="signup-last-name"
            type="text"
            value={form.lastName}
            onChange={(event) => onAccountFieldChange("lastName", event.target.value)}
            placeholder="Dela Cruz"
            className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
          />
          {resolveFieldError(errors, "lastName") ? (
            <p className="mt-1 text-xs text-red-600">{errors.lastName}</p>
          ) : null}
        </div>
      </div>

      <div>
        <label
          htmlFor="signup-display-name"
          className="mb-1 block text-sm font-semibold text-slate-700"
        >
          Display Name <span className="text-slate-400">(Optional)</span>
        </label>
        <input
          id="signup-display-name"
          type="text"
          value={form.displayName}
          onChange={(event) => onAccountFieldChange("displayName", event.target.value)}
          placeholder="How you want to appear in the app"
          className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
        />
      </div>

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
          value={form.email}
          onChange={(event) => onAccountFieldChange("email", event.target.value)}
          placeholder="juandelacruz@email.com"
          className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
        />
        {resolveFieldError(errors, "email") ? (
          <p className="mt-1 text-xs text-red-600">{errors.email}</p>
        ) : null}
      </div>

      <div>
        <label
          htmlFor="signup-mobile"
          className="mb-1 block text-sm font-semibold text-slate-700"
        >
          Mobile Number <span className="text-slate-400">(Optional)</span>
        </label>
        <input
          id="signup-mobile"
          type="tel"
          value={form.mobileNumber}
          onChange={(event) => onAccountFieldChange("mobileNumber", event.target.value)}
          placeholder="+63 912 345 6789"
          className="w-full rounded-xl border border-slate-200 px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
        />
        <p className="mt-1 text-xs text-slate-500">
          Optional, but recommended for account recovery and support follow-up.
        </p>
        {resolveFieldError(errors, "mobileNumber") ? (
          <p className="mt-1 text-xs text-red-600">{errors.mobileNumber}</p>
        ) : null}
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
            value={form.password}
            onChange={(event) => onAccountFieldChange("password", event.target.value)}
            placeholder="At least 8 characters"
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
        {resolveFieldError(errors, "password") ? (
          <p className="mt-1 text-xs text-red-600">{errors.password}</p>
        ) : null}
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
            value={form.confirmPassword}
            onChange={(event) => onAccountFieldChange("confirmPassword", event.target.value)}
            placeholder="Re-enter your password"
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
        {resolveFieldError(errors, "confirmPassword") ? (
          <p className="mt-1 text-xs text-red-600">{errors.confirmPassword}</p>
        ) : null}
      </div>

      <div className="rounded-2xl border border-agri-leaf/10 bg-agri-leaf/5 p-4 text-sm text-slate-600">
        {step.helperText ?? "Use an email address you can verify after signup."}
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
  );
}
