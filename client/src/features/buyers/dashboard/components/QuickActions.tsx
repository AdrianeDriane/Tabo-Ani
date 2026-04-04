import type { QuickAction } from "../types/buyerDashboard.types";

type QuickActionsProps = {
  actions: QuickAction[];
  onActionClick?: (label: string) => void;
};

export default function QuickActions({
  actions,
  onActionClick,
}: QuickActionsProps) {
  return (
    <section data-purpose="quick-actions">
      <h2 className="text-xl font-bold text-agri-green mb-6">Quick Actions</h2>
      <div className="space-y-4">
        {actions.map((action) => (
          <button
            key={action.id}
            className="w-full flex items-center justify-between p-5 bg-white rounded-3xl shadow-[0_10px_40px_-10px_rgba(0,0,0,0.04)] border border-slate-100 group hover:border-agri-leaf transition-all"
            type="button"
            onClick={() => onActionClick?.(action.label)}
          >
            <span className="font-bold text-slate-700">{action.label}</span>
            <div className="w-8 h-8 rounded-full bg-agri-light flex items-center justify-center group-hover:bg-agri-leaf group-hover:text-white transition-all text-agri-green font-bold">
              →
            </div>
          </button>
        ))}
      </div>
    </section>
  );
}
