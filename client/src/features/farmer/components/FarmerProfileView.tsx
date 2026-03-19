import type { FarmerProfile } from "../types/farmer.types";
import { FarmerActiveListingsSection } from "./FarmerActiveListingsSection";
import { FarmerProfileFooter } from "./FarmerProfileFooter";
import { FarmerProfileHeader } from "./FarmerProfileHeader";
import { FarmerProfileNavigation } from "./FarmerProfileNavigation";
import { FarmerReviewsSection } from "./FarmerReviewsSection";
import { FarmOverviewSection } from "./FarmOverviewSection";

type FarmerProfileViewProps = {
  farmer: FarmerProfile;
};

export function FarmerProfileView({ farmer }: FarmerProfileViewProps) {
  return (
    <div className="min-h-screen bg-[#F8F9FA] text-[#14532D]">
      <FarmerProfileNavigation />
      <FarmerProfileHeader farmer={farmer} />

      <main className="mx-auto max-w-7xl px-6 py-12">
        <FarmOverviewSection farmer={farmer} />
        <FarmerActiveListingsSection farmer={farmer} />
        <FarmerReviewsSection farmer={farmer} />
      </main>

      <FarmerProfileFooter />
    </div>
  );
}
