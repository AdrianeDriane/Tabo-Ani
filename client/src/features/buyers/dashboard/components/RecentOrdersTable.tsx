import type { RecentOrder } from "../types/buyerDashboard.types";

type RecentOrdersTableProps = {
  orders: RecentOrder[];
  onQuickReorder?: (orderId: string) => void;
};

const statusStyles: Record<RecentOrder["status"], string> = {
  "IN TRANSIT": "bg-blue-50 text-blue-600",
  DELIVERED: "bg-agri-leaf/10 text-agri-leaf",
};

export default function RecentOrdersTable({
  orders,
  onQuickReorder,
}: RecentOrdersTableProps) {
  return (
    <section data-purpose="recent-orders-table">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-xl font-bold text-agri-green">Recent Orders</h2>
        <button className="text-sm font-semibold text-agri-leaf hover:underline">
          View All
        </button>
      </div>
      <div className="bg-white rounded-3xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)] border border-slate-100 overflow-hidden">
        <div className="overflow-x-auto">
          <table className="w-full text-left">
            <thead className="bg-agri-light/50 border-b border-slate-100">
              <tr>
                <th className="px-6 py-4 text-xs font-bold text-slate-500 uppercase tracking-wider">
                  Order Details
                </th>
                <th className="px-6 py-4 text-xs font-bold text-slate-500 uppercase tracking-wider">
                  Amount
                </th>
                <th className="px-6 py-4 text-xs font-bold text-slate-500 uppercase tracking-wider">
                  Status
                </th>
                <th className="px-6 py-4 text-xs font-bold text-slate-500 uppercase tracking-wider text-right">
                  Actions
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-slate-50">
              {orders.map((order) => (
                <tr
                  key={order.id}
                  className="hover:bg-slate-50/50 transition-colors"
                >
                  <td className="px-6 py-5">
                    <p className="text-xs font-bold text-agri-accent mb-1">
                      {order.id}
                    </p>
                    <p className="font-bold text-slate-800 text-sm">
                      {order.vendor}
                    </p>
                    <p className="text-xs text-slate-400">{order.item}</p>
                  </td>
                  <td className="px-6 py-5 text-sm font-bold text-agri-green">
                    {order.amount}
                  </td>
                  <td className="px-6 py-5">
                    <span
                      className={`inline-flex items-center px-2.5 py-1 rounded-full text-[10px] font-bold ${statusStyles[order.status]}`}
                    >
                      {order.status}
                    </span>
                  </td>
                  <td className="px-6 py-5 text-right">
                    <button
                      className="px-4 py-2 bg-agri-light text-agri-green rounded-xl text-xs font-bold border border-agri-green/10 hover:bg-agri-green hover:text-white transition-all"
                      type="button"
                      onClick={() => onQuickReorder?.(order.id)}
                    >
                      Quick Reorder
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </section>
  );
}
