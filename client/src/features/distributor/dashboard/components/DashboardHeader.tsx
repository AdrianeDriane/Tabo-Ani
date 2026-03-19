type DashboardHeaderProps = {
  eyebrow: string;
  title: string;
  performance: string;
  status: string;
};

export default function DashboardHeader({
  eyebrow,
  title,
  performance,
  status,
}: DashboardHeaderProps) {
  return (
    <header className="mb-10 flex flex-col md:flex-row md:items-end justify-between gap-4">
      <div data-purpose="header-title">
        <p className="text-agri-leaf font-bold uppercase tracking-widest text-sm mb-1">
          {eyebrow}
        </p>
        <h1 className="text-4xl font-extrabold text-agri-green tracking-tight">
          {title}
        </h1>
      </div>
      <div
        className="flex items-center space-x-2 bg-white px-4 py-2 rounded-2xl border border-slate-100 shadow-sm"
        data-purpose="completion-rate"
      >
        <span className="text-xs font-bold text-slate-500 uppercase">
          Performance:
        </span>
        <span className="text-xl font-bold text-agri-leaf">{performance}</span>
        <span className="text-[10px] bg-agri-leaf/10 text-agri-leaf px-2 py-0.5 rounded-full font-bold">
          {status}
        </span>
      </div>
    </header>
  );
}
