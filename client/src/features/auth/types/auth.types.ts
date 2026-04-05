export type SessionRoleCode =
  | "ADMIN"
  | "BUYER"
  | "FARMER"
  | "DISTRIBUTOR";

export type AuthIssueCode =
  | "session_expired"
  | "account_blocked"
  | "network_unavailable";

export type AuthIssue = {
  code: AuthIssueCode;
  message: string;
  accountStatus?: string | null;
};

export type SessionUser = {
  userId: string;
  email: string;
  mobileNumber: string | null;
  firstName: string;
  lastName: string;
  displayName: string | null;
  isEmailVerified: boolean;
  accountStatus: string;
  lastLoginAt: string | null;
  roles: SessionRoleCode[];
};

export type SessionResponse = {
  accessToken: string;
  accessTokenExpiresAt: string;
  user: SessionUser;
};

export type CurrentSessionStatus =
  | "AUTHENTICATED"
  | "ANONYMOUS"
  | "SESSION_EXPIRED"
  | "ACCOUNT_BLOCKED";

export type CurrentSessionResponse = {
  status: CurrentSessionStatus;
  session: SessionResponse | null;
  accountStatus: string | null;
};

export type LoginRequest = {
  email: string;
  password: string;
  rememberMe: boolean;
};

export type LogoutResponse = {
  status: "LOGGED_OUT";
};

export type AuthState = {
  bootstrapStatus: "pending" | "ready";
  sessionStatus: "authenticated" | "anonymous";
  accessToken: string | null;
  accessTokenExpiresAt: string | null;
  user: SessionUser | null;
  loginStatus: "idle" | "pending";
  loginError: string | null;
  logoutStatus: "idle" | "pending";
  logoutError: string | null;
  lastIssue: AuthIssue | null;
};
