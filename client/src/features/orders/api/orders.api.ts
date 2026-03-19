import type { OrderDetailsData } from "../types/orders.types";

const ORDER_DETAILS_BY_ID: Record<string, OrderDetailsData> = {
  "TA-8821": {
    buyerName: "Ricardo Santos",
    buyerInitials: "RS",
    pageTitle: "Your Purchases",
    pageSubtitle:
      "Monitor your active supply chains with full fulfillment visibility. All payments are secured by Tabo-Ani Escrow until quality check is approved.",
    tabs: [
      { id: "active", label: "Active", count: 3 },
      { id: "completed", label: "Completed" },
      { id: "all", label: "All Orders" },
    ],
    selectedTab: "active",
    summary: {
      id: "TA-8821",
      title: "500kg Premium Benguet Potatoes",
      statusLabel: "In Transit",
      statusTone: "info",
      supplierName: "Benguet Highland Farms",
      totalAmountLabel: "PHP 42,500.00",
    },
    detail: {
      orderNumberLabel: "Order #TA-8821",
      productTitle: "500kg Premium Benguet Potatoes",
      statusLabel: "In Transit",
      supplierName: "Benguet Highland Farms",
      totalAmountLabel: "PHP 42,500.00",
      trackingSteps: [
        { id: "s1", label: "Placed", state: "done" },
        { id: "s2", label: "Paid", state: "done" },
        { id: "s3", label: "Ready", state: "done" },
        { id: "s4", label: "In Transit", state: "current" },
        { id: "s5", label: "Delivered", state: "todo" },
      ],
      deliveryPoints: [
        {
          label: "From (Pickup)",
          value: "La Trinidad, Benguet",
          highlight: "origin",
        },
        {
          label: "To (Destination)",
          value: "Metro Manila (Main Warehouse)",
          highlight: "destination",
        },
      ],
      distributorName: "FastTrack Logistics",
      etaLabel: "ETA: Oct 26, 2023",
      qaFreshnessPercent: 98,
      qaSummary:
        "Batch verified by distributor at pickup. Visual proof matches highland standards.",
      escrowLabel: "Funds Protected",
      escrowSummary:
        "PHP 42,500.00 is held in Tabo-Ani Escrow. Funds will only be released once you confirm receipt and quality.",
    },
    secondaryPreview: {
      id: "TA-7720",
      title: "200kg Baguio Strawberries",
      subtitle: "Ready for Pickup - Benguet Farm Direct",
    },
  },
};

export function getOrderDetailsById(orderId: string): OrderDetailsData | null {
  return ORDER_DETAILS_BY_ID[orderId] ?? null;
}
