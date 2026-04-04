# AGENTS.md

## Project Structure & Module Organization

This repository is split into two apps:

- `client/`: React 19 + TypeScript + Vite frontend. Main entry points are `src/main.tsx`, `src/App.tsx`, and `src/store.ts`. Static assets live in `src/assets/` and `public/`.
- `server/TaboAni.Api/`: ASP.NET Core Web API targeting `.NET 10`. API endpoints live in `Controllers/`, EF Core data access in `Data/`, entities in `Models/`, and schema history in `Migrations/`.

Do not commit generated output from `client/dist/`, `client/node_modules/`, `server/TaboAni.Api/bin/`, or `server/TaboAni.Api/obj/`.

## Working Style

- Make minimal, high-confidence changes. Do not introduce large refactors unless explicitly requested.
- Preserve existing UI/UX unless the task clearly requires changes.
- Prioritize clarity, maintainability, and consistency with the current codebase over “clever” solutions.
- Follow existing patterns in both frontend (React + TS) and backend (ASP.NET Core + EF Core) before introducing new approaches.
- Avoid unnecessary abstractions. Prefer simple, readable implementations that match current project style.
- When uncertain, inspect surrounding files and reuse existing patterns instead of guessing.
- Do not assume missing requirements — surface assumptions clearly in the final response.
- If the task is blocked by missing credentials, dashboard-only values, external service configuration, environment variables, secrets, third-party account access, local machine setup, or any other required manual step, do not invent code-based workarounds just to avoid asking.
- In those cases, explicitly stop and tell the user exactly what manual action is needed.
- Prefer the most practical path to completion, even when that means asking the user to retrieve or configure something manually.
- When requesting manual intervention, be specific:
  - explain what is needed
  - explain where to get it
  - explain why it is needed
  - give exact step-by-step instructions when helpful
- Do not replace a required manual setup step with speculative or impractical implementation changes.

## Architecture

### General

- Respect the separation between `client/` and `server/`. Do not mix concerns.
- Prefer incremental, additive changes over rewriting existing logic.

### Frontend (React + TypeScript + Vite)

- Keep components focused and reusable.
- Do not introduce global state changes unless necessary.
- Follow existing state management patterns (Redux or current store setup).
- Avoid breaking component props/contracts.
- Keep UI logic separate from data-fetching logic where possible.
- Maintain TypeScript type safety — do not introduce `any`.
- Make sure to implement DTOs and interfaces as much as possible
- Have proper UI toast error handling.
- Handle consistent success and error json bodies based from backend structure.

### Backend (ASP.NET Core Web API .NET 10)

- Keep controllers thin — business logic should not live in controllers.
- Place data access in appropriate services or data layers (not directly in controllers).
- Follow existing EF Core usage patterns for queries, relationships, and migrations.
- Avoid breaking existing API contracts unless explicitly required.
- Ensure DTOs, models, and database mappings remain consistent.
- Be careful with LINQ queries — ensure correctness and performance.
- Avoid n + 1 database concern.
- Know when to use eager vs lazy loading.
- Have proper error handling and return consistent success and error json bodies.
- Follow the existing Unit of Work pattern for coordinating database operations across repositories or services.
- Keep transaction boundaries explicit and consistent, especially for multi-entity writes.
- Do not bypass the Unit of Work pattern with scattered direct persistence calls unless the existing project pattern clearly does so.

## Manual Intervention Rules

- Recognize when a task cannot be completed correctly without user action.
- Examples include:
  - retrieving connection strings, API keys, secrets, or project IDs from dashboards
  - configuring third-party services
  - updating local environment variables or machine-specific settings
  - running commands that require user-owned access, authentication, approvals, or devices
  - verifying behavior that depends on external systems not available to the agent

- When such a blocker exists:
  1. Do not guess.
  2. Do not create workaround code unless the user explicitly asked for an alternative approach.
  3. Tell the user exactly what needs to be done manually.
  4. Keep the instructions concrete and minimal.
  5. Resume implementation only after the required manual dependency is satisfied.

- If partial progress is still possible, complete the safe code changes first, then clearly separate:
  - what was completed
  - what still requires manual action from the user

## File Reference Style

- Always reference files using repo-relative paths, not absolute local machine paths.
- Paths should start from the repository root.

Examples:

- Use: `client/src/features/auth/components/signup/SignupFlow.tsx:258`
- Use: `server/TaboAni.Api/Application/Implementations/Service/AuthService.cs:101`
- Do not use: `C:/Users/.../Tabo-Ani/client/src/...`

- If mentioning the repository root is useful for clarity, refer to it as `Tabo-Ani` in prose, but do not prepend `Tabo-Ani/` to every file path unless explicitly needed.
- Keep file references short, readable, and easy to scan.

## Safety

- Only modify files directly related to the task.
- Do not rename, move, or delete files unless explicitly required.
- Do not introduce breaking changes to APIs, schemas, or frontend contracts without clear instruction.
- Avoid touching authentication, critical business logic, or database schema unless the task explicitly requires it.
- Do not introduce new dependencies unless necessary and justified.
- Prefer reversible changes (easy to rollback via Git).
- If a change has potential side effects, explicitly call it out.

## Validation

- Ensure logic correctness before focusing on optimization.
- Validate both success and failure cases where applicable.
- For backend:
  - Ensure endpoints handle edge cases (nulls, invalid input, empty results).
  - Ensure queries return expected data shapes.
- For frontend:
  - Ensure UI does not break existing layouts or flows.
  - Ensure data is correctly rendered and handled.
- If full validation (build/test/run) cannot be executed, clearly state what was not verified.
- Prefer predictable, testable behavior over assumptions.

## Interaction Rules

- Do not assume every request is an implementation task.
- If the user is asking a question, giving feedback, requesting review, or asking for planning help, answer directly without pretending code changes were made.
- Only use implementation-oriented response structure when files or code were actually modified.
- Prefer the narrowest applicable behavior for the current request.

## Final Response

When actual code, configuration, or file changes are made, include:

1. **Summary of Changes**
   - What was implemented or modified and why.

2. **Files Changed**
   - List of files touched with brief description per file.

3. **How It Works**
   - Brief explanation of the implementation.

4. **Validation**
   - What was checked or verified.
   - What still needs manual testing (if any).

5. **Assumptions / Risks**
   - Any assumptions made due to missing context.
   - Any potential side effects or edge cases.

6. **Manual Steps Required** (only if applicable)
   - List any required user actions that the agent could not perform directly.
   - Provide exact, practical instructions.
   - Do not hide required manual intervention behind speculative workaround suggestions.

For non-implementation requests such as:

- answering questions
- explaining concepts
- reviewing architecture
- planning
- prompt/task structuring
- discussing options

do **not** force the implementation response format. Respond in the format most appropriate to the user's request.

Keep explanations concise, practical, and focused on helping the developer quickly verify and move forward.
Avoid unnecessary verbosity or theoretical explanations.
