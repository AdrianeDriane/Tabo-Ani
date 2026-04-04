type BuyerDashboardHeaderProps = {
  onNewOrder?: () => void;
};

export default function BuyerDashboardHeader({ onNewOrder }: BuyerDashboardHeaderProps) {
  return (
    <section className="mb-10 flex flex-col md:flex-row md:items-end justify-between gap-4">
      <div>
        <h1 className="text-3xl md:text-4xl font-extrabold text-agri-green tracking-tight">
          Procurement Oversight
        </h1>
        <p className="text-slate-500 mt-2 max-w-md">
          Manage your supply chain efficiency and nurture trusted agricultural
          relationships.
        </p>
      </div>
      <div className="flex gap-3">
        <button
          className="px-6 py-3 bg-agri-green text-white rounded-2xl font-semibold text-sm hover:bg-opacity-90 transition-all shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)]"
          onClick={onNewOrder}
          type="button"
        >
          New Purchase Order
        </button>
      </div>
    </section>
  );
}
