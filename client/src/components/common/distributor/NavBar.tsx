import React from 'react';

const Navbar: React.FC = () => {
  return (
    <nav className="fixed top-8 left-1/2 -translate-x-1/2 w-[90%] max-w-5xl z-[100]">
      <div className="nav-pill">
        <div className="flex items-center gap-3">
          <div className="w-10 h-10 bg-agri-accent rounded-xl flex items-center justify-center shadow-lg shadow-agri-accent/30">
            <span className="text-white font-display font-bold text-xl">T</span>
          </div>
          <span className="text-xl font-display font-extrabold text-white tracking-tight hidden sm:block">
            Tabo-Ani
          </span>
        </div>

        <div className="hidden md:flex items-center space-x-10 font-medium text-gray-200 text-sm">
          <a className="text-agri-accent" href="#">Marketplace</a>
          <a className="hover:text-agri-accent transition-colors" href="#">Wholesale</a>
          <a className="hover:text-agri-accent transition-colors" href="#">Logistics</a>
          <a className="hover:text-agri-accent transition-colors" href="#">About</a>
        </div>

        <div className="flex items-center gap-4">
          <button className="text-white text-sm font-bold hidden sm:block">Sign In</button>
          <button className="px-6 py-2.5 bg-agri-accent hover:bg-white hover:text-agri-accent text-white text-sm font-bold rounded-full transition-all duration-300 flex items-center gap-2 shadow-lg shadow-agri-accent/20">
            Cart (0)
          </button>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
