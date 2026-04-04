import { Link } from "react-router-dom";
import {
  marketplaceFilterGroups,
  marketplaceFooterSections,
  marketplaceNavLinks,
  marketplaceProducts,
} from "../utils/buyerMarketplace.constants";

const HERO_IMAGE_URL =
  "https://lh3.googleusercontent.com/aida-public/AB6AXuCExs0RixUQEh_ovVv7t9ILedF6tSo9SQGcgL2uZ-nrG0HLJ1c6W6Uk-9IICtXEFHgYhkHI_xYgcfjANkSfX_SOS32mW5Co_qnSsw74XgzMCzdX1MnnYxMuJdxdo-oWsJP6u2naU4H-65iRzqpk0ZxPw4Q9RncRcZJOdYzmtsXAmZk27r3Ef-2Upt6Gx35WjYuhsQ8C1L35p_DDQDshCK3TNNkk0rBuLwRH94gARv1RmGoziNKF7Wj-zj-Qitvq_FPHYVS8gSZyaUQW";

function MarketplaceNav() {
  return (
    <nav className="sticky top-0 z-50 border-b border-gray-100 bg-white">
      <div className="mx-auto h-20 w-full max-w-[1680px] px-4 sm:px-6 lg:px-8 2xl:px-12">
        <div className="flex h-full items-center justify-between gap-4">
          <span className="font-display text-xl font-extrabold tracking-tight text-agri-green sm:text-2xl">
            TABO-ANI
          </span>

          <ul className="hidden items-center gap-8 text-sm md:flex lg:gap-10">
            {marketplaceNavLinks.map((link) => (
              <li key={link.label}>
                {link.href.startsWith("/") ? (
                  <Link
                    to={link.href}
                    className={
                      link.isActive
                        ? "border-b-2 border-agri-leaf pb-1 font-semibold text-agri-green"
                        : "font-medium text-gray-500 transition-colors hover:text-agri-green"
                    }
                  >
                    {link.label}
                  </Link>
                ) : (
                  <a
                    href={link.href}
                    className={
                      link.isActive
                        ? "border-b-2 border-agri-leaf pb-1 font-semibold text-agri-green"
                        : "font-medium text-gray-500 transition-colors hover:text-agri-green"
                    }
                  >
                    {link.label}
                  </a>
                )}
              </li>
            ))}
          </ul>

          <div className="flex items-center gap-2 sm:gap-4">
            <Link
              to="/login"
              className="px-2 py-2 text-sm font-medium text-gray-500 sm:px-4"
            >
              Sign In
            </Link>
            <Link
              to="/checkout"
              className="rounded-full bg-agri-accent px-4 py-2.5 text-sm font-bold text-white transition-all active:scale-95 hover:shadow-lg sm:px-8 sm:py-3"
            >
              Cart (0)
            </Link>
          </div>
        </div>
      </div>
    </nav>
  );
}

function ProductCard({
  name,
  price,
  description,
  stock,
  seller,
  rating,
  deliveryWindow,
  imageUrl,
  imageAlt,
}: (typeof marketplaceProducts)[number]) {
  return (
    <article className="group flex flex-col overflow-hidden rounded-[2rem] bg-white shadow-[0_10px_25px_-5px_rgba(0,0,0,0.04),0_8px_10px_-6px_rgba(0,0,0,0.04)] transition-transform hover:-translate-y-1">
      <div className="relative h-56 overflow-hidden sm:h-64">
        <img
          src={imageUrl}
          alt={imageAlt}
          className="size-full object-cover transition-transform duration-500 group-hover:scale-105"
        />
        <div className="absolute top-4 left-4 rounded-full bg-white/95 px-3 py-1 shadow-sm backdrop-blur-sm">
          <span className="text-xs font-bold text-agri-green">{stock}</span>
        </div>
      </div>

      <div className="flex flex-1 flex-col p-5 sm:p-6">
        <div className="mb-2 flex items-start justify-between gap-4">
          <h3 className="font-display text-lg font-bold text-gray-900 sm:text-xl">
            {name}
          </h3>
          <span className="shrink-0 text-sm font-bold text-agri-leaf sm:text-base">
            {price}
          </span>
        </div>

        <p className="mb-4 text-sm text-gray-400">{description}</p>

        <div className="mt-auto space-y-4">
          <div className="flex items-center justify-between border-t border-gray-50 pt-4">
            <div className="flex flex-col">
              <span className="text-[10px] font-bold tracking-wider text-gray-400 uppercase">
                Seller
              </span>
              <span className="text-sm font-semibold text-agri-green">{seller}</span>
            </div>
            <div className="text-right">
              <span className="text-[10px] font-bold tracking-wider text-gray-400 uppercase">
                Rating
              </span>
              <div className="text-sm font-bold text-agri-accent">{rating} *</div>
            </div>
          </div>

          <div className="flex items-center justify-between rounded-2xl bg-gray-50 p-3">
            <span className="text-xs text-gray-500">
              Delivery:{" "}
              <span className="font-medium text-gray-900">{deliveryWindow}</span>
            </span>
          </div>

          <button
            type="button"
            className="w-full rounded-2xl bg-agri-green py-3.5 text-sm font-bold text-white transition-colors active:scale-[0.98] hover:bg-agri-leaf sm:py-4"
          >
            Add to Cart
          </button>
        </div>
      </div>
    </article>
  );
}

