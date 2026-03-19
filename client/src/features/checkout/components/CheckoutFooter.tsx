export default function CheckoutFooter() {
  return (
    <footer className="mx-auto mt-20 max-w-6xl border-t border-gray-200 px-4 py-10">
      <div className="flex flex-col items-center justify-between gap-6 md:flex-row">
        <div className="text-xs font-bold tracking-widest text-gray-400 uppercase">Tabo-Ani System v2.4</div>
        <div className="flex gap-8">
          <a className="text-xs font-bold text-gray-500 hover:text-agri-green" href="#">
            Terms of Trade
          </a>
          <a className="text-xs font-bold text-gray-500 hover:text-agri-green" href="#">
            Logistics Policy
          </a>
          <a className="text-xs font-bold text-gray-500 hover:text-agri-green" href="#">
            Secure Checkout
          </a>
        </div>
      </div>
    </footer>
  );
}
