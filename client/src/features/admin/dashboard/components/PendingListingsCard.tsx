import type { PendingListing } from "../types/dashboard.types";

type PendingListingsCardProps = {
  listings: PendingListing[];
};

export default function PendingListingsCard({ listings }: PendingListingsCardProps) {
  return (
    <div className="rounded-[2rem] border border-agri-green/5 bg-white p-8 shadow-sm">
      <div className="mb-8 flex items-center justify-between">
        <h2 className="font-display text-lg font-bold tracking-tight text-agri-green uppercase">
          Pending Listings
        </h2>
        <span className="rounded-full bg-agri-accent px-2 py-1 text-[10px] font-bold text-white">
          14 New
        </span>
      </div>

      <div className="space-y-6">
        {listings.map((listing, index) => (
          <div
            className={`flex items-start gap-4 ${index < listings.length - 1 ? "border-b border-agri-green/5 pb-6" : ""}`}
            key={listing.id}
          >
            <div className="h-16 w-16 shrink-0 overflow-hidden rounded-2xl bg-agri-light">
              <img alt={listing.title} className="h-full w-full object-cover" src={listing.imageUrl} />
            </div>

            <div>
              <p className="text-sm font-bold text-agri-green">{listing.title}</p>
              <p className="mb-3 text-[10px] font-medium text-agri-leaf">{listing.sellerName}</p>
              <div className="flex gap-2">
                <button
                  className="rounded-lg bg-agri-leaf/10 px-3 py-1 text-[10px] font-bold text-agri-leaf"
                  type="button"
                >
                  Approve
                </button>
                <button
                  className="rounded-lg bg-red-50 px-3 py-1 text-[10px] font-bold text-red-600"
                  type="button"
                >
                  Deny
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
