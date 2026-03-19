import type { LogisticsVolume } from "../types/dashboard.types";

type VolumeAnalyticsCardProps = {
  volumes: LogisticsVolume[];
};

export default function VolumeAnalyticsCard({ volumes }: VolumeAnalyticsCardProps) {
  return (
    <div className="rounded-[2rem] border border-agri-green/5 bg-agri-green p-8 text-white shadow-sm">
      <h2 className="mb-6 text-lg font-bold tracking-tight uppercase font-display">Volume Analytics</h2>

      <div className="space-y-6">
        {volumes.map((volume) => (
          <div key={volume.id}>
            <div className="mb-2 flex justify-between text-[10px] font-bold tracking-widest uppercase opacity-60">
              <span>{volume.regionLabel}</span>
              <span>{volume.percent}%</span>
            </div>
            <div className="h-2 w-full rounded-full bg-white/10">
              <div
                className={`h-full rounded-full ${volume.variant === "leaf" ? "bg-agri-leaf" : "bg-agri-accent"}`}
                style={{ width: `${volume.percent}%` }}
              />
            </div>
          </div>
        ))}

        <div className="mt-6 border-t border-white/10 pt-4">
          <p className="mb-1 text-[10px] text-white/50">Total Active Routes</p>
          <p className="text-2xl font-bold">4,291</p>
        </div>
      </div>
    </div>
  );
}
