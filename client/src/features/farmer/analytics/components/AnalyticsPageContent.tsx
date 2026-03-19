import AnalyticsHeader from "./AnalyticsHeader";
import InsightsCard from "./InsightsCard";
import ProductPerformanceTable from "./ProductPerformanceTable";
import RevenueTrendsCard from "./RevenueTrendsCard";
import SummaryMetrics from "./SummaryMetrics";
import {
  marketInsight,
  metrics,
  productPerformance,
  revenueSeries,
} from "../utils/analytics.constants";

export default function AnalyticsPageContent() {
  return (
    <main className="max-w-6xl mx-auto pt-32 px-6 pb-20">
      <AnalyticsHeader />
      <SummaryMetrics metrics={metrics} />

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        <RevenueTrendsCard data={revenueSeries} />
        <InsightsCard insight={marketInsight} />
      </div>

      <ProductPerformanceTable rows={productPerformance} />
    </main>
  );
}
