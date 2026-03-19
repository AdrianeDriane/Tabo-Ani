import type { RevenuePoint } from "../types/analytics.types";
import RevenueLineChart from "./RevenueLineChart";

type RevenueTrendsCardProps = {
  data: RevenuePoint[];
};

export default function RevenueTrendsCard({ data }: RevenueTrendsCardProps) {
  return (
    <section
      className="lg:col-span-2 bg-white p-8 rounded-3xl shadow-sm border border-gray-100"
      data-purpose="revenue-chart-container"
    >
      <div className="flex items-center justify-between mb-8">
        <h2 className="text-xl font-display font-bold text-agri-green">
          Revenue Trends
        </h2>
        <span className="text-xs font-semibold px-3 py-1 bg-agri-light rounded-full text-agri-leaf">
          Current Harvest Season
        </span>
      </div>
      <div className="h-[350px] w-full">
        <RevenueLineChart data={data} />
      </div>
    </section>
  );
}
