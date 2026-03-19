import type { FormEvent } from "react";
import { useState } from "react";
import type { SignupStepComponentProps } from "../../../types/signup.types";

type SignupRole = {
  value: string;
  label: string;
  description: string;
  icon: string;
};

const SIGNUP_ROLES: SignupRole[] = [
  {
    value: "farmer",
    label: "Farmer",
    description: "Cultivate and sell fresh produce directly to the market.",
    icon: "agriculture",
  },
  {
    value: "distributor",
    label: "Distributor",
    description: "Manage logistics and bridge the gap between farm and retail.",
    icon: "local_shipping",
  },
  {
    value: "buyer",
    label: "Buyer",
    description: "Purchase high-quality bulk agricultural products.",
    icon: "shopping_cart",
  },
  {
    value: "retailer",
    label: "Retailer",
    description: "Sell directly to local consumers and households.",
    icon: "storefront",
  },
];

export function SignupStep2ProfileDetails({
  step,
  onContinue,
  onBack,
}: SignupStepComponentProps) {
  const [selectedRole, setSelectedRole] = useState("farmer");

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onContinue();
  }

  return (
    <div className="space-y-8">
      <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
        {SIGNUP_ROLES.map((role) => {
          const isActive = selectedRole === role.value;
          return (
            <button
              key={role.value}
              type="button"
              onClick={() => setSelectedRole(role.value)}
              className={`group cursor-pointer rounded-2xl p-5 text-left transition-all ${
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
                  <span className="material-symbols-outlined text-3xl">
                    {role.icon}
                  </span>
                </div>

                {isActive ? (
                  <span className="flex size-6 items-center justify-center rounded-full bg-agri-earth text-white">
                    <span className="material-symbols-outlined text-sm">check</span>
                  </span>
                ) : null}
              </div>

              <h3 className="text-lg font-bold text-slate-900">{role.label}</h3>
              <p className="mt-1 text-sm text-slate-500">{role.description}</p>
            </button>
          );
        })}
      </div>

      <form className="space-y-6" onSubmit={handleSubmit}>
        <div>
          <label
            htmlFor="signup-business-name"
            className="mb-2 block text-sm font-semibold text-slate-700"
          >
            Business or Farm Name
          </label>
          <input
            id="signup-business-name"
            type="text"
            placeholder="e.g. Green Valley Farm"
            className="w-full rounded-xl border border-slate-200 px-5 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
          />
        </div>

        <div className="grid grid-cols-1 gap-4 md:grid-cols-3">
          <div>
            <label
              htmlFor="signup-region"
              className="mb-2 block text-sm font-semibold text-slate-700"
            >
              Region
            </label>
            <select
              id="signup-region"
              defaultValue=""
              className="w-full rounded-xl border border-slate-200 bg-white px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
            >
              <option value="" disabled>
                Select Region
              </option>
              <option>Region I</option>
              <option>Region II</option>
              <option>Region III</option>
            </select>
          </div>

          <div>
            <label
              htmlFor="signup-province"
              className="mb-2 block text-sm font-semibold text-slate-700"
            >
              Province
            </label>
            <select
              id="signup-province"
              defaultValue=""
              className="w-full rounded-xl border border-slate-200 bg-white px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
            >
              <option value="" disabled>
                Select Province
              </option>
            </select>
          </div>

          <div>
            <label
              htmlFor="signup-city"
              className="mb-2 block text-sm font-semibold text-slate-700"
            >
              City/Municipality
            </label>
            <select
              id="signup-city"
              defaultValue=""
              className="w-full rounded-xl border border-slate-200 bg-white px-4 py-3 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf"
            >
              <option value="" disabled>
                Select City
              </option>
            </select>
          </div>
        </div>

        <div className="space-y-4 pt-6">
          <button
            type="submit"
            className="flex w-full items-center justify-center gap-2 rounded-full bg-agri-accent py-4 font-bold text-white shadow-lg shadow-agri-accent/20 transition-all hover:scale-[1.01] hover:bg-agri-accent/90 active:scale-[0.99]"
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
    </div>
  );
}
