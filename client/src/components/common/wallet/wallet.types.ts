export type WalletMetric = {
  id: string;
  label: string;
  value: string;
  helper?: string;
  tone?: "default" | "accent";
};

export type WalletBreakdownItem = {
  id: string;
  label: string;
  detail: string;
  amount: string;
  tone?: "green" | "accent";
};

export type WalletTransaction = {
  id: string;
  date: string;
  type: string;
  description: string;
  subtext: string;
  amount: string;
  status: string;
  statusTone?: "settled" | "processed";
  typeTone?: "green" | "accent" | "blue";
  amountTone?: "default" | "negative";
};
