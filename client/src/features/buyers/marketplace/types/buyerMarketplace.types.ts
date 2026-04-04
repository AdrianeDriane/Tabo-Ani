export type MarketplaceNavLink = {
  label: string;
  href: string;
  isActive: boolean;
};

export type MarketplaceFilterGroup = {
  id: string;
  label: string;
  options: string[];
};

export type MarketplaceProduct = {
  id: string;
  name: string;
  price: string;
  description: string;
  stock: string;
  seller: string;
  rating: string;
  deliveryWindow: string;
  imageUrl: string;
  imageAlt: string;
};

export type MarketplaceFooterSection = {
  title: string;
  links: string[];
};
