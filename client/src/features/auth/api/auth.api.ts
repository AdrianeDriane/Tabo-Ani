import type {
  EmailVerificationStatusResponse,
  ResendEmailVerificationResponse,
  SignupFormState,
  SignupResponse,
  VerifyEmailRequest,
} from "../types/signup.types";
import type {
  CurrentSessionResponse,
  LoginRequest,
  LogoutResponse,
  SessionResponse,
} from "../types/auth.types";
import type { ApiResponse } from "./authApi.types";
import {
  CURRENT_PRIVACY_VERSION,
  CURRENT_TERMS_VERSION,
} from "../constants/authPolicies";
import { requestJson } from "../../../api/request";

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
  return requestJson<ApiResponse<SignupResponse>>("/api/v1/auth/signup", {
    method: "POST",
    payload,
  });
}

export async function resendVerification(
  email: string
): Promise<ApiResponse<ResendEmailVerificationResponse>> {
  return requestJson<ApiResponse<ResendEmailVerificationResponse>>(
    "/api/v1/auth/verify-email/resend",
    {
      method: "POST",
      payload: {
        email,
      },
    }
  );
}

export async function verifyEmail(
  payload: VerifyEmailRequest
): Promise<ApiResponse<EmailVerificationStatusResponse>> {
  return requestJson<ApiResponse<EmailVerificationStatusResponse>>(
    "/api/v1/auth/verify-email",
    {
      method: "POST",
      payload,
    }
  );
}

export async function login(
  payload: LoginRequest
): Promise<ApiResponse<SessionResponse>> {
  return requestJson<ApiResponse<SessionResponse>>("/api/v1/auth/login", {
    method: "POST",
    payload,
    credentials: "include",
  });
}

export async function refreshSession(): Promise<ApiResponse<SessionResponse>> {
  return requestJson<ApiResponse<SessionResponse>>("/api/v1/auth/refresh", {
    method: "POST",
    credentials: "include",
  });
}

export async function getCurrentSession(): Promise<
  ApiResponse<CurrentSessionResponse>
> {
  return requestJson<ApiResponse<CurrentSessionResponse>>(
    "/api/v1/auth/session",
    {
      method: "GET",
      credentials: "include",
    }
  );
}

export async function logoutSession(): Promise<ApiResponse<LogoutResponse>> {
  return requestJson<ApiResponse<LogoutResponse>>("/api/v1/auth/logout", {
    method: "POST",
    credentials: "include",
  });
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
