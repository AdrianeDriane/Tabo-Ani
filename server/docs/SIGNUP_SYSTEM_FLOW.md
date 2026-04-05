# Sign Up System Flow

## Purpose

This document describes the current sign up flow implemented across the React frontend in `client/` and the ASP.NET Core backend in `server/TaboAni.Api/`.

It covers:

- user-facing sign up flow
- email verification flow
- resend and recovery behavior
- backend state transitions
- technical request and persistence flow

## User Flow

1. The user opens the sign up page in the client app.
2. The user completes the multi-step sign up form:
   - account credentials
   - profile details
   - requested role applications
   - policy acceptance and final review
3. The frontend submits the sign up payload to `POST /api/v1/auth/signup`.
4. If the request succeeds, the UI does not reveal any verification token.
5. The user sees a "check your email" state and can request a resend from the frontend.
6. The backend sends a verification email containing a frontend link such as:
   - `/verify-email?token=...`
7. The user opens the verification link from email.
8. The frontend verify page calls `POST /api/v1/auth/verify-email` with the token.
9. If verification succeeds, the user is redirected to `/login?emailVerified=1`.
10. If the link is invalid, expired, or replaced, the verify page offers a resend flow.

## Backend Account Lifecycle

### During Sign Up

- A `User` record is created with:
  - `IsEmailVerified = false`
  - `AccountStatus = PENDING_EMAIL_VERIFICATION`
- Requested role applications create downstream entities such as:
  - `BuyerProfile`
  - `FarmerProfile`
  - `KycApplication`
- New KYC applications begin in:
  - `PENDING_EMAIL_VERIFICATION`
- Terms and privacy acceptance are stored in:
  - `user_policy_acceptances`
- A new email verification token is created:
  - raw token is generated in memory
  - hashed token is stored in `email_verification_tokens`
  - token expiry is stored
  - raw token is only used to build the verification link

### During Resend

- The resend endpoint accepts an email address.
- The backend returns a generic accepted response to avoid account enumeration.
- If the account is eligible for resend:
  - previous pending tokens are invalidated
  - a fresh token is created
  - a new verification email is sent
- Cooldown protection prevents repeated rapid resend attempts.

### During Verification

- The verification endpoint accepts only the raw token.
- The backend hashes the token and looks up the stored hash.
- Verification outcomes:
  - `VERIFIED`
  - `ALREADY_VERIFIED`
  - `INVALID_OR_EXPIRED`
- On successful verification:
  - `IsEmailVerified` becomes `true`
  - `AccountStatus` becomes `ACTIVE`
  - the matched token is marked consumed
  - other pending tokens are invalidated
  - related KYC applications move from `PENDING_EMAIL_VERIFICATION` to `PENDING_REVIEW`

## Technical Flow

## Frontend

### Sign Up

- The sign up UI collects:
  - credentials
  - optional mobile number
  - basic profile details
  - buyer and/or farmer application data
  - terms and privacy acceptance
- The frontend builds a request payload and sends it to:
  - `POST /api/v1/auth/signup`
- The frontend no longer previews or stores the verification token in the UI.

### Verification

- The router exposes a dedicated route:
  - `/verify-email`
- The verification page reads the `token` query parameter.
- On page load, it calls the verify API once.
- Success states redirect to login after a short delay.
- Invalid or expired states expose a resend form.

### Login Notice

- The login page reads:
  - `emailVerified=1`
- It shows a one-time success banner after verification redirect.

## Backend

### Request Validation

- The backend validates:
  - email format
  - password rules
  - confirm password match
  - allowed role applications
  - policy acceptance flags
  - policy version values

### Transactions

- Sign up work is executed in an explicit transaction.
- The same applies to resend and verification state changes.
- If email delivery fails during sign up or resend, the transaction rolls back.

### Email Delivery

- The backend uses `IEmailVerificationNotifier` as the mail abstraction.
- The current notifier sends mail through Gmail SMTP using configured credentials.
- The email contains:
  - a plain-text body
  - an HTML body
  - the verification link pointing to the frontend app

### Persistence

- Key tables involved:
  - `users`
  - `buyer_profiles`
  - `farmer_profiles`
  - `kyc_applications`
  - `email_verification_tokens`
  - `user_policy_acceptances`

## Operational Notes

- The frontend base URL must be correct or the email link will point to the wrong environment.
- The database schema must include the latest migration that adds:
  - `email_verification_tokens.invalidated_at`
  - `user_policy_acceptances`
- Gmail SMTP requires:
  - a Gmail account
  - 2-Step Verification
  - a Google App Password
- Rate limiting is enabled for:
  - signup
  - resend
  - verify email

## Failure Modes

- Missing DB schema update:
  - sign up fails during persistence before any email is sent
- Invalid SMTP configuration:
  - sign up or resend fails when the notifier attempts delivery
- Invalid or expired token:
  - verify endpoint returns invalid or expired status
- Reused token after successful verification:
  - verify endpoint returns already verified status when appropriate
