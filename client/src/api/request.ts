import { API_BASE_URL } from "./config";
import type { ErrorResponse } from "../types/api.types";

export type RequestJsonOptions = {
  method: "GET" | "POST" | "PATCH" | "PUT" | "DELETE";
  payload?: unknown;
  credentials?: RequestCredentials;
  headers?: HeadersInit;
  signal?: AbortSignal;
};

export type ApiErrorDetail = {
  code: string | null;
  message: string;
  raw: string;
};

export class ApiRequestError extends Error {
  readonly status: number;
  readonly responseMessage: string;
  readonly errors: ApiErrorDetail[];
  readonly code: string | null;

  constructor(status: number, responseMessage: string, errors: ApiErrorDetail[]) {
    super(errors[0]?.message ?? responseMessage ?? "Request failed.");
    this.name = "ApiRequestError";
    this.status = status;
    this.responseMessage = responseMessage;
    this.errors = errors;
    this.code = errors[0]?.code ?? null;
  }
}

export function isApiRequestError(error: unknown): error is ApiRequestError {
  return error instanceof ApiRequestError;
}

export async function requestJson<T>(
  path: string,
  options: RequestJsonOptions
): Promise<T> {
  const headers = new Headers(options.headers ?? undefined);

  if (options.payload !== undefined && !headers.has("Content-Type")) {
    headers.set("Content-Type", "application/json");
  }

  const response = await fetch(`${API_BASE_URL}${path}`, {
    method: options.method,
    headers,
    credentials: options.credentials,
    body:
      options.payload !== undefined ? JSON.stringify(options.payload) : undefined,
    signal: options.signal,
  });

  if (!response.ok) {
    const errorBody = (await response
      .json()
      .catch(() => null)) as ErrorResponse | null;
    const errorDetails = (errorBody?.errors ?? []).map(parseApiErrorDetail);

    throw new ApiRequestError(
      response.status,
      errorBody?.message ?? "Request failed.",
      errorDetails.length > 0
        ? errorDetails
        : [{ code: null, message: errorBody?.message ?? "Request failed.", raw: "" }]
    );
  }

  return (await response.json()) as T;
}

function parseApiErrorDetail(raw: string): ApiErrorDetail {
  const separatorIndex = raw.indexOf(":");
  if (separatorIndex <= 0) {
    return {
      code: null,
      message: raw.trim(),
      raw,
    };
  }

  return {
    code: raw.slice(0, separatorIndex).trim() || null,
    message: raw.slice(separatorIndex + 1).trim(),
    raw,
  };
}
