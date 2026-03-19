import type { EscrowItem } from "../types/dashboard.types";

type EscrowMonitorCardProps = {
  items: EscrowItem[];
};

export default function EscrowMonitorCard({ items }: EscrowMonitorCardProps) {
  return (
    <div className="rounded-[2rem] border border-agri-green/5 bg-white p-8 shadow-sm">
      <h2 className="mb-6 text-lg font-bold tracking-tight text-agri-green uppercase font-display">
        Escrow Monitor
      </h2>

      <div className="space-y-4">
        {items.map((item) => (
          <div
            className={`flex items-center justify-between rounded-2xl bg-agri-light p-4 ${
              item.variant === "muted" ? "opacity-50" : ""
            }`}
            key={item.id}
          >
            <div>
              <p className="text-xs font-bold text-agri-green">{item.orderLabel}</p>
              <p className="text-[10px] font-medium text-agri-leaf">{item.statusLabel}</p>
            </div>
            <p className="text-sm font-bold text-agri-green">{item.amountLabel}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
