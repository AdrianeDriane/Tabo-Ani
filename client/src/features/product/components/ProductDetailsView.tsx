import { useMemo, useState } from "react";
import type { ProductDetail } from "../types/product.types";
import { ProductBodySections } from "./ProductBodySections";
import { ProductDetailsFooter } from "./ProductDetailsFooter";
import { ProductDetailsHeader } from "./ProductDetailsHeader";
import { ProductImageGallery } from "./ProductImageGallery";
import { ProductPurchasePanel } from "./ProductPurchasePanel";

type ProductDetailsViewProps = {
  product: ProductDetail;
};

export function ProductDetailsView({ product }: ProductDetailsViewProps) {
  const [selectedImageIndex, setSelectedImageIndex] = useState(0);
  const [quantity, setQuantity] = useState(1);

  const galleryImages = useMemo(() => {
    if (product.galleryImageUrls.length === 0) {
      return [product.heroImageUrl];
    }

    return product.galleryImageUrls;
  }, [product.galleryImageUrls, product.heroImageUrl]);

  return (
    <>
      <ProductDetailsHeader />

      <main className="px-4 pb-20 pt-32 text-slate-800 antialiased">
        <div className="mx-auto max-w-6xl">
          <section className="mb-20 grid grid-cols-1 gap-12 lg:grid-cols-2">
            <ProductImageGallery
              images={galleryImages}
              onSelectImage={setSelectedImageIndex}
              productName={product.name}
              selectedImageIndex={selectedImageIndex}
            />

            <ProductPurchasePanel
              onDecreaseQuantity={() => setQuantity((prev) => Math.max(1, prev - 1))}
              onIncreaseQuantity={() => setQuantity((prev) => prev + 1)}
              product={product}
              quantity={quantity}
            />
          </section>

          <ProductBodySections product={product} />
        </div>
      </main>

      <ProductDetailsFooter />
    </>
  );
}
