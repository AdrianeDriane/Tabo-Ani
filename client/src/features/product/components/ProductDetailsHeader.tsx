export function ProductDetailsHeader() {
  return (
    <header className="fixed left-0 right-0 top-6 z-50 px-4">
      <nav className="pill-nav shadow-soft mx-auto flex w-full max-w-6xl items-center justify-between rounded-full px-8 py-3">
        <div className="flex items-center gap-2">
          <div className="flex h-8 w-8 items-center justify-center rounded-lg bg-[#14532D] font-bold text-white">
            T
          </div>
          <span className="font-display tracking-tight font-bold text-[#14532D]">TABO-ANI</span>
        </div>

        <div className="hidden items-center gap-8 text-sm font-medium text-slate-600 md:flex">
          <a className="transition-colors hover:text-[#14532D]" href="#">
            Marketplace
          </a>
          <a className="transition-colors hover:text-[#14532D]" href="#">
            Wholesale
          </a>
          <a className="transition-colors hover:text-[#14532D]" href="#">
            Logistics
          </a>
          <a className="transition-colors hover:text-[#14532D]" href="#">
            About
          </a>
        </div>

        <div className="flex items-center gap-6">
          <a className="text-sm font-medium text-slate-600 hover:text-[#14532D]" href="#">
            Sign In
          </a>
          <button
            className="relative rounded-full bg-[#14532D] px-5 py-2 text-sm font-semibold text-white"
            type="button"
          >
            Cart (0)
          </button>
        </div>
      </nav>
    </header>
  );
}
