import type {
  CategoryPerformance,
  EscrowItem,
  InsightItem,
  LogisticsVolume,
  MetricCard,
  PendingListing,
  QaFeedItem,
  TrafficSource,
  VerificationApplication,
} from "../types/dashboard.types";

export const metrics: MetricCard[] = [
  {
    id: "total-farmers",
    label: "Total Farmers",
    value: "12.4k",
    sublabel: "+12% vs last month",
    description: "",
    variant: "stat",
  },
  {
    id: "distributors",
    label: "Distributors",
    value: "840",
    sublabel: "Verified hubs only",
    description: "",
    variant: "stat",
  },
  {
    id: "buyers",
    label: "Buyers",
    value: "45k",
    sublabel: "Active retail/bulk",
    description: "",
    variant: "stat",
  },
  {
    id: "gmv",
    label: "Volume (GMV)",
    value: "PHP 1.2B",
    sublabel: "YTD Performance",
    description: "",
    variant: "progress",
    progressPercent: 78,
  },
  {
    id: "platform-health",
    label: "Platform Health",
    value: "98%",
    sublabel: "Uptime Stable",
    description: "",
    variant: "health",
  },
];

export const categoryBreakdown: CategoryPerformance[] = [
  {
    id: "vegetables",
    label: "Vegetables",
    percent: 65,
    note: "Primarily Highland crops (Baguio Beans, Cabbage)",
    barColorClass: "bg-agri-green",
  },
  {
    id: "fruits",
    label: "Fruits",
    percent: 25,
    note: "Seasonal peak for Calamansi and Mangoes",
    barColorClass: "bg-agri-leaf",
  },
  {
    id: "grains",
    label: "Grains",
    percent: 10,
    note: "Premium Dinorado and Heirloom Rice",
    barColorClass: "bg-agri-accent",
  },
];

export const customerInsights: InsightItem[] = [
  {
    id: "wholesale-loyalty",
    title: "Wholesale Loyalty",
    description:
      "Recurring buyers from Manila-based restaurant chains show 85% re-order consistency within 14 days of first delivery.",
  },
  {
    id: "buyer-volume",
    title: "Buyer Volume",
    description:
      "New buyers are currently purchasing smaller trial volumes, while returning buyers account for 80% of total bulk volume.",
  },
];

export const trafficSources: TrafficSource[] = [
  {
    id: "marketplace-search",
    label: "Marketplace Search",
    percent: 70,
    description:
      "Buyers finding you via the 'Benguet Veggies' and 'Wholesale Rice' tags.",
    barColorClass: "bg-agri-green",
  },
  {
    id: "direct-visits",
    label: "Direct Visits",
    percent: 20,
    description:
      "Trusted clients using your unique store profile link directly.",
    barColorClass: "bg-agri-leaf",
  },
  {
    id: "referrals",
    label: "Referrals",
    percent: 10,
    description:
      "Traffic from community posts and farmer-to-farmer recommendations.",
    barColorClass: "bg-agri-accent",
  },
];

export const verificationApplications: VerificationApplication[] = [
  {
    id: "FRM-9921",
    farmerName: "Ricardo Pangilinan",
    initials: "RP",
    region: "Central Luzon",
    cropType: "Rice & Corn",
  },
  {
    id: "FRM-8832",
    farmerName: "Elena Arceo",
    initials: "EA",
    region: "Davao Region",
    cropType: "Cacao",
  },
  {
    id: "FRM-7741",
    farmerName: "Juan Mercado",
    initials: "JM",
    region: "Ilocos Norte",
    cropType: "Vegetables",
  },
];

export const escrowItems: EscrowItem[] = [
  {
    id: "TB-8821",
    orderLabel: "Order #TB-8821",
    statusLabel: "Released to Farmer",
    amountLabel: "PHP 45,200",
    variant: "normal",
  },
  {
    id: "TB-8819",
    orderLabel: "Order #TB-8819",
    statusLabel: "Released to Logistics",
    amountLabel: "PHP 8,400",
    variant: "normal",
  },
  {
    id: "TB-8815",
    orderLabel: "Order #TB-8815",
    statusLabel: "Held for QA",
    amountLabel: "PHP 124,000",
    variant: "muted",
  },
];

export const qaFeedItems: QaFeedItem[] = [
  {
    id: "qa1",
    name: "Benguet Strawberries",
    scoreLabel: "9.8",
    scorePercent: 98,
    variant: "leaf",
  },
  {
    id: "qa2",
    name: "Cavite Bananas",
    scoreLabel: "7.2",
    scorePercent: 72,
    variant: "accent",
  },
  {
    id: "qa3",
    name: "Pangasinan Salt",
    scoreLabel: "9.1",
    scorePercent: 91,
    variant: "leaf",
  },
];

export const pendingListings: PendingListing[] = [
  {
    id: "pl1",
    title: "Premium Dinorado Rice (25kg)",
    sellerName: "Posted by San Jose Farms",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuCixcJJx6_gQsR0CXirGyeTjJC3hE1MYWBdXdv8fpnlRpY_OW9rszsedhy5k4kpSMkiH296WsG-vzANgtZ4ypLgH9AmnVOLUSoBMaNC25GmihOHrPP6Qdj7Y6PIThs8vHS-aWSmUnKLYP8RPJu5JaSiTCB4w1EK4TpC_LMoIoVvCV_4jmV5WEzUaDPsRb36RbrbO8r-CRP0001jdiRtDOB2uUQ56g9uzcR7jkDPUy8HvZp2_l3joQXZkSXkFfzm62D4_L4vCwcJI50j",
  },
  {
    id: "pl2",
    title: "Fresh Milkfish (Bangus)",
    sellerName: "Posted by Navotas Trading",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuA_fdmuVLEKOnzYHTL8hLbi8alYtW4NAZ4vg1Y7f-bPGMIhcREuW9AoI3gCSB25kIwdwlSUxnSRfgohtwij3iZ6xl0gqreAhjELVH3h7bhMIAN-R3l6HEOJggQfNx1FUS6VnmPQmqAR8fQnEPWUmHju-a_rR-NEdqg-FFf6pHMSV1dJ-BA30jGaHwvS8giLrj3a-Ecg21R_0d11j43cjCJp_trC16YBgbcIm9AQ_9T0rx_0904WGzjwKEgntYxo6nYQe7xs5sDMyFi1",
  },
];

export const logisticsVolumes: LogisticsVolume[] = [
  { id: "lv1", regionLabel: "Luzon Logistics", percent: 68, variant: "leaf" },
  { id: "lv2", regionLabel: "Visayas Logistics", percent: 42, variant: "accent" },
  { id: "lv3", regionLabel: "Mindanao Logistics", percent: 55, variant: "leaf" },
];
