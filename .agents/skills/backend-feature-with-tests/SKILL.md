---
name: backend-feature-with-tests
description: Implement or modify ASP.NET Core Web API backend functionality and always create or update matching unit tests and integration tests.
---

# Purpose

Use this skill when working on backend functionality in `server/TaboAni.Api/`.

This includes:

- new API routes
- new controllers/services
- modifying existing backend logic
- updating request/response behavior

Always ensure unit tests and integration tests are created or updated.

# Workflow

1. Inspect existing backend architecture.
   - Review controllers, services, DTOs, models, Unit of Work, and test patterns.
   - Follow existing naming and folder conventions.

2. Scope the change.
   - Identify required endpoints, services, DTOs, or persistence changes.
   - Keep changes minimal and focused.

3. Implement backend functionality.
   - Follow the existing backend architecture patterns (controllers, services, EF Core, Unit of Work).

4. Update or create unit tests.
   - Cover success, failure, and key edge cases.
   - Follow existing test structure and conventions.

5. Update or create integration tests.
   - Verify endpoint behavior, status codes, and response structure.
   - Cover representative success and failure scenarios.

6. Validate consistency.
   - Ensure DTOs, models, and contracts remain aligned.
   - Ensure tests match the final implementation.

7. Summarize clearly.
   - Backend files changed
   - Test files created/updated
   - What was implemented
   - What was validated / needs manual testing

# Constraints

- Do not bypass existing repository or Unit of Work patterns.
- Do not skip test creation or updates unless explicitly instructed.
- Extend existing test infrastructure instead of creating new patterns.
- If tests cannot be executed, clearly state it in the final response.

# Final Response Format

1. Summary of backend changes
2. Files changed
3. Unit tests created or updated
4. Integration tests created or updated
5. Validation performed
6. Risks, assumptions, or manual checks needed
