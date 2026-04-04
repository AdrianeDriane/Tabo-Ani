import type { ChangeEvent, FormEvent } from "react";
import { useId, useState } from "react";
import type { SignupStepComponentProps } from "../../../types/signup.types";

const ACCEPTED_IDS = [
  "PhilID / ePhilID",
  "Driver's License",
  "Philippine Passport",
  "SSS / GSIS UMID",
];

export function SignupStep3BusinessVerification({
  step,
  onContinue,
  onBack,
}: SignupStepComponentProps) {
  const inputId = useId();
  const [selectedFileName, setSelectedFileName] = useState<string>("");

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    onContinue();
  }

  function handleFileChange(event: ChangeEvent<HTMLInputElement>) {
    const uploadedFile = event.target.files?.[0];
    if (!uploadedFile) {
      setSelectedFileName("");
      return;
    }

    setSelectedFileName(uploadedFile.name);
  }

  return (
    <div className="space-y-6 sm:space-y-8">
      <section className="space-y-4">
        <h3 className="flex items-center gap-2 text-base font-bold text-slate-900 sm:text-lg">
          <span className="flex size-7 items-center justify-center rounded-full bg-agri-green text-xs font-bold text-white">
            1
          </span>
          Submit Primary Identification
        </h3>
        <div className="group relative flex cursor-pointer flex-col items-center justify-center rounded-3xl border-2 border-dashed border-slate-200 bg-stone-50 p-6 transition-all hover:border-agri-leaf sm:p-10">
          <input
            id={inputId}
            type="file"
            accept=".jpg,.png,.pdf"
            className="absolute inset-0 cursor-pointer opacity-0"
            onChange={handleFileChange}
          />
          <div className="mb-4 flex size-14 items-center justify-center rounded-2xl bg-agri-leaf/10 text-agri-leaf transition-transform group-hover:scale-110 sm:size-16">
            <span className="material-symbols-outlined text-3xl sm:text-4xl">id_card</span>
          </div>
          <p className="mb-1 font-bold text-slate-800">Drag and drop or click to upload</p>
          <p className="mb-4 text-sm text-slate-500">Supports JPG, PNG, PDF (Max 10MB)</p>
          <label
            htmlFor={inputId}
            className="cursor-pointer rounded-xl border border-slate-200 bg-white px-6 py-2 text-sm font-semibold text-slate-700 transition-colors hover:bg-slate-50"
          >
            Select File
          </label>
          {selectedFileName ? (
            <p className="mt-4 text-xs font-medium text-agri-green">{selectedFileName}</p>
          ) : null}
        </div>
      </section>

      <section className="space-y-4">
        <h3 className="flex items-center gap-2 text-base font-bold text-slate-900 sm:text-lg">
          <span className="flex size-7 items-center justify-center rounded-full bg-agri-green text-xs font-bold text-white">
            2
          </span>
          Identity Confirmation
        </h3>
        <div className="flex flex-col items-center gap-6 rounded-3xl border border-slate-100 bg-white p-5 md:flex-row md:p-6">
          <div className="flex size-24 shrink-0 items-center justify-center overflow-hidden rounded-2xl border-4 border-white bg-slate-100 shadow-sm sm:size-32">
            <span className="material-symbols-outlined text-4xl text-slate-300 sm:text-5xl">
              account_circle
            </span>
          </div>
          <div className="flex-1">
            <p className="mb-4 text-sm text-slate-600">
              Capture a self-portrait while holding your official ID next to your
              face. Both must be legible.
            </p>
            <button
              type="button"
              className="flex w-full items-center justify-center gap-2 rounded-2xl bg-agri-green px-6 py-3 font-bold text-white transition-colors hover:bg-agri-green/90 md:w-auto"
            >
              <span className="material-symbols-outlined text-xl">photo_camera</span>
              Open Camera
            </button>
          </div>
        </div>
      </section>

      <div className="grid gap-4 sm:gap-6 md:grid-cols-2">
        <div className="rounded-3xl border border-slate-100 bg-stone-50 p-5 sm:p-6">
          <h4 className="mb-4 flex items-center gap-2 text-[11px] font-bold tracking-widest text-slate-400 uppercase">
            <span className="material-symbols-outlined text-sm">info</span>
            Accepted IDs
          </h4>
          <ul className="space-y-2 text-sm font-medium text-slate-700">
            {ACCEPTED_IDS.map((acceptedId) => (
              <li key={acceptedId} className="flex items-center gap-2">
                <span className="material-symbols-outlined text-sm text-agri-leaf">
                  check_circle
                </span>
                {acceptedId}
              </li>
            ))}
          </ul>
        </div>

        <div className="flex flex-col items-center justify-center rounded-3xl border border-agri-leaf/10 bg-agri-leaf/5 p-5 text-center sm:p-6">
          <div className="mb-3 flex size-12 items-center justify-center rounded-full bg-agri-leaf text-white">
            <span className="material-symbols-outlined text-2xl">verified_user</span>
          </div>
          <h4 className="mb-1 text-sm font-bold text-agri-green">
            Certified Data Protection
          </h4>
          <p className="text-[11px] leading-relaxed text-slate-600">
            All uploaded documents are secured with industry-leading encryption and
            adhere strictly to data privacy laws.
          </p>
        </div>
      </div>

      <form className="space-y-4 pt-4 sm:pt-6" onSubmit={handleSubmit}>
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
      </form>
    </div>
  );
}
