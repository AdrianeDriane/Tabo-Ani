type FormActionsProps = {
  onEscalate?: () => void;
};

export default function FormActions({ onEscalate }: FormActionsProps) {
  return (
    <div className="flex flex-col sm:flex-row gap-4 items-center">
      <button
        className="w-full sm:flex-grow py-4 bg-agri-green text-white font-display font-bold rounded-xl shadow-lg shadow-green-900/20 hover:bg-green-800 transition-all transform active:scale-[0.98]"
        type="submit"
      >
        SUBMIT VERIFIED REPORT
      </button>
      <button
        className="w-full sm:w-auto px-8 py-4 bg-white text-red-600 font-bold rounded-xl hover:bg-red-50 transition-colors"
        type="button"
        onClick={onEscalate}
      >
        ESCALATE DISCREPANCY
      </button>
    </div>
  );
}
