import type { FarmerProfile } from "../types/farmer.types";

const FARMER_PROFILES_BY_ID: Record<string, FarmerProfile> = {
  "1": {
    id: "1",
    name: "Farmer Roberto",
    locationLabel: "La Trinidad, Benguet",
    activeSinceLabel: "Since 2008",
    ratingLabel: "4.9",
    reviewCountLabel: "124 verified reviews",
    avatarImageUrl:
      "https://lh3.googleusercontent.com/aida-public/AB6AXuAnCAcSVPTXPWgilKrzKZ8Zj-j1Cj8iWCijT_HioprcIRf6pcJ9nsQo0ZduWW9Ugdv8aQcGG7fz31J0_UsW6kMt9UgN517PimvWo_pvGOCy6pZE_0JziSgOtdgOOEIc5znzAI8rA-a3abcMQNCLXRgd6PRr0Duyd2v-tcofebwmQK_2ZcZUS0vnbk2ZuU_qdTt1krJWt43BWTnsIexzbezqOFkEanI62JcjV65ExrIGkBLF-DixhlWR9v22iTWdXsJGmaWj0Mulapt7",
    galleryImageUrls: [
      "https://lh3.googleusercontent.com/aida-public/AB6AXuDMQgfVaOopJFUX_IU2QEPUaHm5EvSLnqSMb2FqmA-U-xkMc9euNzimhkDX-8IKJyeZtVy1HO9X_ERkzMzdwcs8VZJ74R3En3SaJSQdLms88VpkGjtb7oPpQYXw5XkPrtEGnaAmrkKOASnPq95NO_2pZI2JKDSRzB3RS002fFx3lTzLQif9tu6mB1QYkW2Z9_TfEBgNLXUq0WaaWV96pDsQn0Toh0BmvqUGuzXcDD_9emNzRkphcgpd4rzabgZNrmGvRAU-xL6CUV_i",
      "https://lh3.googleusercontent.com/aida-public/AB6AXuBc4tdQV1ftKzlYq79tmx0CsjwYhzrBNT9ZhefCWiSb8pVuvskRC3r2VyceModYdbXEZqKNMjUceNwHkWpO7j48NuyrCIPZvR6KNLVj-Oi11H0etxT4XrEMyYx8jpj8Mu7L0JHQXvmXMua15e_CblHLW_Sor6tNxZzs2nRs6yl-5FOHbjmlbcgWh9o-XQmzArz-u-_OscYZUfgJlGVPMwPG6VsM-SxlPGnHl_G642OhdhtTFmDShoLQ8NbRblYCfHMy34lfFF6pXl7x",
      "https://lh3.googleusercontent.com/aida-public/AB6AXuBNmJytMv3NQaE8ZKVDziFjdTTg36j2jww96ubfvocbQIJx2tsZ-UV30oPUKm8KTSyQTxgQWlaKcUTsGNRmNePlZOGpFISY0zOo5f2LJrANkF9f7ue2dkiQrqarFwLey_x2aAvtZoGDkwiI6PrOomlB7isYzLwP7mhG4hW_JG307Pg8V6LRVDka-qpESXI5QnPTLR9ZBDO_vU51KdynnCPn9aD4wZiYiGZwSQPasWevAyqOP05RjSn7qeZH3VJCNkzCW5Ju0m03-NGM",
    ],
    statistics: [
      { label: "Land Area", value: "12 Hectares" },
      { label: "Methodology", value: "Organic Certified" },
      { label: "Expertise", value: "15+ Years Experience" },
      { label: "Focus", value: "Highland Root Crops" },
    ],
    quote:
      "My family has been tilling these lands for three generations. We believe in high-altitude nutrition and sustainable earth-to-table practices.",
    listingsSectionTitle: "Current Season Offerings",
    listingsSectionSubtitle: "Freshly harvested from the La Trinidad highlands.",
    listings: [
      {
        id: "l1",
        name: "Benguet Carrots",
        priceLabel: "P45/kg",
        description: "Premium grade, earthy and sweet carrots harvested daily.",
        imageUrl:
          "https://lh3.googleusercontent.com/aida-public/AB6AXuDWfd_NSM3zmwV0X0ypcbfaLsXOcmgnQoRuvO-pcHNV5Abxp2fr9cDnZqejap9Zz9WUzJrdQPXcm85DIbn-5DPeUpqknrYGpyY8gVBxFrS3iFXrRLvde5WBBAiA_wzI6XcHPOEepQF3Yv5E7axGpea_c3oMYSy_MeWnuUS_Ms4xVOUm0y_IuitshWx9xjYqjFSnyWeyqXlyX7E7QLW4Z17WXs4ZuZ6Q8wuAxBXyVrMq7OvkS_GvVa1iZK94Rd1GCB_ksnvDqdHPd9c2",
      },
      {
        id: "l2",
        name: "Yukon Gold Potatoes",
        priceLabel: "P72/kg",
        description: "High-altitude potatoes with a rich, buttery texture and skin.",
        imageUrl:
          "https://lh3.googleusercontent.com/aida-public/AB6AXuBEmp1AQGKWqIFXvK0mcyFfyR1E0L39UcxuTLBAcMRYyhvjcCqcR7WcQm0gERrWbzV-nGx-zAfZ0XhCIG78LXoqxQlYhJp52KIdXIlv4a3JVQ-1YNu4MWo6iZxLwb5pGv7sQHnJ5bK7yYv1Qdxlb9yw7vO42im9EP7I0PxoBZkbaRjWK-K1HR_TILAR9P3afej5zz3Vt0NyvJbeSDWq5VNkJDxDeCEKO1e2n4y974p6tJPpFoHHdMvcXt0h5M6sCGV6M45ds9f02vgr",
      },
      {
        id: "l3",
        name: "Highland Radish",
        priceLabel: "P38/kg",
        description: "Crisp, peppery radishes perfect for fresh salads and pickling.",
        imageUrl:
          "https://lh3.googleusercontent.com/aida-public/AB6AXuA7cVHBvQ6TkupADC7ds-q33m3WWSTVzDmxK7h-8VCISw2w1ajWFUf9JICuEOZ0hKO8aiSmRFF8rqbsw4h5eqMwUK0mmcGCfKSta_E19RVV_vli2c--K8ici5XvDBeLyYdKDb-D863YApuds7leTd_lMgWGp7GhrL51PK06qnsGnGzIChUqJjBnC70WXV2qcB1lkJpDpSMv696Qq3TUqOiX5OvJzevVb8-hRdCugpYroszo0nt1RXcGS_EYplTYGCFE72rvtd-eZMry",
      },
      {
        id: "l4",
        name: "Iceberg Lettuce",
        priceLabel: "P55/pc",
        description: "Crisp heads of lettuce grown without harmful pesticides.",
        imageUrl:
          "https://lh3.googleusercontent.com/aida-public/AB6AXuBqsEQC-DMonA4dd0raATQWvZyKbneh5kmwdwStcBk52Uw69dxLBzL8JzFtrgx_o3np4LaLe9pvTWima4v-onXnRbu8VFRTbpTW3Mj0dQBvl7YjAn7jHWOAPZaGQKhOybg9FCSAAsAZNp_bd7CfRxW8CQxllyNCRexh9KCXmqXTjHsT4F8wN0Muf2CanaCxJEz1S7U5n7P64hHI288iIGVI8FhKlczH5wVl5Ei33LS-sI38cn3PXzrwTrO-oxZN8d1vgnVNjXpxvHqC",
      },
    ],
    reviews: [
      {
        id: "r1",
        buyerName: "Elena Marasigan",
        buyerMetaLabel: "Wholesale Buyer",
        postedAtLabel: "2 weeks ago",
        comment:
          "The quality of Farmer Roberto's potatoes is consistently the best in the marketplace. We use them for our restaurant chain and they never fail in terms of freshness and shelf-life. Highly recommended for bulk orders.",
        ratingLabel: "★★★★★",
      },
      {
        id: "r2",
        buyerName: "Chef Miguel T.",
        buyerMetaLabel: "Hotel Supplier",
        postedAtLabel: "1 month ago",
        comment:
          "Reliability is key in my business, and Roberto has been our top supplier for 3 years. Delivery is always on time and the packaging is superior, minimizing transit damage for delicate highland greens.",
        ratingLabel: "★★★★★",
      },
    ],
  },
};

export function getFarmerProfileById(farmerId: string): FarmerProfile | null {
  return FARMER_PROFILES_BY_ID[farmerId] ?? null;
}
