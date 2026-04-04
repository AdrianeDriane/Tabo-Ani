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

## Final Response

Always include:

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

Keep explanations concise, practical, and focused on helping the developer quickly verify and move forward.
Avoid unnecessary verbosity or theoretical explanations.
