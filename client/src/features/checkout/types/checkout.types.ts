export type PaymentMethod = "cod" | "gcash";

export type CheckoutNavLink = {
  id: string;
  label: string;
  href: string;
};

export type DeliveryField = {
  id: string;
  label: string;
  placeholder: string;
  value?: string;
  type?: "text";
  colSpan?: "single" | "full";
};

export type PaymentOption = {
  id: PaymentMethod;
  label: string;
  description: string;
};

export type CheckoutItem = {
  id: string;
  name: string;
  farmName: string;
  variantLabel: string;
  imageUrl: string;
  amountLabel: string;
};

export type CheckoutTotals = {
  subtotalLabel: string;
  deliveryFeeLabel: string;
  totalLabel: string;
};

export type CheckoutData = {
  buyerName: string;
  buyerInitials: string;
  navLinks: CheckoutNavLink[];
  pageTitle: string;
  pageSubtitle: string;
  deliveryFields: DeliveryField[];
  paymentOptions: PaymentOption[];
  defaultPaymentMethod: PaymentMethod;
  handlingNotesPlaceholder: string;
  items: CheckoutItem[];
  totals: CheckoutTotals;
};
