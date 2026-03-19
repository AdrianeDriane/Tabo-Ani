import type { PreferredSupplier } from "../types/buyerDashboard.types";

type PreferredSuppliersProps = {
  suppliers: PreferredSupplier[];
  onView?: (supplierName: string) => void;
};

export default function PreferredSuppliers({
  suppliers,
  onView,
}: PreferredSuppliersProps) {
  return (
    <section data-purpose="preferred-suppliers">
      <h2 className="text-xl font-bold text-agri-green mb-6">
        Preferred Suppliers
      </h2>
      <div className="bg-white rounded-3xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)] border border-slate-100 p-6">
        <ul className="space-y-6">
          {suppliers.map((supplier) => (
            <li key={supplier.name} className="flex items-center justify-between">
              <div className="flex items-center gap-3">
                <div className="w-10 h-10 rounded-full bg-slate-100 flex items-center justify-center font-bold text-xs text-slate-500">
                  {supplier.initials}
                </div>
                <div>
                  <p className="text-sm font-bold text-slate-800">
                    {supplier.name}
                  </p>
                  <p
                    className={
                      supplier.status === "Active Now"
                        ? "text-[10px] text-agri-leaf font-semibold"
                        : "text-[10px] text-slate-400 font-semibold"
                    }
                  >
                    {supplier.status}
                  </p>
                </div>
              </div>
              <button
                className="text-xs font-bold text-agri-accent hover:underline"
                type="button"
                onClick={() => onView?.(supplier.name)}
              >
                View
              </button>
            </li>
          ))}
        </ul>
      </div>
    </section>
  );
}
