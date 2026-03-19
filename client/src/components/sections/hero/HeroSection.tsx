const heroStats = [
  { value: "4.9/5", label: "Quality Score" },
  { value: "24h", label: "Avg. Delivery" },
  { value: "Direct", label: "Farm Origin" },
];

export function HeroSection() {
  return (
    <section className="showcase-section min-h-screen">
      <img
        alt="Lush Highland Farm"
        className="absolute inset-0 h-full w-full object-cover"
        src="https://lh3.googleusercontent.com/aida-public/AB6AXuCExs0RixUQEh_ovVv7t9ILedF6tSo9SQGcgL2uZ-nrG0HLJ1c6W6Uk-9IICtXEFHgYhkHI_xYgcfjANkSfX_SOS32mW5Co_qnSsw74XgzMCzdX1MnnYxMuJdxdo-oWsJP6u2naU4H-65iRzqpk0ZxPw4Q9RncRcZJOdYzmtsXAmZk27r3Ef-2Upt6Gx35WjYuhsQ8C1L35p_DDQDshCK3TNNkk0rBuLwRH94gARv1RmGoziNKF7Wj-zj-Qitvq_FPHYVS8gSZyaUQW"
      />
      <div className="showcase-overlay"></div>

      <div className="showcase-content">
        <div className="max-w-3xl">
          <span className="mb-6 inline-block rounded-full bg-agri-leaf px-6 py-2 text-[10px] font-black uppercase tracking-[0.3em] text-white shadow-lg">
            Seasonal Discovery
          </span>
          <h1 className="mb-8 font-display text-6xl font-extrabold leading-[0.9] tracking-tighter text-white md:text-8xl">
            Harvest Fresh <br />
            <span className="text-agri-leaf">Highland Produce</span>
          </h1>
          <p className="mb-12 max-w-xl text-xl font-medium leading-relaxed text-gray-200 md:text-2xl">
            Directly sourced from the mountainous regions of Benguet. Premium
            quality, expertly graded, and delivered within 24 hours.
          </p>

          <div className="mb-16 flex flex-wrap gap-4">
            {heroStats.map((stat) => (
              <div
                key={stat.label}
                className="flex min-w-35 flex-col rounded-2xl border border-white/10 bg-black/40 p-4 backdrop-blur-xl"
              >
                <span className="text-2xl font-bold text-agri-leaf">
                  {stat.value}
                </span>
                <span className="text-[10px] font-black uppercase tracking-widest text-gray-400">
                  {stat.label}
                </span>
              </div>
            ))}
          </div>

          <div className="group relative max-w-xl">
            <input
              className="w-full rounded-3xl border border-white/20 bg-white/10 p-6 pl-14 text-lg text-white backdrop-blur-2xl transition-all placeholder:text-gray-400 focus:bg-white/20 focus:ring-2 focus:ring-agri-accent"
              placeholder="Search for fresh harvests..."
              type="text"
            />
            <span className="material-symbols-outlined absolute left-5 top-1/2 -translate-y-1/2 text-2xl text-white/50">
              search
            </span>
          </div>
        </div>
      </div>
    </section>
  );
}

