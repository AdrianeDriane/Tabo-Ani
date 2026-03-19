import type { DeliveryTab } from "../types/distributorDashboard.types";

type DeliveryFiltersProps = {
  tabs: DeliveryTab[];
  activeId: DeliveryTab["id"];
};

export default function DeliveryFilters({
  tabs,
  activeId,
}: DeliveryFiltersProps) {
  return (
    <div className="flex items-center justify-between mb-6">
      <div
        className="flex space-x-2 bg-slate-200/50 p-1.5 rounded-2xl"
        data-purpose="tab-navigation"
      >
        {tabs.map((tab) => (
          <button
            key={tab.id}
            className={
              tab.id === activeId
                ? "px-6 py-2.5 rounded-xl text-sm font-bold bg-white shadow-sm text-agri-green"
                : "px-6 py-2.5 rounded-xl text-sm font-bold text-slate-500 hover:text-slate-700"
            }
          >
            {tab.label}
          </button>
        ))}
      </div>
      <button className="text-sm font-bold text-agri-leaf border-b-2 border-agri-leaf pb-0.5">
        Filter by Date
      </button>
    </div>
  );
}
