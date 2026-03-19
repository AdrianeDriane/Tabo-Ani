import type { SignupStep } from "../types/signup.types";

export const SIGNUP_STEPS: SignupStep[] = [
  {
    id: 1,
    title: "Account Credentials",
    ctaLabel: "Continue to Profile Details",
  },
  {
    id: 2,
    title: "Profile Details",
    ctaLabel: "Continue to Business Verification",
  },
  {
    id: 3,
    title: "Business Verification",
    ctaLabel: "Continue to Review",
  },
  {
    id: 4,
    title: "Review & Submit",
    ctaLabel: "Submit Registration",
  },
];

export const TOTAL_SIGNUP_STEPS = SIGNUP_STEPS.length;
