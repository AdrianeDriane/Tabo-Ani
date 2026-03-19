type BalanceCardProps = {
  label: string;
  amount: string;
  payoutNote: string;
  payoutAction: string;
};

export default function BalanceCard({
  label,
  amount,
  payoutNote,
  payoutAction,
}: BalanceCardProps) {
  return (
    <section
      className="bg-agri-green rounded-3xl p-8 text-white shadow-2xl relative overflow-hidden"
      data-purpose="account-balance"
    >
      <div className="absolute -right-10 -top-10 w-40 h-40 bg-white/10 rounded-full blur-3xl" />
      <div className="relative z-10">
        <p className="text-agri-leaf font-bold text-sm tracking-widest uppercase mb-1">
          {label}
        </p>
        <h2 className="text-5xl font-extrabold mb-8 leading-none">
          {amount}
        </h2>
        <div className="flex items-center justify-between pt-6 border-t border-white/10">
          <div className="flex items-center gap-2">
            <div className="w-2 h-2 rounded-full bg-agri-accent animate-pulse" />
            <span className="text-xs font-medium text-white/80">
              {payoutNote}
            </span>
          </div>
          <button className="text-xs font-bold bg-white/20 hover:bg-white/30 px-3 py-1.5 rounded-lg transition-colors">
            {payoutAction}
          </button>
        </div>
      </div>
    </section>
  );
}
