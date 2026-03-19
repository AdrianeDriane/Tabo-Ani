import type { WalletTransaction } from "./wallet.types";

type TransactionTableProps = {
  title: string;
  transactions: WalletTransaction[];
};

const typeToneClasses: Record<
  NonNullable<WalletTransaction["typeTone"]>,
  string
> = {
  green: "bg-green-100 text-agri-green",
  accent: "bg-orange-100 text-agri-accent",
  blue: "bg-blue-100 text-blue-700",
};

const statusToneClasses: Record<
  NonNullable<WalletTransaction["statusTone"]>,
  string
> = {
  settled: "text-agri-leaf border-agri-leaf/20",
  processed: "text-gray-400 border-gray-200",
};

export default function TransactionTable({
  title,
  transactions,
}: TransactionTableProps) {
  return (
    <section
      className="bg-white rounded-3xl shadow-sm border border-gray-100 overflow-hidden"
      data-purpose="transaction-history"
    >
      <div className="p-8 border-b border-gray-50 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <h3 className="text-lg font-bold text-gray-900">{title}</h3>
        <div className="flex items-center gap-2">
          <select className="bg-gray-50 border-none rounded-full text-xs font-semibold px-4 py-2 focus:ring-agri-leaf">
            <option>Last 30 Days</option>
            <option>Last 90 Days</option>
            <option>Custom Range</option>
          </select>
        </div>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-gray-50/50">
              <th className="px-8 py-4 text-[11px] font-bold uppercase tracking-wider text-gray-400">
                Date
              </th>
              <th className="px-8 py-4 text-[11px] font-bold uppercase tracking-wider text-gray-400">
                Type
              </th>
              <th className="px-8 py-4 text-[11px] font-bold uppercase tracking-wider text-gray-400">
                Description
              </th>
              <th className="px-8 py-4 text-[11px] font-bold uppercase tracking-wider text-gray-400 text-right">
                Amount
              </th>
              <th className="px-8 py-4 text-[11px] font-bold uppercase tracking-wider text-gray-400 text-center">
                Status
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-50">
            {transactions.map((row) => (
              <tr key={row.id} className="hover:bg-gray-50/80 transition-colors">
                <td className="px-8 py-6 text-sm text-gray-600">{row.date}</td>
                <td className="px-8 py-6">
                  <span
                    className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-bold ${
                      row.typeTone
                        ? typeToneClasses[row.typeTone]
                        : "bg-gray-100 text-gray-600"
                    }`}
                  >
                    {row.type}
                  </span>
                </td>
                <td className="px-8 py-6">
                  <p className="text-sm font-bold text-gray-800">
                    {row.description}
                  </p>
                  <p className="text-xs text-gray-400">{row.subtext}</p>
                </td>
                <td
                  className={`px-8 py-6 text-right font-bold text-sm ${
                    row.amountTone === "negative" ? "text-red-500" : "text-gray-900"
                  }`}
                >
                  {row.amount}
                </td>
                <td className="px-8 py-6 text-center">
                  <span
                    className={`text-[10px] font-extrabold uppercase border px-2 py-1 rounded ${
                      row.statusTone
                        ? statusToneClasses[row.statusTone]
                        : "text-gray-400 border-gray-200"
                    }`}
                  >
                    {row.status}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="p-8 bg-gray-50/30 text-center">
        <button className="text-sm font-bold text-agri-leaf hover:text-agri-green transition-colors flex items-center gap-2 mx-auto">
          View All Transactions
          <span>→</span>
        </button>
      </div>
    </section>
  );
}
