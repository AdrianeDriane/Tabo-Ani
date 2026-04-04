import type { OrderDetailsData } from "../types/orders.types";
import { OrderTrackingCard } from "./OrderTrackingCard";
import { OrdersHeaderSection } from "./OrdersHeaderSection";
import { OrdersNavigation } from "./OrdersNavigation";
import { OrdersSecondaryPreview } from "./OrdersSecondaryPreview";
import { OrdersTabs } from "./OrdersTabs";

type OrdersViewProps = {
  data: OrderDetailsData;
};

export function OrdersView({ data }: OrdersViewProps) {
  return (
    <div className="min-h-screen bg-[#F3F4F6] pb-20">
      <OrdersNavigation buyerInitials={data.buyerInitials} buyerName={data.buyerName} />

      <main className="mx-auto max-w-5xl px-4 pt-32">
        <OrdersHeaderSection subtitle={data.pageSubtitle} title={data.pageTitle} />
        <OrdersTabs selectedTab={data.selectedTab} tabs={data.tabs} />
        <OrderTrackingCard data={data} />
        <OrdersSecondaryPreview preview={data.secondaryPreview} />
      </main>
    </div>
  );
}
