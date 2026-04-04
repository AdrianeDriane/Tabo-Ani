---
name: backend-feature
description: Use when the task requires implementing or modifying ASP.NET Core Web API backend functionality in `server/TaboAni.Api/`. This includes creating or updating API routes, controllers, services, DTOs, models, or database interactions. Follow existing backend architecture patterns and ensure consistency with current conventions.
---

# Purpose

Use this skill when working on backend functionality in `server/TaboAni.Api/`.

This includes:

- new API routes
- new controllers/services
- modifying existing backend logic
- updating request/response behavior

Do not use this skill for:

- frontend-only changes
- architectural discussion without implementation
- prompt structuring
- git commit planning
- general Q&A

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
