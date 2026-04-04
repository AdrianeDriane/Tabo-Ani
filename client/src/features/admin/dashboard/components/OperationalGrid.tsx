import {
  escrowItems,
  logisticsVolumes,
  pendingListings,
  qaFeedItems,
  verificationApplications,
} from "../utils/dashboard.constants";
import EscrowMonitorCard from "./EscrowMonitorCard";
import FarmerVerificationPanel from "./FarmerVerificationPanel";
import PendingListingsCard from "./PendingListingsCard";
import QaQualityFeedCard from "./QaQualityFeedCard";
import VolumeAnalyticsCard from "./VolumeAnalyticsCard";

export default function OperationalGrid() {
  return (
    <div className="grid grid-cols-1 gap-8 lg:grid-cols-3">
      <section className="space-y-6 lg:col-span-2">
        <FarmerVerificationPanel applications={verificationApplications} />

        <div className="grid grid-cols-1 gap-8 md:grid-cols-2">
          <EscrowMonitorCard items={escrowItems} />
          <QaQualityFeedCard items={qaFeedItems} />
        </div>
      </section>

      <aside className="space-y-8">
        <PendingListingsCard listings={pendingListings} />
        <VolumeAnalyticsCard volumes={logisticsVolumes} />
      </aside>
    </div>
  );
}
