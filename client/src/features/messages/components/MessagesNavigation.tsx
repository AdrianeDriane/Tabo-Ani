import { Link } from "react-router-dom";

type MessagesNavigationProps = {
  buyerName: string;
  buyerInitials: string;
};

export function MessagesNavigation({ buyerName, buyerInitials }: MessagesNavigationProps) {
  return (
    <nav className="fixed left-1/2 top-6 z-50 w-full max-w-4xl -translate-x-1/2 px-4">
      <div className="flex items-center justify-between rounded-full border border-gray-100 bg-white px-8 py-3 shadow-sm">
        <div className="flex items-center gap-8">
          <span className="font-display text-xl font-bold tracking-tight">Tabo-Ani</span>
          <div className="hidden items-center gap-6 text-sm font-medium md:flex">
            <Link className="transition-colors hover:text-[#16A34A]" to="/">
              Marketplace
            </Link>
            <a className="transition-colors hover:text-[#16A34A]" href="#">
              Orders
            </a>
            <span className="font-semibold text-[#16A34A]">Messages</span>
            <Link className="transition-colors hover:text-[#16A34A]" to="/farmer/1">
              Suppliers
            </Link>
          </div>
        </div>

        <div className="flex items-center gap-2">
          <span className="text-sm font-medium">{buyerName}</span>
          <div className="flex h-8 w-8 items-center justify-center rounded-full bg-[#14532D] text-[10px] text-white">
            {buyerInitials}
          </div>
        </div>
      </div>
    </nav>
  );
}
