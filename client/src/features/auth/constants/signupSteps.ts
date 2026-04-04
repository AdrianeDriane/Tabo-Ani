import type { SignupStep } from "../types/signup.types";

export const SIGNUP_STEPS: SignupStep[] = [
  {
    id: 1,
    title: "Create Your Account",
    ctaLabel: "Continue to Role Selection",
    contentContainerClassName: "max-w-md",
    brandPanel: {
      logoIcon: "agriculture",
      heroTitle: "Join the Future of Philippine Agriculture",
      heroDescription:
        "Connect directly with local farmers, access fair market prices, and build a sustainable supply chain for the nation.",
      imageUrl:
        "https://lh3.googleusercontent.com/aida-public/AB6AXuAVGrnukIzrXGXGm10769KXG_OaXIe0Zk2kQqLgDlc7kj52iE01oqtt18VLyftdOVI74qBLt_cI99HnAZZ_m0t9lxzbJNy5NnIYfMyBPq-IbPb1C9aDQ1gLRuRHwnEBPjA0evG1zvcoW-pOys3GsCa1qG51MmR0JBx2kAtyXGwm0iP13mRrGFLBG-Z6VsW2oICuUbgMnB4TvWsZMe2mn02GrIYAy0ZXsftVMUOy57vhLCKHaiDZ_QqfzCJkh8BALgUW3pq7aASlHCXI",
      imageAlt: "Lush green Philippine rice terraces at sunrise",
      imageOpacityClassName: "opacity-60",
      overlays: ["bg-gradient-to-t from-agri-green via-agri-green/40 to-transparent"],
      highlights: [
        { icon: "verified_user", label: "Verified Traders" },
        { icon: "trending_up", label: "Market Analytics" },
        { icon: "payments", label: "Secure Payments" },
        { icon: "location_on", label: "Local Logistics" },
      ],
    },
  },
  {
    id: 2,
    title: "Choose Your Roles",
    ctaLabel: "Continue to Role Details",
    helperText: "You can apply for BUYER, FARMER, or both in one account.",
    contentContainerClassName: "max-w-xl",
    brandPanel: {
      logoIcon: "eco",
      heroTitle: "Join our growing ecosystem",
      heroDescription:
        "Connecting farmers, distributors, and retailers to create a more efficient and sustainable food supply chain.",
      imageUrl:
        "https://lh3.googleusercontent.com/aida-public/AB6AXuDS3obm0bTFyhL8OSsZXOp5nkkJpjZM5chXsN7IhsM2VFoGgN1spGOcCxJsTILC10rLYQH32mbyzz6em1e60N1HqISLjrhV0AtI949FD4pC4BaIOQah2LZ0qyJ2B3XiLZbcixEMW5jEhw46ex1UQy0ZcDRsgIXHqvIvTWk3UdvGBpKz1dPMeRuUUV0bDW9PocdJuKLoMeCucsD_6pYAXWXm2aRr405kh58vHYpy0J2R_5u67h067Ux7U82ZcGXljOp7Od5i2p9IhVaG",
      imageAlt: "Farmer tending to healthy green crops",
      overlays: [
        "bg-gradient-to-tr from-agri-accent/40 via-agri-green/60 to-agri-leaf/40 mix-blend-multiply",
        "bg-gradient-to-t from-agri-green via-agri-green/20 to-transparent",
      ],
      footerNote: "(c) 2024 Tabo-Ani Agricultural Solutions",
    },
  },
  {
    id: 3,
    title: "Role Application Details",
    ctaLabel: "Continue to Review",
    helperText:
      "Complete the profile details required for each role you selected. KYC documents will be collected after account creation.",
    contentContainerClassName: "max-w-xl",
    brandPanel: {
      logoIcon: "eco",
      heroTitle: "Empowering the Filipino Farmer",
      heroDescription:
        "Join the most trusted digital marketplace connecting farmers directly to consumers and businesses across the Philippines.",
      imageUrl:
        "https://lh3.googleusercontent.com/aida-public/AB6AXuDS3obm0bTFyhL8OSsZXOp5nkkJpjZM5chXsN7IhsM2VFoGgN1spGOcCxJsTILC10rLYQH32mbyzz6em1e60N1HqISLjrhV0AtI949FD4pC4BaIOQah2LZ0qyJ2B3XiLZbcixEMW5jEhw46ex1UQy0ZcDRsgIXHqvIvTWk3UdvGBpKz1dPMeRuUUV0bDW9PocdJuKLoMeCucsD_6pYAXWXm2aRr405kh58vHYpy0J2R_5u67h067Ux7U82ZcGXljOp7Od5i2p9IhVaG",
      imageAlt: "Farmer tending to healthy green crops",
      overlays: [
        "bg-gradient-to-tr from-agri-accent/40 via-agri-green/60 to-agri-leaf/40 mix-blend-multiply",
        "bg-gradient-to-t from-agri-green via-agri-green/20 to-transparent",
      ],
      testimonial: {
        quote:
          "Setting up my account was simple, and I could start the verification steps right after confirming my email.",
        name: "Ricardo Santos",
        role: "Benguet Farmer",
        avatarUrl:
          "https://lh3.googleusercontent.com/aida-public/AB6AXuDV_TUMeg4YUd0VK4AKYxBcOFW4GibLQNjxuSOpmlTO_TtdLTHzkM0TOOFGn0agjdD3bdQqoXFfHcTsuvDbA7k3UPR4vyQr-f1Kzir79kcziKMq7BUc8HLFfePU4lVO2aW3Yl7ewCX-g0m3NTdf49ciVJfffRmhtBYDs9ugibQkEuH5pGt4qG7t47nE3GvhGAs8Fg4pTkRDbQnX_0GcK0mdpIW3ZGKTfDtQpfbGpbe_X7IYA6BW7YBUg9VJ1TO96NrX3FCYJ4cqknws",
      },
      footerNote: "(c) 2024 Tabo-Ani Agricultural Solutions",
    },
  },
  {
    id: 4,
    title: "Review And Verify",
    ctaLabel: "Create My Account",
    helperText:
      "Review your application, accept the policies, and finish account creation. We will email your verification link immediately after signup.",
    contentContainerClassName: "max-w-xl",
    brandPanel: {
      logoIcon: "agriculture",
      heroTitle: "Shape the Future of Philippine Agriculture",
      heroDescription:
        "Your journey to digital empowerment begins. Unlock new opportunities, achieve fair value, and cultivate your legacy with Tabo-Ani.",
      imageUrl:
        "https://lh3.googleusercontent.com/aida-public/AB6AXuAVGrnukIzrXGXGm10769KXG_OaXIe0Zk2kQqLgDlc7kj52iE01oqtt18VLyftdOVI74qBLt_cI99HnAZZ_m0t9lxzbJNy5NnIYfMyBPq-IbPb1C9aDQ1gLRuRHwnEBPjA0evG1zvcoW-pOys3GsCa1qG51MmR0JBx2kAtyXGwm0iP13mRrGFLBG-Z6VsW2oICuUbgMnB4TvWsZMe2mn02GrIYAy0ZXsftVMUOy57vhLCKHaiDZ_QqfzCJkh8BALgUW3pq7aASlHCXI",
      imageAlt: "Lush green Philippine rice terraces at sunrise",
      imageOpacityClassName: "opacity-60",
      overlays: ["bg-gradient-to-t from-agri-green via-agri-green/40 to-transparent"],
      highlights: [
        { icon: "verified_user", label: "Secure Your Future" },
        { icon: "trending_up", label: "Grow Your Business" },
        { icon: "payments", label: "Seamless Transactions" },
        { icon: "location_on", label: "Connect Locally" },
      ],
    },
  },
];

export const TOTAL_SIGNUP_STEPS = SIGNUP_STEPS.length;
