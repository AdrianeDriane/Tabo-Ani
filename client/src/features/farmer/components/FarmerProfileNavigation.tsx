export function FarmerProfileNavigation() {
  return (
    <nav className="sticky top-0 z-50 border-b border-gray-100 bg-white">
      <div className="mx-auto flex h-20 max-w-7xl items-center justify-between px-6">
        <div className="flex items-center gap-12">
          <a className="text-2xl font-extrabold tracking-tighter text-[#14532D]" href="/">
            TABO-ANI
          </a>

          <div className="hidden items-center gap-8 text-sm font-medium text-gray-600 md:flex">
            <a className="transition-colors hover:text-[#16A34A]" href="#">
              Marketplace
            </a>
            <a className="transition-colors hover:text-[#16A34A]" href="#">
              Orders
            </a>
            <a className="transition-colors hover:text-[#16A34A]" href="#">
              Messages
            </a>
            <a className="font-semibold text-[#14532D]" href="#">
              Suppliers
            </a>
          </div>
        </div>

        <div className="flex items-center gap-4">
          <div className="hidden text-right sm:block">
            <p className="text-xs text-gray-400">Buyer Account</p>
            <button className="text-sm font-semibold text-[#14532D]" type="button">
              Ricardo Santos ▾
            </button>
          </div>

          <div className="flex h-10 w-10 items-center justify-center rounded-full border border-[#16A34A33] bg-[#16A34A1A] font-bold text-[#14532D]">
            RS
          </div>
        </div>
      </div>
    </nav>
  );
}
