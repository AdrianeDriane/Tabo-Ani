export type OrderTab = "active" | "completed" | "all";

export type TrackingStepState = "done" | "current" | "todo";

export type TrackingStep = {
  id: string;
  label: string;
  state: TrackingStepState;
};

export type DeliveryPoint = {
  label: string;
  value: string;
  highlight: "origin" | "destination";
};

export type OrderSummaryCard = {
  id: string;
  title: string;
  statusLabel: string;
  statusTone: "info" | "success" | "neutral";
  supplierName: string;
  totalAmountLabel: string;
};

export type OrderTrackingDetail = {
  orderNumberLabel: string;
  productTitle: string;
  statusLabel: string;
  supplierName: string;
  totalAmountLabel: string;
  trackingSteps: TrackingStep[];
  deliveryPoints: DeliveryPoint[];
  distributorName: string;
  etaLabel: string;
  qaFreshnessPercent: number;
  qaSummary: string;
  escrowLabel: string;
  escrowSummary: string;
};

export type OrderPreview = {
  id: string;
  title: string;
  subtitle: string;
};

export type OrderDetailsData = {
  buyerName: string;
  buyerInitials: string;
  pageTitle: string;
  pageSubtitle: string;
  tabs: Array<{ id: OrderTab; label: string; count?: number }>;
  selectedTab: OrderTab;
  summary: OrderSummaryCard;
  detail: OrderTrackingDetail;
  secondaryPreview: OrderPreview;
};
