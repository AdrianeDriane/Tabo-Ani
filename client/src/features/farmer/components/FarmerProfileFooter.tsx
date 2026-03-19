export function FarmerProfileFooter() {
  return (
    <footer className="mt-20 bg-[#14532D] py-16 text-white">
      <div className="mx-auto grid max-w-7xl grid-cols-1 gap-12 px-6 md:grid-cols-4">
        <div className="md:col-span-2">
          <h3 className="mb-6 text-2xl font-extrabold tracking-tighter">TABO-ANI</h3>
          <p className="max-w-sm leading-relaxed text-[#16A34A]">
            Bridging the gap between Filipino highland farmers and the modern kitchen.
            Sustainable, transparent, and premium agricultural sourcing.
          </p>
        </div>

        <div>
          <h4 className="mb-4 text-sm font-bold tracking-widest text-[#16A34A] uppercase">
            Platform
          </h4>
          <ul className="space-y-3 text-sm">
            <li>
              <a className="hover:underline" href="#">
                About Our Mission
              </a>
            </li>
            <li>
              <a className="hover:underline" href="#">
                Farmer Verification
              </a>
            </li>
            <li>
              <a className="hover:underline" href="#">
                Pricing Structure
              </a>
            </li>
          </ul>
        </div>

        <div>
          <h4 className="mb-4 text-sm font-bold tracking-widest text-[#16A34A] uppercase">
            Support
          </h4>
          <ul className="space-y-3 text-sm">
            <li>
              <a className="hover:underline" href="#">
                Help Center
              </a>
            </li>
            <li>
              <a className="hover:underline" href="#">
                Dispute Resolution
              </a>
            </li>
            <li>
              <a className="hover:underline" href="#">
                Contact Support
              </a>
            </li>
          </ul>
        </div>
      </div>

      <div className="mx-auto mt-16 flex max-w-7xl flex-col justify-between gap-4 border-t border-white/10 px-6 pt-8 text-xs text-[#16A34A] md:flex-row">
        <p>© 2024 Tabo-Ani Agricultural Marketplace. All rights reserved.</p>
        <div className="flex gap-6">
          <a href="#">Privacy Policy</a>
          <a href="#">Terms of Service</a>
        </div>
      </div>
    </footer>
  );
}
