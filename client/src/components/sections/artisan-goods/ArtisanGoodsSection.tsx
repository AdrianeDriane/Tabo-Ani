type SpotlightItem = {
  name: string;
  subtitle: string;
  price: number;
  unit: string;
  icon: string;
};

const spotlightItems: SpotlightItem[] = [
  {
    name: "Wild Honeycomb",
    subtitle: "Directly from Davao forests",
    price: 850,
    unit: "per jar",
    icon: "eco",
  },
  {
    name: "Red Quinoa Grains",
    subtitle: "Organic mountain harvest",
    price: 320,
    unit: "per kg",
    icon: "nutrition",
  },
  {
    name: "Dried Hibiscus Tea",
    subtitle: "Farm-fresh flower petals",
    price: 180,
    unit: "per bag",
    icon: "local_florist",
  },
];

export function ArtisanGoodsSection() {
  return (
    <section className="relative overflow-hidden bg-agri-green py-24 text-white">
      <div className="pointer-events-none absolute inset-0 opacity-10">
        <span className="material-symbols-outlined text-[40rem] text-white">
          coffee
        </span>
      </div>

      <div className="relative z-10 mx-auto max-w-7xl px-6 md:px-12 lg:px-20">
        <div className="mb-16 flex flex-col justify-between gap-8 md:flex-row md:items-end">
          <div className="max-w-2xl">
            <span className="-mb-8 block font-display text-7xl font-black leading-none text-agri-accent opacity-30">
              02
            </span>
            <h2 className="font-display text-5xl font-extrabold leading-[1.1] text-white md:text-7xl">
              Artisan Grains <br />
              &amp; Roasts
            </h2>
          </div>
        </div>

        <div className="grid grid-cols-1 gap-12 md:grid-cols-2">
          <div className="flex flex-col items-center gap-10 rounded-[3rem] border border-white/10 bg-white/5 p-10 backdrop-blur-xl md:flex-row">
            <div className="aspect-square w-full overflow-hidden rounded-4xl md:w-1/2">
              <img
                alt="Bukidnon Coffee Beans"
                className="h-full w-full object-cover"
                src="https://lh3.googleusercontent.com/aida-public/AB6AXuDl2FA67XtEHlfdiu4EdcXdnIyUgpPkaUzaILbySfKhax-hSk6XB7HTSTSYcZT1gAQhnbvGZcU_SM68oaoiZwsYWwEE_oQwHmZv_qa2hGJDvusaK_QGanjj0rNq9xa8pKNfGwjsUfHz6dE09IjF0815CNp8xWhmkKzUfDUVeP1VwVD3CQfF6KgxOgMC3KrZZR61wdWbAAUDGQ-mw0_ufboILcrgXFmkjfAIi4uAhIloGRher5QeaPpbJCMlFvXQTmz8Oq26ACuMAq7L"
              />
            </div>

            <div className="w-full md:w-1/2">
              <span className="mb-4 block text-[10px] font-black uppercase tracking-[0.3em] text-agri-accent">
                Bukidnon Pride
              </span>
              <h3 className="mb-4 font-display text-3xl font-extrabold leading-tight">
                Bukidnon Arabica Roast
              </h3>
              <p className="mb-8 text-gray-300">
                Single-origin medium roast from high elevation plantations.
                Distinct notes of dark chocolate and citrus.
              </p>
              <div className="mb-8 flex items-center justify-between">
                <div>
                  <span className="mb-1 block text-[10px] font-black uppercase tracking-widest text-gray-500">
                    Price
                  </span>
                  <span className="font-display text-3xl font-black text-agri-leaf">
                    &#8369;1,200
                  </span>
                </div>
                <div className="text-right">
                  <span className="mb-1 block text-[10px] font-black uppercase tracking-widest text-gray-500">
                    Stock
                  </span>
                  <span className="font-bold">15kg Left</span>
                </div>
              </div>
              <button className="w-full rounded-2xl bg-agri-accent py-5 text-sm font-black uppercase tracking-widest text-white shadow-2xl shadow-agri-accent/20 transition-transform hover:scale-[1.02]">
                Pre-Order Batch
              </button>
            </div>
          </div>

          <div className="space-y-6">
            {spotlightItems.map((item) => (
              <div
                key={item.name}
                className="flex cursor-pointer items-center gap-6 rounded-4xl border border-white/10 bg-white/5 p-6 transition-all hover:bg-white/10"
              >
                <div className="flex h-20 w-20 items-center justify-center rounded-2xl bg-agri-leaf/20">
                  <span className="material-symbols-outlined text-4xl text-agri-leaf">
                    {item.icon}
                  </span>
                </div>
                <div className="grow">
                  <h4 className="text-xl font-bold">{item.name}</h4>
                  <p className="text-sm text-gray-400">{item.subtitle}</p>
                </div>
                <div className="text-right">
                  <span className="text-xl font-bold text-agri-leaf">
                    &#8369;{item.price}
                  </span>
                  <span className="block text-[10px] uppercase text-gray-500">
                    {item.unit}
                  </span>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </section>
  );
}