export default function BuyerMarketplacePage() {
  return (
    <div className="bg-agri-bg text-gray-800 antialiased">
      <MarketplaceNav />

      <main className="mx-auto w-full max-w-[1680px] px-4 py-8 sm:px-6 sm:py-10 lg:px-8 2xl:px-12">
        <section className="mb-12">
          <div className="flex flex-col gap-6 rounded-[2rem] bg-white p-5 shadow-[0_10px_25px_-5px_rgba(0,0,0,0.04),0_8px_10px_-6px_rgba(0,0,0,0.04)] sm:p-6 lg:flex-row lg:items-end lg:p-8">
            <div className="w-full flex-1">
              <label
                htmlFor="marketplace-search"
                className="mb-2 ml-1 block text-xs font-bold tracking-wider text-gray-400 uppercase"
              >
                Search Products
              </label>
              <input
                id="marketplace-search"
                type="text"
                placeholder="Search fresh produce, grains, or poultry..."
                className="w-full rounded-2xl border-none bg-gray-50 p-4 text-sm transition-all placeholder:text-gray-400 focus:ring-2 focus:ring-agri-leaf"
              />
            </div>

            <div className="grid w-full grid-cols-1 gap-4 sm:grid-cols-2 lg:w-auto lg:grid-cols-3">
              {marketplaceFilterGroups.map((group) => (
                <div key={group.id}>
                  <label
                    htmlFor={group.id}
                    className="mb-2 ml-1 block text-xs font-bold tracking-wider text-gray-400 uppercase"
                  >
                    {group.label}
                  </label>
                  <select
                    id={group.id}
                    className="w-full cursor-pointer rounded-2xl border-none bg-gray-50 p-4 text-sm font-medium text-gray-600 transition-all focus:ring-2 focus:ring-agri-leaf lg:w-48"
                    defaultValue={group.options[0]}
                  >
                    {group.options.map((option) => (
                      <option key={option}>{option}</option>
                    ))}
                  </select>
                </div>
              ))}
            </div>
          </div>
        </section>

        <section className="mb-14 sm:mb-16">
          <div className="relative flex min-h-[340px] items-center overflow-hidden rounded-[2.5rem] bg-agri-green sm:min-h-[380px] lg:min-h-[420px]">
            <div className="absolute inset-0 opacity-40">
              <img
                src={HERO_IMAGE_URL}
                alt="Agricultural landscape"
                className="size-full object-cover"
              />
            </div>
            <div className="relative z-10 max-w-2xl px-6 py-10 sm:px-10 md:px-14 lg:px-20">
              <span className="mb-4 inline-block rounded-full bg-agri-leaf px-4 py-1 text-[11px] font-bold tracking-widest text-white uppercase">
                Seasonal Special
              </span>
              <h1 className="mb-6 font-display text-3xl leading-tight font-extrabold text-white sm:text-4xl lg:text-5xl">
                Harvest Fresh Highland Produce
              </h1>
              <p className="mb-8 text-base leading-relaxed text-gray-200 sm:text-lg">
                Directly sourced from the mountainous regions of Benguet. Premium
                quality, expertly graded, and delivered within 24 hours.
              </p>
              <button
                type="button"
                className="rounded-full bg-white px-8 py-3 text-sm font-bold text-agri-green transition-all hover:bg-gray-100 sm:px-10 sm:py-4 sm:text-base"
              >
                Explore Collection
              </button>
            </div>
          </div>
        </section>

        <section>
          <div className="mb-8 flex flex-col gap-2 sm:flex-row sm:items-center sm:justify-between">
            <h2 className="font-display text-2xl font-bold text-agri-green sm:text-3xl">
              Available Today
            </h2>
            <span className="text-sm font-medium text-gray-500">
              Showing 24 products
            </span>
          </div>

          <div className="grid grid-cols-1 gap-6 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 2xl:gap-8">
            {marketplaceProducts.map((product) => (
              <ProductCard key={product.id} {...product} />
            ))}
          </div>
        </section>

        <section className="mt-14 flex justify-center sm:mt-16">
          <button
            type="button"
            className="rounded-full border-2 border-agri-green px-8 py-3 text-sm font-bold text-agri-green transition-all hover:bg-agri-green hover:text-white sm:px-12 sm:py-4 sm:text-base"
          >
            Load More Products
          </button>
        </section>
      </main>

      <footer className="mt-16 border-t border-gray-100 bg-white py-12 sm:mt-24 sm:py-16">
        <div className="mx-auto w-full max-w-[1680px] px-4 sm:px-6 lg:px-8 2xl:px-12">
          <div className="grid grid-cols-1 gap-12 md:grid-cols-4">
            <div className="md:col-span-1">
              <span className="font-display text-2xl font-extrabold tracking-tight text-agri-green">
                TABO-ANI
              </span>
              <p className="mt-4 leading-relaxed text-gray-500">
                The premier agricultural marketplace connecting professional
                buyers with the finest Filipino farmers.
              </p>
            </div>

            {marketplaceFooterSections.map((section) => (
              <div key={section.title}>
                <h4 className="mb-6 font-display text-sm font-bold tracking-wider text-gray-900 uppercase">
                  {section.title}
                </h4>
                <ul className="space-y-4 text-gray-500">
                  {section.links.map((link) => (
                    <li key={link}>
                      <a href="#" className="transition-colors hover:text-agri-leaf">
                        {link}
                      </a>
                    </li>
                  ))}
                </ul>
              </div>
            ))}
          </div>

          <div className="mt-12 flex flex-col items-center justify-between gap-4 border-t border-gray-50 pt-8 md:mt-16 md:flex-row">
            <p className="text-center text-sm text-gray-400 md:text-left">
              (c) 2023 Tabo-Ani Agricultural Marketplace. All rights reserved.
            </p>
            <div className="flex items-center gap-6">
              <a href="#" className="text-sm text-gray-400 transition-colors hover:text-agri-green">
                Privacy Policy
              </a>
              <a href="#" className="text-sm text-gray-400 transition-colors hover:text-agri-green">
                Compliance
              </a>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
}
