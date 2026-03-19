export type Kpi = {
  id: string;
  label: string;
  value: string;
  helper?: string;
  badge?: string;
  tone?: "default" | "highlight";
};

export type DeliveryTab = {
  id: "all" | "active" | "completed";
  label: string;
};

export type ActiveDelivery = {
  id: string;
  status: string;
  product: string;
  details: string;
  pickup: string;
  dropoff: string;
  payout: string;
};

export type ActionCard = {
  id: string;
  title: string;
  description: string;
  cta: string;
  tone?: "default" | "accent";
};

export type WeeklyGoal = {
  progress: number;
  headline: string;
  detail: string;
};
