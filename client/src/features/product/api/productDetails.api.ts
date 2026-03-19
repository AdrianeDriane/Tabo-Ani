import type { ProductDetail } from "../types/product.types";

const PRODUCT_DETAILS_BY_ID: Record<string, ProductDetail> = {
  "1": {
    id: "1",
    categoryLabel: "Highland Root Vegetables",
    name: "Organic Highland Carrots",
    pricePerUnit: 145,
    unitLabel: "kg",
    availableStockLabel: "In Stock (450kg available)",
    farmerName: "Ricardo Dela Cruz",
    farmerRatingLabel: "4.9/5.0",
    heroImageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuBqT29Zvd0_CM0ybZfPpKYMC12Qev81nYME34jDAjwf521y2UZJYXyPwdjmsauwRTteah_ghp8aMnhXCneefFt9uKMa8SvS893q1XqZ6chW5HpAPpiFmd7hmosrQ1MmgedVwN24TRNveYbGDXPUeDOqq2SDJxO_KHf75XPn6gl55uygx0PXnp6Z3rWFzE44UQmhOQSzbYelmws4e5ms-C1kwGIfNdiSsAQN7fo91nH-_sbk_LR5UkF0OH2tRnNRe75z30y3EVHOm0JU",
    galleryImageUrls: [
      "https://lh3.googleusercontent.com/aida-public/AB6AXuB6ERBWbjGcE7lTgaWaEFoi1oDb7-kd9PwjzyFhIEtMhAhqUXVzkCbjCm2toccgmoP87R2pf2D4bMR-SdKlJNXaD-whlqHbHbdo0cTiUnBqk7KjWoVEOX8ubBxL-a-aunLHrvH0FrDlIWP1pbL1GyIAEUb6TrvDU7uTSYAhf3nai6ZeGp2Gju_XfS6T0BVnsb7z7vy-2lrbvaNzTSL4GktGKSs8TrfFSh-UduOQggoLO7e_zKPSXAR90QJKSnyGvvfI96t6eck6S1OJ",
      "https://lh3.googleusercontent.com/aida-public/AB6AXuAcEgu-YFAtgUuNf56zGVfJvSJTou3UYmvUGn3VDSmN8pQwY04xHxGcaTgerNUgthkpsczTdis7QX5XhHzPSu_WC8nJWDUtD-_4NEbd3XaI61hE01Bj24m3e6Z39HXxbpi-N_L__pWQEPkjPbYqwv9ZeziFp3OpiJaOWAO7SobUypJiGWnrP7siISKo_Zqo3E3SAquy_qrBIIbawOA6lDWfCBq2Y527E72OvzcnMmgwjo4qEvqOKPYy0zPnmo_oIxZON6dnb6jNiqk0",
      "https://lh3.googleusercontent.com/aida-public/AB6AXuBTtw5SfQZw3IMr2E1Rvew01_CnWz1XULDq9BaDWdxNT1j00gC4qvz2lZ7KCjw4FPhaqIGQtcTNJMcuiuVaZC5N0tFuHLS7fBi07fLR22osfnOCMXIe0ek2PB5gKLy2NcnZY6RN1-QrVAnorZ35WrDaPYLAfEZQQTOTbh_pi6eaYnNEvfmP9kHXIKkmSS_hY3uwVuxJoJX9iJxOZAX0Wf_O1DdcnfjiEZqSkPBXqL_0SYMeAXxLq_PzXchHLHbfM57Kl0MHT7kpVzqp",
      "https://lh3.googleusercontent.com/aida-public/AB6AXuCF_xLPz77o0raTy20a2PaYXqzr-ZuRQRdtPvXkdVOqDYW3U5ADTXLfnH2ICx4aJTZOFj1JdCBALaZuVUI-GfDgxnXb6tSEBk1o4OxGcbenrHDGKKwwYlkls3pao_kYRe6sWTABvvJyMQKvYtNjZCA6xx76oX27zNko2aZO60IWAmCiWW2bmvLmp6MdvpvtgQdwG7oJGXpfcaXFfiAvGZcBEJDKKmdNpZS55twysCQ_Lsf7h2p1FYwGXlBrPl-KUy_CmQ0qvNMnKC6I",
    ],
    productStoryParagraphs: [
      "Grown at an elevation of 1,500 meters above sea level, our Highland Carrots thrive in the mineral-rich volcanic soil of Benguet. This unique environment, characterized by cool temperatures and natural mountain mist, results in a carrot that is exceptionally sweet, crisp, and packed with nutrients.",
      "Each batch is harvested manually to prevent bruising and ensured to be free from synthetic pesticides. These are the same vibrant, orange roots that have made the \"Salad Bowl of the Philippines\" world-renowned.",
    ],
    farmerLocationLabel: "Atok, Benguet",
    farmerExperienceLabel: "15 Years in Agriculture",
    farmerQuote:
      "I believe that the land gives back exactly what you put in. By using traditional composting and respecting the natural cycle of the mountain, we produce vegetables that are healthy for the body and the soul.",
    farmerAvatarUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuAq_ebV93Ovs5bP8N_sPkJUiITGoJzgPafC8gtqaqZJANcljmTv66y19QsdtIKIOfTHo9hB0WJZ2b0LQeraEWlHtBjmWXkMDsb1fD-Y-HTO_5Y9Nc_byqgjQO2J9QC_GadPMcJkIHoZzcdcDA-FZGe_X7IVAqABm2r3VxFScovU6_OQkU5qA8lcbHXYnd7823M7pzrDOXp3xdwDxfX9Lj22mMoLCkdBwY0XAL5dpuUnJcF6enbE-IjoIM1Bf3pMKRxWPZX1JDyHjrWT",
    quickFacts: [
      { label: "Origin", value: "Benguet" },
      { label: "Method", value: "Organic" },
      { label: "Weight", value: "1kg per unit" },
    ],
    customerReviews: [
      {
        id: "r1",
        customerName: "Maria C.",
        postedAtLabel: "2 days ago",
        comment:
          "The sweetest carrots I've ever tasted! You can really tell the difference in the crunch. Arrived in perfect condition.",
      },
      {
        id: "r2",
        customerName: "Jonathan L.",
        postedAtLabel: "1 week ago",
        comment:
          "Perfect for juicing. The color is so vibrant and there was no dirt residue. Exceptional quality for the price.",
      },
    ],
  },
};

export function getProductDetailsById(productId: string): ProductDetail | null {
  return PRODUCT_DETAILS_BY_ID[productId] ?? null;
}
