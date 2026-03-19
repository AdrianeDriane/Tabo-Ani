import { Footer } from "../components/common/Footer";
import { Header } from "../components/common/Header";
import { ArtisanGoodsSection } from "../components/sections/artisan-goods/ArtisanGoodsSection";
import { HeroSection } from "../components/sections/hero/HeroSection";
import { MarketplaceCtaSection } from "../components/sections/marketplace-cta/MarketplaceCtaSection";
import { PremiumProduceSection } from "../components/sections/premium-produce/PremiumProduceSection";

export function Landing() {
  return (
    <div className="bg-agri-light font-sans text-gray-900">
      <Header />
      <main>
        <HeroSection />
        <PremiumProduceSection />
        <ArtisanGoodsSection />
        <MarketplaceCtaSection />
      </main>
      <Footer />
    </div>
  );
}
