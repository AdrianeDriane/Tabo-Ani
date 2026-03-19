import type { OrderDetailsData, TrackingStep } from "../types/orders.types";

type OrderTrackingCardProps = {
  data: OrderDetailsData;
};

function getStatusBadgeClass(statusTone: "info" | "success" | "neutral") {
  if (statusTone === "info") {
    return "bg-blue-50 text-blue-600";
  }

  if (statusTone === "success") {
    return "bg-green-50 text-green-600";
  }

  return "bg-gray-100 text-gray-500";
}

function getTimelineStepClasses(step: TrackingStep): { dot: string; label: string } {
  if (step.state === "done") {
    return { dot: "h-4 w-4 bg-[#16A34A] ring-4 ring-white", label: "text-[#16A34A]" };
  }

  if (step.state === "current") {
    return {
      dot: "h-6 w-6 border-2 border-[#16A34A] bg-white ring-4 ring-white",
      label: "text-[#14532D]",
    };
  }

  return { dot: "h-4 w-4 bg-gray-200 ring-4 ring-white", label: "text-gray-400" };
}

export function OrderTrackingCard({ data }: OrderTrackingCardProps) {
  const progressPercent =
    ((data.detail.trackingSteps.findIndex((step) => step.state === "current") + 1) /
      data.detail.trackingSteps.length) *
    100;

  return (
    <article className="mb-8 overflow-hidden rounded-[2rem] border border-gray-100 bg-white shadow-sm">
      <div className="flex flex-wrap items-start justify-between gap-4 border-b border-gray-50 p-8">
        <div>
          <div className="mb-1 flex items-center gap-3">
            <span className="text-xs font-bold tracking-widest text-gray-400 uppercase">
              {data.detail.orderNumberLabel}
            </span>
            <span
              className={`rounded-full px-3 py-1 text-xs font-bold uppercase ${getStatusBadgeClass(data.summary.statusTone)}`}
            >
              {data.detail.statusLabel}
            </span>
          </div>
          <h2 className="font-display text-2xl font-bold text-gray-900">{data.detail.productTitle}</h2>
          <p className="mt-1 text-gray-500">
            Supplier: <span className="font-semibold text-[#14532D]">{data.detail.supplierName}</span>
          </p>
        </div>

        <div className="text-right">
          <p className="text-sm font-medium text-gray-400">Total Amount</p>
          <p className="text-2xl font-bold text-gray-900">{data.detail.totalAmountLabel}</p>
        </div>
      </div>

      <div className="bg-gray-50/50 px-8 py-10">
        <div className="relative flex w-full items-center justify-between px-4">
          <div className="absolute left-0 top-1/2 z-0 h-0.5 w-full -translate-y-1/2 bg-gray-200" />
          <div
            className="absolute left-0 top-1/2 z-0 h-0.5 -translate-y-1/2 bg-[#16A34A]"
            style={{ width: `${progressPercent}%` }}
          />

          {data.detail.trackingSteps.map((step) => {
            const style = getTimelineStepClasses(step);

            return (
              <div className="relative z-10 flex flex-col items-center" key={step.id}>
                <div className={`flex items-center justify-center rounded-full ${style.dot}`}>
                  {step.state === "current" ? (
                    <div className="h-2.5 w-2.5 animate-pulse rounded-full bg-[#16A34A]" />
                  ) : null}
                </div>
                <span
                  className={`absolute -bottom-8 whitespace-nowrap text-[10px] font-bold tracking-wider uppercase ${style.label}`}
                >
                  {step.label}
                </span>
              </div>
            );
          })}
        </div>
      </div>

      <div className="grid grid-cols-1 gap-8 p-8 md:grid-cols-3">
        <div className="space-y-4">
          <h3 className="text-xs font-bold tracking-widest text-gray-400 uppercase">
            Delivery Details
          </h3>
          <div className="space-y-3">
            {data.detail.deliveryPoints.map((point) => (
              <div className="flex items-start gap-3" key={point.label}>
                <div
                  className={`mt-1 h-2 w-2 rounded-full ${
                    point.highlight === "destination" ? "bg-[#16A34A]" : "bg-gray-300"
                  }`}
                />
                <div>
                  <p className="text-xs text-gray-400">{point.label}</p>
                  <p className="text-sm font-medium text-gray-800">{point.value}</p>
                </div>
              </div>
            ))}

            <div className="pt-2">
              <p className="text-xs text-gray-400">Distributor</p>
              <p className="text-sm font-semibold text-gray-800">{data.detail.distributorName}</p>
              <p className="text-[10px] font-bold text-[#16A34A]">{data.detail.etaLabel}</p>
            </div>
          </div>
        </div>

        <div className="space-y-4">
          <h3 className="text-xs font-bold tracking-widest text-gray-400 uppercase">
            Quality Assurance
          </h3>
          <div className="rounded-xl border border-gray-100 bg-[#F8F9FA] p-4">
            <div className="mb-2 flex items-end justify-between">
              <span className="text-sm font-bold text-[#14532D]">QA Freshness</span>
              <span className="font-display text-xl font-bold text-[#14532D]">
                {data.detail.qaFreshnessPercent}%
              </span>
            </div>
            <div className="h-2 w-full overflow-hidden rounded-full bg-gray-200">
              <div
                className="h-full bg-[#16A34A]"
                style={{ width: `${data.detail.qaFreshnessPercent}%` }}
              />
            </div>
            <p className="mt-3 text-[10px] leading-relaxed text-gray-500">{data.detail.qaSummary}</p>
          </div>
        </div>

        <div className="space-y-4">
          <h3 className="text-xs font-bold tracking-widest text-gray-400 uppercase">
            Escrow Security
          </h3>
          <div className="rounded-xl border border-orange-100 bg-orange-50 p-4">
            <div className="mb-2 flex items-center gap-2">
              <div className="h-2 w-2 rounded-full bg-[#FF6B00]" />
              <span className="text-sm font-bold text-[#FF6B00]">{data.detail.escrowLabel}</span>
            </div>
            <p className="text-xs leading-relaxed text-orange-800">{data.detail.escrowSummary}</p>
            <button
              className="mt-3 border-b border-[#FF6B00] pb-0.5 text-[10px] font-bold text-[#FF6B00] uppercase"
              type="button"
            >
              View Escrow Terms
            </button>
          </div>
        </div>
      </div>

      <div className="flex items-center justify-between border-t border-gray-100 bg-gray-50 px-8 py-5">
        <button className="text-sm font-bold text-red-500 transition-colors hover:text-red-700" type="button">
          Report Issue
        </button>

        <div className="flex gap-4">
          <button
            className="rounded-full border border-gray-200 bg-white px-6 py-2.5 text-sm font-bold text-gray-700 transition-all hover:bg-gray-50"
            type="button"
          >
            Order Details
          </button>
          <button
            className="rounded-full bg-[#14532D] px-6 py-2.5 text-sm font-bold text-white shadow-md transition-all hover:bg-opacity-90"
            type="button"
          >
            Message Participants
          </button>
        </div>
      </div>
    </article>
  );
}
