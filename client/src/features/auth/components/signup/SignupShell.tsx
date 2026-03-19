import type { ReactNode } from "react";
import { Link } from "react-router-dom";
import type { SignupBrandPanelContent } from "../../types/signup.types";
import { SignupBrandPanel } from "./SignupBrandPanel";
import { SignupProgress } from "./SignupProgress";

type SignupShellProps = {
  children: ReactNode;
  currentStep: number;
  totalSteps: number;
  stepTitle: string;
  stepHelperText?: string;
  contentContainerClassName: string;
  brandPanelContent: SignupBrandPanelContent;
};

const LEGAL_LINKS = [
  { label: "Terms of Service", to: "/" },
  { label: "Privacy Policy", to: "/" },
  { label: "Help Center", to: "/" },
];

export function SignupShell({
  children,
  currentStep,
  totalSteps,
  stepTitle,
  stepHelperText,
  contentContainerClassName,
  brandPanelContent,
}: SignupShellProps) {
  return (
    <div className="flex min-h-212.5 w-full max-w-360 flex-col overflow-hidden bg-white shadow-2xl lg:flex-row">
      <SignupBrandPanel content={brandPanelContent} />

      <section className="flex flex-1 flex-col justify-center overflow-y-auto bg-white p-8 md:p-12 lg:p-16">
        <div className={`mx-auto w-full ${contentContainerClassName}`}>
          <header className="mb-8">
            <div className="mb-4 flex items-end justify-between">
              <div>
                <span className="text-sm font-bold tracking-widest text-agri-leaf uppercase">
                  Step {currentStep} of {totalSteps}
                </span>
                <h2 className="mt-1 font-display text-3xl font-bold text-slate-900">
                  {stepTitle}
                </h2>
              </div>
              <div className="hidden sm:block">
                <span className="text-sm text-slate-400">
                  Already have an account?{" "}
                  <Link
                    to="/"
                    className="font-bold text-agri-accent hover:underline"
                  >
                    Log in
                  </Link>
                </span>
              </div>
            </div>

            <SignupProgress currentStep={currentStep} totalSteps={totalSteps} />

            {stepHelperText ? (
              <p className="mt-4 text-slate-500">{stepHelperText}</p>
            ) : null}
          </header>

          {children}

          <footer className="mt-8 text-center sm:hidden">
            <span className="text-sm text-slate-400">
              Already have an account?{" "}
              <Link
                to="/"
                className="font-bold text-agri-accent hover:underline"
              >
                Log in
              </Link>
            </span>
          </footer>

          <div className="mt-12 flex flex-wrap justify-center gap-x-8 gap-y-2 text-[11px] font-medium tracking-widest text-slate-400 uppercase">
            {LEGAL_LINKS.map((link) => (
              <Link
                key={link.label}
                to={link.to}
                className="transition-colors hover:text-agri-leaf"
              >
                {link.label}
              </Link>
            ))}
          </div>
        </div>
      </section>
    </div>
  );
}
