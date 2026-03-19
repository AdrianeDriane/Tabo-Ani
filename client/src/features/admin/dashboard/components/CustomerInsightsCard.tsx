import type { InsightItem } from "../types/dashboard.types";

type CustomerInsightsCardProps = {
  insights: InsightItem[];
};

export default function CustomerInsightsCard({ insights }: CustomerInsightsCardProps) {
  return (
    <section className="rounded-[2.5rem] bg-agri-green p-10 text-white shadow-xl">
      <h2 className="mb-8 text-2xl font-bold">Customer Insights</h2>

      <div className="mb-10 flex items-center space-x-12">
        <div className="text-center">
          <p className="mb-2 text-5xl font-bold">32%</p>
          <p className="text-xs font-semibold tracking-widest text-agri-leaf uppercase">Returning</p>
        </div>
        <div className="h-12 w-px bg-white/20" />
        <div className="text-center">
          <p className="mb-2 text-5xl font-bold">68%</p>
          <p className="text-xs font-semibold tracking-widest text-white/60 uppercase">New Buyers</p>
        </div>
      </div>

      <div className="space-y-6">
        {insights.map((insight) => (
          <div className="rounded-2xl border border-white/10 bg-white/10 p-6" key={insight.id}>
            <h3 className="mb-2 text-lg font-bold">{insight.title}</h3>
            <p className="text-sm leading-relaxed text-white/80">{insight.description}</p>
          </div>
        ))}
      </div>
    </section>
  );
}
