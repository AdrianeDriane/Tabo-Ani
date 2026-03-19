import type { OrderHistoryItem } from "../types/buyerDashboard.types";

type OrderHistoryTimelineProps = {
  items: OrderHistoryItem[];
};

export default function OrderHistoryTimeline({
  items,
}: OrderHistoryTimelineProps) {
  return (
    <section data-purpose="order-history-timeline">
      <h2 className="text-xl font-bold text-agri-green mb-6">Order History</h2>
      <div className="relative pl-6 space-y-8 before:content-[''] before:absolute before:left-[11px] before:top-2 before:bottom-2 before:w-0.5 before:bg-slate-100">
        {items.map((item) => (
          <div key={item.invoice} className="relative">
            <div
              className={
                item.tone === "accent"
                  ? "absolute -left-[21px] top-1 w-[12px] h-[12px] rounded-full bg-agri-accent ring-4 ring-white"
                  : "absolute -left-[21px] top-1 w-[12px] h-[12px] rounded-full bg-slate-200 ring-4 ring-white"
              }
            />
            <div>
              <p className="text-[10px] font-bold text-slate-400 uppercase tracking-wider">
                {item.date}
              </p>
              <p className="text-sm font-bold text-slate-800 mt-1">
                {item.invoice}
              </p>
              <p className="text-xs text-slate-500">
                {item.vendor} · {item.amount}
              </p>
              <button className="inline-block mt-2 text-[10px] font-extrabold text-agri-leaf uppercase tracking-tighter">
                View Invoice
              </button>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
