import type {
  Metric,
  OrderHistoryItem,
  PreferredSupplier,
  QuickAction,
  RecentOrder,
  SupplierDirectoryItem,
} from "../types/buyerDashboard.types";

export const metrics: Metric[] = [
  {
    id: "total-spend",
    label: "Total Spend",
    value: "₱842,500.20",
    badge: "+12% vs last month",
  },
  {
    id: "active-orders",
    label: "Active Orders",
    value: "12",
    helper: "4 shipments arriving today",
  },
  {
    id: "saved-suppliers",
    label: "Saved Suppliers",
    value: "45",
    helper: "2 new added this week",
  },
  {
    id: "rebate-balance",
    label: "Rebate Balance",
    value: "₱12,400.00",
    helper: "Apply to next order",
    tone: "gradient",
  },
];

export const recentOrders: RecentOrder[] = [
  {
    id: "#TA-8921",
    vendor: "Sari-Sari Mart Inc.",
    item: "Premium Benguet Potatoes (500kg)",
    amount: "₱12,450.00",
    status: "IN TRANSIT",
  },
  {
    id: "#TA-8845",
    vendor: "Benguet Highland Farms",
    item: "Organic Cabbage & Carrots Bundle",
    amount: "₱8,920.00",
    status: "DELIVERED",
  },
];

export const supplierDirectory: SupplierDirectoryItem[] = [
  {
    name: "Benguet Highland Farms",
    initials: "BH",
    rating: "4.9",
    ratingNote: "High Yield",
  },
  {
    name: "GreenWay Logistics",
    initials: "GW",
    rating: "4.7",
    ratingNote: "Reliable",
  },
];

export const quickActions: QuickAction[] = [
  { id: "browse", label: "Browse Marketplace" },
  { id: "reorder", label: "Reorder Common Items" },
  { id: "track", label: "Track Active Orders" },
];

export const preferredSuppliers: PreferredSupplier[] = [
  {
    name: "Sari-Sari Mart Inc.",
    initials: "SS",
    status: "Active Now",
  },
  {
    name: "Davao Fruit Hub",
    initials: "DF",
    status: "Offline",
  },
  {
    name: "Nueva Vizcaya Direct",
    initials: "NV",
    status: "Active Now",
  },
];

export const orderHistory: OrderHistoryItem[] = [
  {
    date: "Oct 24, 2023",
    invoice: "Invoice #INV-2045",
    vendor: "Benguet Highland Farms",
    amount: "₱15,200.00",
    tone: "accent",
  },
  {
    date: "Oct 21, 2023",
    invoice: "Invoice #INV-2012",
    vendor: "GreenWay Logistics",
    amount: "₱4,300.00",
    tone: "muted",
  },
];
