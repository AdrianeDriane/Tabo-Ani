import type { Checkpoint } from "../types/qaReporting.types";

type InspectionCheckpointProps = {
  checkpoint: Checkpoint;
  onChange: (next: Checkpoint) => void;
};

export default function InspectionCheckpoint({
  checkpoint,
  onChange,
}: InspectionCheckpointProps) {
  return (
    <div className="bg-white p-6 rounded-xl shadow-sm">
      <h2 className="font-display text-lg font-bold text-gray-900 mb-4">
        1. Inspection Checkpoint
      </h2>
      <div className="grid grid-cols-2 gap-4">
        <label className="relative flex items-center p-4 rounded-lg cursor-pointer hover:bg-gray-50 transition-colors has-[:checked]:bg-green-50">
          <input
            checked={checkpoint === "pickup"}
            className="w-4 h-4 text-agri-green focus:ring-agri-green"
            name="checkpoint"
            type="radio"
            value="pickup"
            onChange={() => onChange("pickup")}
          />
          <div className="ml-4">
            <p className="text-sm font-bold text-gray-900">Pickup Validation</p>
            <p className="text-xs text-gray-500">
              Document condition at supplier source
            </p>
          </div>
        </label>
        <label className="relative flex items-center p-4 rounded-lg cursor-pointer hover:bg-gray-50 transition-colors has-[:checked]:bg-green-50">
          <input
            checked={checkpoint === "delivery"}
            className="w-4 h-4 text-agri-green focus:ring-agri-green"
            name="checkpoint"
            type="radio"
            value="delivery"
            onChange={() => onChange("delivery")}
          />
          <div className="ml-4">
            <p className="text-sm font-bold text-gray-900">
              Delivery Validation
            </p>
            <p className="text-xs text-gray-500">
              Document condition at hub arrival
            </p>
          </div>
        </label>
      </div>
    </div>
  );
}
