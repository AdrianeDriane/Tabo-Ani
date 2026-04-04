import { useState } from "react";
import type { ComponentType } from "react";
import { SIGNUP_STEPS, TOTAL_SIGNUP_STEPS } from "../../constants/signupSteps";
import type {
  SignupStepComponentProps,
  SignupStepId,
} from "../../types/signup.types";
import { SignupShell } from "./SignupShell";
import {
  SignupStep1Credentials,
  SignupStep2ProfileDetails,
  SignupStep3BusinessVerification,
  SignupStep4ReviewSubmit,
} from "./steps";

const SIGNUP_STEP_COMPONENTS: Record<
  SignupStepId,
  ComponentType<SignupStepComponentProps>
> = {
  1: SignupStep1Credentials,
  2: SignupStep2ProfileDetails,
  3: SignupStep3BusinessVerification,
  4: SignupStep4ReviewSubmit,
};

export function SignupFlow() {
  const [currentStep, setCurrentStep] = useState<SignupStepId>(1);
  const currentStepDefinition = SIGNUP_STEPS[currentStep - 1];
  const CurrentStepComponent = SIGNUP_STEP_COMPONENTS[currentStep];

  function handleContinue() {
    setCurrentStep((previousStep) => {
      if (previousStep >= TOTAL_SIGNUP_STEPS) {
        return previousStep;
      }

      return (previousStep + 1) as SignupStepId;
    });
  }

  function handleBack() {
    setCurrentStep((previousStep) => {
      if (previousStep <= 1) {
        return previousStep;
      }

      return (previousStep - 1) as SignupStepId;
    });
  }

  return (
    <SignupShell
      currentStep={currentStep}
      totalSteps={TOTAL_SIGNUP_STEPS}
      stepTitle={currentStepDefinition.title}
      stepHelperText={currentStepDefinition.helperText}
      contentContainerClassName={currentStepDefinition.contentContainerClassName}
      brandPanelContent={currentStepDefinition.brandPanel}
      showLoginPrompt={currentStep === 1}
    >
      <CurrentStepComponent
        step={currentStepDefinition}
        onContinue={handleContinue}
        onBack={handleBack}
      />
    </SignupShell>
  );
}
