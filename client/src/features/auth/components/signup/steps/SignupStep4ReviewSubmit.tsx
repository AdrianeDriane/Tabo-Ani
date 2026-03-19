import type { SignupStepComponentProps } from "../../../types/signup.types";

export function SignupStep4ReviewSubmit({ step }: SignupStepComponentProps) {
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
          Final summary and submission confirmation will be implemented here.
        </p>
      </div>

      <button
        type="button"
        disabled
        className="w-full cursor-not-allowed rounded-full bg-agri-accent/40 py-4 font-bold text-white"
      >
        {step.ctaLabel}
      </button>
    </div>
  );
}
