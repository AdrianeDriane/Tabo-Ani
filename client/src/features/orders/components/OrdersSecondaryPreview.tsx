import type { OrderPreview } from "../types/orders.types";

type OrdersSecondaryPreviewProps = {
  preview: OrderPreview;
};

export function OrdersSecondaryPreview({ preview }: OrdersSecondaryPreviewProps) {
  return (
    <article className="overflow-hidden rounded-[2rem] border border-gray-100 bg-white opacity-60 shadow-sm">
      <div className="flex items-center justify-between p-6">
        <div className="flex items-center gap-4">
          <div className="font-display flex h-12 w-12 items-center justify-center rounded-xl bg-gray-100 font-bold text-gray-400">
            #TA
          </div>
          <div>
            <h4 className="font-bold text-gray-800">{preview.title}</h4>
            <p className="text-xs text-gray-400">{preview.subtitle}</p>
          </div>
        </div>
        <button className="text-sm font-bold text-[#14532D]" type="button">
          View Full Progress
        </button>
      </div>
    </article>
  );
}
