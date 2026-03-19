import type { Metric } from "../types/analytics.types";

type SummaryMetricsProps = {
  metrics: Metric[];
};

export default function SummaryMetrics({ metrics }: SummaryMetricsProps) {
  return (
    <section
      className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-12"
      data-purpose="summary-stats"
    >
      {metrics.map((metric) => (
        <div
          key={metric.id}
          className="bg-white p-8 rounded-3xl shadow-sm border border-gray-100 transition-transform hover:-translate-y-0.5"
        >
          <p className="text-xs uppercase tracking-widest text-gray-400 font-bold mb-4">
            {metric.label}
          </p>
          <h3
            className={
              metric.tone === "accent"
                ? "text-3xl font-display font-bold text-agri-accent"
                : "text-3xl font-display font-bold text-agri-green"
            }
          >
            {metric.value}
          </h3>
          <p className="text-xs text-gray-500 mt-2 font-medium">
            {metric.helper}
          </p>
        </div>
      ))}
    </section>
  );
}
