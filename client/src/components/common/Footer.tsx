const marketplaceLinks = [
  "Vegetables",
  "Wholesale Fruits",
  "Grains & Seeds",
  "Livestock",
];

const credibilityLinks = [
  "Farmer Verification",
  "Quality Control",
  "Logistics Partners",
  "Buyer Protection",
];

const companyLinks = [
  "About Tabo-Ani",
  "Sustainability",
  "Contact Us",
  "Terms of Trade",
];

const socialLinks = ["FB", "TW", "LI"];

function FooterColumn({ title, links }: { title: string; links: string[] }) {
  return (
    <div>
      <h4 className="mb-10 text-sm font-black uppercase tracking-widest text-white">
        {title}
      </h4>
      <ul className="space-y-6 font-medium text-gray-400">
        {links.map((link) => (
          <li key={link}>
            <a className="transition-colors hover:text-agri-leaf" href="#">
              {link}
            </a>
          </li>
        ))}
      </ul>
    </div>
  );
}

export function Footer() {
  return (
    <footer className="border-t border-white/5 bg-gray-950 pb-16 pt-32 text-white">
      <div className="mx-auto max-w-7xl px-6">
        <div className="mb-24 grid grid-cols-2 gap-16 lg:grid-cols-5">
          <div className="col-span-2">
            <div className="mb-10 flex items-center gap-4">
              <div className="flex h-12 w-12 items-center justify-center rounded-2xl bg-agri-accent shadow-xl shadow-agri-accent/20">
                <span className="font-display text-2xl font-bold text-white">
                  T
                </span>
              </div>
              <span className="font-display text-3xl font-extrabold tracking-tighter">
                Tabo-Ani
              </span>
            </div>
            <p className="mb-12 max-w-sm text-lg leading-loose text-gray-400">
              Connecting the heart of the farm to the tables of the world.
              Premium sourcing for professional buyers.
            </p>
            <div className="flex gap-6">
              {socialLinks.map((social) => (
                <a
                  key={social}
                  className="flex h-14 w-14 items-center justify-center rounded-2xl bg-white/5 transition-all hover:-translate-y-1 hover:bg-agri-accent"
                  href="#"
                >
                  {social}
                </a>
              ))}
            </div>
          </div>

          <FooterColumn title="Marketplace" links={marketplaceLinks} />
          <FooterColumn title="Credibility" links={credibilityLinks} />
          <FooterColumn title="Company" links={companyLinks} />
        </div>

        <div className="border-t border-white/5 pt-12 text-center font-medium text-gray-500">
          <p>
            &copy; 2024 Tabo-Ani Agrotech Solutions. All rights reserved.
            Premium Agricultural Exchange.
          </p>
        </div>
      </div>
    </footer>
  );
}
