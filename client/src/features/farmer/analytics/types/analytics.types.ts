export type Metric = {
  id: string;
  label: string;
  value: string;
  helper: string;
  tone?: "default" | "accent";
};

export type RevenuePoint = {
  label: string;
  value: number;
};

export type Insight = {
  title: string;
  highlight: string;
  detail: string;
};

export type ProductPerformance = {
  name: string;
  subtitle: string;
  volume: string;
  revenue: string;
  performance: number;
  tone?: "leaf" | "accent";
};
