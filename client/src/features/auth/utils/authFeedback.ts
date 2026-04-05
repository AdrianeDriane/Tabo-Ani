import { isApiRequestError } from "../../../api/request";
import type {
  AuthIssue,
  CurrentSessionResponse,
  SessionRoleCode,
} from "../types/auth.types";

const AUTH_ISSUE_MESSAGES: Record<AuthIssue["code"], string> = {
  session_expired: "Your session has expired. Please log in again.",
  account_blocked:
    "Your account cannot access the platform right now. Please complete verification or contact the operations team.",
  network_unavailable:
    "We could not verify your session right now. Check your connection and try again.",
};

export function resolveCurrentSessionIssue(
  session: CurrentSessionResponse
): AuthIssue | null {
  switch (session.status) {
    case "SESSION_EXPIRED":
      return createAuthIssue("session_expired");
    case "ACCOUNT_BLOCKED":
      return createAuthIssue("account_blocked", session.accountStatus);
    default:
      return null;
  }
}

export function resolveAuthIssueFromApiError(error: unknown): AuthIssue {
  if (!isApiRequestError(error)) {
    return createAuthIssue("network_unavailable");
  }

  switch (error.code) {
    case "auth.invalid_refresh_token":
    case "auth.unauthorized":
    case "auth.not_authenticated":
      return createAuthIssue("session_expired");
    case "auth.account_status_not_allowed":
      return createAuthIssue("account_blocked");
    default:
      return createAuthIssue("network_unavailable");
  }
}

export function resolveLoginErrorMessage(error: unknown): string {
  if (!isApiRequestError(error)) {
    return AUTH_ISSUE_MESSAGES.network_unavailable;
  }

  switch (error.code) {
    case "auth.invalid_credentials":
      return "Incorrect email or password.";
    case "auth.account_status_not_allowed":
      return "Your account is not ready for login yet. Complete verification or wait for approval.";
    case "auth.invalid_origin":
      return "This sign-in request came from an unapproved origin. Refresh the page and try again.";
    default:
      return error.message || "We couldn't sign you in right now. Please try again.";
  }
}

export function resolveAuthIssueBanner(issue: AuthIssue | null) {
  return issue?.message ?? null;
}

export function hasAnyRole(roles: readonly string[] | null | undefined) {
  return Boolean(roles && roles.length > 0);
}

export function normalizeRoleCodes(roles: readonly string[] | null | undefined) {
  return (roles ?? []) as SessionRoleCode[];
}

function createAuthIssue(
  code: AuthIssue["code"],
  accountStatus?: string | null
): AuthIssue {
  return {
    code,
    message: AUTH_ISSUE_MESSAGES[code],
    accountStatus: accountStatus ?? null,
  };
}
