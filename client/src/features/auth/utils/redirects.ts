import type { SessionRoleCode } from "../types/auth.types";

const ROLE_ROUTE_PRIORITY: Record<SessionRoleCode, string> = {
  ADMIN: "/admin",
  FARMER: "/farmer/dashboard",
  BUYER: "/buyers/dashboard",
  DISTRIBUTOR: "/distributor/dashboard",
};

const ROLE_PRIORITY: SessionRoleCode[] = [
  "ADMIN",
  "FARMER",
  "BUYER",
  "DISTRIBUTOR",
];

const ROLE_ROUTE_PREFIXES: Record<SessionRoleCode, readonly string[]> = {
  ADMIN: ["/admin"],
  FARMER: ["/farmer"],
  BUYER: ["/buyers", "/checkout", "/orders"],
  DISTRIBUTOR: ["/distributor"],
};

const AUTH_ONLY_ROUTE_PREFIXES = ["/auth-check"];

export function resolvePostLoginPath(
  roles: readonly string[] | null | undefined
) {
  for (const role of ROLE_PRIORITY) {
    if (roles?.includes(role)) {
      return ROLE_ROUTE_PRIORITY[role];
    }
  }

  return "/access-pending";
}

export function resolveLoginDestination(
  roles: readonly string[] | null | undefined,
  returnTo: string | null | undefined
) {
  if (returnTo && isPathAllowedForRoles(returnTo, roles)) {
    return returnTo;
  }

  return resolvePostLoginPath(roles);
}

export function resolveRoleMismatchPath(
  roles: readonly string[] | null | undefined
) {
  return roles?.length ? "/access-denied" : "/access-pending";
}

function isPathAllowedForRoles(
  path: string,
  roles: readonly string[] | null | undefined
) {
  const pathname = normalizeInternalPath(path);

  if (AUTH_ONLY_ROUTE_PREFIXES.some((prefix) => pathname.startsWith(prefix))) {
    return Boolean(roles?.length);
  }

  for (const role of ROLE_PRIORITY) {
    if (!roles?.includes(role)) {
      continue;
    }

    if (
      ROLE_ROUTE_PREFIXES[role].some((prefix) => pathname.startsWith(prefix))
    ) {
      return true;
    }
  }

  return false;
}

function normalizeInternalPath(path: string) {
  return new URL(path, "https://taboani.local").pathname;
}
