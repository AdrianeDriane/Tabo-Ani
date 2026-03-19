import type { Kpi } from "../types/distributorDashboard.types";

type KpiSectionProps = {
  items: Kpi[];
};

export default function KpiSection({ items }: KpiSectionProps) {
  return (
    <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-12">
      {items.map((kpi) => {
        if (kpi.tone === "highlight") {
          return (
            <div
              key={kpi.id}
              className="bg-agri-green p-6 rounded-3xl border border-agri-green shadow-xl"
              data-purpose="kpi-card-highlight"
            >
              <p className="text-agri-leaf font-semibold text-sm mb-2">
                {kpi.label}
              </p>
              <div className="flex items-baseline space-x-1">
                <span className="text-3xl font-extrabold text-white">
                  {kpi.value}
                </span>
              </div>
              <div className="mt-4 w-full h-1 bg-white/10 rounded-full overflow-hidden">
                <div className="w-3/4 h-full bg-agri-accent" />
              </div>
            </div>
          );
        }

        return (
          <div
            key={kpi.id}
            className="bg-white p-6 rounded-3xl border border-slate-100 shadow-sm hover:shadow-md transition-shadow"
            data-purpose="kpi-card"
          >
            <p className="text-slate-500 font-medium text-sm mb-2">
              {kpi.label}
            </p>
            <div className="flex items-baseline space-x-2">
              <span className="text-4xl font-extrabold text-agri-green">
                {kpi.value}
              </span>
              {kpi.badge && (
                <span className="text-xs font-bold text-agri-leaf bg-agri-leaf/10 px-2 py-0.5 rounded-full">
                  {kpi.badge}
                </span>
              )}
              {kpi.helper && (
                <span className="text-xs font-bold text-slate-400">
                  {kpi.helper}
                </span>
              )}
            </div>
          </div>
        );
      })}
    </section>
  );
}
