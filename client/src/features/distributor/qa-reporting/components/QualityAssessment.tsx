import type { Grade, GradeOption } from "../types/qaReporting.types";

type QualityAssessmentProps = {
  freshness: number;
  onFreshnessChange: (value: number) => void;
  quantityMatch: boolean;
  onQuantityMatchChange: (value: boolean) => void;
  grade: Grade;
  onGradeChange: (value: Grade) => void;
  gradeOptions: GradeOption[];
};

export default function QualityAssessment({
  freshness,
  onFreshnessChange,
  quantityMatch,
  onQuantityMatchChange,
  grade,
  onGradeChange,
  gradeOptions,
}: QualityAssessmentProps) {
  return (
    <div className="bg-white p-6 rounded-xl shadow-sm">
      <h2 className="font-display text-lg font-bold text-gray-900 mb-6">
        2. Produce Quality Assessment
      </h2>
      <div className="space-y-8">
        <div className="space-y-3">
          <div className="flex justify-between items-end">
            <label className="text-sm font-bold text-gray-700">
              Freshness / Ripeness
            </label>
            <span className="text-xl font-display font-bold text-agri-green">
              {freshness}/10
            </span>
          </div>
          <input
            className="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer"
            max={10}
            min={1}
            type="range"
            value={freshness}
            onChange={(event) =>
              onFreshnessChange(Number(event.target.value))
            }
          />
          <div className="flex justify-between text-[10px] text-gray-400 font-bold uppercase tracking-wider">
            <span>Overripe/Poor</span>
            <span>Perfect/Prime</span>
          </div>
        </div>

        <div className="pt-4 flex items-center justify-between">
          <div>
            <p className="text-sm font-bold text-gray-700">
              Quantity &amp; Weight Match
            </p>
            <p className="text-xs text-gray-500">
              Does physical count match Waybill: 450kg?
            </p>
          </div>
          <div className="flex bg-gray-100 p-1 rounded-lg">
            <button
              className={
                quantityMatch
                  ? "px-4 py-1.5 text-xs font-bold rounded-md bg-agri-leaf/10 text-agri-green shadow-sm"
                  : "px-4 py-1.5 text-xs font-bold rounded-md text-gray-500 hover:text-agri-accent transition-colors"
              }
              type="button"
              onClick={() => onQuantityMatchChange(true)}
            >
              MATCH
            </button>
            <button
              className={
                quantityMatch
                  ? "px-4 py-1.5 text-xs font-bold rounded-md text-gray-500 hover:text-agri-accent transition-colors"
                  : "px-4 py-1.5 text-xs font-bold rounded-md bg-agri-accent/10 text-agri-accent shadow-sm"
              }
              type="button"
              onClick={() => onQuantityMatchChange(false)}
            >
              DISCREPANCY
            </button>
          </div>
        </div>

        <div className="pt-4">
          <p className="text-sm font-bold text-gray-700 mb-3">
            Overall Grade
          </p>
          <div className="flex gap-2">
            {gradeOptions.map((option) => (
              <button
                key={option.value}
                className={
                  grade === option.value
                    ? "flex-1 py-2 bg-agri-leaf/15 text-agri-green font-bold text-sm rounded-lg"
                    : "flex-1 py-2 text-gray-500 font-bold text-sm rounded-lg hover:bg-agri-leaf/10"
                }
                type="button"
                onClick={() => onGradeChange(option.value)}
              >
                {option.label}
              </button>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
