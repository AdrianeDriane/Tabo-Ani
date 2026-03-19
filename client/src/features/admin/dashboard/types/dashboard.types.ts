export type MetricCard = {
  id: string;
  label: string;
  value: string;
  sublabel: string;
  description: string;
  variant: "stat" | "progress" | "health";
  progressPercent?: number;
};

export type CategoryPerformance = {
  id: string;
  label: string;
  percent: number;
  note: string;
  barColorClass: string;
};

export type InsightItem = {
  id: string;
  title: string;
  description: string;
};

export type VerificationApplication = {
  id: string;
  farmerName: string;
  initials: string;
  region: string;
  cropType: string;
};

export type EscrowItem = {
  id: string;
  orderLabel: string;
  statusLabel: string;
  amountLabel: string;
  variant: "normal" | "muted";
};

export type QaFeedItem = {
  id: string;
  name: string;
  scoreLabel: string;
  scorePercent: number;
  variant: "leaf" | "accent";
};

export type PendingListing = {
  id: string;
  title: string;
  sellerName: string;
  imageUrl: string;
};

export type LogisticsVolume = {
  id: string;
  regionLabel: string;
  percent: number;
  variant: "leaf" | "accent";
};

export type TrafficSource = {
  id: string;
  label: string;
  percent: number;
  description: string;
  barColorClass: string;
};
