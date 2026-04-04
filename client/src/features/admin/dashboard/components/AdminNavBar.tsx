import { Link } from "react-router-dom";

export default function AdminNavBar() {
  return (
    <header className="fixed left-0 right-0 top-6 z-50 px-4 md:px-8">
      <nav className="mx-auto flex max-w-7xl items-center justify-between rounded-full border border-[rgb(20_83_45_/_5%)] bg-white/80 px-6 py-3 shadow-sm backdrop-blur-md">
        <div className="flex items-center space-x-2">
          <div className="h-8 w-8 rounded-lg bg-agri-leaf" />
          <span className="font-display text-xl font-bold tracking-tight text-agri-green">TABO-ANI</span>
          <span className="rounded-full bg-agri-accent px-2 py-0.5 text-[10px] font-bold tracking-widest text-white uppercase">
            Admin
          </span>
        </div>

        <div className="hidden items-center space-x-8 text-sm font-semibold text-agri-green/70 md:flex">
          <Link className="border-b-2 border-agri-leaf pb-1 text-agri-green" to="/admin">
            Dashboard
          </Link>
          <a className="transition-colors hover:text-agri-green" href="#">
            Users
          </a>
          <a className="transition-colors hover:text-agri-green" href="#">
            Listings
          </a>
          <Link className="transition-colors hover:text-agri-green" to="/orders/TA-8821">
            Orders
          </Link>
          <a className="transition-colors hover:text-agri-green" href="#">
            Transactions
          </a>
          <a className="transition-colors hover:text-agri-green" href="#">
            Reports
          </a>
        </div>

        <div className="flex items-center space-x-4">
          <div className="hidden text-right sm:block">
            <p className="text-xs font-bold text-agri-green">Admin Maria Santos</p>
            <p className="text-[10px] text-agri-leaf">Super Administrator</p>
          </div>

          <div className="flex h-10 w-10 items-center justify-center rounded-full bg-agri-green text-xs font-bold text-white">
            MS
          </div>
        </div>
      </nav>
    </header>
  );
}
