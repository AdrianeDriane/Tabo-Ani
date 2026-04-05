import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { login, logoutSession, refreshSession } from "./api/auth.api";
import type {
  AuthState,
  LoginRequest,
  SessionResponse,
} from "./types/auth.types";

const initialState: AuthState = {
  bootstrapStatus: "pending",
  sessionStatus: "anonymous",
  accessToken: null,
  accessTokenExpiresAt: null,
  user: null,
  loginStatus: "idle",
  loginError: null,
};

export const bootstrapSession = createAsyncThunk<SessionResponse>(
  "auth/bootstrapSession",
  async () => {
    const response = await refreshSession();
    return response.data;
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
    return rejectWithValue(
      error instanceof Error ? error.message : "Unable to log in right now."
    );
  }
});

export const logoutUser = createAsyncThunk("auth/logoutUser", async () => {
  await logoutSession();
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(bootstrapSession.pending, (state) => {
        state.bootstrapStatus = "pending";
      })
      .addCase(bootstrapSession.fulfilled, (state, action) => {
        hydrateSession(state, action.payload);
        state.bootstrapStatus = "ready";
      })
      .addCase(bootstrapSession.rejected, (state) => {
        clearSession(state);
        state.bootstrapStatus = "ready";
      })
      .addCase(loginUser.pending, (state) => {
        state.loginStatus = "pending";
        state.loginError = null;
      })
      .addCase(loginUser.fulfilled, (state, action) => {
        hydrateSession(state, action.payload);
        state.bootstrapStatus = "ready";
        state.loginStatus = "idle";
        state.loginError = null;
      })
      .addCase(loginUser.rejected, (state, action) => {
        state.loginStatus = "idle";
        state.loginError = action.payload ?? "Unable to log in right now.";
      })
      .addCase(logoutUser.fulfilled, (state) => {
        clearSession(state);
        state.bootstrapStatus = "ready";
      })
      .addCase(logoutUser.rejected, (state) => {
        clearSession(state);
        state.bootstrapStatus = "ready";
      });
  },
});

function hydrateSession(state: AuthState, session: SessionResponse) {
  state.sessionStatus = "authenticated";
  state.accessToken = session.accessToken;
  state.accessTokenExpiresAt = session.accessTokenExpiresAt;
  state.user = session.user;
}

function clearSession(state: AuthState) {
  state.sessionStatus = "anonymous";
  state.accessToken = null;
  state.accessTokenExpiresAt = null;
  state.user = null;
  state.loginStatus = "idle";
  state.loginError = null;
}

export const authReducer = authSlice.reducer;
