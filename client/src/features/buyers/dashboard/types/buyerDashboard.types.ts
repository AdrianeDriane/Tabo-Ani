export type Metric = {
  id: string;
  label: string;
  value: string;
  helper?: string;
  badge?: string;
  tone?: "default" | "accent" | "gradient";
};

export type OrderStatus = "IN TRANSIT" | "DELIVERED";

export type RecentOrder = {
  id: string;
  vendor: string;
  item: string;
  amount: string;
  status: OrderStatus;
};

export type SupplierDirectoryItem = {
  name: string;
  initials: string;
  rating: string;
  ratingNote: string;
};

export type QuickAction = {
  id: string;
  label: string;
};

export type PreferredSupplier = {
  name: string;
  initials: string;
  status: "Active Now" | "Offline";
};

export type OrderHistoryItem = {
  date: string;
  invoice: string;
  vendor: string;
  amount: string;
  tone?: "accent" | "muted";
};
