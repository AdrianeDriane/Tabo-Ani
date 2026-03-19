import { Link } from "react-router-dom";
import type { CheckoutNavLink } from "../types/checkout.types";

type CheckoutNavigationProps = {
  buyerName: string;
  buyerInitials: string;
  navLinks: CheckoutNavLink[];
};

export default function CheckoutNavigation({
  buyerName,
  buyerInitials,
  navLinks,
}: CheckoutNavigationProps) {
  return (
    <nav className="fixed left-0 right-0 top-6 z-50 px-4">
      <div className="mx-auto flex max-w-5xl items-center justify-between rounded-full border border-gray-100 bg-white/90 px-8 py-3 shadow-lg backdrop-blur-md">
        <div className="flex items-center gap-8">
          <span className="text-xl font-extrabold tracking-tight text-agri-green">Tabo-Ani</span>
          <div className="hidden items-center gap-6 md:flex">
            {navLinks.map((link) => (
              <Link
                className="text-sm font-medium text-gray-600 transition-colors hover:text-agri-green"
                key={link.id}
                to={link.href}
              >
                {link.label}
              </Link>
            ))}
          </div>
        </div>

        <div className="flex items-center gap-3">
          <span className="text-sm font-semibold text-gray-900">{buyerName}</span>
          <div className="flex h-8 w-8 items-center justify-center rounded-full bg-agri-green text-xs font-bold text-white">
            {buyerInitials}
          </div>
        </div>
      </div>
    </nav>
  );
}
