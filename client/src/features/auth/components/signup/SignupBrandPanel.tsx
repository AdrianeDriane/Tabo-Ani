const BRAND_IMAGE_URL =
  "https://lh3.googleusercontent.com/aida-public/AB6AXuAVGrnukIzrXGXGm10769KXG_OaXIe0Zk2kQqLgDlc7kj52iE01oqtt18VLyftdOVI74qBLt_cI99HnAZZ_m0t9lxzbJNy5NnIYfMyBPq-IbPb1C9aDQ1gLRuRHwnEBPjA0evG1zvcoW-pOys3GsCa1qG51MmR0JBx2kAtyXGwm0iP13mRrGFLBG-Z6VsW2oICuUbgMnB4TvWsZMe2mn02GrIYAy0ZXsftVMUOy57vhLCKHaiDZ_QqfzCJkh8BALgUW3pq7aASlHCXI";

const HIGHLIGHTS = [
  { icon: "verified_user", label: "Verified Traders" },
  { icon: "trending_up", label: "Market Analytics" },
  { icon: "payments", label: "Secure Payments" },
  { icon: "location_on", label: "Local Logistics" },
];

export function SignupBrandPanel() {
  return (
    <aside className="relative hidden overflow-hidden bg-agri-green p-12 text-white md:flex md:w-5/12 md:flex-col md:justify-between lg:w-1/2">
      <div className="absolute inset-0 opacity-60">
        <img
          src={BRAND_IMAGE_URL}
          alt="Lush green Philippine rice terraces at sunrise"
          className="size-full object-cover"
        />
      </div>
      <div className="absolute inset-0 bg-gradient-to-t from-agri-green via-agri-green/40 to-transparent" />

      <div className="relative z-10">
        <div className="mb-12 flex items-center gap-2">
          <div className="rounded-lg bg-white p-2">
            <span className="material-symbols-outlined font-bold text-agri-green">
              agriculture
            </span>
          </div>
          <span className="text-2xl font-extrabold tracking-tight">Tabo-Ani</span>
        </div>

        <h1 className="mb-6 font-display text-4xl leading-tight font-extrabold lg:text-5xl">
          Join the Future of Philippine Agriculture
        </h1>
        <p className="max-w-md text-lg leading-relaxed text-white/90">
          Connect directly with local farmers, access fair market prices, and
          build a sustainable supply chain for the nation.
        </p>
      </div>

      <div className="relative z-10 mt-auto grid grid-cols-2 gap-6">
        {HIGHLIGHTS.map((item) => (
          <div key={item.label} className="flex items-center gap-3">
            <span className="material-symbols-outlined text-agri-leaf">
              {item.icon}
            </span>
            <span className="text-sm font-medium">{item.label}</span>
          </div>
        ))}
      </div>
    </aside>
  );
}
