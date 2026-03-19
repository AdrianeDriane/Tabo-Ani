const ctaButtons = ["View All Grains", "View All Livestock", "View All Fruits"];

export function MarketplaceCtaSection() {
  return (
    <section className="bg-agri-light py-32 text-center">
      <div className="mx-auto max-w-4xl px-6">
        <h2 className="mb-8 font-display text-4xl font-extrabold text-agri-green md:text-6xl">
          Looking for specific produce?
        </h2>
        <p className="mb-12 text-xl text-gray-500">
          Browse through 12,000+ verified listings from all across the
          archipelago.
        </p>

        <div className="mb-16 flex flex-wrap justify-center gap-4">
          {ctaButtons.map((label) => (
            <button
              key={label}
              className="rounded-2xl border-2 border-agri-green/10 bg-white px-10 py-4 text-sm font-black uppercase tracking-widest text-agri-green transition-all hover:border-agri-green"
            >
              {label}
            </button>
          ))}
        </div>

        <button className="rounded-3xl bg-agri-accent px-16 py-6 text-xl font-black text-white shadow-2xl shadow-agri-accent/30 transition-transform hover:scale-105">
          Load Marketplace
        </button>
      </div>
    </section>
  );
}
