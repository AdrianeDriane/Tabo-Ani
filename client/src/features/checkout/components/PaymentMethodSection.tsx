import type { PaymentMethod, PaymentOption } from "../types/checkout.types";

type PaymentMethodSectionProps = {
  options: PaymentOption[];
  defaultMethod: PaymentMethod;
};

export default function PaymentMethodSection({
  options,
  defaultMethod,
}: PaymentMethodSectionProps) {
  return (
    <div className="rounded-3xl border border-gray-100 bg-white p-10 shadow-sm">
      <h2 className="mb-8 text-xl font-bold text-gray-900 font-display">Payment Method</h2>

      <div className="grid grid-cols-1 gap-4 md:grid-cols-2">
        {options.map((option) => (
          <div className="relative" key={option.id}>
            <input
              className="peer hidden"
              defaultChecked={defaultMethod === option.id}
              id={option.id}
              name="payment"
              type="radio"
            />
            <label
              className="block cursor-pointer rounded-2xl border-2 border-gray-100 p-6 transition-all hover:border-gray-200 peer-checked:border-agri-accent peer-checked:bg-orange-50"
              htmlFor={option.id}
            >
              <span className="text-sm font-bold text-gray-900">{option.label}</span>
              <span className="mt-1 block text-xs text-gray-500">{option.description}</span>
            </label>
          </div>
        ))}
      </div>
    </div>
  );
}
