type ProductImageGalleryProps = {
  productName: string;
  images: string[];
  selectedImageIndex: number;
  onSelectImage: (index: number) => void;
};

export function ProductImageGallery({
  productName,
  images,
  selectedImageIndex,
  onSelectImage,
}: ProductImageGalleryProps) {
  const selectedImage = images[selectedImageIndex] ?? images[0];

  return (
    <div className="space-y-4">
      <div className="shadow-soft aspect-square overflow-hidden rounded-[1.5rem] bg-slate-100">
        <img alt={productName} className="h-full w-full object-cover" src={selectedImage} />
      </div>

      <div className="grid grid-cols-4 gap-4">
        {images.map((imageUrl, index) => (
          <button
            key={imageUrl}
            className={`aspect-square overflow-hidden rounded-2xl bg-slate-100 shadow-sm ${
              index === selectedImageIndex
                ? "border-2 border-[#14532D]"
                : "border border-transparent"
            }`}
            onClick={() => onSelectImage(index)}
            type="button"
          >
            <img
              alt={`${productName} thumbnail ${index + 1}`}
              className="h-full w-full object-cover"
              src={imageUrl}
            />
          </button>
        ))}
      </div>
    </div>
  );
}
