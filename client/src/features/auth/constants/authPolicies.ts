const DEFAULT_TERMS_OF_SERVICE_URL = "https://www.tabo-ani.com/terms-of-service";
const DEFAULT_PRIVACY_POLICY_URL = "https://www.tabo-ani.com/privacy-policy";
const DEFAULT_TERMS_VERSION = "2026-04-05";
const DEFAULT_PRIVACY_VERSION = "2026-04-05";

export const TERMS_OF_SERVICE_URL =
  import.meta.env.VITE_TERMS_OF_SERVICE_URL?.trim() ||
  DEFAULT_TERMS_OF_SERVICE_URL;

export const PRIVACY_POLICY_URL =
  import.meta.env.VITE_PRIVACY_POLICY_URL?.trim() ||
  DEFAULT_PRIVACY_POLICY_URL;

export const CURRENT_TERMS_VERSION =
  import.meta.env.VITE_TERMS_VERSION?.trim() || DEFAULT_TERMS_VERSION;

export const CURRENT_PRIVACY_VERSION =
  import.meta.env.VITE_PRIVACY_VERSION?.trim() || DEFAULT_PRIVACY_VERSION;
