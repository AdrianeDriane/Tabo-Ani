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

export function resolvePostLoginPath(roles: readonly string[] | null | undefined) {
  for (const role of ROLE_PRIORITY) {
    if (roles?.includes(role)) {
      return ROLE_ROUTE_PRIORITY[role];
    }
  }

  return "/";
}
