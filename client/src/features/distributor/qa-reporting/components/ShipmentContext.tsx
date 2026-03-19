type ShipmentContextProps = {
  selectedDeliveryId: string;
};

export default function ShipmentContext({
  selectedDeliveryId,
}: ShipmentContextProps) {
  return (
    <div className="mb-8">
      <h1 className="font-display text-3xl font-bold text-agri-green">
        QA Inspection Report
      </h1>
      <div className="mt-4 flex flex-wrap gap-4 items-center bg-white p-4 rounded-xl border border-agri-border">
        <div className="flex flex-col">
          <span className="text-[10px] uppercase font-bold text-gray-400">
            Order ID
          </span>
          <span className="font-mono text-sm font-semibold">
            #{selectedDeliveryId}-A1
          </span>
        </div>
        <div className="h-8 w-px bg-gray-200" />
        <div className="flex flex-col">
          <span className="text-[10px] uppercase font-bold text-gray-400">
            Origin
          </span>
          <span className="text-sm font-semibold">Benguet Cooperative</span>
        </div>
        <div className="h-8 w-px bg-gray-200" />
        <div className="flex flex-col">
          <span className="text-[10px] uppercase font-bold text-gray-400">
            Destination
          </span>
          <span className="text-sm font-semibold">QC Distribution Hub</span>
        </div>
        <div className="ml-auto flex items-center gap-2">
          <span className="w-2 h-2 rounded-full bg-agri-accent animate-pulse" />
          <span className="text-xs font-bold text-agri-accent uppercase">
            Live Inspection
          </span>
        </div>
      </div>
    </div>
  );
}
