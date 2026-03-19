import type {
  ActionCardData,
  ActiveDelivery,
  DeliveryTab,
  Kpi,
  WeeklyGoal,
} from "../types/distributorDashboard.types";

export const pageHeader = {
  eyebrow: "Partner Overview",
  title: "Delivery Operations",
  performance: "98.2%",
  status: "EXCELLENT",
};

export const kpis: Kpi[] = [
  {
    id: "active",
    label: "Active Deliveries",
    value: "4",
    badge: "LIVE",
  },
  {
    id: "completed",
    label: "Completed Today",
    value: "12",
    helper: "Target: 15",
  },
  {
    id: "total",
    label: "Total Deliveries",
    value: "482",
  },
  {
    id: "earnings",
    label: "Delivery Earnings",
    value: "₱12,450.00",
    tone: "highlight",
  },
];

export const deliveryTabs: DeliveryTab[] = [
  { id: "all", label: "All" },
  { id: "active", label: "Active" },
  { id: "completed", label: "Completed" },
];

export const activeDelivery: ActiveDelivery = {
  id: "#TA-9921",
  status: "Assigned",
  product: "Upland Potatoes",
  details: "500kg • Fresh Harvest",
  pickup: "La Trinidad, Benguet",
  dropoff: "Pasig Hub, Metro Manila",
  payout: "₱2,500.00",
};

export const actionCards: ActionCardData[] = [
  {
    id: "qa",
    title: "Quality Assurance",
    description: "1 pending QA report for order #TA-9918",
    cta: "Submit QA Report",
  },
  {
    id: "chat",
    title: "Operations Chat",
    description: "Hub Manager and Fleet Team",
    cta: "Group Chat",
    tone: "accent",
  },
];

export const weeklyGoal: WeeklyGoal = {
  progress: 80,
  headline: "Weekly Goal Status",
  detail:
    "Complete 8 more deliveries by Sunday to unlock Tier-2 incentives.",
};
