import type { FormEvent } from "react";
import { useState } from "react";
import { Link, useSearchParams } from "react-router-dom";

const HERO_IMAGE_URL =
  "https://lh3.googleusercontent.com/aida-public/AB6AXuD1YrUv4NDMS7DVCbp5p3DmxAC_HSDHcqqVvr0CL_s4mfDvlr2TZFtoQOSdLWNSKvlo9ScJo9uEMjCTTCnwt8rGCN8geAT85Egv8-Q7JNJ3QuhXKjoGxsGmnuxfVjo1iJxtQ8va5GXmeIIYp6MwoLNPYEWkxHZ_-io1_O8ndRYVKTh2UFPXWBPRtEuzqOle1_QsA75oZ5MTeVuTcf-TEzqrD4Oo_RNx5VJuhXZuhamfN0-bqydwiqKOYhzXnCQ-3BEtDM2fheAXGDo2";

export function LoginPage() {
  const [isPasswordVisible, setIsPasswordVisible] = useState(false);
  const [searchParams] = useSearchParams();
  const hasEmailVerifiedNotice = searchParams.get("emailVerified") === "1";

  function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
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
          <div className="mx-auto flex w-full max-w-140 flex-col px-4 py-6 sm:px-6 sm:py-8 lg:px-8 lg:py-8 xl:px-10 h-full 2xl:justify-center">
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
                    htmlFor="login-identifier"
                    className="ml-1 block text-sm font-semibold text-slate-700"
                  >
                    Email or Mobile Number
                  </label>
                  <input
                    id="login-identifier"
                    type="text"
                    placeholder="Enter your email or mobile"
                    className="h-12 w-full rounded-2xl border border-slate-200 bg-slate-50 px-4 text-slate-900 placeholder:text-slate-400 transition-all duration-200 focus:border-agri-accent focus:ring-2 focus:ring-agri-accent sm:h-14 sm:px-5"
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
                    <Link
                      to="/"
                      className="text-xs font-bold text-agri-accent transition-colors hover:text-agri-accent/80"
                    >
                      Forgot password?
                    </Link>
                  </div>
                  <div className="relative">
                    <input
                      id="login-password"
                      type={isPasswordVisible ? "text" : "password"}
                      placeholder="Enter your password"
                      className="h-12 w-full rounded-2xl border border-slate-200 bg-slate-50 px-4 text-slate-900 placeholder:text-slate-400 transition-all duration-200 focus:border-agri-accent focus:ring-2 focus:ring-agri-accent sm:h-14 sm:px-5"
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
                  className="flex h-12 w-full items-center justify-center rounded-full bg-agri-accent text-base font-bold text-white shadow-lg shadow-agri-accent/25 transition-all active:scale-[0.98] hover:bg-agri-accent/90 sm:h-14 sm:text-lg"
                >
                  Log In
                </button>

                <div className="relative flex items-center py-1">
                  <div className="grow border-t border-slate-200" />
                  <span className="mx-4 shrink-0 text-sm font-medium text-slate-400">
                    OR
                  </span>
                  <div className="grow border-t border-slate-200" />
                </div>

                <button
                  type="button"
                  className="flex h-12 w-full items-center justify-center gap-3 rounded-full border-2 border-slate-100 text-sm font-bold text-slate-700 transition-all hover:bg-slate-50 sm:h-14 sm:text-base"
                >
                  <svg
                    viewBox="0 0 24 24"
                    className="size-5"
                    aria-hidden="true"
                  >
                    <path
                      d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
                      fill="#4285F4"
                    />
                    <path
                      d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
                      fill="#34A853"
                    />
                    <path
                      d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l3.66-2.84z"
                      fill="#FBBC05"
                    />
                    <path
                      d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
                      fill="#EA4335"
                    />
                  </svg>
                  Continue with Google
                </button>
              </form>

              <div className="mt-8 text-center sm:mt-10">
                <p className="font-medium text-slate-600">
                  Don't have an account?
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
              <p className="text-sm font-medium text-slate-400 italic">
                Need help?
              </p>
              <div className="flex items-center gap-4">
                <Link
                  to="/"
                  className="text-sm font-semibold text-slate-600 underline decoration-slate-200 underline-offset-4 transition-colors hover:text-agri-accent"
                >
                  Account Recovery
                </Link>
                <span className="text-slate-300">|</span>
                <Link
                  to="/"
                  className="text-sm font-semibold text-slate-600 underline decoration-slate-200 underline-offset-4 transition-colors hover:text-agri-accent"
                >
                  Contact Support
                </Link>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}
