import type { FormEvent } from "react";
import type { SignupStepComponentProps } from "../../../types/signup.types";

const REGIONS = ["Region I", "Region II", "Region III", "NCR", "CAR"];

function renderLocationFields(
  rolePrefix: "buyer" | "farmer",
  label: string,
  region: string,
  province: string,
  city: string,
  errors: SignupStepComponentProps["errors"],
  onChange: (field: "region" | "province" | "city", value: string) => void,
) {
  return (
    <div className="grid grid-cols-1 gap-4 md:grid-cols-3">
      <div>
        <label
          htmlFor={`${rolePrefix}-region`}
          className="mb-2 block text-sm font-semibold text-slate-700"
        >
          {label} Region
        </label>
        <select
          id={`${rolePrefix}-region`}
          value={region}
          onChange={(event) => onChange("region", event.target.value)}
          className="w-full rounded-xl border border-slate-200 bg-white px-4 py-2.5 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf sm:py-3"
        >
          <option value="">Select Region</option>
          {REGIONS.map((regionOption) => (
            <option key={regionOption} value={regionOption}>
              {regionOption}
            </option>
          ))}
        </select>
        {errors[`${rolePrefix}.region`] ? (
          <p className="mt-1 text-xs text-red-600">{errors[`${rolePrefix}.region`]}</p>
        ) : null}
      </div>

      <div>
        <label
          htmlFor={`${rolePrefix}-province`}
          className="mb-2 block text-sm font-semibold text-slate-700"
        >
          {label} Province
        </label>
        <input
          id={`${rolePrefix}-province`}
          type="text"
          value={province}
          onChange={(event) => onChange("province", event.target.value)}
          placeholder="e.g. Benguet"
          className="w-full rounded-xl border border-slate-200 bg-white px-4 py-2.5 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf sm:py-3"
        />
        {errors[`${rolePrefix}.province`] ? (
          <p className="mt-1 text-xs text-red-600">{errors[`${rolePrefix}.province`]}</p>
        ) : null}
      </div>

      <div>
        <label
          htmlFor={`${rolePrefix}-city`}
          className="mb-2 block text-sm font-semibold text-slate-700"
        >
          {label} City/Municipality
        </label>
        <input
          id={`${rolePrefix}-city`}
          type="text"
          value={city}
          onChange={(event) => onChange("city", event.target.value)}
          placeholder="e.g. La Trinidad"
          className="w-full rounded-xl border border-slate-200 bg-white px-4 py-2.5 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf sm:py-3"
        />
        {errors[`${rolePrefix}.city`] ? (
          <p className="mt-1 text-xs text-red-600">{errors[`${rolePrefix}.city`]}</p>
        ) : null}
      </div>
    </div>
  );
}

export function SignupStep3BusinessVerification({
  step,
  form,
  errors,
  onContinue,
  onBack,
  onBuyerFieldChange,
  onFarmerFieldChange,
}: SignupStepComponentProps) {
  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onContinue();
  }

  return (
    <form className="space-y-6 sm:space-y-8" onSubmit={handleSubmit}>
      {form.buyerApplication.isSelected ? (
        <section className="space-y-4 rounded-3xl border border-slate-100 bg-white p-5 sm:p-6">
          <div>
            <h3 className="text-base font-bold text-slate-900 sm:text-lg">
              Buyer Application Details
            </h3>
            <p className="mt-1 text-sm text-slate-500">
              Tell us about the business that will buy through Tabo-Ani.
            </p>
          </div>

          <div>
            <label
              htmlFor="signup-buyer-business-name"
              className="mb-2 block text-sm font-semibold text-slate-700"
            >
              Business Name
            </label>
            <input
              id="signup-buyer-business-name"
              type="text"
              value={form.buyerApplication.businessName}
              onChange={(event) => onBuyerFieldChange("businessName", event.target.value)}
              placeholder="e.g. Fresh Harvest Trading"
              className="w-full rounded-xl border border-slate-200 px-4 py-2.5 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf sm:px-5 sm:py-3"
            />
            {errors["buyer.businessName"] ? (
              <p className="mt-1 text-xs text-red-600">{errors["buyer.businessName"]}</p>
            ) : null}
          </div>

          <div>
            <label
              htmlFor="signup-buyer-business-type"
              className="mb-2 block text-sm font-semibold text-slate-700"
            >
              Business Type
            </label>
            <input
              id="signup-buyer-business-type"
              type="text"
              value={form.buyerApplication.businessType}
              onChange={(event) => onBuyerFieldChange("businessType", event.target.value)}
              placeholder="e.g. Restaurant, Reseller, Market"
              className="w-full rounded-xl border border-slate-200 px-4 py-2.5 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf sm:px-5 sm:py-3"
            />
            {errors["buyer.businessType"] ? (
              <p className="mt-1 text-xs text-red-600">{errors["buyer.businessType"]}</p>
            ) : null}
          </div>

          {renderLocationFields(
            "buyer",
            "Business",
            form.buyerApplication.location.region,
            form.buyerApplication.location.province,
            form.buyerApplication.location.city,
            errors,
            (field, value) => onBuyerFieldChange(field, value),
          )}
        </section>
      ) : null}

      {form.farmerApplication.isSelected ? (
        <section className="space-y-4 rounded-3xl border border-slate-100 bg-white p-5 sm:p-6">
          <div>
            <h3 className="text-base font-bold text-slate-900 sm:text-lg">
              Farmer Application Details
            </h3>
            <p className="mt-1 text-sm text-slate-500">
              Provide the farm details that will be used in your farmer application.
            </p>
          </div>

          <div>
            <label
              htmlFor="signup-farm-name"
              className="mb-2 block text-sm font-semibold text-slate-700"
            >
              Farm Name
            </label>
            <input
              id="signup-farm-name"
              type="text"
              value={form.farmerApplication.farmName}
              onChange={(event) => onFarmerFieldChange("farmName", event.target.value)}
              placeholder="e.g. Green Valley Farm"
              className="w-full rounded-xl border border-slate-200 px-4 py-2.5 outline-none transition-all focus:border-transparent focus:ring-2 focus:ring-agri-leaf sm:px-5 sm:py-3"
            />
            {errors["farmer.farmName"] ? (
              <p className="mt-1 text-xs text-red-600">{errors["farmer.farmName"]}</p>
            ) : null}
          </div>

          {renderLocationFields(
            "farmer",
            "Farm",
            form.farmerApplication.location.region,
            form.farmerApplication.location.province,
            form.farmerApplication.location.city,
            errors,
            (field, value) => onFarmerFieldChange(field, value),
          )}
        </section>
      ) : null}

      <div className="rounded-3xl border border-agri-leaf/10 bg-agri-leaf/5 p-5 text-sm text-slate-600">
        KYC and supporting documents will be collected after your account is created and your
        email has been verified.
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
