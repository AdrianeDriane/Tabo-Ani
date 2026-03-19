import type { MetricCard } from "../types/dashboard.types";

type MetricHighlightsProps = {
  metrics: MetricCard[];
};

export default function MetricHighlights({ metrics }: MetricHighlightsProps) {
  return (
    <section className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-5">
      {metrics.map((metric) => (
        <div
          className={`rounded-3xl border p-6 shadow-sm ${
            metric.variant === "health"
              ? "border-agri-green/5 bg-agri-green"
              : "border-agri-green/5 bg-white"
          }`}
          key={metric.id}
        >
          <p
            className={`mb-2 text-[10px] font-bold tracking-widest uppercase ${
              metric.variant === "health" ? "text-white/60" : "text-agri-leaf"
            }`}
          >
            {metric.label}
          </p>

          <div className="flex items-baseline space-x-2">
            <span
              className={`stat-value text-3xl font-bold ${
                metric.variant === "health" ? "text-white" : "text-agri-green"
              }`}
            >
              {metric.value}
            </span>
            {metric.sublabel ? (
              <span
                className={`text-xs font-semibold ${
                  metric.variant === "health"
                    ? "text-agri-leaf"
                    : metric.variant === "progress"
                      ? "text-agri-accent"
                      : "text-agri-leaf"
                }`}
              >
                {metric.sublabel}
              </span>
            ) : null}
          </div>

          {metric.variant === "progress" ? (
            <div className="mt-3 h-1.5 w-full overflow-hidden rounded-full bg-gray-100">
              <div
                className="h-full bg-agri-leaf"
                style={{ width: `${metric.progressPercent ?? 0}%` }}
              />
            </div>
          ) : null}

          {metric.description ? (
            <p
              className={`mt-2 text-xs leading-relaxed ${
                metric.variant === "health" ? "text-white/70" : "text-gray-500"
              }`}
            >
              {metric.description}
            </p>
          ) : null}
        </div>
      ))}
    </section>
  );
}
