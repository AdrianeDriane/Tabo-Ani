---
name: backend-feature
description: Implement or modify ASP.NET Core Web API backend functionality.
---

# Purpose

Use this skill when working on backend functionality in `server/TaboAni.Api/`.

This includes:

- new API routes
- new controllers/services
- modifying existing backend logic
- updating request/response behavior

# Workflow

1. Inspect existing backend architecture.
   - Review controllers, services, DTOs, models, Unit of Work.
   - Follow existing naming and folder conventions.

2. Scope the change.
   - Identify required endpoints, services, DTOs, or persistence changes.
   - Keep changes minimal and focused.

3. Implement backend functionality.
   - Follow the existing backend architecture patterns (controllers, services, EF Core, Unit of Work).

4. Validate consistency.
   - Ensure DTOs, models, and contracts remain aligned.

5. Summarize clearly.
   - Backend files changed
   - What was implemented
   - What was validated / needs manual testing

# Constraints

- Do not bypass existing repository or Unit of Work patterns.
- Maintain consistency with existing architecture and conventions.

# Final Response Format

1. Summary of backend changes
2. Files changed
3. Validation performed
4. Risks, assumptions, or manual checks needed
