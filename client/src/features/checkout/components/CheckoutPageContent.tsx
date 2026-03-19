import { checkoutData } from "../utils/checkout.constants";
import CheckoutFooter from "./CheckoutFooter";
import CheckoutHeader from "./CheckoutHeader";
import CheckoutNavigation from "./CheckoutNavigation";
import DeliveryAddressForm from "./DeliveryAddressForm";
import HandlingInstructionsSection from "./HandlingInstructionsSection";
import OrderSummarySidebar from "./OrderSummarySidebar";
import PaymentMethodSection from "./PaymentMethodSection";

export default function CheckoutPageContent() {
  return (
    <div className="min-h-screen bg-[#F9FAFB] pb-20">
      <CheckoutNavigation
        buyerInitials={checkoutData.buyerInitials}
        buyerName={checkoutData.buyerName}
        navLinks={checkoutData.navLinks}
      />

      <main className="mx-auto max-w-6xl px-4 pt-32">
        <CheckoutHeader subtitle={checkoutData.pageSubtitle} title={checkoutData.pageTitle} />

        <div className="grid grid-cols-1 gap-10 lg:grid-cols-12">
          <section className="space-y-8 lg:col-span-7">
            <DeliveryAddressForm fields={checkoutData.deliveryFields} />
            <PaymentMethodSection
              defaultMethod={checkoutData.defaultPaymentMethod}
              options={checkoutData.paymentOptions}
            />
            <HandlingInstructionsSection placeholder={checkoutData.handlingNotesPlaceholder} />
          </section>

          <OrderSummarySidebar items={checkoutData.items} totals={checkoutData.totals} />
        </div>
      </main>

      <CheckoutFooter />
    </div>
  );
}
