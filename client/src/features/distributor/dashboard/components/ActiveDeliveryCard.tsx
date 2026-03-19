import type { ActiveDelivery } from "../types/distributorDashboard.types";

type ActiveDeliveryCardProps = {
  delivery: ActiveDelivery;
};

export default function ActiveDeliveryCard({
  delivery,
}: ActiveDeliveryCardProps) {
  return (
    <div
      className="bg-white rounded-[2rem] border border-slate-100 shadow-sm overflow-hidden flex flex-col"
      data-purpose="delivery-card"
    >
      <div className="p-8">
        <div className="flex justify-between items-start mb-6">
          <div>
            <span className="text-[10px] font-black text-slate-400 uppercase tracking-[0.2em]">
              Transaction ID
            </span>
            <h3 className="text-2xl font-extrabold text-agri-green">
              {delivery.id}
            </h3>
          </div>
          <div className="px-4 py-1.5 bg-agri-accent/10 rounded-full">
            <span className="text-xs font-black text-agri-accent uppercase">
              {delivery.status}
            </span>
          </div>
        </div>
        <div className="space-y-6">
          <div className="flex items-center space-x-4">
            <div className="w-16 h-16 bg-agri-light rounded-2xl flex items-center justify-center shrink-0">
              <div className="w-8 h-8 border-4 border-agri-leaf/20 border-t-agri-leaf rounded-full animate-spin" />
            </div>
            <div>
              <h4 className="text-lg font-bold text-slate-900">
                {delivery.product}
              </h4>
              <p className="text-slate-500 font-medium">{delivery.details}</p>
            </div>
          </div>
          <div className="relative pl-8 space-y-8 before:content-[''] before:absolute before:left-2 before:top-2 before:bottom-2 before:w-[2px] before:bg-slate-100">
            <div className="relative">
              <div className="absolute -left-8 top-1.5 w-4 h-4 rounded-full border-4 border-white bg-agri-leaf shadow-sm z-10" />
              <p className="text-[10px] font-bold text-slate-400 uppercase tracking-wider leading-none mb-1">
                Pickup From
              </p>
              <p className="font-bold text-slate-800">{delivery.pickup}</p>
            </div>
            <div className="relative">
              <div className="absolute -left-8 top-1.5 w-4 h-4 rounded-full border-4 border-white bg-agri-accent shadow-sm z-10" />
              <p className="text-[10px] font-bold text-slate-400 uppercase tracking-wider leading-none mb-1">
                Deliver To
              </p>
              <p className="font-bold text-slate-800">{delivery.dropoff}</p>
            </div>
          </div>
        </div>
      </div>
      <div className="mt-auto bg-slate-50 border-t border-slate-100 p-6 flex items-center justify-between">
        <div>
          <p className="text-[10px] font-bold text-slate-400 uppercase">
            Payout Fee
          </p>
          <p className="text-xl font-black text-agri-green">
            {delivery.payout}
          </p>
        </div>
        <button className="bg-agri-green text-white font-bold py-3 px-6 rounded-2xl hover:bg-agri-green/90 transition-all shadow-lg shadow-agri-green/20">
          Start Pickup
        </button>
      </div>
    </div>
  );
}
