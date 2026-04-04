export type FarmerStat = {
  label: string;
  value: string;
};

export type FarmerListing = {
  id: string;
  name: string;
  priceLabel: string;
  description: string;
  imageUrl: string;
};

export type FarmerReview = {
  id: string;
  buyerName: string;
  buyerMetaLabel: string;
  postedAtLabel: string;
  comment: string;
  ratingLabel: string;
};

export type FarmerProfile = {
  id: string;
  name: string;
  locationLabel: string;
  activeSinceLabel: string;
  ratingLabel: string;
  reviewCountLabel: string;
  avatarImageUrl: string;
  galleryImageUrls: string[];
  statistics: FarmerStat[];
  quote: string;
  listingsSectionTitle: string;
  listingsSectionSubtitle: string;
  listings: FarmerListing[];
  reviews: FarmerReview[];
};
