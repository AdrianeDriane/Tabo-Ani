import type { WalletBreakdownItem } from "./wallet.types";

type IncomeBreakdownProps = {
  title: string;
  items: WalletBreakdownItem[];
  pendingTitle: string;
  pendingBody: string;
};

export default function IncomeBreakdown({
  title,
  items,
  pendingTitle,
  pendingBody,
}: IncomeBreakdownProps) {
  return (
    <section
      className="bg-white rounded-3xl p-8 shadow-sm border border-gray-100"
      data-purpose="income-breakdown"
    >
      <h3 className="text-lg font-bold text-gray-900 mb-6 flex items-center gap-2">
        <span className="inline-flex h-5 w-5 items-center justify-center text-agri-leaf">
          ◎
        </span>
        {title}
      </h3>
      <div className="space-y-5">
        {items.map((item) => (
          <div key={item.id} className="flex justify-between items-center">
            <div className="flex items-center gap-3">
              <div
                className={
                  item.tone === "accent"
                    ? "w-10 h-10 rounded-xl bg-orange-50 flex items-center justify-center text-agri-accent"
                    : "w-10 h-10 rounded-xl bg-green-50 flex items-center justify-center text-agri-leaf"
                }
              >
                <span className="text-sm font-bold">%</span>
              </div>
              <div>
                <p className="text-sm font-semibold text-gray-800">
                  {item.label}
                </p>
                <p className="text-xs text-gray-500">{item.detail}</p>
              </div>
            </div>
            <span className="text-sm font-bold text-gray-900">
              {item.amount}
            </span>
          </div>
        ))}
      </div>
      <div className="mt-8 pt-6 border-t border-dashed border-gray-200">
        <div className="bg-gray-50 rounded-2xl p-4 flex items-start gap-3">
          <span className="text-blue-500 mt-0.5">ℹ</span>
          <p className="text-[13px] text-gray-600 leading-relaxed">
            <span className="font-bold text-gray-800 block mb-1">
              {pendingTitle}
            </span>
            {pendingBody}
          </p>
        </div>
      </div>
    </section>
  );
}
