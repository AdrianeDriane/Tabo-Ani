type FormActionsProps = {
  onEscalate?: () => void;
  onSubmitClick?: () => void;
};

export default function FormActions({
  onEscalate,
  onSubmitClick,
}: FormActionsProps) {
  return (
    <div className="flex flex-col sm:flex-row gap-4 items-center">
      <button
        className="w-full sm:flex-grow py-4 bg-agri-green text-white font-display font-bold rounded-xl shadow-lg shadow-green-900/20 hover:bg-agri-green/90 transition-all transform active:scale-[0.98]"
        type="button"
        onClick={onSubmitClick}
      >
        SUBMIT VERIFIED REPORT
      </button>
      <button
        className="w-full sm:w-auto px-8 py-4 bg-agri-accent/10 text-agri-accent font-bold rounded-xl hover:bg-agri-accent/20 transition-colors"
        type="button"
        onClick={onEscalate}
      >
        ESCALATE DISCREPANCY
      </button>
    </div>
  );
}
