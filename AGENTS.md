# Repository Guidelines

## Project Structure & Module Organization

This repository is split into two apps:

- `client/`: React 19 + TypeScript + Vite frontend. Main entry points are `src/main.tsx`, `src/App.tsx`, and `src/store.ts`. Static assets live in `src/assets/` and `public/`.
- `server/TaboAni.Api/`: ASP.NET Core Web API targeting `.NET 10`. API endpoints live in `Controllers/`, EF Core data access in `Data/`, entities in `Models/`, and schema history in `Migrations/`.

Do not commit generated output from `client/dist/`, `client/node_modules/`, `server/TaboAni.Api/bin/`, or `server/TaboAni.Api/obj/`.

## Build, Test, and Development Commands

Frontend commands, run from `client/`:

- `npm install`: install dependencies.
- `npm run dev`: start the Vite dev server on `http://localhost:5173`.
- `npm run build`: run TypeScript build checks and produce a production bundle.
- `npm run lint`: run ESLint across `*.ts` and `*.tsx`.

Backend commands, run from `server/TaboAni.Api/`:

- `dotnet restore`: restore NuGet packages.
- `dotnet run`: start the API on `https://localhost:7225` and `http://localhost:5091`.
- `dotnet build`: compile the API.
- `dotnet ef database update`: apply EF Core migrations to the configured PostgreSQL database.

## Coding Style & Naming Conventions

Use 2 spaces in frontend files and 4 spaces in C# files. Follow the existing style in each app: React components and types use `PascalCase`, hooks and variables use `camelCase`, and API controllers, models, and DTOs use `PascalCase`.

Prefer small components, explicit TypeScript types for API payloads, and async EF Core queries. Keep controller routes under `api/[ControllerName]`. Use ESLint in `client/eslint.config.js` before opening a PR.

## Testing Guidelines

There are currently no dedicated test projects in the repository. At minimum, run `npm run lint`, `npm run build`, and `dotnet build` before submitting changes. When adding tests, place frontend tests beside the feature or under `client/src/__tests__/`, and add backend tests in a separate `server/tests/` project.

## Commit & Pull Request Guidelines

Git history is not available in this workspace, so use short, imperative commit messages such as `client: add produce listing form validation` or `server: add listing create endpoint`. Keep commits focused on one concern.

Pull requests should include a brief summary, affected areas (`client` or `server`), setup or migration notes, and screenshots for UI changes. If a change affects the database, mention the migration file and required local update command.

## Security & Configuration Tips

Keep secrets and connection strings out of source control. Store the PostgreSQL connection string in local configuration, and make sure frontend API URLs match the backend launch settings and CORS policy in `Program.cs`.

