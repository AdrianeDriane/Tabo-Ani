import type { RoleLocationFormState } from "../types/signup.types";

export function composeLocationText(location: RoleLocationFormState): string {
  return [location.city.trim(), location.province.trim(), location.region.trim()]
    .filter(Boolean)
    .join(", ");
}
