import type { SupplierDirectoryItem } from "../types/buyerDashboard.types";

type SupplierDirectoryProps = {
  suppliers: SupplierDirectoryItem[];
  onQuickReorder?: (supplierName: string) => void;
  onMessage?: (supplierName: string) => void;
};

export default function SupplierDirectory({
  suppliers,
  onQuickReorder,
  onMessage,
}: SupplierDirectoryProps) {
  return (
    <section data-purpose="supplier-directory">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-xl font-bold text-agri-green">Supplier Directory</h2>
        <div className="flex gap-2">
          <button className="p-2 bg-white rounded-xl border border-slate-100 shadow-sm">
            <span className="sr-only">Filter</span>
            <div className="w-4 h-4 bg-slate-400 rounded-sm" />
          </button>
        </div>
      </div>
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
        {suppliers.map((supplier) => (
          <div
            key={supplier.name}
            className="bg-white p-5 rounded-3xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)] border border-slate-100 flex items-start gap-4"
          >
            <div className="w-14 h-14 rounded-2xl bg-agri-light flex-shrink-0 flex items-center justify-center">
              <span className="text-agri-green font-bold text-lg">
                {supplier.initials}
              </span>
            </div>
            <div className="flex-grow">
              <h3 className="font-bold text-slate-800 leading-tight">
                {supplier.name}
              </h3>
              <div className="flex items-center gap-1 mt-1 mb-3">
                <div className="w-3 h-3 bg-agri-accent rounded-full" />
                <span className="text-[10px] font-bold text-slate-500">
                  {supplier.rating} Rating ({supplier.ratingNote})
                </span>
              </div>
              <div className="flex gap-2">
                <button
                  className="flex-1 py-2 bg-agri-green text-white text-[10px] font-bold rounded-xl"
                  type="button"
                  onClick={() => onQuickReorder?.(supplier.name)}
                >
                  Quick Reorder
                </button>
                <button
                  className="px-3 py-2 bg-agri-light text-agri-green text-[10px] font-bold rounded-xl border border-agri-green/10"
                  type="button"
                  onClick={() => onMessage?.(supplier.name)}
                >
                  Message
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
