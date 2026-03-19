import BuyerDashboardHeader from "./BuyerDashboardHeader";
import MetricsSection from "./MetricsSection";
import OrderHistoryTimeline from "./OrderHistoryTimeline";
import PreferredSuppliers from "./PreferredSuppliers";
import QuickActions from "./QuickActions";
import RecentOrdersTable from "./RecentOrdersTable";
import SupplierDirectory from "./SupplierDirectory";
import {
  metrics,
  orderHistory,
  preferredSuppliers,
  quickActions,
  recentOrders,
  supplierDirectory,
} from "../utils/buyerDashboard.constants";

export default function BuyerDashboardPage() {
  return (
    <main className="pt-32 pb-20 px-4 max-w-7xl mx-auto text-slate-800 antialiased">
      <BuyerDashboardHeader />
      <MetricsSection metrics={metrics} />

      <div className="grid grid-cols-1 lg:grid-cols-12 gap-8">
        <div className="lg:col-span-8 space-y-8">
          <RecentOrdersTable orders={recentOrders} />
          <SupplierDirectory suppliers={supplierDirectory} />
        </div>

        <div className="lg:col-span-4 space-y-8">
          <QuickActions actions={quickActions} />
          <PreferredSuppliers suppliers={preferredSuppliers} />
          <OrderHistoryTimeline items={orderHistory} />
        </div>
      </div>
    </main>
  );
}
