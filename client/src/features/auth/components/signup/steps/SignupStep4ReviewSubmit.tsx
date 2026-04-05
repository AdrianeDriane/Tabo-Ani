import type { FormEvent } from "react";
import { Link } from "react-router-dom";
import type { SignupStepComponentProps } from "../../../types/signup.types";
import {
  PRIVACY_POLICY_URL,
  TERMS_OF_SERVICE_URL,
} from "../../../constants/authPolicies";
import { composeLocationText } from "../../../utils/signup";

export function SignupStep4ReviewSubmit({
  step,
  form,
  errors,
  isSubmitting,
  isVerificationSubmitting,
  submitError,
  verificationError,
  verificationNotice,
  signupResponse,
  onBack,
  onAgreementChange,
  onSubmitSignup,
  onResendVerification,
}: SignupStepComponentProps) {
  const selectedRoles = [
    form.buyerApplication.isSelected ? "BUYER" : null,
    form.farmerApplication.isSelected ? "FARMER" : null,
  ].filter(Boolean);

  const buyerLocationText = composeLocationText(form.buyerApplication.location);
  const farmerLocationText = composeLocationText(
    form.farmerApplication.location
  );

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onSubmitSignup();
  }

  return (
    <div className="space-y-5 sm:space-y-6">
      <div className="rounded-2xl border border-slate-100 bg-agri-light p-5 sm:p-6">
        <h4 className="mb-4 font-bold text-slate-900">Account Details</h4>
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Full Name
            </span>
            <p className="text-sm font-medium text-slate-700">
              {form.firstName} {form.lastName}
            </p>
          </div>
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Email
            </span>
            <p className="text-sm font-medium text-slate-700">{form.email}</p>
          </div>
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Display Name
            </span>
            <p className="text-sm font-medium text-slate-700">
              {form.displayName.trim() || "Not provided"}
            </p>
          </div>
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Mobile
            </span>
            <p className="text-sm font-medium text-slate-700">
              {form.mobileNumber.trim() || "Not provided"}
            </p>
          </div>
        </div>
      </div>

      <div className="rounded-2xl border border-slate-100 bg-agri-light p-5 sm:p-6">
        <h4 className="mb-4 font-bold text-slate-900">Requested Roles</h4>
        <div className="space-y-4">
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Roles
            </span>
            <p className="text-sm font-medium text-slate-700">
              {selectedRoles.join(", ")}
            </p>
          </div>

          {form.buyerApplication.isSelected ? (
            <div className="rounded-xl border border-slate-100 bg-white p-4">
              <p className="text-sm font-bold text-slate-900">Buyer Profile</p>
              <p className="mt-2 text-sm text-slate-700">
                {form.buyerApplication.businessName} /{" "}
                {form.buyerApplication.businessType}
              </p>
              <p className="text-sm text-slate-500">{buyerLocationText}</p>
            </div>
          ) : null}

          {form.farmerApplication.isSelected ? (
            <div className="rounded-xl border border-slate-100 bg-white p-4">
              <p className="text-sm font-bold text-slate-900">Farmer Profile</p>
              <p className="mt-2 text-sm text-slate-700">
                {form.farmerApplication.farmName}
              </p>
              <p className="text-sm text-slate-500">{farmerLocationText}</p>
            </div>
          ) : null}
        </div>
      </div>

      {signupResponse ? null : (
        <>
          {submitError ? (
            <div className="rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
              {submitError}
            </div>
          ) : null}

          <form
            className="space-y-4 border-t border-slate-100 pt-4"
            onSubmit={handleSubmit}
          >
            <div className="flex items-start gap-3">
              <input
                id="signup-tos"
                type="checkbox"
                checked={form.hasAcceptedTerms}
                onChange={(event) =>
                  onAgreementChange("hasAcceptedTerms", event.target.checked)
                }
                className="mt-1 size-5 rounded border-slate-300 text-agri-accent focus:ring-agri-accent"
              />
              <div className="flex-1">
                <label
                  htmlFor="signup-tos"
                  className="text-sm font-semibold text-slate-700"
                >
                  I understand and agree to the Tabo-Ani Terms of Service
                </label>
              </div>
            </div>
            {errors.hasAcceptedTerms ? (
              <p className="text-xs text-red-600">{errors.hasAcceptedTerms}</p>
            ) : null}

            <div className="flex items-start gap-3">
              <input
                id="signup-privacy"
                type="checkbox"
                checked={form.hasAcceptedPrivacy}
                onChange={(event) =>
                  onAgreementChange("hasAcceptedPrivacy", event.target.checked)
                }
                className="mt-1 size-5 rounded border-slate-300 text-agri-accent focus:ring-agri-accent"
              />
              <div className="flex-1">
                <label
                  htmlFor="signup-privacy"
                  className="text-sm font-semibold text-slate-700"
                >
                  I acknowledge and accept the Privacy Policy
                </label>
              </div>
            </div>
            {errors.hasAcceptedPrivacy ? (
              <p className="text-xs text-red-600">
                {errors.hasAcceptedPrivacy}
              </p>
            ) : null}

            <p className="text-sm text-slate-500">
              Review the{" "}
              <a
                href={TERMS_OF_SERVICE_URL}
                target="_blank"
                rel="noreferrer"
                className="font-semibold text-agri-accent hover:underline"
              >
                Terms of Service
              </a>{" "}
              and{" "}
              <a
                href={PRIVACY_POLICY_URL}
                target="_blank"
                rel="noreferrer"
                className="font-semibold text-agri-accent hover:underline"
              >
                Privacy Policy
              </a>{" "}
              before creating your account.
            </p>

            <div className="flex flex-col gap-4 pt-6 sm:flex-row">
              <button
                type="submit"
                disabled={isSubmitting}
                className="flex-1 rounded-full bg-agri-accent py-3.5 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition-all hover:scale-[1.02] hover:bg-agri-accent/90 active:scale-[0.98] disabled:cursor-not-allowed disabled:bg-agri-accent/40 disabled:shadow-none sm:py-4 sm:text-base"
              >
                {isSubmitting ? "Creating account..." : step.ctaLabel}
              </button>
              <button
                type="button"
                onClick={onBack}
                className="flex-1 rounded-full bg-slate-100 py-3.5 text-sm font-bold text-slate-600 transition-all hover:bg-slate-200 sm:py-4 sm:text-base"
              >
                Back
              </button>
            </div>
          </form>
        </>
      )}

      {signupResponse ? (
        <section className="space-y-4 rounded-2xl border border-agri-leaf/20 bg-agri-leaf/5 p-5 sm:p-6">
          <div>
            <h4 className="font-bold text-slate-900">Check Your Email</h4>
            <p className="mt-2 text-sm text-slate-600">
              Your account has been created and is waiting for email
              verification. Open the verification link we sent to{" "}
              <strong>{signupResponse.email}</strong> to activate your account
              and move your role applications into review.
            </p>
          </div>

          <div className="rounded-xl border border-agri-green/20 bg-white px-4 py-3 text-sm text-slate-700">
            <p className="font-semibold text-slate-900">Account status</p>
            <p className="mt-1">Pending email verification</p>
          </div>

          {verificationNotice ? (
            <div className="rounded-xl border border-green-200 bg-green-50 px-4 py-3 text-sm text-green-700">
              {verificationNotice}
            </div>
          ) : null}

          {verificationError ? (
            <div className="rounded-xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
              {verificationError}
            </div>
          ) : null}

          <div className="rounded-xl border border-slate-200 bg-white px-4 py-3 text-sm text-slate-600">
            If you do not see the email within a few minutes, check your spam
            folder or request a fresh verification link.
          </div>

          <div className="flex flex-col gap-3 sm:flex-row">
            <button
              type="button"
              disabled={isVerificationSubmitting}
              onClick={onResendVerification}
              className="flex-1 rounded-full bg-agri-green py-3 text-sm font-bold text-white transition-colors hover:bg-agri-green/90 disabled:cursor-not-allowed disabled:opacity-60"
            >
              {isVerificationSubmitting
                ? "Sending..."
                : "Resend Verification Email"}
            </button>
            <Link
              to="/login"
              className="flex-1 rounded-full border border-agri-green/20 bg-white py-3 text-center text-sm font-bold text-agri-green transition-colors hover:bg-agri-green/5"
            >
              Go to Login
            </Link>
          </div>
        </section>
      ) : null}
    </div>
  );
}
