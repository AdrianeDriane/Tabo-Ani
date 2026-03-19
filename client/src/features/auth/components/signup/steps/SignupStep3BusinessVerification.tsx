import type { SignupStepComponentProps } from "../../../types/signup.types";

export function SignupStep3BusinessVerification({
  step,
  onContinue,
}: SignupStepComponentProps) {
  return (
    <div className="space-y-6">
      <div className="rounded-2xl border border-dashed border-slate-300 bg-slate-50 p-8 text-center">
        <p className="text-sm font-semibold tracking-widest text-agri-leaf uppercase">
          Step {step.id}
        </p>
        <h3 className="mt-2 font-display text-2xl font-bold text-slate-900">
          {step.title}
        </h3>
        <p className="mt-3 text-slate-600">
          Business verification upload and validation controls go here.
        </p>
      </div>

      <button
        type="button"
        onClick={onContinue}
        className="w-full rounded-full bg-agri-accent py-4 font-bold text-white shadow-lg shadow-agri-accent/20 transition-all hover:scale-[1.02] hover:bg-agri-accent/90 active:scale-[0.98]"
      >
        {step.ctaLabel}
      </button>
    </div>
  );
}
