import {
  createAsyncThunk,
  createSlice,
  type PayloadAction,
} from "@reduxjs/toolkit";
import { getCurrentSession, login, logoutSession } from "./api/auth.api";
import type {
  AuthIssue,
  AuthState,
  CurrentSessionResponse,
  LoginRequest,
  SessionResponse,
} from "./types/auth.types";
import {
  resolveAuthIssueFromApiError,
  resolveCurrentSessionIssue,
  resolveLoginErrorMessage,
} from "./utils/authFeedback";

const initialState: AuthState = {
  bootstrapStatus: "pending",
  sessionStatus: "anonymous",
  accessToken: null,
  accessTokenExpiresAt: null,
  user: null,
  loginStatus: "idle",
  loginError: null,
  logoutStatus: "idle",
  logoutError: null,
  lastIssue: null,
};

export const bootstrapSession = createAsyncThunk<
  CurrentSessionResponse,
  void,
  { rejectValue: AuthIssue }
>(
  "auth/bootstrapSession",
  async (_, { rejectWithValue }) => {
    try {
      const response = await getCurrentSession();
      return response.data;
    } catch (error) {
      return rejectWithValue(resolveAuthIssueFromApiError(error));
    }
  }
);

export const loginUser = createAsyncThunk<
  SessionResponse,
  LoginRequest,
  { rejectValue: string }
>("auth/loginUser", async (payload, { rejectWithValue }) => {
  try {
    const response = await login(payload);
    return response.data;
  } catch (error) {
    return rejectWithValue(resolveLoginErrorMessage(error));
  }
});

export const logoutUser = createAsyncThunk<
  void,
  void,
  { rejectValue: string }
>("auth/logoutUser", async (_, { rejectWithValue }) => {
  try {
    await logoutSession();
  } catch {
    return rejectWithValue(
      "We couldn't log you out right now. Please try again."
    );
  }
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    sessionEstablished(state, action: PayloadAction<SessionResponse>) {
      hydrateSession(state, action.payload);
      state.bootstrapStatus = "ready";
      state.loginStatus = "idle";
      state.loginError = null;
      state.logoutStatus = "idle";
      state.logoutError = null;
      state.lastIssue = null;
    },
    sessionInvalidated(state, action: PayloadAction<AuthIssue | null>) {
      clearSessionState(state);
      state.bootstrapStatus = "ready";
      state.logoutStatus = "idle";
      state.logoutError = null;
      state.lastIssue = action.payload;
    },
    sessionIssueSet(state, action: PayloadAction<AuthIssue | null>) {
      state.lastIssue = action.payload;
    },
    dismissAuthIssue(state) {
      state.lastIssue = null;
    },
    clearLogoutError(state) {
      state.logoutError = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(bootstrapSession.pending, (state) => {
        state.bootstrapStatus = "pending";
      })
      .addCase(bootstrapSession.fulfilled, (state, action) => {
        state.bootstrapStatus = "ready";

        if (action.payload.status === "AUTHENTICATED" && action.payload.session) {
          hydrateSession(state, action.payload.session);
          state.lastIssue = null;
          return;
        }

        clearSessionState(state);
        state.lastIssue = resolveCurrentSessionIssue(action.payload);
      })
      .addCase(bootstrapSession.rejected, (state, action) => {
        state.bootstrapStatus = "ready";
        if (state.sessionStatus !== "authenticated") {
          clearSessionState(state);
        }

        state.lastIssue = action.payload ?? resolveAuthIssueFromApiError(null);
      })
      .addCase(loginUser.pending, (state) => {
        state.loginStatus = "pending";
        state.loginError = null;
        state.lastIssue = null;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        hydrateSession(state, action.payload);
        state.bootstrapStatus = "ready";
        state.loginStatus = "idle";
        state.loginError = null;
        state.logoutError = null;
        state.lastIssue = null;
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loginStatus = "idle";
        state.loginError = action.payload ?? "Unable to log in right now.";
      })
      .addCase(logoutUser.pending, (state) => {
        state.logoutStatus = "pending";
        state.logoutError = null;
      })
      .addCase(logoutUser.fulfilled, (state) => {
        clearSessionState(state);
        state.bootstrapStatus = "ready";
        state.logoutStatus = "idle";
        state.logoutError = null;
        state.lastIssue = null;
      })
      .addCase(logoutUser.rejected, (state, action) => {
        state.logoutStatus = "idle";
        state.logoutError =
          action.payload ?? "We couldn't log you out right now.";
        state.lastIssue = resolveAuthIssueFromApiError(null);
      });
  },
});

function hydrateSession(state: AuthState, session: SessionResponse) {
  state.sessionStatus = "authenticated";
  state.accessToken = session.accessToken;
  state.accessTokenExpiresAt = session.accessTokenExpiresAt;
  state.user = session.user;
}

function clearSessionState(state: AuthState) {
  state.sessionStatus = "anonymous";
  state.accessToken = null;
  state.accessTokenExpiresAt = null;
  state.user = null;
  state.loginStatus = "idle";
  state.loginError = null;
}

export const {
  clearLogoutError,
  dismissAuthIssue,
  sessionEstablished,
  sessionInvalidated,
  sessionIssueSet,
} = authSlice.actions;
export const authReducer = authSlice.reducer;
