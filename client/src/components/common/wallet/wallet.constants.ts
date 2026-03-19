import type {
  WalletBreakdownItem,
  WalletTransaction,
} from "./wallet.types";

export const walletSummary = {
  title: "Partner Wallet",
  subtitle: "Manage your logistics earnings and payouts",
};

export const balanceCard = {
  label: "Total Available Balance",
  amount: "₱ 12,450.00",
  payoutNote: "Next payout scheduled: Monday",
  payoutAction: "Payout Settings",
};

export const breakdown: WalletBreakdownItem[] = [
  {
    id: "fees",
    label: "Delivery Fees",
    detail: "42 orders completed",
    amount: "₱ 10,200.00",
    tone: "green",
  },
  {
    id: "bonus",
    label: "Performance Bonus",
    detail: "100% on-time rating",
    amount: "₱ 2,250.00",
    tone: "accent",
  },
];

export const pendingNote = {
  title: "Pending Transaction",
  body: "Awaiting completion of Order #TA-8845 for payout processing.",
};

export const transactions: WalletTransaction[] = [
  {
    id: "ta-8830",
    date: "Oct 24, 2023",
    type: "Delivery Payment",
    description: "Batch Delivery #TA-8830",
    subtext: "12 drop-off points",
    amount: "₱ 3,450.00",
    status: "Settled",
    statusTone: "settled",
    typeTone: "green",
  },
  {
    id: "bonus-week",
    date: "Oct 23, 2023",
    type: "Bonus",
    description: "Weekly Performance Bonus",
    subtext: "High efficiency rating",
    amount: "₱ 1,000.00",
    status: "Settled",
    statusTone: "settled",
    typeTone: "accent",
  },
  {
    id: "ta-8799",
    date: "Oct 22, 2023",
    type: "Delivery Payment",
    description: "Express Order #TA-8799",
    subtext: "Direct farm-to-door",
    amount: "₱ 850.00",
    status: "Settled",
    statusTone: "settled",
    typeTone: "green",
  },
  {
    id: "gcash",
    date: "Oct 20, 2023",
    type: "Withdrawal",
    description: "GCash Transfer",
    subtext: "Ref: 998-001-442",
    amount: "- ₱ 5,000.00",
    status: "Processed",
    statusTone: "processed",
    typeTone: "blue",
    amountTone: "negative",
  },
  {
    id: "ta-8750",
    date: "Oct 19, 2023",
    type: "Delivery Payment",
    description: "Batch Delivery #TA-8750",
    subtext: "8 drop-off points",
    amount: "₱ 2,150.00",
    status: "Settled",
    statusTone: "settled",
    typeTone: "green",
  },
];
