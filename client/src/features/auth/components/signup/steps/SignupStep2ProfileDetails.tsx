import type { FormEvent } from "react";
import type { RoleCode, SignupStepComponentProps } from "../../../types/signup.types";

type SignupRoleOption = {
  value: RoleCode;
  label: string;
  description: string;
  icon: string;
};

const SIGNUP_ROLES: SignupRoleOption[] = [
  {
    value: "BUYER",
    label: "Buyer",
    description: "Purchase fresh produce for restaurants, resellers, or food operations.",
    icon: "shopping_cart",
  },
  {
    value: "FARMER",
    label: "Farmer",
    description: "Register your farm and prepare your products for marketplace participation.",
    icon: "agriculture",
  },
];

export function SignupStep2ProfileDetails({
  step,
  form,
  errors,
  onContinue,
  onBack,
  onToggleRole,
}: SignupStepComponentProps) {
  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onContinue();
  }

  return (
    <form className="space-y-6 sm:space-y-8" onSubmit={handleSubmit}>
      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
        {SIGNUP_ROLES.map((role) => {
          const isActive =
            role.value === "BUYER"
              ? form.buyerApplication.isSelected
              : form.farmerApplication.isSelected;

          return (
            <button
              key={role.value}
              type="button"
              onClick={() => onToggleRole(role.value)}
              className={`group cursor-pointer rounded-2xl p-4 text-left transition-all sm:p-5 ${
                isActive
                  ? "border-2 border-agri-earth bg-white ring-1 ring-agri-earth shadow-lg shadow-agri-leaf/10"
                  : "border-2 border-slate-100 bg-stone-50 hover:border-slate-200"
              }`}
            >
              <div className="mb-4 flex items-start justify-between">
                <div
                  className={`flex size-12 items-center justify-center rounded-xl ${
                    isActive
                      ? "bg-agri-leaf/10 text-agri-leaf"
                      : "bg-slate-200/50 text-slate-500"
                  }`}
                >
                  <span className="material-symbols-outlined text-2xl sm:text-3xl">
                    {role.icon}
                  </span>
                </div>

                {isActive ? (
                  <span className="flex size-6 items-center justify-center rounded-full bg-agri-earth text-white">
                    <span className="material-symbols-outlined text-sm">check</span>
                  </span>
                ) : null}
              </div>

              <h3 className="text-base font-bold text-slate-900 sm:text-lg">{role.label}</h3>
              <p className="mt-1 text-sm text-slate-500">{role.description}</p>
            </button>
          );
        })}
      </div>

      {errors.roles ? <p className="text-sm text-red-600">{errors.roles}</p> : null}

      <div className="rounded-2xl border border-slate-100 bg-agri-light p-5 text-sm text-slate-600">
        <p className="font-semibold text-slate-800">Role application policy</p>
        <p className="mt-2">
          Selected roles will be submitted as pending applications. You can apply for
          both BUYER and FARMER under the same account.
        </p>
      </div>

      <div className="space-y-4 pt-4 sm:pt-6">
        <button
          type="submit"
          className="flex w-full items-center justify-center gap-2 rounded-full bg-agri-accent py-3.5 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition-all hover:scale-[1.01] hover:bg-agri-accent/90 active:scale-[0.99] sm:py-4 sm:text-base"
        >
          {step.ctaLabel}
          <span className="material-symbols-outlined text-xl">arrow_forward</span>
        </button>

        <div className="text-center">
          <button
            type="button"
            onClick={onBack}
            className="inline-flex items-center justify-center gap-1 text-sm font-semibold text-slate-500 transition-colors hover:text-agri-leaf"
          >
            <span className="material-symbols-outlined text-lg">arrow_back</span>
            Back
          </button>
        </div>
      </div>
    </form>
  );
}
