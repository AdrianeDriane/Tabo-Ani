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

export type SignupStepComponentProps = {
  step: SignupStep;
  onContinue: () => void;
  onBack: () => void;
};
