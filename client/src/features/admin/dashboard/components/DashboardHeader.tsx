export default function DashboardHeader() {
  return (
    <section className="flex flex-col justify-between gap-4 md:flex-row md:items-end">
      <div>
        <h1 className="font-display text-4xl font-bold tracking-tight text-agri-green">
          Marketplace Overview
        </h1>
        <p className="mt-1 font-medium text-agri-leaf">
          Real-time agricultural trade monitoring for the Philippines.
        </p>
      </div>

      <div className="flex gap-3">
        <button
          className="rounded-2xl border border-agri-green/10 bg-white px-6 py-3 text-sm font-bold transition-all hover:bg-gray-50"
          type="button"
        >
          Export CSV
        </button>
        <button
          className="rounded-2xl bg-agri-green px-6 py-3 text-sm font-bold text-white transition-all hover:opacity-90"
          type="button"
        >
          Generate Report
        </button>
      </div>
    </section>
  );
}
