import type { PhotoCard } from "../types/qaReporting.types";

type PhotoEvidenceProps = {
  photoCards: PhotoCard[];
};

export default function PhotoEvidence({ photoCards }: PhotoEvidenceProps) {
  return (
    <div className="bg-white p-6 rounded-xl shadow-sm">
      <div className="flex justify-between items-center mb-6">
        <h2 className="font-display text-lg font-bold text-gray-900">
          3. Photographic Evidence
        </h2>
        <span className="px-3 py-1 bg-red-50 text-red-600 text-[10px] font-bold rounded-full uppercase">
          Mandatory: 3 Photos
        </span>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        {photoCards.map((card) => (
          <div
            key={card.title}
            className="aspect-square rounded-xl flex flex-col items-center justify-center p-4 text-center hover:bg-green-50/30 transition-all group cursor-pointer"
          >
            <div className="w-12 h-12 bg-gray-50 rounded-full flex items-center justify-center mb-3 group-hover:bg-agri-leaf/20">
              {card.shape === "square" && (
                <div className="w-6 h-6 bg-gray-300 rounded" />
              )}
              {card.shape === "circle" && (
                <div className="w-6 h-6 bg-gray-300 rounded-full" />
              )}
              {card.shape === "line" && (
                <div className="h-1 w-6 bg-gray-400 group-hover:bg-agri-green" />
              )}
            </div>
            <p className="text-xs font-bold text-gray-700">{card.title}</p>
            <p className="text-[10px] text-gray-400 mt-1">
              {card.description}
            </p>
          </div>
        ))}
      </div>
    </div>
  );
}
