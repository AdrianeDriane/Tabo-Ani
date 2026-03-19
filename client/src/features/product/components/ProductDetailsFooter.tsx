export function ProductDetailsFooter() {
  return (
    <footer className="border-t border-slate-100 bg-slate-50 py-12">
      <div className="mx-auto max-w-6xl px-4 text-center">
        <div className="mb-4 flex items-center justify-center gap-2">
          <div className="flex h-6 w-6 items-center justify-center rounded bg-slate-300 text-xs font-bold text-white">
            T
          </div>
          <span className="font-display tracking-tight font-bold text-slate-400">
            TABO-ANI
          </span>
        </div>
        <p className="text-xs tracking-widest text-slate-400 uppercase">
          © 2023 Connecting Filipino Farmers to Your Table.
        </p>
      </div>
    </footer>
  );
}
