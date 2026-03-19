import type { Insight, Metric, ProductPerformance, RevenuePoint } from "../types/analytics.types";

export const metrics: Metric[] = [
  {
    id: "total-sales",
    label: "Total Sales",
    value: "₱428,500",
    helper: "Life-to-date",
  },
  {
    id: "avg-order",
    label: "Avg. Order Value",
    value: "₱8,570",
    helper: "Per wholesale lot",
  },
  {
    id: "retention",
    label: "Retention",
    value: "24%",
    helper: "Recurring buyers",
  },
  {
    id: "growth",
    label: "Growth Trend",
    value: "+12.5%",
    helper: "Vs last season",
    tone: "accent",
  },
];

export const revenueSeries: RevenuePoint[] = [
  { label: "Oct", value: 42000 },
  { label: "Nov", value: 58000 },
  { label: "Dec", value: 85000 },
  { label: "Jan", value: 72000 },
  { label: "Feb", value: 91000 },
  { label: "Mar", value: 80500 },
];

export const marketInsight: Insight = {
  title: "Market Insights",
  highlight: "Leafy greens are seeing 15% higher demand in Metro Manila this week.",
  detail:
    "Consider prioritizing your Cabbage and Pechay harvests for the upcoming Wednesday logistics window.",
};

export const productPerformance: ProductPerformance[] = [
  {
    name: "Highland Carrots",
    subtitle: "Class A Standard",
    volume: "1,240 kg",
    revenue: "₱111,600",
    performance: 92,
    tone: "leaf",
  },
  {
    name: "Benguet Strawberries",
    subtitle: "Premium Export Grade",
    volume: "450 kg",
    revenue: "₱180,000",
    performance: 85,
    tone: "leaf",
  },
  {
    name: "Red Creole Onions",
    subtitle: "Cured Bulb Selection",
    volume: "2,100 kg",
    revenue: "₱136,900",
    performance: 60,
    tone: "accent",
  },
];
