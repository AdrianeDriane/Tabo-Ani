import type { OrderTab } from "../types/orders.types";

type OrdersTabsProps = {
  tabs: Array<{ id: OrderTab; label: string; count?: number }>;
  selectedTab: OrderTab;
};

export function OrdersTabs({ tabs, selectedTab }: OrdersTabsProps) {
  return (
    <div className="mb-8 flex gap-8 border-b border-gray-200">
      {tabs.map((tab) => {
        const isSelected = tab.id === selectedTab;

        return (
          <button
            className={`pb-4 text-sm ${
              isSelected
                ? "border-b-2 border-[#14532D] font-semibold text-[#14532D]"
                : "font-medium text-gray-400 hover:text-gray-600"
            }`}
            key={tab.id}
            type="button"
          >
            {tab.label}
            {typeof tab.count === "number" ? ` (${tab.count})` : ""}
          </button>
        );
      })}
    </div>
  );
}
