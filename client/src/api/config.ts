const DEFAULT_API_BASE_URL = "https://localhost:7225";

export const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL?.trim() || DEFAULT_API_BASE_URL;
