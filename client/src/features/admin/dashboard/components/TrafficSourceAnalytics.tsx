import type { TrafficSource } from "../types/dashboard.types";

type TrafficSourceAnalyticsProps = {
  sources: TrafficSource[];
};

export default function TrafficSourceAnalytics({ sources }: TrafficSourceAnalyticsProps) {
  return (
    <section className="rounded-[2.5rem] border border-gray-100 bg-white p-10 shadow-sm">
      <div className="mb-10 flex flex-col justify-between gap-4 md:flex-row md:items-end">
        <div>
          <h2 className="text-2xl font-bold text-agri-green">Traffic Source Analytics</h2>
          <p className="font-medium text-gray-500">
            How buyers are discovering your agricultural listings
          </p>
        </div>

        <div className="text-right">
          <span className="text-xs font-bold tracking-widest text-gray-400 uppercase">Total Visits</span>
          <p className="text-3xl font-bold text-agri-green">12,482</p>
        </div>
      </div>

      <div className="grid grid-cols-1 gap-12 md:grid-cols-3">
        {sources.map((source) => (
          <div className="relative pt-6" key={source.id}>
            <div className="mb-4 flex items-center justify-between">
              <span className="text-lg font-bold">{source.label}</span>
              <span className="rounded-full bg-agri-leaf/10 px-3 py-1 text-xs font-bold text-agri-leaf">
                {source.percent}%
              </span>
            </div>

            <div className="h-2 w-full rounded-full bg-gray-50">
              <div
                className={`h-full rounded-full ${source.barColorClass}`}
                style={{ width: `${source.percent}%` }}
              />
            </div>

            <p className="mt-4 text-sm leading-relaxed text-gray-500">{source.description}</p>
          </div>
        ))}
      </div>
    </section>
  );
}
