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
  showLoginPrompt: boolean;
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
  showLoginPrompt,
}: SignupShellProps) {
  return (
    <div className="min-h-screen bg-agri-light md:h-screen md:overflow-hidden ">
      <div className="mx-auto h-full w-full overflow-hidden bg-white shadow-2xl ">
        <div className="flex h-full flex-col lg:flex-row">
          <SignupBrandPanel content={brandPanelContent} />

          <section className="flex min-h-0 flex-1 flex-col justify-start overflow-y-auto bg-white px-4 py-6 sm:px-6 sm:py-8 md:px-10 md:py-10 lg:px-14 lg:py-12">
            <div className={`mx-auto w-full ${contentContainerClassName}`}>
              <header className="mb-8">
                <div className="mb-4 flex items-end justify-between">
                  <div>
                    <span className="text-xs font-bold tracking-widest text-agri-leaf uppercase sm:text-sm">
                      Step {currentStep} of {totalSteps}
                    </span>
                    <h2 className="mt-1 font-display text-2xl font-bold text-slate-900 sm:text-3xl">
                      {stepTitle}
                    </h2>
                  </div>
                  {showLoginPrompt ? (
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
                  ) : null}
                </div>

                <SignupProgress
                  currentStep={currentStep}
                  totalSteps={totalSteps}
                />

                {stepHelperText ? (
                  <p className="mt-4 text-sm leading-relaxed text-slate-500 sm:text-base">
                    {stepHelperText}
                  </p>
                ) : null}
              </header>

              {children}

              {showLoginPrompt ? (
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
              ) : null}

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
      </div>
    </div>
  );
}
