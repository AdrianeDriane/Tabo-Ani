import type { FormEvent } from "react";
import { useState } from "react";
import type { SignupStepComponentProps } from "../../../types/signup.types";

export function SignupStep4ReviewSubmit({
  step,
  onContinue,
  onBack,
}: SignupStepComponentProps) {
  const [hasAcceptedTerms, setHasAcceptedTerms] = useState(false);
  const [hasAcceptedPrivacy, setHasAcceptedPrivacy] = useState(false);

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (!hasAcceptedTerms || !hasAcceptedPrivacy) {
      return;
    }

    onContinue();
  }

  return (
    <div className="space-y-6">
      <div className="rounded-2xl border border-slate-100 bg-agri-light p-6">
        <div className="mb-4 flex items-center justify-between">
          <h4 className="font-bold text-slate-900">Your Account Details</h4>
          <button
            type="button"
            className="text-xs font-bold tracking-wider text-agri-leaf uppercase hover:underline"
          >
            Edit
          </button>
        </div>
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Email
            </span>
            <p className="text-sm font-medium text-slate-700">juan.delacruz@example.com</p>
          </div>
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Mobile
            </span>
            <p className="text-sm font-medium text-slate-700">+63 912 345 6789</p>
          </div>
        </div>
      </div>

      <div className="rounded-2xl border border-slate-100 bg-agri-light p-6">
        <div className="mb-4 flex items-center justify-between">
          <h4 className="font-bold text-slate-900">Your Profile Information</h4>
          <button
            type="button"
            className="text-xs font-bold tracking-wider text-agri-leaf uppercase hover:underline"
          >
            Edit
          </button>
        </div>
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Role
            </span>
            <p className="text-sm font-medium text-slate-700">Farmer</p>
          </div>
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Business Name
            </span>
            <p className="text-sm font-medium text-slate-700">De La Cruz Farms</p>
          </div>
          <div className="sm:col-span-2">
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Location
            </span>
            <p className="flex items-center gap-1 text-sm font-medium text-slate-700">
              <span className="material-symbols-outlined text-sm text-agri-leaf">
                location_on
              </span>
              Benguet, PH
            </p>
          </div>
        </div>
      </div>

      <div className="rounded-2xl border border-slate-100 bg-agri-light p-6">
        <div className="mb-4 flex items-center justify-between">
          <h4 className="font-bold text-slate-900">Identity Verification Status</h4>
          <button
            type="button"
            className="text-xs font-bold tracking-wider text-agri-leaf uppercase hover:underline"
          >
            Edit
          </button>
        </div>
        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              ID Type
            </span>
            <p className="text-sm font-medium text-slate-700">National ID</p>
          </div>
          <div>
            <span className="text-[10px] font-bold tracking-widest text-slate-400 uppercase">
              Status
            </span>
            <p className="flex items-center gap-1 text-sm font-bold text-agri-accent">
              <span className="material-symbols-outlined text-sm">schedule</span>
              Pending Review
            </p>
          </div>
        </div>
      </div>

      <form className="space-y-4 border-t border-slate-100 pt-4" onSubmit={handleSubmit}>
        <div className="flex items-start gap-3">
          <input
            id="signup-tos"
            type="checkbox"
            checked={hasAcceptedTerms}
            onChange={(event) => setHasAcceptedTerms(event.target.checked)}
            className="mt-1 size-5 rounded border-slate-300 text-agri-accent focus:ring-agri-accent"
          />
          <div className="flex-1">
            <label htmlFor="signup-tos" className="text-sm font-semibold text-slate-700">
              I understand and agree to the Tabo-Ani Terms of Service
            </label>
            <p className="text-xs text-slate-500">
              This ensures a secure and fair platform for all users.
            </p>
          </div>
        </div>

        <div className="flex items-start gap-3">
          <input
            id="signup-privacy"
            type="checkbox"
            checked={hasAcceptedPrivacy}
            onChange={(event) => setHasAcceptedPrivacy(event.target.checked)}
            className="mt-1 size-5 rounded border-slate-300 text-agri-accent focus:ring-agri-accent"
          />
          <div className="flex-1">
            <label htmlFor="signup-privacy" className="text-sm font-semibold text-slate-700">
              I acknowledge and accept the Privacy Policy
            </label>
            <p className="text-xs text-slate-500">
              We are committed to protecting your data and privacy.
            </p>
          </div>
        </div>

        <div className="flex flex-col gap-4 pt-6 sm:flex-row">
          <button
            type="submit"
            disabled={!hasAcceptedTerms || !hasAcceptedPrivacy}
            className="flex-1 rounded-full bg-agri-accent py-4 font-bold text-white shadow-lg shadow-agri-accent/20 transition-all hover:scale-[1.02] hover:bg-agri-accent/90 active:scale-[0.98] disabled:cursor-not-allowed disabled:bg-agri-accent/40 disabled:shadow-none"
          >
            {step.ctaLabel}
          </button>
          <button
            type="button"
            onClick={onBack}
            className="flex-1 rounded-full bg-slate-100 py-4 font-bold text-slate-600 transition-all hover:bg-slate-200"
          >
            Review KYC Details
          </button>
        </div>
      </form>
    </div>
  );
}
