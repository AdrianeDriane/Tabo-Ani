import type { FarmerProfile } from "../types/farmer.types";

type FarmerActiveListingsSectionProps = {
  farmer: FarmerProfile;
};

export function FarmerActiveListingsSection({ farmer }: FarmerActiveListingsSectionProps) {
  return (
    <section className="mb-20">
      <div className="mb-8 flex items-end justify-between">
        <div>
          <h2 className="text-3xl font-extrabold text-[#14532D]">{farmer.listingsSectionTitle}</h2>
          <p className="mt-2 text-gray-500">{farmer.listingsSectionSubtitle}</p>
        </div>
      </div>

      <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-4">
        {farmer.listings.map((listing) => (
          <div
            className="product-card group overflow-hidden rounded-[1.5rem] border border-gray-100 bg-white shadow-sm"
            key={listing.id}
          >
            <div className="h-48 overflow-hidden bg-gray-100">
              <img
                alt={listing.name}
                className="h-full w-full object-cover transition-transform duration-300 group-hover:scale-105"
                src={listing.imageUrl}
              />
            </div>

            <div className="p-6">
              <div className="mb-2 flex items-start justify-between gap-3">
                <h4 className="text-lg font-bold text-[#14532D]">{listing.name}</h4>
                <span className="font-bold text-[#16A34A]">{listing.priceLabel}</span>
              </div>

              <p className="mb-4 line-clamp-2 text-sm text-gray-500">{listing.description}</p>

              <button
                className="w-full rounded-xl bg-[#F8F9FA] py-3 font-bold text-[#14532D] transition-colors hover:bg-[#14532D] hover:text-white"
                type="button"
              >
                Add to Order
              </button>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
}
