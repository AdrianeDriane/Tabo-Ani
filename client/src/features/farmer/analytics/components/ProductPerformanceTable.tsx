import type { ProductPerformance } from "../types/analytics.types";

type ProductPerformanceTableProps = {
  rows: ProductPerformance[];
};

export default function ProductPerformanceTable({
  rows,
}: ProductPerformanceTableProps) {
  return (
    <section
      className="mt-8 bg-white p-8 rounded-3xl shadow-sm border border-gray-100"
      data-purpose="ranking-table"
    >
      <h2 className="text-xl font-display font-bold text-agri-green mb-8">
        Product Performance Ranking
      </h2>
      <div className="overflow-x-auto">
        <table className="w-full text-left">
          <thead>
            <tr className="text-xs uppercase tracking-widest text-gray-400 font-bold border-b border-gray-50">
              <th className="pb-4 font-bold">Crop Variety</th>
              <th className="pb-4 font-bold">Volume Sold</th>
              <th className="pb-4 font-bold">Total Revenue</th>
              <th className="pb-4 font-bold text-right">Performance</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {rows.map((row) => (
              <tr key={row.name} className="group">
                <td className="py-6">
                  <span className="font-semibold text-agri-green block">
                    {row.name}
                  </span>
                  <span className="text-xs text-gray-400">{row.subtitle}</span>
                </td>
                <td className="py-6 text-sm text-gray-600">{row.volume}</td>
                <td className="py-6 text-sm font-semibold text-agri-green">
                  {row.revenue}
                </td>
                <td className="py-6 text-right">
                  <span className="inline-block w-24 h-1.5 bg-gray-100 rounded-full overflow-hidden">
                    <span
                      className={
                        row.tone === "accent"
                          ? "block h-full bg-agri-accent"
                          : "block h-full bg-agri-leaf"
                      }
                      style={{ width: `${row.performance}%` }}
                    />
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  );
}
