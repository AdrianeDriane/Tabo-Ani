export type SignupStepId = 1 | 2 | 3 | 4;

export type SignupBrandHighlight = {
  icon: string;
  label: string;
};

export type SignupBrandTestimonial = {
  quote: string;
  name: string;
  role: string;
  avatarUrl: string;
  stars?: number;
};

export type SignupBrandPanelContent = {
  logoIcon: string;
  heroTitle: string;
  heroDescription: string;
  imageUrl: string;
  imageAlt: string;
  imageOpacityClassName?: string;
  overlays?: string[];
  highlights?: SignupBrandHighlight[];
  testimonial?: SignupBrandTestimonial;
  footerNote?: string;
};

export type SignupStep = {
  id: SignupStepId;
  title: string;
  ctaLabel: string;
  helperText?: string;
  contentContainerClassName: string;
  brandPanel: SignupBrandPanelContent;
};

export type RoleCode = "BUYER" | "FARMER";

export type RoleLocationFormState = {
  region: string;
  province: string;
  city: string;
};

export type BuyerApplicationFormState = {
  isSelected: boolean;
  businessName: string;
  businessType: string;
  location: RoleLocationFormState;
};

export type FarmerApplicationFormState = {
  isSelected: boolean;
  farmName: string;
  location: RoleLocationFormState;
};

export type SignupFormState = {
  email: string;
  mobileNumber: string;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
  displayName: string;
  buyerApplication: BuyerApplicationFormState;
  farmerApplication: FarmerApplicationFormState;
  hasAcceptedTerms: boolean;
  hasAcceptedPrivacy: boolean;
  verificationToken: string;
};

export type SignupFormErrors = Partial<Record<string, string>>;

export type SignupRoleApplicationResponse = {
  roleCode: RoleCode;
  kycApplicationId: string;
  applicationStatus: string;
};

export type SignupResponse = {
  userId: string;
  email: string;
  mobileNumber: string | null;
  firstName: string;
  lastName: string;
  displayName: string | null;
  isEmailVerified: boolean;
  accountStatus: string;
  requestedRoles: SignupRoleApplicationResponse[];
  emailVerificationTokenPreview: string | null;
};

export type EmailVerificationStatusResponse = {
  userId: string;
  email: string;
  isEmailVerified: boolean;
  verifiedAt: string | null;
  emailVerificationTokenPreview: string | null;
};

export type VerifyEmailRequest = {
  email: string;
  token: string;
};

export type SignupStepComponentProps = {
  step: SignupStep;
  form: SignupFormState;
  errors: SignupFormErrors;
  isSubmitting: boolean;
  isVerificationSubmitting: boolean;
  submitError: string | null;
  verificationError: string | null;
  signupResponse: SignupResponse | null;
  emailVerificationStatus: EmailVerificationStatusResponse | null;
  onContinue: () => void;
  onBack: () => void;
  onAccountFieldChange: (
    field: "email" | "mobileNumber" | "password" | "confirmPassword" | "firstName" | "lastName" | "displayName",
    value: string,
  ) => void;
  onToggleRole: (roleCode: RoleCode) => void;
  onBuyerFieldChange: (
    field: "businessName" | "businessType" | "region" | "province" | "city",
    value: string,
  ) => void;
  onFarmerFieldChange: (
    field: "farmName" | "region" | "province" | "city",
    value: string,
  ) => void;
  onAgreementChange: (field: "hasAcceptedTerms" | "hasAcceptedPrivacy", checked: boolean) => void;
  onVerificationTokenChange: (value: string) => void;
  onSubmitSignup: () => void;
  onVerifyEmail: () => void;
  onResendVerification: () => void;
};
