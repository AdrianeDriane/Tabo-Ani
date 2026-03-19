const navItems = ["Marketplace", "Wholesale", "Logistics", "About"];

export function Header() {
  return (
    <nav className="fixed left-1/2 top-8 z-100 w-[90%] max-w-5xl -translate-x-1/2">
      <div className="nav-pill">
        <div className="flex items-center gap-3">
          <div className="flex h-10 w-10 items-center justify-center rounded-xl bg-agri-accent shadow-lg shadow-agri-accent/30">
            <span className="font-display text-xl font-bold text-white">T</span>
          </div>
          <span className="hidden font-display text-xl font-extrabold tracking-tight text-white sm:block">
            Tabo-Ani
          </span>
        </div>

        <div className="hidden items-center space-x-10 text-sm font-medium text-gray-200 md:flex">
          {navItems.map((item, index) => (
            <a
              key={item}
              className={
                index === 0
                  ? "text-agri-accent"
                  : "transition-colors hover:text-agri-accent"
              }
              href="#"
            >
              {item}
            </a>
          ))}
        </div>

        <div className="flex items-center gap-4">
          <button className="hidden text-sm font-bold text-white sm:block">
            Sign In
          </button>
          <button className="flex items-center gap-2 rounded-full bg-agri-accent px-6 py-2.5 text-sm font-bold text-white shadow-lg shadow-agri-accent/20 transition-all duration-300 hover:bg-white hover:text-agri-accent">
            Cart (0)
          </button>
        </div>
      </div>
    </nav>
  );
}

