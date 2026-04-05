import { useState } from "react";
import type { ComponentType } from "react";
import { SIGNUP_STEPS, TOTAL_SIGNUP_STEPS } from "../../constants/signupSteps";
import { buildSignupPayload, resendVerification, signup } from "../../api/auth.api";
import { composeLocationText } from "../../utils/signup";
import type {
  RoleCode,
  SignupFormErrors,
  SignupFormState,
  SignupResponse,
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

const INITIAL_LOCATION = {
  region: "",
  province: "",
  city: "",
};

const INITIAL_FORM_STATE: SignupFormState = {
  email: "",
  mobileNumber: "",
  password: "",
  confirmPassword: "",
  firstName: "",
  lastName: "",
  displayName: "",
  buyerApplication: {
    isSelected: false,
    businessName: "",
    businessType: "",
    location: INITIAL_LOCATION,
  },
  farmerApplication: {
    isSelected: false,
    farmName: "",
    location: INITIAL_LOCATION,
  },
  hasAcceptedTerms: false,
  hasAcceptedPrivacy: false,
};

export function SignupFlow() {
  const [currentStep, setCurrentStep] = useState<SignupStepId>(1);
  const [form, setForm] = useState<SignupFormState>(INITIAL_FORM_STATE);
  const [errors, setErrors] = useState<SignupFormErrors>({});
  const [submitError, setSubmitError] = useState<string | null>(null);
  const [verificationError, setVerificationError] = useState<string | null>(
    null
  );
  const [verificationNotice, setVerificationNotice] = useState<string | null>(
    null
  );
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isVerificationSubmitting, setIsVerificationSubmitting] =
    useState(false);
  const [signupResponse, setSignupResponse] = useState<SignupResponse | null>(
    null
  );

  const currentStepDefinition = SIGNUP_STEPS[currentStep - 1];
  const CurrentStepComponent = SIGNUP_STEP_COMPONENTS[currentStep];

  function setFieldError(nextErrors: SignupFormErrors) {
    setErrors(nextErrors);
  }

  function handleContinue() {
    const nextErrors = validateCurrentStep(currentStep, form);
    if (Object.keys(nextErrors).length > 0) {
      setFieldError(nextErrors);
      return;
    }

    setErrors({});
    setCurrentStep((previousStep) => {
      if (previousStep >= TOTAL_SIGNUP_STEPS) {
        return previousStep;
      }

      return (previousStep + 1) as SignupStepId;
    });
  }

  function handleBack() {
    setSubmitError(null);
    setVerificationError(null);
    setVerificationNotice(null);
    setErrors({});
    setCurrentStep((previousStep) => {
      if (previousStep <= 1) {
        return previousStep;
      }

      return (previousStep - 1) as SignupStepId;
    });
  }

  function handleAccountFieldChange(
    field:
      | "email"
      | "mobileNumber"
      | "password"
      | "confirmPassword"
      | "firstName"
      | "lastName"
      | "displayName",
    value: string
  ) {
    setForm((currentForm) => ({
      ...currentForm,
      [field]: value,
    }));
    setErrors((currentErrors) => {
      const nextErrors = { ...currentErrors };
      delete nextErrors[field];
      return nextErrors;
    });
  }

  function handleToggleRole(roleCode: RoleCode) {
    setForm((currentForm) => ({
      ...currentForm,
      buyerApplication:
        roleCode === "BUYER"
          ? {
              ...currentForm.buyerApplication,
              isSelected: !currentForm.buyerApplication.isSelected,
            }
          : currentForm.buyerApplication,
      farmerApplication:
        roleCode === "FARMER"
          ? {
              ...currentForm.farmerApplication,
              isSelected: !currentForm.farmerApplication.isSelected,
            }
          : currentForm.farmerApplication,
    }));
    setErrors((currentErrors) => {
      const nextErrors = { ...currentErrors };
      delete nextErrors.roles;
      return nextErrors;
    });
  }

  function handleBuyerFieldChange(
    field: "businessName" | "businessType" | "region" | "province" | "city",
    value: string
  ) {
    setForm((currentForm) => ({
      ...currentForm,
      buyerApplication:
        field === "businessName" || field === "businessType"
          ? {
              ...currentForm.buyerApplication,
              [field]: value,
            }
          : {
              ...currentForm.buyerApplication,
              location: {
                ...currentForm.buyerApplication.location,
                [field]: value,
              },
            },
    }));

    setErrors((currentErrors) => {
      const nextErrors = { ...currentErrors };
      delete nextErrors[`buyer.${field}`];
      return nextErrors;
    });
  }

  function handleFarmerFieldChange(
    field: "farmName" | "region" | "province" | "city",
    value: string
  ) {
    setForm((currentForm) => ({
      ...currentForm,
      farmerApplication:
        field === "farmName"
          ? {
              ...currentForm.farmerApplication,
              farmName: value,
            }
          : {
              ...currentForm.farmerApplication,
              location: {
                ...currentForm.farmerApplication.location,
                [field]: value,
              },
            },
    }));

    setErrors((currentErrors) => {
      const nextErrors = { ...currentErrors };
      delete nextErrors[`farmer.${field}`];
      return nextErrors;
    });
  }

  function handleAgreementChange(
    field: "hasAcceptedTerms" | "hasAcceptedPrivacy",
    checked: boolean
  ) {
    setForm((currentForm) => ({
      ...currentForm,
      [field]: checked,
    }));

    setErrors((currentErrors) => {
      const nextErrors = { ...currentErrors };
      delete nextErrors[field];
      return nextErrors;
    });
  }

  async function handleSubmitSignup() {
    const nextErrors = validateCurrentStep(4, form);
    if (Object.keys(nextErrors).length > 0) {
      setFieldError(nextErrors);
      return;
    }

    setIsSubmitting(true);
    setSubmitError(null);
    setVerificationError(null);
    setVerificationNotice(null);
    setErrors({});

    try {
      const buyerLocationText = composeLocationText(form.buyerApplication.location);
      const farmerLocationText = composeLocationText(
        form.farmerApplication.location
      );
      const response = await signup(
        buildSignupPayload(form, buyerLocationText, farmerLocationText)
      );

      setSignupResponse(response.data);
      setVerificationNotice(
        `We sent a verification link to ${response.data.email}. Open that email to activate your account.`
      );
    } catch (error) {
      setSubmitError(
        error instanceof Error
          ? error.message
          : "Failed to create your account."
      );
    } finally {
      setIsSubmitting(false);
    }
  }

  async function handleResendVerification() {
    const emailToVerify = signupResponse?.email ?? form.email;
    if (!emailToVerify.trim()) {
      setVerificationError("Email is required before resending verification.");
      return;
    }

    setIsVerificationSubmitting(true);
    setVerificationError(null);
    setVerificationNotice(null);

    try {
      await resendVerification(emailToVerify.trim());
      setVerificationNotice(
        "If your account is eligible, a new verification link is on its way. Please check your inbox and spam folder."
      );
    } catch (error) {
      setVerificationError(
        error instanceof Error
          ? error.message
          : "Failed to resend verification."
      );
    } finally {
      setIsVerificationSubmitting(false);
    }
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
        form={form}
        errors={errors}
        isSubmitting={isSubmitting}
        isVerificationSubmitting={isVerificationSubmitting}
        submitError={submitError}
        verificationError={verificationError}
        verificationNotice={verificationNotice}
        signupResponse={signupResponse}
        onContinue={handleContinue}
        onBack={handleBack}
        onAccountFieldChange={handleAccountFieldChange}
        onToggleRole={handleToggleRole}
        onBuyerFieldChange={handleBuyerFieldChange}
        onFarmerFieldChange={handleFarmerFieldChange}
        onAgreementChange={handleAgreementChange}
        onSubmitSignup={handleSubmitSignup}
        onResendVerification={handleResendVerification}
      />
    </SignupShell>
  );
}

