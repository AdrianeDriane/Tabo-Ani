import {
  sessionEstablished,
  sessionIssueSet,
  sessionInvalidated,
} from "../features/auth/authSlice";
import type { SessionResponse } from "../features/auth/types/auth.types";
import { resolveAuthIssueFromApiError } from "../features/auth/utils/authFeedback";
import { store } from "../store";
import { ApiRequestError, isApiRequestError, requestJson } from "./request";
import type { RequestJsonOptions } from "./request";
import type { ApiResponse } from "../types/api.types";

let refreshPromise: Promise<boolean> | null = null;

export async function requestAuthenticatedJson<T>(
  path: string,
  options: RequestJsonOptions
): Promise<T> {
  const initialAccessToken = store.getState().auth.accessToken;
  if (!initialAccessToken) {
    throw new ApiRequestError(401, "Authentication failed.", [
      {
        code: "auth.not_authenticated",
        message: "You need to log in to continue.",
        raw: "auth.not_authenticated: You need to log in to continue.",
      },
    ]);
  }

  try {
    return await sendAuthenticatedRequest<T>(path, options, initialAccessToken);
  } catch (error) {
    if (!shouldAttemptRefresh(error)) {
      throw error;
    }

    const refreshed = await ensureFreshSession();
    if (!refreshed) {
      throw error;
    }

    const nextAccessToken = store.getState().auth.accessToken;
    if (!nextAccessToken) {
      throw error;
    }

    return sendAuthenticatedRequest<T>(path, options, nextAccessToken);
  }
}

async function sendAuthenticatedRequest<T>(
  path: string,
  options: RequestJsonOptions,
  accessToken: string
): Promise<T> {
  const headers = new Headers(options.headers ?? undefined);
  headers.set("Authorization", `Bearer ${accessToken}`);

  return requestJson<T>(path, {
    ...options,
    headers,
  });
}

function shouldAttemptRefresh(error: unknown) {
  return isApiRequestError(error) && error.status === 401;
}

async function ensureFreshSession() {
  if (!refreshPromise) {
    refreshPromise = refreshAccessToken();
  }

  try {
    return await refreshPromise;
  } finally {
    refreshPromise = null;
  }
}

async function refreshAccessToken() {
  try {
    const response = await requestJson<ApiResponse<SessionResponse>>(
      "/api/v1/auth/refresh",
      {
        method: "POST",
        credentials: "include",
      }
    );
    store.dispatch(sessionEstablished(response.data));
    return true;
  } catch (error) {
    const issue = resolveAuthIssueFromApiError(error);
    if (issue.code === "network_unavailable") {
      store.dispatch(sessionIssueSet(issue));
      return false;
    }

    store.dispatch(sessionInvalidated(issue));
    return false;
  }
}
