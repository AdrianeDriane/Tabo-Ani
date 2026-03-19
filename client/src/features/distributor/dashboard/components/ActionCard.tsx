import type { ActionCard as ActionCardType } from "../types/distributorDashboard.types";

type ActionCardProps = {
  card: ActionCardType;
};

export default function ActionCard({ card }: ActionCardProps) {
  if (card.tone === "accent") {
    return (
      <div
        className="bg-agri-leaf p-8 rounded-[2rem] shadow-lg flex flex-col md:flex-row items-center justify-between gap-6"
        data-purpose="communication-card"
      >
        <div className="text-white">
          <h4 className="text-xl font-extrabold mb-1">{card.title}</h4>
          <p className="text-white/80 text-sm font-medium">
            {card.description}
          </p>
        </div>
        <button className="w-full md:w-auto px-8 py-3 bg-white/20 text-white font-bold rounded-2xl hover:bg-white/30 transition-colors backdrop-blur-md">
          {card.cta}
        </button>
      </div>
    );
  }

  return (
    <div
      className="bg-white p-8 rounded-[2rem] border border-slate-100 shadow-sm flex flex-col md:flex-row items-center justify-between gap-6"
      data-purpose="action-card"
    >
      <div>
        <h4 className="text-xl font-extrabold text-agri-green mb-1">
          {card.title}
        </h4>
        <p className="text-slate-500 text-sm font-medium">{card.description}</p>
      </div>
      <button className="w-full md:w-auto px-8 py-3 bg-white border-2 border-agri-leaf text-agri-leaf font-bold rounded-2xl hover:bg-agri-leaf/5 transition-colors">
        {card.cta}
      </button>
    </div>
  );
}
