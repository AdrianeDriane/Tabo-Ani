import type {
  MarketplaceFilterGroup,
  MarketplaceFooterSection,
  MarketplaceNavLink,
  MarketplaceProduct,
} from "../types/buyerMarketplace.types";

export const marketplaceNavLinks: MarketplaceNavLink[] = [
  { label: "Marketplace", href: "/buyers/marketplace", isActive: true },
  { label: "Wholesale", href: "/buyers/dashboard", isActive: false },
  { label: "Logistics", href: "/checkout", isActive: false },
  { label: "About", href: "/", isActive: false },
];

export const marketplaceFilterGroups: MarketplaceFilterGroup[] = [
  {
    id: "category",
    label: "Category",
    options: ["All Produce", "Vegetables", "Fruits", "Grains", "Livestock"],
  },
  {
    id: "price",
    label: "Price Range",
    options: ["Any Price", "Under P500", "P500 - P2,000", "P2,000+"],
  },
  {
    id: "location",
    label: "Location",
    options: ["Benguet", "Bukidnon", "Davao", "Iloilo", "Batangas"],
  },
];

export const marketplaceProducts: MarketplaceProduct[] = [
  {
    id: "strawberries",
    name: "Premium Highland Strawberries",
    price: "P450.00",
    description: "Grade A, hand-picked from La Trinidad farms.",
    stock: "In Stock: 50kg",
    seller: "Farm of Ricardo S.",
    rating: "4.9",
    deliveryWindow: "24-48 Hours",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuDfi7tsV81Ji5Lkd-JB5pGMiWjTeUEkGdkLJvPE38ePV1oZwamnajArbUxP3eaHGCZ7vfm9sgtsOwSwMmT_nrsOU2EeBquues8Ww2iODH1aLogHiEJYCEh8XXcnHGRh3xigw1FO9LwxiL8k1nyi_qzb9DNQa_SagLvmW2adRnM6qpHr0VqmHPMsw1cQaDVb363BDl2ZX2QaYAxBD1y8SdZV1xspZMnnKiJ68EsbPtjLeyVAHr2BELku_HOWGtTrAIc38ucb5teATlvC",
    imageAlt: "Highland strawberries",
  },
  {
    id: "brown-rice",
    name: "Organic Heirloom Brown Rice",
    price: "P120.00",
    description: "Nutrient-rich, unpolished mountain variety.",
    stock: "In Stock: 200kg",
    seller: "Cordillera Co-op",
    rating: "5.0",
    deliveryWindow: "3-5 Days",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuDhzu_gb8ydvTMlnou1X8cC_AyyOK50pw94Np78A7KuhfHypqhoIm_Xc2dxY4MJwafX_ho0YtQnsj3PZzNsszIt5pCSHGRYTDmC2G-pBmscFsYlh2TiLkjy6pWXfBRlr6-sUbt78D5OOCyxEXzA2HlcErmTaOeAycw5hgwM8R0OCBrNr7mtSk_ELQeDVImM-BionrtajLXy4bSXqpWH-3LYYlPU5ErhvpkU_RjSZ-QD7cDqDzfdVM-V2WcDNUEaZx5uFgC8qaozX4NN",
    imageAlt: "Organic mountain rice",
  },
  {
    id: "cabbage",
    name: "Sweet Highland Cabbage",
    price: "P85.00",
    description: "Extra crunchy and sweet, harvested daily.",
    stock: "In Stock: 150kg",
    seller: "Elena G. Harvests",
    rating: "4.8",
    deliveryWindow: "24 Hours",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuB5tTmRshziix0DfHlsQE_vBtvbCm14q9H2r13FXZO1d-9XrSAEtxAWIpoYHiB-qp8RTc_YmphFq4PmXvN0ySgd8nz7AwPg4NcMReZsiwoN7rnjAM0TO1827mQdrYQ9JB5eqEYgVzsSTJTtQ39_7q073reYpvs_s0EXS58s20qeb_yGXBkiOfkxi7qU4DFA0su7gkWiYIACSUarsBtPJcDT_k9kdENZ5NQbaGni0Kw-Enu2aSEQpGFnLj7H3qOUdqXklz5VUBhqdgHt",
    imageAlt: "Highland cabbage",
  },
  {
    id: "coffee",
    name: "Bukidnon Arabica Beans",
    price: "P1,200.00",
    description: "Single-origin medium roast, high elevation.",
    stock: "In Stock: 15kg",
    seller: "Peak Brew Farms",
    rating: "4.9",
    deliveryWindow: "3-4 Days",
    imageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuDl2FA67XtEHlfdiu4EdcXdnIyUgpPkaUzaILbySfKhax-hSk6XB7HTSTSYcZT1gAQhnbvGZcU_SM68oaoiZwsYWwEE_oQwHmZv_qa2hGJDvusaK_QGanjj0rNq9xa8pKNfGwjsUfHz6dE09IjF0815CNp8xWhmkKzUfDUVeP1VwVD3CQfF6KgxOgMC3KrZZR61wdWbAAUDGQ-mw0_ufboILcrgXFmkjfAIi4uAhIloGRher5QeaPpbJCMlFvXQTmz8Oq26ACuMAq7L",
    imageAlt: "Bukidnon coffee beans",
  },
];

export const marketplaceFooterSections: MarketplaceFooterSection[] = [
  {
    title: "Marketplace",
    links: ["Vegetables", "Fruits", "Grains", "Livestock"],
  },
  {
    title: "Credibility",
    links: [
      "Farmer Verification",
      "Quality Control",
      "Shipping Policies",
      "Buyer Protection",
    ],
  },
  {
    title: "Support",
    links: ["Contact Us", "FAQs", "Seller Portal", "Terms of Trade"],
  },
];
