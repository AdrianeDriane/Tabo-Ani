type AuthLoadingSpinnerProps = {
  fullScreen?: boolean;
  label?: string;
};

export function AuthLoadingSpinner({
  fullScreen = true,
  label = "Loading",
}: AuthLoadingSpinnerProps) {
  return (
    <div
      className={
        fullScreen
          ? "flex min-h-screen items-center justify-center bg-agri-light px-6"
          : "flex min-h-64 items-center justify-center px-6 py-10"
      }
    >
      <div className="flex items-center justify-center rounded-full bg-white/90 p-6 shadow-xl shadow-agri-accent/10">
        <div className="relative size-12">
          <div className="absolute inset-0 rounded-full border-4 border-slate-200" />
          <div className="absolute inset-0 animate-spin rounded-full border-4 border-transparent border-t-agri-accent border-r-agri-green" />
          <span className="sr-only">{label}</span>
        </div>
      </div>
    </div>
  );
}
