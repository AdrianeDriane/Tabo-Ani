import type { ProductDetail } from "../types/product.types";

type ProductBodySectionsProps = {
  product: ProductDetail;
};

export function ProductBodySections({ product }: ProductBodySectionsProps) {
  return (
    <div className="grid grid-cols-1 gap-12 border-t border-slate-100 pt-16 lg:grid-cols-3">
      <div className="space-y-16 lg:col-span-2">
        <article>
          <h2 className="font-display mb-6 text-2xl font-bold text-slate-900">
            Product Story
          </h2>
          <div className="max-w-none space-y-4 leading-relaxed text-slate-600">
            {product.productStoryParagraphs.map((paragraph) => (
              <p key={paragraph}>{paragraph}</p>
            ))}
          </div>
        </article>

        <article className="rounded-[2rem] bg-[#14532D] p-10 text-white shadow-xl">
          <div className="flex flex-col items-start gap-8 md:flex-row">
            <div className="h-24 w-24 shrink-0 overflow-hidden rounded-full border-4 border-white/20 bg-slate-200">
              <img
                alt={product.farmerName}
                className="h-full w-full object-cover"
                src={product.farmerAvatarUrl}
              />
            </div>

            <div>
              <h2 className="font-display mb-2 text-2xl font-bold">
                Farmer {product.farmerName}
              </h2>
              <p className="mb-4 font-medium text-green-100/80 italic">
                {product.farmerLocationLabel} • {product.farmerExperienceLabel}
              </p>
              <p className="mb-6 text-sm leading-relaxed text-green-50">
                "{product.farmerQuote}"
              </p>
              <div className="inline-block rounded-full bg-white/10 px-4 py-2 text-xs font-bold tracking-widest uppercase">
                Certified Organic Partner
              </div>
            </div>
          </div>
        </article>

        <article>
          <h2 className="font-display mb-6 text-2xl font-bold text-slate-900">
            Shipping &amp; Handling
          </h2>
          <div className="grid grid-cols-1 gap-6 md:grid-cols-2">
            <div className="rounded-[1.5rem] bg-slate-50 p-6">
              <p className="mb-2 font-bold text-slate-900">24-48 Hour Delivery</p>
              <p className="text-sm leading-relaxed text-slate-600">
                Direct from the highland consolidation center to our Metro Manila hub,
                ensuring freshness is preserved.
              </p>
            </div>

            <div className="rounded-[1.5rem] bg-slate-50 p-6">
              <p className="mb-2 font-bold text-slate-900">Distributor-Led Logistics</p>
              <p className="text-sm leading-relaxed text-slate-600">
                Our fleet of temperature-controlled vehicles maintains the optimal climate
                for root vegetables during transit.
              </p>
            </div>
          </div>
        </article>

        <article>
          <div className="mb-8 flex items-center justify-between">
            <h2 className="font-display text-2xl font-bold text-slate-900">
              Verified Customer Reviews
            </h2>
            <div className="text-sm font-bold text-[#14532D]">4.9 Overall Rating</div>
          </div>

          <div className="space-y-6">
            {product.customerReviews.map((review) => (
              <div key={review.id} className="rounded-[1.5rem] border border-slate-100 p-8">
                <div className="mb-4 flex justify-between">
                  <span className="font-bold">{review.customerName}</span>
                  <span className="text-xs font-medium text-slate-400">
                    {review.postedAtLabel}
                  </span>
                </div>
                <p className="text-sm leading-relaxed text-slate-600">{review.comment}</p>
              </div>
            ))}
          </div>
        </article>
      </div>

      <aside className="hidden lg:block">
        <div className="sticky top-40 space-y-6">
          <div className="shadow-soft rounded-[1.5rem] border border-slate-100 bg-white p-6">
            <h4 className="font-display mb-4 text-sm font-bold tracking-wide text-slate-900 uppercase">
              Quick Facts
            </h4>
            <ul className="space-y-3 text-sm">
              {product.quickFacts.map((fact) => (
                <li className="flex justify-between" key={fact.label}>
                  <span className="text-slate-500">{fact.label}</span>
                  <span className="font-semibold text-[#14532D]">{fact.value}</span>
                </li>
              ))}
            </ul>
          </div>
        </div>
      </aside>
    </div>
  );
}
