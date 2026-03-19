import { Link, useRouteError } from "react-router-dom";

export function ErrorPage() {
  const error = useRouteError();

  return (
    <main className="flex min-h-screen items-center justify-center bg-agri-light px-6 py-12">
      <section className="w-full max-w-xl rounded-3xl border border-black/5 bg-white p-10 text-center shadow-lg">
        <p className="mb-3 text-xs font-black uppercase tracking-[0.2em] text-agri-accent">
          Routing Error
        </p>
        <h1 className="mb-4 font-display text-4xl font-extrabold text-agri-green">
          Something went wrong
        </h1>
        <p className="mb-8 text-gray-600">
          {(error as Error | undefined)?.message ??
            "The requested page could not be loaded."}
        </p>
        <Link
          className="inline-flex rounded-2xl bg-agri-green px-8 py-3 text-sm font-black uppercase tracking-widest text-white transition-colors hover:bg-agri-leaf"
          to="/"
        >
          Back to Landing
        </Link>
      </section>
    </main>
  );
}
