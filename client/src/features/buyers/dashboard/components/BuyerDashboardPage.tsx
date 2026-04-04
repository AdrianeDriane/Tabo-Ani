import { useState } from "react";
import BuyerDashboardHeader from "./BuyerDashboardHeader";
import MetricsSection from "./MetricsSection";
import OrderHistoryTimeline from "./OrderHistoryTimeline";
import PreferredSuppliers from "./PreferredSuppliers";
import QuickActions from "./QuickActions";
import RecentOrdersTable from "./RecentOrdersTable";
import SupplierDirectory from "./SupplierDirectory";
import BuyerDashboardModal from "./BuyerDashboardModal";
import {
  metrics,
  orderHistory,
  preferredSuppliers,
  quickActions,
  recentOrders,
  supplierDirectory,
} from "../utils/buyerDashboard.constants";

export default function BuyerDashboardPage() {
  const [modal, setModal] = useState<{ title: string; description: string } | null>(
    null,
  );

  const openModal = (title: string, description: string) => {
    setModal({ title, description });
  };

  return (
    <main className="pt-32 pb-20 px-4 max-w-7xl mx-auto text-slate-800 antialiased">
      <BuyerDashboardHeader
        onNewOrder={() =>
          openModal(
            "New Purchase Order",
            "Start a new order request for your suppliers.",
          )
        }
      />
      <MetricsSection metrics={metrics} />

      <div className="grid grid-cols-1 lg:grid-cols-12 gap-8">
        <div className="lg:col-span-8 space-y-8">
          <RecentOrdersTable
            orders={recentOrders}
            onQuickReorder={(orderId) =>
              openModal(
                "Quick Reorder",
                `A reorder draft has been prepared for ${orderId}.`,
              )
            }
          />
          <SupplierDirectory
            suppliers={supplierDirectory}
            onQuickReorder={(name) =>
              openModal("Quick Reorder", `Starting a reorder with ${name}.`)
            }
            onMessage={(name) =>
              openModal("Send Message", `Opening a message thread with ${name}.`)
            }
          />
        </div>

        <div className="lg:col-span-4 space-y-8">
          <QuickActions
            actions={quickActions}
            onActionClick={(label) =>
              openModal("Quick Action", `Launching: ${label}.`)
            }
          />
          <PreferredSuppliers
            suppliers={preferredSuppliers}
            onView={(name) =>
              openModal("Supplier Profile", `Viewing profile for ${name}.`)
            }
          />
          <OrderHistoryTimeline items={orderHistory} />
        </div>
      </div>
      <BuyerDashboardModal
        isOpen={Boolean(modal)}
        title={modal?.title ?? ""}
        description={modal?.description ?? ""}
        onClose={() => setModal(null)}
      />
    </main>
  );
}
