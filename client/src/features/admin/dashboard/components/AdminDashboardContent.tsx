import {
  categoryBreakdown,
  customerInsights,
  metrics,
  trafficSources,
} from "../utils/dashboard.constants";
import AdminNavBar from "./AdminNavBar";
import CategoryBreakdownCard from "./CategoryBreakdownCard";
import CustomerInsightsCard from "./CustomerInsightsCard";
import DashboardHeader from "./DashboardHeader";
import MetricHighlights from "./MetricHighlights";
import OperationalGrid from "./OperationalGrid";
import TrafficSourceAnalytics from "./TrafficSourceAnalytics";

export default function AdminDashboardContent() {
  return (
    <div className="min-h-screen bg-agri-light pb-12">
      <AdminNavBar />

      <main className="mx-auto max-w-7xl space-y-8 px-4 pt-32 md:px-8">
        <DashboardHeader />
        <MetricHighlights metrics={metrics} />

        <OperationalGrid />

        <div className="mb-12 grid grid-cols-1 gap-8 lg:grid-cols-2">
          <CategoryBreakdownCard rows={categoryBreakdown} />
          <CustomerInsightsCard insights={customerInsights} />
        </div>

        <TrafficSourceAnalytics sources={trafficSources} />
      </main>

      <footer className="mx-auto mt-12 max-w-7xl border-t border-agri-green/5 px-8 py-8 text-center">
        <p className="text-[10px] font-bold tracking-widest text-agri-green/30 uppercase">
          Tabo-Ani Management System • © 2024 • Secured by Agricultural Escrow
        </p>
      </footer>
    </div>
  );
}