function validateCurrentStep(
  step: SignupStepId,
  form: SignupFormState
): SignupFormErrors {
  switch (step) {
    case 1:
      return validateAccountStep(form);
    case 2:
      return validateRoleStep(form);
    case 3:
      return validateRoleDetailsStep(form);
    case 4:
      return validateReviewStep(form);
    default:
      return {};
  }
}

function validateAccountStep(form: SignupFormState): SignupFormErrors {
  const nextErrors: SignupFormErrors = {};

  if (!form.firstName.trim()) {
    nextErrors.firstName = "First name is required.";
  }

  if (!form.lastName.trim()) {
    nextErrors.lastName = "Last name is required.";
  }

  if (!form.email.trim()) {
    nextErrors.email = "Email is required.";
  }

  if (
    form.mobileNumber.trim() &&
    form.mobileNumber.replace(/\D/g, "").length < 10
  ) {
    nextErrors.mobileNumber = "Mobile number must contain at least 10 digits.";
  }

  if (!form.password) {
    nextErrors.password = "Password is required.";
  } else if (form.password.length < 8) {
    nextErrors.password = "Password must be at least 8 characters.";
  }

  if (!form.confirmPassword) {
    nextErrors.confirmPassword = "Confirm password is required.";
  } else if (form.confirmPassword !== form.password) {
    nextErrors.confirmPassword = "Password and confirm password must match.";
  }

  return nextErrors;
}

function validateRoleStep(form: SignupFormState): SignupFormErrors {
  if (!form.buyerApplication.isSelected && !form.farmerApplication.isSelected) {
    return {
      roles: "Select at least one role application.",
    };
  }

  return {};
}

function validateRoleDetailsStep(form: SignupFormState): SignupFormErrors {
  const nextErrors: SignupFormErrors = {};

  if (form.buyerApplication.isSelected) {
    if (!form.buyerApplication.businessName.trim()) {
      nextErrors["buyer.businessName"] = "Business name is required.";
    }
    if (!form.buyerApplication.businessType.trim()) {
      nextErrors["buyer.businessType"] = "Business type is required.";
    }
    if (!form.buyerApplication.location.region.trim()) {
      nextErrors["buyer.region"] = "Business region is required.";
    }
    if (!form.buyerApplication.location.province.trim()) {
      nextErrors["buyer.province"] = "Business province is required.";
    }
    if (!form.buyerApplication.location.city.trim()) {
      nextErrors["buyer.city"] = "Business city is required.";
    }
  }

  if (form.farmerApplication.isSelected) {
    if (!form.farmerApplication.farmName.trim()) {
      nextErrors["farmer.farmName"] = "Farm name is required.";
    }
    if (!form.farmerApplication.location.region.trim()) {
      nextErrors["farmer.region"] = "Farm region is required.";
    }
    if (!form.farmerApplication.location.province.trim()) {
      nextErrors["farmer.province"] = "Farm province is required.";
    }
    if (!form.farmerApplication.location.city.trim()) {
      nextErrors["farmer.city"] = "Farm city is required.";
    }
  }

  return nextErrors;
}

function validateReviewStep(form: SignupFormState): SignupFormErrors {
  const nextErrors: SignupFormErrors = {};

  if (!form.hasAcceptedTerms) {
    nextErrors.hasAcceptedTerms = "You must accept the Terms of Service.";
  }

  if (!form.hasAcceptedPrivacy) {
    nextErrors.hasAcceptedPrivacy = "You must accept the Privacy Policy.";
  }

  return nextErrors;
}
