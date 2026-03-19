import type { QaFeedItem } from "../types/dashboard.types";

type QaQualityFeedCardProps = {
  items: QaFeedItem[];
};

export default function QaQualityFeedCard({ items }: QaQualityFeedCardProps) {
  return (
    <div className="rounded-[2rem] border border-agri-green/5 bg-white p-8 shadow-sm">
      <h2 className="mb-6 text-lg font-bold tracking-tight text-agri-green uppercase font-display">
        QA Quality Feed
      </h2>

      <div className="space-y-6">
        {items.map((item) => {
          const tone =
            item.variant === "leaf"
              ? "bg-agri-leaf/10 text-agri-leaf"
              : "bg-agri-accent/10 text-agri-accent";
          const bar = item.variant === "leaf" ? "bg-agri-leaf" : "bg-agri-accent";

          return (
            <div className="flex items-center gap-4" key={item.id}>
              <div
                className={`flex h-12 w-12 items-center justify-center rounded-xl text-sm font-bold ${tone}`}
              >
                {item.scoreLabel}
              </div>
              <div>
                <p className="text-xs font-bold text-agri-green">{item.name}</p>
                <div className="mt-1 h-1.5 w-32 rounded-full bg-gray-100">
                  <div
                    className={`h-full rounded-full ${bar}`}
                    style={{ width: `${item.scorePercent}%` }}
                  />
                </div>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
