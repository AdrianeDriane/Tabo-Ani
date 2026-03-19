import type { Delivery } from "../types/qaReporting.types";

type AssignedDeliveriesSidebarProps = {
  deliveries: Delivery[];
};

export default function AssignedDeliveriesSidebar({
  deliveries,
}: AssignedDeliveriesSidebarProps) {
  return (
    <aside
      className="w-80 flex-shrink-0 transition-all duration-300"
      data-purpose="collapsible-sidebar"
      id="delivery-sidebar"
    >
      <div className="sticky top-28 bg-white border border-agri-border rounded-xl p-5 shadow-sm">
        <h3 className="font-display text-sm font-bold text-agri-green uppercase tracking-wider mb-4">
          Assigned Deliveries
        </h3>
        <div className="space-y-3 custom-scrollbar max-h-[70vh] overflow-y-auto pr-2">
          {deliveries.map((item) => (
            <div
              key={item.id}
              className={
                item.isActive
                  ? "p-3 bg-agri-light border-l-4 border-agri-green rounded-r-lg"
                  : "p-3 hover:bg-gray-50 border border-transparent rounded-lg cursor-pointer transition-colors"
              }
            >
              <p
                className={
                  item.isActive
                    ? "text-xs font-bold text-agri-green mb-1"
                    : "text-xs font-bold text-gray-400 mb-1"
                }
              >
                ID: #{item.id}
              </p>
              <p
                className={
                  item.isActive
                    ? "text-sm font-semibold text-gray-900 leading-tight"
                    : "text-sm font-semibold text-gray-700 leading-tight"
                }
              >
                {item.title}
              </p>
              <p
                className={
                  item.isActive
                    ? "text-xs text-gray-500 mt-1"
                    : "text-xs text-gray-400 mt-1"
                }
              >
                Status: {item.status}
              </p>
            </div>
          ))}
        </div>
      </div>
    </aside>
  );
}
