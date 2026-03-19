export { default as CheckoutFooter } from "./components/CheckoutFooter";
export { default as CheckoutHeader } from "./components/CheckoutHeader";
export { default as CheckoutNavigation } from "./components/CheckoutNavigation";
export { default as CheckoutPageContent } from "./components/CheckoutPageContent";
export { default as DeliveryAddressForm } from "./components/DeliveryAddressForm";
export { default as HandlingInstructionsSection } from "./components/HandlingInstructionsSection";
export { default as OrderSummarySidebar } from "./components/OrderSummarySidebar";
export { default as PaymentMethodSection } from "./components/PaymentMethodSection";

export { checkoutData } from "./utils/checkout.constants";

export type {
  CheckoutData,
  CheckoutItem,
  CheckoutNavLink,
  CheckoutTotals,
  DeliveryField,
  PaymentMethod,
  PaymentOption,
} from "./types/checkout.types";
