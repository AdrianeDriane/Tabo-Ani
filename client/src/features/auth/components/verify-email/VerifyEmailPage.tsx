import { useEffect, useState } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import { resendVerification, verifyEmail } from "../../api/auth.api";
import type { EmailVerificationStatusResponse } from "../../types/signup.types";

const HERO_IMAGE_URL =
  "https://lh3.googleusercontent.com/aida-public/AB6AXuD1YrUv4NDMS7DVCbp5p3DmxAC_HSDHcqqVvr0CL_s4mfDvlr2TZFtoQOSdLWNSKvlo9ScJo9uEMjCTTCnwt8rGCN8geAT85Egv8-Q7JNJ3QuhXKjoGxsGmnuxfVjo1iJxtQ8va5GXmeIIYp6MwoLNPYEWkxHZ_-io1_O8ndRYVKTh2UFPXWBPRtEuzqOle1_QsA75oZ5MTeVuTcf-TEzqrD4Oo_RNx5VJuhXZuhamfN0-bqydwiqKOYhzXnCQ-3BEtDM2fheAXGDo2";

type VerificationViewState =
  | "pending"
  | "verified"
  | "already-verified"
  | "invalid"
  | "error";

export function VerifyEmailPage() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [verificationState, setVerificationState] =
    useState<VerificationViewState>("pending");
  const [verificationResult, setVerificationResult] =
    useState<EmailVerificationStatusResponse | null>(null);
  const [verificationError, setVerificationError] = useState<string | null>(
    null
  );
  const [resendEmail, setResendEmail] = useState("");
  const [resendMessage, setResendMessage] = useState<string | null>(null);
  const [resendError, setResendError] = useState<string | null>(null);
  const [isResending, setIsResending] = useState(false);

  const token = searchParams.get("token")?.trim() ?? "";

  useEffect(() => {
    let isActive = true;
    let redirectTimer: number | undefined;

    async function processVerification() {
      if (!token) {
        setVerificationState("invalid");
        return;
      }

      setVerificationState("pending");
      setVerificationError(null);
      setVerificationResult(null);

      try {
        const response = await verifyEmail({ token });
        if (!isActive) {
          return;
        }

        setVerificationResult(response.data);

        if (response.data.status === "VERIFIED") {
          setVerificationState("verified");
          redirectTimer = window.setTimeout(() => {
            navigate("/login?emailVerified=1", { replace: true });
          }, 2500);
          return;
        }

        if (response.data.status === "ALREADY_VERIFIED") {
          setVerificationState("already-verified");
          redirectTimer = window.setTimeout(() => {
            navigate("/login?emailVerified=1", { replace: true });
          }, 2500);
          return;
        }

        setVerificationState("invalid");
      } catch (error) {
        if (!isActive) {
          return;
        }

        setVerificationError(
          error instanceof Error
            ? error.message
            : "We couldn't verify your email right now."
        );
        setVerificationState("error");
      }
    }

    void processVerification();

    return () => {
      isActive = false;
      if (redirectTimer) {
        window.clearTimeout(redirectTimer);
      }
    };
  }, [navigate, token]);

  async function handleResendVerification() {
    if (!resendEmail.trim()) {
      setResendError("Enter the email address you used during signup.");
      return;
    }

    setIsResending(true);
    setResendError(null);
    setResendMessage(null);

    try {
      await resendVerification(resendEmail.trim());
      setResendMessage(
        "If the account is eligible, a new verification link will arrive shortly."
      );
    } catch (error) {
      setResendError(
        error instanceof Error
          ? error.message
          : "We couldn't resend the verification link."
      );
    } finally {
      setIsResending(false);
    }
  }

  const showRecoveryForm =
    verificationState === "invalid" || verificationState === "error";

  return (
    <div className="min-h-screen bg-agri-light lg:h-screen lg:overflow-hidden">
      <div className="flex min-h-screen w-full flex-col lg:h-screen lg:min-h-0 lg:flex-row">
        <aside className="relative hidden overflow-hidden bg-agri-green lg:flex lg:h-full lg:min-h-0 lg:w-1/2 xl:w-7/12">
          <div className="absolute inset-0 z-0">
            <img
              src={HERO_IMAGE_URL}
              alt="Lush green Philippine rice terraces at dawn"
              className="size-full object-cover opacity-60"
            />
            <div className="absolute inset-0 bg-linear-to-t from-agri-green/80 via-transparent to-black/40" />
          </div>

          <div className="relative z-10 flex w-full flex-col justify-between p-10 xl:p-12">
            <div className="flex items-center gap-3">
              <div className="flex size-12 items-center justify-center rounded-xl bg-agri-accent p-2 shadow-lg">
                <span className="font-display text-2xl font-extrabold text-white">
                  T
                </span>
              </div>
              <span className="font-display text-2xl font-bold tracking-tight text-white">
                Tabo-Ani
              </span>
            </div>

            <div className="max-w-xl">
              <h1 className="mb-6 font-display text-4xl leading-tight font-extrabold text-white xl:text-6xl">
                Finish Activating Your Account
              </h1>
              <p className="max-w-md text-base text-white/80 xl:text-lg">
                Verify your email to activate your account and move your role
                applications into review.
              </p>
            </div>

            <p className="text-sm text-white/60">
              (c) 2024 Tabo-Ani Agricultural Network. All rights reserved.
            </p>
          </div>
        </aside>

        <section className="min-h-0 flex-1 overflow-y-auto bg-agri-light">
          <div className="mx-auto flex w-full max-w-140 flex-col px-4 py-6 h-full sm:px-6 sm:py-8 lg:px-8 lg:py-8 xl:px-10 2xl:justify-center">
            <div className="mb-8 flex items-center justify-center gap-3 lg:hidden">
              <div className="flex size-10 items-center justify-center rounded-xl bg-agri-accent p-2 shadow-lg">
                <span className="font-display text-xl font-extrabold text-white">
                  T
                </span>
              </div>
              <span className="font-display text-xl font-bold tracking-tight text-slate-900">
                Tabo-Ani
              </span>
            </div>

            <div className="rounded-3xl border border-slate-100 bg-white p-6 shadow-2xl shadow-agri-accent/5 sm:p-8 lg:p-10">
              <div className="mb-8">
                <p className="text-sm font-semibold tracking-[0.2em] text-agri-leaf uppercase">
                  Email Verification
                </p>
                <h2 className="mt-3 font-display text-3xl font-extrabold text-slate-900">
                  {resolveHeadline(verificationState)}
                </h2>
                <p className="mt-3 text-sm leading-relaxed text-slate-500 sm:text-base">
                  {resolveDescription(verificationState, verificationResult)}
                </p>
              </div>

              {verificationState === "pending" ? (
                <div className="rounded-2xl border border-agri-accent/20 bg-agri-accent/5 px-5 py-4 text-sm text-slate-700">
                  We are validating your verification link now.
                </div>
              ) : null}

              {verificationState === "verified" ? (
                <div className="rounded-2xl border border-green-200 bg-green-50 px-5 py-4 text-sm text-green-700">
                  Your email has been verified successfully. Redirecting you to
                  the login page.
                </div>
              ) : null}

              {verificationState === "already-verified" ? (
                <div className="rounded-2xl border border-emerald-200 bg-emerald-50 px-5 py-4 text-sm text-emerald-700">
                  This account is already verified. Redirecting you to the login
                  page.
                </div>
              ) : null}

              {verificationState === "invalid" ? (
                <div className="rounded-2xl border border-amber-200 bg-amber-50 px-5 py-4 text-sm text-amber-800">
                  This verification link is invalid, expired, or has already
                  been replaced. Request a fresh link below.
                </div>
              ) : null}

              {verificationState === "error" ? (
                <div className="rounded-2xl border border-red-200 bg-red-50 px-5 py-4 text-sm text-red-700">
                  {verificationError ??
                    "We couldn't complete email verification right now."}
                </div>
              ) : null}

              {showRecoveryForm ? (
                <div className="mt-6 space-y-4 rounded-2xl border border-slate-100 bg-slate-50 p-5">
                  <div>
                    <label
                      htmlFor="resend-email"
                      className="mb-1 block text-sm font-semibold text-slate-700"
                    >
                      Signup Email Address
                    </label>
                    <input
                      id="resend-email"
                      type="email"
                      value={resendEmail}
                      onChange={(event) => setResendEmail(event.target.value)}
                      placeholder="juandelacruz@email.com"
                      className="w-full rounded-2xl border border-slate-200 bg-white px-4 py-3 text-slate-900 outline-none transition-all focus:border-agri-accent focus:ring-2 focus:ring-agri-accent"
                    />
                  </div>

                  {resendMessage ? (
                    <div className="rounded-2xl border border-green-200 bg-green-50 px-4 py-3 text-sm text-green-700">
                      {resendMessage}
                    </div>
                  ) : null}

                  {resendError ? (
                    <div className="rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
                      {resendError}
                    </div>
                  ) : null}

                  <button
                    type="button"
                    disabled={isResending}
                    onClick={handleResendVerification}
                    className="w-full rounded-full bg-agri-green py-3 text-sm font-bold text-white transition-colors hover:bg-agri-green/90 disabled:cursor-not-allowed disabled:opacity-60"
                  >
                    {isResending
                      ? "Sending Verification Link..."
                      : "Send New Verification Link"}
                  </button>
                </div>
              ) : null}

              <div className="mt-8 flex flex-col gap-3 sm:flex-row">
                <Link
                  to="/login"
                  className="flex-1 rounded-full bg-agri-accent py-3 text-center text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition-colors hover:bg-agri-accent/90"
                >
                  Continue to Login
                </Link>
                <Link
                  to="/signup"
                  className="flex-1 rounded-full border border-slate-200 bg-white py-3 text-center text-sm font-bold text-slate-700 transition-colors hover:bg-slate-50"
                >
                  Back to Signup
                </Link>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}

function resolveHeadline(state: VerificationViewState) {
  switch (state) {
    case "verified":
      return "Email Verified";
    case "already-verified":
      return "Account Already Verified";
    case "invalid":
      return "Verification Link Expired";
    case "error":
      return "Verification Unavailable";
    default:
      return "Verifying Your Email";
  }
}

function resolveDescription(
  state: VerificationViewState,
  verificationResult: EmailVerificationStatusResponse | null
) {
  switch (state) {
    case "verified":
      return `Your email for ${
        verificationResult?.email || "your account"
      } has been verified.`;
    case "already-verified":
      return `The email for ${
        verificationResult?.email || "this account"
      } is already verified.`;
    case "invalid":
      return "Verification links expire after a limited time and older links stop working once a newer one is issued.";
    case "error":
      return "Something interrupted the verification request. You can still request a fresh verification link below.";
    default:
      return "Please wait while we validate your email verification link.";
  }
}
