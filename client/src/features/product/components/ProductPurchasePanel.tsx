import type { ProductDetail } from "../types/product.types";

type ProductPurchasePanelProps = {
  product: ProductDetail;
  quantity: number;
  onDecreaseQuantity: () => void;
  onIncreaseQuantity: () => void;
};

export function ProductPurchasePanel({
  product,
  quantity,
  onDecreaseQuantity,
  onIncreaseQuantity,
}: ProductPurchasePanelProps) {
  return (
    <div className="flex flex-col justify-center">
      <div className="mb-2 text-xs font-bold tracking-[0.2em] text-[#14532D] uppercase">
        {product.categoryLabel}
      </div>

      <h1 className="font-display mb-4 text-4xl leading-tight font-bold text-slate-900 md:text-5xl">
        {product.name}
      </h1>

      <div className="mb-6 flex items-baseline gap-4">
        <span className="font-display text-3xl font-bold text-[#14532D]">
          {new Intl.NumberFormat("en-PH", {
            style: "currency",
            currency: "PHP",
            minimumFractionDigits: 2,
          }).format(product.pricePerUnit)}
        </span>
        <span className="font-medium text-slate-400">/ {product.unitLabel}</span>
      </div>

      <div className="mb-8 space-y-4">
        <div className="flex items-center gap-3">
          <span className="h-2.5 w-2.5 rounded-full bg-green-500" />
          <span className="text-sm font-semibold text-slate-700">
            {product.availableStockLabel}
          </span>
        </div>

        <div className="flex items-center justify-between rounded-2xl border border-green-100 bg-[#F8FAF8] p-4">
          <div>
            <p className="text-xs font-medium tracking-wide text-slate-500 uppercase">
              Farmer Partner
            </p>
            <p className="font-bold text-[#14532D]">{product.farmerName}</p>
          </div>

          <div className="text-right">
            <p className="text-xs font-medium tracking-wide text-slate-500 uppercase">
              Rating
            </p>
            <p className="font-bold text-slate-800">{product.farmerRatingLabel}</p>
          </div>
        </div>
      </div>

      <div className="flex flex-wrap items-center gap-4">
        <div className="flex items-center rounded-full bg-slate-100 px-2 py-1">
          <button
            className="flex h-10 w-10 items-center justify-center text-xl font-bold hover:text-[#14532D]"
            onClick={onDecreaseQuantity}
            type="button"
          >
            -
          </button>

          <input
            className="w-12 border-none bg-transparent text-center font-bold focus:ring-0"
            readOnly
            type="number"
            value={quantity}
          />

          <button
            className="flex h-10 w-10 items-center justify-center text-xl font-bold hover:text-[#14532D]"
            onClick={onIncreaseQuantity}
            type="button"
          >
            +
          </button>
        </div>

        <button
          className="font-display min-w-[200px] flex-1 rounded-full bg-[#FF6B00] px-8 py-4 font-bold text-white shadow-lg shadow-orange-200 transition-all hover:brightness-110"
          type="button"
        >
          Add to Cart
        </button>
      </div>
    </div>
  );
}
