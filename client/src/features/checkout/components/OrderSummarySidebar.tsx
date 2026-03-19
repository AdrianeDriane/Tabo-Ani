import type { CheckoutItem, CheckoutTotals } from "../types/checkout.types";

type OrderSummarySidebarProps = {
  items: CheckoutItem[];
  totals: CheckoutTotals;
};

export default function OrderSummarySidebar({ items, totals }: OrderSummarySidebarProps) {
  return (
    <section className="lg:col-span-5">
      <div className="sticky top-32 rounded-3xl border border-gray-100 bg-white p-10 shadow-sm">
        <h2 className="mb-8 text-xl font-bold text-gray-900 font-display">Order Summary</h2>

        <div className="mb-8 space-y-6">
          {items.map((item) => (
            <div className="flex items-start justify-between" key={item.id}>
              <div className="flex gap-4">
                <div className="h-16 w-16 shrink-0 overflow-hidden rounded-2xl bg-gray-100">
                  <img alt={item.name} className="h-full w-full object-cover" src={item.imageUrl} />
                </div>

                <div>
                  <h3 className="font-bold text-gray-900">{item.name}</h3>
                  <p className="text-sm text-gray-500">{item.farmName}</p>
                  <p className="mt-1 text-sm font-medium text-gray-900">{item.variantLabel}</p>
                </div>
              </div>

              <span className="font-bold text-gray-900">{item.amountLabel}</span>
            </div>
          ))}
        </div>

        <div className="space-y-4 border-t border-gray-100 pt-6">
          <div className="flex justify-between text-sm">
            <span className="font-medium text-gray-500">Subtotal</span>
            <span className="font-semibold text-gray-900">{totals.subtotalLabel}</span>
          </div>
          <div className="flex justify-between text-sm">
            <span className="font-medium text-gray-500">Delivery Fee (Logistics)</span>
            <span className="font-semibold text-gray-900">{totals.deliveryFeeLabel}</span>
          </div>
          <div className="flex justify-between border-t border-gray-100 pt-4 text-lg">
            <span className="font-bold text-gray-900">Total</span>
            <span className="font-extrabold text-agri-green">{totals.totalLabel}</span>
          </div>
        </div>

        <button
          className="mt-10 w-full rounded-2xl bg-agri-accent py-5 text-lg font-extrabold text-white shadow-lg shadow-orange-100 transition-colors hover:bg-[#E66000]"
          type="button"
        >
          Review Order
        </button>

        <div className="mt-6 text-center">
          <p className="text-xs font-medium text-gray-400">
            Prices are inclusive of VAT and agricultural levies.
          </p>
        </div>
      </div>
    </section>
  );
}
