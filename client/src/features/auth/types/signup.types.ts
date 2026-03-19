export type SignupStepId = 1 | 2 | 3 | 4;

export type SignupStep = {
  id: SignupStepId;
  title: string;
  ctaLabel: string;
};

export type SignupStepComponentProps = {
  step: SignupStep;
  onContinue: () => void;
};
