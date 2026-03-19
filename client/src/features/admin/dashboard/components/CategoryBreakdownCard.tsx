import type { CategoryPerformance } from "../types/dashboard.types";

type CategoryBreakdownCardProps = {
  rows: CategoryPerformance[];
};

export default function CategoryBreakdownCard({ rows }: CategoryBreakdownCardProps) {
  return (
    <section className="rounded-[2.5rem] border border-gray-100 bg-white p-10 shadow-sm">
      <h2 className="mb-8 text-2xl font-bold text-agri-green">Sales by Category</h2>

      <div className="space-y-8">
        {rows.map((row) => (
          <div className="group" key={row.id}>
            <div className="mb-3 flex items-center justify-between">
              <span className="text-lg font-bold">{row.label}</span>
              <span className="font-bold text-agri-green">{row.percent}%</span>
            </div>
            <div className="h-4 w-full rounded-full bg-gray-50">
              <div
                className={`h-full rounded-full transition-all duration-1000 ${row.barColorClass}`}
                style={{ width: `${row.percent}%` }}
              />
            </div>
            <p className="mt-2 text-xs font-medium text-gray-400">{row.note}</p>
          </div>
        ))}
      </div>
    </section>
  );
}
