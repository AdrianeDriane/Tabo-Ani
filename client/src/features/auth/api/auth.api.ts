import type {
  EmailVerificationStatusResponse,
  ResendEmailVerificationResponse,
  SignupFormState,
  SignupResponse,
  VerifyEmailRequest,
} from "../types/signup.types";
import {
  CURRENT_PRIVACY_VERSION,
  CURRENT_TERMS_VERSION,
} from "../constants/authPolicies";
import { API_BASE_URL } from "../../../api/config";

type ApiResponse<T> = {
  success: boolean;
  message: string;
  data: T;
};

type ErrorResponse = {
  success: boolean;
  message: string;
  errors: string[];
};

type SignupRequestPayload = {
  email: string;
  mobileNumber: string | null;
  password: string;
  confirmPassword: string;
  firstName: string;
  lastName: string;
  displayName: string | null;
  hasAcceptedTerms: boolean;
  termsVersion: string;
  hasAcceptedPrivacy: boolean;
  privacyVersion: string;
  buyerApplication: {
    businessName: string;
    businessType: string;
    locationText: string;
  } | null;
  farmerApplication: {
    farmName: string;
    locationText: string;
  } | null;
};

export async function signup(
  payload: SignupRequestPayload
): Promise<ApiResponse<SignupResponse>> {
  return postJson<ApiResponse<SignupResponse>>("/api/v1/auth/signup", payload);
}

export async function resendVerification(
  email: string
): Promise<ApiResponse<ResendEmailVerificationResponse>> {
  return postJson<ApiResponse<ResendEmailVerificationResponse>>(
    "/api/v1/auth/verify-email/resend",
    {
      email,
    }
  );
}

export async function verifyEmail(
  payload: VerifyEmailRequest
): Promise<ApiResponse<EmailVerificationStatusResponse>> {
  return postJson<ApiResponse<EmailVerificationStatusResponse>>(
    "/api/v1/auth/verify-email",
    payload
  );
}

export function buildSignupPayload(
  form: SignupFormState,
  buyerLocationText: string,
  farmerLocationText: string
): SignupRequestPayload {
  return {
    email: form.email.trim(),
    mobileNumber: form.mobileNumber.trim() ? form.mobileNumber.trim() : null,
    password: form.password,
    confirmPassword: form.confirmPassword,
    firstName: form.firstName.trim(),
    lastName: form.lastName.trim(),
    displayName: form.displayName.trim() ? form.displayName.trim() : null,
    hasAcceptedTerms: form.hasAcceptedTerms,
    termsVersion: CURRENT_TERMS_VERSION,
    hasAcceptedPrivacy: form.hasAcceptedPrivacy,
    privacyVersion: CURRENT_PRIVACY_VERSION,
    buyerApplication: form.buyerApplication.isSelected
      ? {
          businessName: form.buyerApplication.businessName.trim(),
          businessType: form.buyerApplication.businessType.trim(),
          locationText: buyerLocationText,
        }
      : null,
    farmerApplication: form.farmerApplication.isSelected
      ? {
          farmName: form.farmerApplication.farmName.trim(),
          locationText: farmerLocationText,
        }
      : null,
  };
}

async function postJson<T>(path: string, payload: object): Promise<T> {
  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!response.ok) {
    const errorBody = (await response
      .json()
      .catch(() => null)) as ErrorResponse | null;
    const errorMessage =
      errorBody?.errors?.[0] ?? errorBody?.message ?? "Request failed.";
    throw new Error(errorMessage);
  }

  return (await response.json()) as T;
}
