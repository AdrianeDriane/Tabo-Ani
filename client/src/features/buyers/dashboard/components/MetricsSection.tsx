import type { Metric } from "../types/buyerDashboard.types";

type MetricsSectionProps = {
  metrics: Metric[];
};

export default function MetricsSection({ metrics }: MetricsSectionProps) {
  return (
    <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-12">
      {metrics.map((metric) => {
        if (metric.tone === "gradient") {
          return (
            <div
              key={metric.id}
              className="p-6 rounded-3xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)] text-white border-none bg-gradient-to-br from-agri-green to-agri-leaf"
            >
              <p className="text-xs font-bold text-white/70 uppercase tracking-wider mb-1">
                {metric.label}
              </p>
              <p className="text-2xl font-extrabold">{metric.value}</p>
              <button className="mt-4 text-[10px] font-bold underline underline-offset-4">
                {metric.helper}
              </button>
            </div>
          );
        }

        return (
          <div
            key={metric.id}
            className="bg-white p-6 rounded-3xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)] border border-slate-100"
          >
            <p className="text-xs font-bold text-slate-400 uppercase tracking-wider mb-1">
              {metric.label}
            </p>
            <p className="text-2xl font-extrabold text-agri-green">
              {metric.value}
            </p>
            {metric.badge && (
              <div className="mt-4 flex items-center gap-2">
                <span className="text-[10px] bg-agri-leaf/10 text-agri-leaf px-2 py-0.5 rounded-full font-bold">
                  {metric.badge}
                </span>
              </div>
            )}
            {metric.helper && !metric.badge && (
              <p className="text-xs text-slate-400 mt-4">{metric.helper}</p>
            )}
          </div>
        );
      })}
    </section>
  );
}
