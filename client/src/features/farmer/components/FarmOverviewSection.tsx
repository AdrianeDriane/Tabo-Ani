import type { FarmerProfile } from "../types/farmer.types";

type FarmOverviewSectionProps = {
  farmer: FarmerProfile;
};

export function FarmOverviewSection({ farmer }: FarmOverviewSectionProps) {
  const [primaryImage, secondaryImage, tertiaryImage] = farmer.galleryImageUrls;

  return (
    <section className="mb-20 grid grid-cols-1 gap-10 lg:grid-cols-3">
      <div className="space-y-6 lg:col-span-2">
        <h2 className="mb-4 text-sm font-bold tracking-wider text-[#14532D] uppercase">
          Farm Gallery
        </h2>

        <div className="gallery-grid">
          <div className="gallery-item-1 overflow-hidden rounded-[1.5rem] shadow-sm">
            <img
              alt="Farm Landscape"
              className="h-full w-full object-cover"
              src={primaryImage}
            />
          </div>

          <div className="overflow-hidden rounded-[1.5rem] shadow-sm">
            <img
              alt="Harvesting"
              className="h-full w-full object-cover"
              src={secondaryImage}
            />
          </div>

          <div className="overflow-hidden rounded-[1.5rem] shadow-sm">
            <img
              alt="Farming Methods"
              className="h-full w-full object-cover"
              src={tertiaryImage}
            />
          </div>
        </div>
      </div>

      <div className="flex h-full flex-col justify-center rounded-[2rem] border border-gray-100 bg-white p-10 shadow-sm">
        <h3 className="mb-8 text-xl font-extrabold text-[#14532D]">Farm Statistics</h3>

        <div className="space-y-8">
          {farmer.statistics.map((stat) => (
            <div className="flex flex-col gap-1" key={stat.label}>
              <span className="text-xs font-bold tracking-widest text-gray-400 uppercase">
                {stat.label}
              </span>
              <span className="text-2xl font-bold text-[#15803D]">{stat.value}</span>
            </div>
          ))}
        </div>

        <div className="mt-12 border-t border-gray-100 pt-8">
          <p className="leading-relaxed text-gray-600 italic">"{farmer.quote}"</p>
        </div>
      </div>
    </section>
  );
}
