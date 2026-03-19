import { Link } from "react-router-dom";

type OrdersNavigationProps = {
  buyerName: string;
  buyerInitials: string;
};

export function OrdersNavigation({ buyerName, buyerInitials }: OrdersNavigationProps) {
  return (
    <header className="fixed left-0 right-0 top-6 z-50 flex justify-center px-4">
      <nav className="flex w-full max-w-5xl items-center justify-between rounded-full border border-gray-100 bg-white/90 px-8 py-3 shadow-sm backdrop-blur-md">
        <div className="flex items-center gap-8">
          <span className="font-display text-xl font-bold tracking-tight text-[#14532D]">
            TABO-ANI
          </span>

          <div className="hidden items-center gap-6 text-sm font-medium text-gray-600 md:flex">
            <Link className="transition-colors hover:text-[#14532D]" to="/">
              Marketplace
            </Link>
            <span className="relative text-[#14532D] after:absolute after:-bottom-1 after:left-0 after:h-0.5 after:w-full after:bg-[#14532D]">
              Orders
            </span>
            <Link className="transition-colors hover:text-[#14532D]" to="/messages">
              Messages
            </Link>
            <Link className="transition-colors hover:text-[#14532D]" to="/farmer/1">
              Suppliers
            </Link>
          </div>
        </div>

        <div className="flex items-center gap-3 rounded-full border border-gray-100 bg-gray-50 py-1.5 pl-4 pr-1.5">
          <span className="text-sm font-semibold text-gray-700">{buyerName}</span>
          <div className="flex h-8 w-8 items-center justify-center rounded-full bg-[#14532D] text-xs font-bold text-white">
            {buyerInitials}
          </div>
        </div>
      </nav>
    </header>
  );
}
