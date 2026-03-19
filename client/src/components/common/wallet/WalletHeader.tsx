type WalletHeaderProps = {
  title: string;
  subtitle: string;
};

export default function WalletHeader({ title, subtitle }: WalletHeaderProps) {
  return (
    <header className="mb-10 flex flex-col md:flex-row md:items-end justify-between gap-6">
      <div data-purpose="header-title">
        <h1 className="text-3xl font-extrabold text-agri-green tracking-tight">
          {title}
        </h1>
        <p className="text-gray-500 mt-1">{subtitle}</p>
      </div>
      <div className="flex gap-3">
        <button className="bg-white border border-gray-200 text-gray-700 px-6 py-2.5 rounded-full font-semibold text-sm hover:bg-gray-50 transition-all shadow-sm flex items-center gap-2">
          <span className="inline-flex h-4 w-4 items-center justify-center">
            ↓
          </span>
          Export Report
        </button>
        <button className="bg-agri-accent text-white px-6 py-2.5 rounded-full font-bold text-sm hover:scale-[1.02] transition-all shadow-lg shadow-orange-200 flex items-center gap-2">
          <span className="inline-flex h-4 w-4 items-center justify-center">
            ₱
          </span>
          Withdraw Funds
        </button>
      </div>
    </header>
  );
}
