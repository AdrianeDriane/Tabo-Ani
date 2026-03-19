import type { FarmerProfile } from "../types/farmer.types";

type FarmerProfileHeaderProps = {
  farmer: FarmerProfile;
};

export function FarmerProfileHeader({ farmer }: FarmerProfileHeaderProps) {
  return (
    <header className="bg-white pb-8 pt-12">
      <div className="mx-auto max-w-7xl px-6">
        <div className="flex flex-col items-start justify-between gap-6 md:flex-row md:items-end">
          <div className="flex items-center gap-8">
            <div className="h-32 w-32 overflow-hidden rounded-[1.5rem] border-4 border-white bg-gray-200 shadow-lg">
              <img
                alt={farmer.name}
                className="h-full w-full object-cover"
                src={farmer.avatarImageUrl}
              />
            </div>

            <div>
              <div className="mb-2 flex items-center gap-3">
                <h1 className="text-4xl font-extrabold tracking-tight text-[#14532D]">
                  {farmer.name}
                </h1>
                <span className="verified-badge">Verified Producer</span>
              </div>

              <p className="mb-3 text-lg text-gray-500">
                {farmer.locationLabel} • {farmer.activeSinceLabel}
              </p>

              <div className="flex items-center gap-4">
                <div className="flex items-center rounded-full border border-gray-100 bg-[#F8F9FA] px-3 py-1">
                  <span className="font-bold text-[#FF6B00]">{farmer.ratingLabel}</span>
                  <span className="ml-2 text-sm text-gray-400">({farmer.reviewCountLabel})</span>
                </div>
              </div>
            </div>
          </div>

          <div className="flex w-full gap-4 md:w-auto">
            <button
              className="flex-1 rounded-2xl bg-[#FF6B00] px-8 py-4 font-bold text-white shadow-lg shadow-[#FF6B0033] transition-all hover:opacity-90 md:flex-none"
              type="button"
            >
              View Marketplace Listings
            </button>
            <button
              className="flex-1 rounded-2xl border-2 border-[#14532D] bg-white px-8 py-4 font-bold text-[#14532D] transition-all hover:bg-[#F8F9FA] md:flex-none"
              type="button"
            >
              Message Farmer
            </button>
          </div>
        </div>
      </div>
    </header>
  );
}
