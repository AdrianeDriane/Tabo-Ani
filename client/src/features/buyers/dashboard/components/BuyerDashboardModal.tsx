type BuyerDashboardModalProps = {
  isOpen: boolean;
  title: string;
  description: string;
  onClose: () => void;
  actionLabel?: string;
};

export default function BuyerDashboardModal({
  isOpen,
  title,
  description,
  onClose,
  actionLabel = "Close",
}: BuyerDashboardModalProps) {
  if (!isOpen) {
    return null;
  }

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center px-4 py-8">
      <div
        className="absolute inset-0 bg-slate-900/40 backdrop-blur-sm"
        onClick={onClose}
        aria-hidden="true"
      />
      <div className="relative w-full max-w-md rounded-2xl bg-white p-6 shadow-xl">
        <h3 className="text-lg font-bold text-agri-green">{title}</h3>
        <p className="mt-2 text-sm text-slate-600">{description}</p>
        <div className="mt-6 flex flex-col gap-3 sm:flex-row sm:justify-end">
          <button
            type="button"
            className="w-full rounded-xl bg-agri-green px-5 py-3 text-sm font-bold text-white transition-colors hover:bg-agri-green/90 sm:w-auto"
            onClick={onClose}
          >
            {actionLabel}
          </button>
        </div>
      </div>
    </div>
  );
}
