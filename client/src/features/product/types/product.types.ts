export type CustomerReview = {
  id: string;
  customerName: string;
  postedAtLabel: string;
  comment: string;
};

export type ProductDetail = {
  id: string;
  categoryLabel: string;
  name: string;
  pricePerUnit: number;
  unitLabel: string;
  availableStockLabel: string;
  farmerName: string;
  farmerRatingLabel: string;
  heroImageUrl: string;
  galleryImageUrls: string[];
  productStoryParagraphs: string[];
  farmerLocationLabel: string;
  farmerExperienceLabel: string;
  farmerQuote: string;
  farmerAvatarUrl: string;
  quickFacts: Array<{ label: string; value: string }>;
  customerReviews: CustomerReview[];
};
