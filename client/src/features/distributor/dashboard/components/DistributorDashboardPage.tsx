import ActionCard from "./ActionCard";
import ActiveDeliveryCard from "./ActiveDeliveryCard";
import DashboardHeader from "./DashboardHeader";
import DeliveryFilters from "./DeliveryFilters";
import KpiSection from "./KpiSection";
import WeeklyGoalSummary from "./WeeklyGoalSummary";
import {
  actionCards,
  activeDelivery,
  deliveryTabs,
  kpis,
  pageHeader,
  weeklyGoal,
} from "../utils/distributorDashboard.constants";

export default function DistributorDashboardPage() {
  return (
    <main className="pt-32 pb-20 px-4 text-slate-800 min-h-screen">
      <div className="max-w-6xl mx-auto">
        <DashboardHeader
          eyebrow={pageHeader.eyebrow}
          title={pageHeader.title}
          performance={pageHeader.performance}
          status={pageHeader.status}
        />
        <KpiSection items={kpis} />

        <section>
          <DeliveryFilters tabs={deliveryTabs} activeId="all" />

          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
            <ActiveDeliveryCard delivery={activeDelivery} />
            <div className="flex flex-col gap-6">
              {actionCards.map((card) => (
                <ActionCard key={card.id} card={card} />
              ))}
              <WeeklyGoalSummary goal={weeklyGoal} />
            </div>
          </div>
        </section>
      </div>
    </main>
  );
}
