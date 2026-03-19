import type { FarmerProfile } from "../types/farmer.types";

type FarmerReviewsSectionProps = {
  farmer: FarmerProfile;
};

export function FarmerReviewsSection({ farmer }: FarmerReviewsSectionProps) {
  return (
    <section>
      <div className="mb-8">
        <h2 className="text-3xl font-extrabold text-[#14532D]">Marketplace Feedback</h2>
        <p className="mt-2 text-gray-500">Verified stories from wholesale and retail buyers.</p>
      </div>

      <div className="grid grid-cols-1 gap-8 md:grid-cols-2">
        {farmer.reviews.map((review) => (
          <div
            className="relative rounded-[1.5rem] border border-gray-100 bg-white p-8 shadow-sm"
            key={review.id}
          >
            <div className="mb-4 flex items-start justify-between">
              <div>
                <p className="font-bold text-[#14532D]">{review.buyerName}</p>
                <p className="text-xs text-gray-400">
                  {review.buyerMetaLabel} • {review.postedAtLabel}
                </p>
              </div>

              <div className="flex gap-1 font-bold text-[#FF6B00]">{review.ratingLabel}</div>
            </div>

            <p className="leading-relaxed text-gray-600">{review.comment}</p>

            <div className="mt-4 inline-flex items-center gap-2 rounded-full bg-gray-50 px-3 py-1 text-xs font-semibold tracking-tighter text-gray-500 uppercase">
              Verified Purchase
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
