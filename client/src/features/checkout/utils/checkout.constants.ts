import type { CheckoutData } from "../types/checkout.types";

export const checkoutData: CheckoutData = {
  buyerName: "Ricardo Santos",
  buyerInitials: "RS",
  navLinks: [
    { id: "marketplace", label: "Marketplace", href: "/" },
    { id: "orders", label: "Orders", href: "/orders/TA-8821" },
    { id: "messages", label: "Messages", href: "/messages" },
    { id: "suppliers", label: "Suppliers", href: "/farmer/1" },
  ],
  pageTitle: "Checkout",
  pageSubtitle: "Finalize your agricultural trade order for fresh produce.",
  deliveryFields: [
    {
      id: "full-name",
      label: "Full Name",
      placeholder: "",
      value: "Ricardo Santos",
      colSpan: "full",
    },
    {
      id: "street-address",
      label: "Street Address",
      placeholder: "House No., Street, Brgy.",
      colSpan: "full",
    },
    {
      id: "city",
      label: "City / Municipality",
      placeholder: "e.g. La Trinidad",
      colSpan: "single",
    },
    {
      id: "province",
      label: "Province",
      placeholder: "e.g. Benguet",
      colSpan: "single",
    },
    {
      id: "postal-code",
      label: "Postal Code",
      placeholder: "2601",
      colSpan: "single",
    },
  ],
  paymentOptions: [
    {
      id: "cod",
      label: "Cash on Delivery",
      description: "Pay upon produce arrival",
    },
    {
      id: "gcash",
      label: "GCash",
      description: "Direct digital transfer",
    },
  ],
  defaultPaymentMethod: "cod",
  handlingNotesPlaceholder:
    "Specify requirements for crate handling or storage during transit...",
  items: [
    {
      id: "item-1",
      name: "Organic Highland Carrots",
      farmName: "Benguet Heritage Farm",
      variantLabel: "50kg Bulk Crate",
      imageUrl:
        "https://lh3.googleusercontent.com/aida-public/AB6AXuAiae2K0z_xNDPu4x_qoKEzDmPLRxGFYXAzojI_DjW1X_1fH3TzS7-ptAk_fTiK0r_paOUQzqgb1fIU6gbRrk0JEhjHaa0Ll5cWIBQzJeYPH_XQ5nsYVauW49eoRfAXam1pjw1OSqUR3qZxhGjHcddEfaE4xGkqNqfWVErRFu0yvx-uCF_Oq6gxx9LgVjvMlVNLsDb_qk01mBkQoO0Vfw7ZMToZ55wBiYDS8ZQ4WYVxZ3I4P71Kp4tVfmpP2vIOpa1_IN_q39i7wFNy",
      amountLabel: "PHP 4,250.00",
    },
  ],
  totals: {
    subtotalLabel: "PHP 4,250.00",
    deliveryFeeLabel: "PHP 350.00",
    totalLabel: "PHP 4,600.00",
  },
};
