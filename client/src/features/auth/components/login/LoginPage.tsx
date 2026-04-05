import type { FormEvent } from "react";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useLocation, useNavigate, useSearchParams } from "react-router-dom";
import type { AppDispatch, RootState } from "../../../../store";
import { dismissAuthIssue, loginUser } from "../../authSlice";
import { resolveLoginDestination } from "../../utils/redirects";
import { resolveAuthIssueBanner } from "../../utils/authFeedback";

const HERO_IMAGE_URL =
  "https://lh3.googleusercontent.com/aida-public/AB6AXuD1YrUv4NDMS7DVCbp5p3DmxAC_HSDHcqqVvr0CL_s4mfDvlr2TZFtoQOSdLWNSKvlo9ScJo9uEMjCTTCnwt8rGCN8geAT85Egv8-Q7JNJ3QuhXKjoGxsGmnuxfVjo1iJxtQ8va5GXmeIIYp6MwoLNPYEWkxHZ_-io1_O8ndRYVKTh2UFPXWBPRtEuzqOle1_QsA75oZ5MTeVuTcf-TEzqrD4Oo_RNx5VJuhXZuhamfN0-bqydwiqKOYhzXnCQ-3BEtDM2fheAXGDo2";

export function LoginPage() {
  const dispatch = useDispatch<AppDispatch>();
  const location = useLocation();
  const navigate = useNavigate();
  const auth = useSelector((state: RootState) => state.auth);
  const [isPasswordVisible, setIsPasswordVisible] = useState(false);
  const [searchParams] = useSearchParams();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const hasEmailVerifiedNotice = searchParams.get("emailVerified") === "1";
  const isSubmitting = auth.loginStatus === "pending";
  const returnTo =
    typeof location.state === "object" &&
    location.state !== null &&
    "returnTo" in location.state
      ? String(location.state.returnTo)
      : null;
  const authIssueMessage = resolveAuthIssueBanner(auth.lastIssue);

  useEffect(() => {
    if (auth.sessionStatus !== "authenticated") {
      return;
    }

    navigate(resolveLoginDestination(auth.user?.roles, returnTo), {
      replace: true,
    });
  }, [auth.sessionStatus, auth.user?.roles, navigate, returnTo]);

  useEffect(() => {
    if (!auth.lastIssue) {
      return;
    }

    return () => {
      dispatch(dismissAuthIssue());
    };
  }, [auth.lastIssue, dispatch]);

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    try {
      const session = await dispatch(
        loginUser({
          email: email.trim(),
          password,
          rememberMe,
        })
      ).unwrap();

      navigate(resolveLoginDestination(session.user.roles, returnTo), {
        replace: true,
      });
    } catch {
      // Slice state already captures the user-facing error.
    }
  }

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
                Welcome Back to the Future of Farming
              </h1>
              <p className="max-w-md text-base text-white/80 xl:text-lg">
                Connecting local producers with global opportunities through
                sustainable technology.
              </p>
            </div>

            <p className="text-sm text-white/60">
              (c) 2024 Tabo-Ani Agricultural Network. All rights reserved.
            </p>
          </div>
        </aside>

        <section className="min-h-0 flex-1 overflow-y-auto bg-agri-light">
          <div className="mx-auto h-full w-full max-w-140 flex-col px-4 py-6 sm:px-6 sm:py-8 lg:flex lg:px-8 lg:py-8 xl:px-10 2xl:justify-center">
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
              <div className="mb-8 sm:mb-10">
                {hasEmailVerifiedNotice ? (
                  <div className="mb-6 rounded-2xl border border-green-200 bg-green-50 px-4 py-3 text-sm text-green-700">
                    Your email has been verified. You can now log in to your
                    Tabo-Ani account.
                  </div>
                ) : null}
                {auth.loginError ? (
                  <div className="mb-6 rounded-2xl border border-red-200 bg-red-50 px-4 py-3 text-sm text-red-700">
                    {auth.loginError}
                  </div>
                ) : null}
                {!auth.loginError && authIssueMessage ? (
                  <div className="mb-6 rounded-2xl border border-amber-200 bg-amber-50 px-4 py-3 text-sm text-amber-800">
                    {authIssueMessage}
                  </div>
                ) : null}
                <h2 className="mb-2 font-display text-3xl font-extrabold text-slate-900">
                  Login
                </h2>
                <p className="font-medium text-slate-500">
                  Access your agricultural ecosystem.
                </p>
              </div>

              <form className="space-y-6" onSubmit={handleSubmit}>
                <div className="space-y-2">
                  <label
                    htmlFor="login-email"
                    className="ml-1 block text-sm font-semibold text-slate-700"
                  >
                    Email Address
                  </label>
                  <input
                    id="login-email"
                    type="email"
                    autoComplete="email"
                    required
                    disabled={isSubmitting}
                    value={email}
                    onChange={(event) => setEmail(event.target.value)}
                    placeholder="Enter your email address"
                    className="h-12 w-full rounded-2xl border border-slate-200 bg-slate-50 px-4 text-slate-900 placeholder:text-slate-400 transition-all duration-200 focus:border-agri-accent focus:ring-2 focus:ring-agri-accent disabled:cursor-not-allowed disabled:opacity-60 sm:h-14 sm:px-5"
                  />
                </div>

                <div className="space-y-2">
                  <div className="flex items-center justify-between px-1">
                    <label
                      htmlFor="login-password"
                      className="block text-sm font-semibold text-slate-700"
                    >
                      Password
                    </label>
                  </div>
                  <div className="relative">
                    <input
                      id="login-password"
                      type={isPasswordVisible ? "text" : "password"}
                      autoComplete="current-password"
                      required
                      disabled={isSubmitting}
                      value={password}
                      onChange={(event) => setPassword(event.target.value)}
                      placeholder="Enter your password"
                      className="h-12 w-full rounded-2xl border border-slate-200 bg-slate-50 px-4 text-slate-900 placeholder:text-slate-400 transition-all duration-200 focus:border-agri-accent focus:ring-2 focus:ring-agri-accent disabled:cursor-not-allowed disabled:opacity-60 sm:h-14 sm:px-5"
                    />
                    <button
                      type="button"
                      onClick={() =>
                        setIsPasswordVisible((current) => !current)
                      }
                      className="absolute top-1/2 right-4 -translate-y-1/2 text-slate-400 transition-colors hover:text-slate-600"
                    >
                      <span className="material-symbols-outlined text-[20px]">
                        {isPasswordVisible ? "visibility_off" : "visibility"}
                      </span>
                    </button>
                  </div>
                </div>

                <div className="flex items-center px-1">
                  <input
                    id="remember"
                    type="checkbox"
                    checked={rememberMe}
                    onChange={(event) => setRememberMe(event.target.checked)}
                    className="size-5 rounded border-slate-300 text-agri-accent focus:ring-agri-accent"
                  />
                  <label
                    htmlFor="remember"
                    className="ml-3 text-sm font-medium text-slate-600"
                  >
                    Remember me
                  </label>
                </div>

                <button
                  type="submit"
                  disabled={isSubmitting}
                  className="flex h-12 w-full items-center justify-center rounded-full bg-agri-accent text-base font-bold text-white shadow-lg shadow-agri-accent/25 transition-all active:scale-[0.98] hover:bg-agri-accent/90 disabled:cursor-not-allowed disabled:opacity-60 sm:h-14 sm:text-lg"
                >
                  {isSubmitting ? "Logging In..." : "Log In"}
                </button>
              </form>

              <div className="mt-8 text-center sm:mt-10">
                <p className="font-medium text-slate-600">
                  Don&apos;t have an account?
                  <Link
                    to="/signup"
                    className="ml-1 font-bold text-agri-accent hover:underline"
                  >
                    Sign Up
                  </Link>
                </p>
              </div>

              <div className="mt-6 flex items-center justify-center gap-2 rounded-2xl bg-slate-50 px-4 py-3 sm:mt-8">
                <span className="material-symbols-outlined text-[20px] text-agri-green">
                  shield
                </span>
                <span className="text-center text-[11px] font-semibold tracking-wider text-slate-500 uppercase sm:text-xs">
                  Secure, KYC-verified access
                </span>
              </div>
            </div>

            <div className="mt-6 flex flex-col items-center justify-center gap-3 pb-10 sm:mt-8 sm:flex-row sm:gap-6">
              <p className="text-center text-sm font-medium text-slate-500">
                Secure access for verified accounts only.
              </p>
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}
