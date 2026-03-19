export { default as AnalyticsPageContent } from "./components/AnalyticsPageContent";
export { default as AnalyticsHeader } from "./components/AnalyticsHeader";
export { default as InsightsCard } from "./components/InsightsCard";
export { default as ProductPerformanceTable } from "./components/ProductPerformanceTable";
export { default as RevenueLineChart } from "./components/RevenueLineChart";
export { default as RevenueTrendsCard } from "./components/RevenueTrendsCard";
export { default as SummaryMetrics } from "./components/SummaryMetrics";

export {
  marketInsight,
  metrics,
  productPerformance,
  revenueSeries,
} from "./utils/analytics.constants";

export type {
  Insight,
  Metric,
  ProductPerformance,
  RevenuePoint,
} from "./types/analytics.types";
