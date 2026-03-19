import type { Insight } from "../types/analytics.types";

type InsightsCardProps = {
  insight: Insight;
};

export default function InsightsCard({ insight }: InsightsCardProps) {
  return (
    <section className="flex flex-col gap-6">
      <div
        className="bg-agri-green p-8 rounded-3xl text-white relative overflow-hidden h-full"
        data-purpose="insights-card"
      >
        <div className="relative z-10">
          <p className="text-xs uppercase tracking-widest opacity-60 font-bold mb-4">
            {insight.title}
          </p>
          <p className="text-xl font-display leading-snug">
            {insight.highlight.split("15% higher demand").length > 1 ? (
              <>
                {insight.highlight.split("15% higher demand")[0]}
                <span className="text-agri-accent">15% higher demand</span>
                {insight.highlight.split("15% higher demand")[1]}
              </>
            ) : (
              insight.highlight
            )}
          </p>
          <div className="mt-8 pt-8 border-t border-white/10">
            <p className="text-sm opacity-80 leading-relaxed">
              {insight.detail}
            </p>
          </div>
        </div>
        <div className="absolute -bottom-10 -right-10 w-40 h-40 bg-agri-leaf/20 rounded-full blur-3xl" />
      </div>
    </section>
  );
}
