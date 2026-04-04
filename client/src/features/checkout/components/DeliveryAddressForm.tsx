import type { DeliveryField } from "../types/checkout.types";

type DeliveryAddressFormProps = {
  fields: DeliveryField[];
};

export default function DeliveryAddressForm({ fields }: DeliveryAddressFormProps) {
  return (
    <div className="rounded-3xl border border-gray-100 bg-white p-10 shadow-sm">
      <h2 className="mb-8 text-xl font-bold text-gray-900 font-display">Delivery Address</h2>

      <form className="grid grid-cols-1 gap-6 md:grid-cols-2">
        {fields.map((field) => (
          <div className={field.colSpan === "full" ? "md:col-span-2" : ""} key={field.id}>
            <label className="mb-2 block text-xs font-bold tracking-wider text-gray-500 uppercase">
              {field.label}
            </label>
            <input
              className="w-full rounded-xl border-gray-200 p-4 text-gray-900 focus:border-agri-accent focus:ring-agri-accent"
              defaultValue={field.value}
              placeholder={field.placeholder}
              type={field.type ?? "text"}
            />
          </div>
        ))}
      </form>
    </div>
  );
}
