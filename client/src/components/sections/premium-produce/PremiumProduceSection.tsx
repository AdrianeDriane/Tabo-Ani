type ProduceCard = {
  name: string;
  pricePerKg: number;
  description: string;
  stock: string;
  cultivator: string;
  trustRating: string;
  timeline: string;
  imageUrl: string;
  elevated?: boolean;
};

const produceCards: ProduceCard[] = [
  {
    name: "Highland Strawberries",
    pricePerKg: 450,
    description:
      "Grade A, hand-picked from La Trinidad farms. Guaranteed fresh arrival.",
    stock: "In Stock: 50kg",
    cultivator: "Ricardo S. Farm",
    trustRating: "4.9",
    timeline: "24-48 Hours",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuDfi7tsV81Ji5Lkd-JB5pGMiWjTeUEkGdkLJvPE38ePV1oZwamnajArbUxP3eaHGCZ7vfm9sgtsOwSwMmT_nrsOU2EeBquues8Ww2iODH1aLogHiEJYCEh8XXcnHGRh3xigw1FO9LwxiL8k1nyi_qzb9DNQa_SagLvmW2adRnM6qpHr0VqmHPMsw1cQaDVb363BDl2ZX2QaYAxBD1y8SdZV1xspZMnnKiJ68EsbPtjLeyVAHr2BELku_HOWGtTrAIc38ucb5teATlvC",
  },
  {
    name: "Heirloom Brown Rice",
    pricePerKg: 120,
    description:
      "Nutrient-rich, unpolished mountain variety sourced from Cordillera terraces.",
    stock: "In Stock: 200kg",
    cultivator: "Mountain Co-op",
    trustRating: "5.0",
    timeline: "3-5 Days",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuDhzu_gb8ydvTMlnou1X8cC_AyyOK50pw94Np78A7KuhfHypqhoIm_Xc2dxY4MJwafX_ho0YtQnsj3PZzNsszIt5pCSHGRYTDmC2G-pBmscFsYlh2TiLkjy6pWXfBRlr6-sUbt78D5OOCyxEXzA2HlcErmTaOeAycw5hgwM8R0OCBrNr7mtSk_ELQeDVImM-BionrtajLXy4bSXqpWH-3LYYlPU5ErhvpkU_RjSZ-QD7cDqDzfdVM-V2WcDNUEaZx5uFgC8qaozX4NN",
    elevated: true,
  },
  {
    name: "Sweet Highland Cabbage",
    pricePerKg: 85,
    description:
      "Extra crunchy and sweet, harvested daily at peak ripeness.",
    stock: "In Stock: 150kg",
    cultivator: "Elena Harvests",
    trustRating: "4.8",
    timeline: "24 Hours",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuB5tTmRshziix0DfHlsQE_vBtvbCm14q9H2r13FXZO1d-9XrSAEtxAWIpoYHiB-qp8RTc_YmphFq4PmXvN0ySgd8nz7AwPg4NcMReZsiwoN7rnjAM0TO1827mQdrYQ9JB5eqEYgVzsSTJTtQ39_7q073reYpvs_s0EXS58s20qeb_yGXBkiOfkxi7qU4DFA0su7gkWiYIACSUarsBtPJcDT_k9kdENZ5NQbaGni0Kw-Enu2aSEQpGFnLj7H3qOUdqXklz5VUBhqdgHt",
  },
];

function ProduceCardItem({
  produceCard,
}: {
  produceCard: ProduceCard;
}) {
  return (
    <article className={`product-card ${produceCard.elevated ? "translate-y-8" : ""}`}>
      <div className="relative h-72 overflow-hidden">
        <img
          alt={produceCard.name}
          className="h-full w-full object-cover"
          src={produceCard.imageUrl}
        />
        <div className="absolute left-6 top-6 rounded-full bg-white/90 px-4 py-1.5 shadow-xl backdrop-blur-md">
          <span className="text-xs font-black uppercase tracking-widest text-agri-green">
            {produceCard.stock}
          </span>
        </div>
      </div>

      <div className="p-8">
        <div className="mb-4 flex items-start justify-between">
          <h3 className="font-display text-2xl font-extrabold leading-tight text-gray-900">
            {produceCard.name}
          </h3>
          <div className="text-right">
            <span className="font-display text-2xl font-black text-agri-leaf">
              &#8369;{produceCard.pricePerKg}
            </span>
            <span className="block text-[10px] font-bold uppercase text-gray-400">
              per kg
            </span>
          </div>
        </div>

        <p className="mb-8 leading-relaxed text-gray-500">
          {produceCard.description}
        </p>

        <div className="mb-8 grid grid-cols-2 gap-4 border-t border-gray-100 pt-6">
          <div>
            <span className="mb-1 block text-[10px] font-black uppercase tracking-widest text-gray-400">
              Cultivator
            </span>
            <span className="font-bold text-agri-green">{produceCard.cultivator}</span>
          </div>
          <div className="text-right">
            <span className="mb-1 block text-[10px] font-black uppercase tracking-widest text-gray-400">
              Trust Rating
            </span>
            <div className="flex justify-end gap-1 text-agri-accent">
              <span className="material-symbols-outlined material-symbols-filled text-sm">
                star
              </span>
              <span className="font-bold">{produceCard.trustRating}</span>
            </div>
          </div>
        </div>

        <div className="flex items-center justify-between gap-4">
          <div className="flex flex-col">
            <span className="text-[10px] font-black uppercase tracking-widest text-gray-400">
              Timeline
            </span>
            <span className="text-sm font-bold">{produceCard.timeline}</span>
          </div>
          <button className="rounded-2xl bg-agri-green px-8 py-4 text-sm font-black uppercase tracking-widest text-white shadow-lg shadow-agri-green/20 transition-all hover:bg-agri-leaf">
            Add to Cart
          </button>
        </div>
      </div>
    </article>
  );
}

export function PremiumProduceSection() {
  return (
    <section className="overflow-hidden bg-white py-24">
      <div className="mx-auto max-w-7xl px-6 md:px-12 lg:px-20">
        <div className="mb-16 flex flex-col justify-between gap-8 md:flex-row md:items-end">
          <div className="max-w-2xl">
            <span className="-mb-8 block font-display text-7xl font-black leading-none text-agri-accent opacity-10">
              01
            </span>
            <h2 className="font-display text-5xl font-extrabold leading-[1.1] text-agri-green md:text-7xl">
              Premium Berries <br />
              &amp; Fruits
            </h2>
          </div>
          <div className="flex items-center gap-4 text-xs font-bold uppercase tracking-widest text-gray-500">
            <span>Explore Category</span>
            <span className="material-symbols-outlined text-agri-leaf">
              arrow_forward
            </span>
          </div>
        </div>

        <div className="grid grid-cols-1 gap-10 md:grid-cols-2 lg:grid-cols-3">
          {produceCards.map((produceCard) => (
            <ProduceCardItem key={produceCard.name} produceCard={produceCard} />
          ))}
        </div>
      </div>
    </section>
  );
}
