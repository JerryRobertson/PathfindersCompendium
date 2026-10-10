# Pathfinder's Compendium

A full-stack web app for building and managing **Pathfinder (1st Edition)** character sheets. Players sign in, create characters, and track everything from ability scores and saves to the feats and spells they've chosen, with the core math handled for them.

Built as my capstone project, using a React front end, an ASP.NET Core Web API, and SQL Server.

<!-- Add 2–3 screenshots here: the character sheet Main tab, the Spells tab, and the Search tab.
![Character sheet](docs/screenshots/character-sheet.png)
-->

## Features

- **Accounts and sign-in** with Firebase Authentication. Every character sheet belongs to its owner, and the API rejects edits to sheets you don't own.
- **Character sheets** you can create, edit, and delete: identity and description, level and experience, hit points, ability scores, armor class (normal, touch, flat-footed), saving throws, combat stats (BAB, CMB, CMD, melee, ranged), movement speeds, and the full skill list.
- **Derived stats calculated server-side**, such as ability modifiers, so the client stays simple and the numbers stay consistent.
- **Feats and spells per character.** Search the rules reference and add or remove feats and spells from a sheet.
- **Rules reference** of about 3,000 feats and 2,900 spells, seeded into SQL Server and searchable in the app.
- **Get Started and Information tabs** to help new players find their way around.

## Tech stack

| Layer | Technology |
|---|---|
| Front end | React 17, React Router, Reactstrap / Bootstrap 4, react-tabs, react-toastify |
| API | ASP.NET Core Web API (.NET 5), Swagger / OpenAPI |
| Data | SQL Server, Entity Framework Core 5 |
| Auth | Firebase Authentication on the client, JWT bearer validation on the API |

## Architecture

```
React client  ──fetch /api/*──▶  ASP.NET Core controllers
   │                                   │
Firebase Auth (ID token)          Repositories (EF Core, DI)
                                       │
                                   SQL Server
```

- **Controllers** expose REST endpoints for character sheets, feats, spells, classes, user profiles, and the sheet–feat / sheet–spell links.
- **Repositories** wrap EF Core behind interfaces (`ICharacterSheetRepository`, `ISpellRepository`, and so on) and are injected through ASP.NET Core dependency injection.
- **Authentication:** the client signs in with Firebase and sends the ID token as a bearer token. The API validates the token's issuer, audience, and lifetime against the Firebase project, then maps the Firebase user to a local `UserProfile`.

### Main endpoints

| Resource | Endpoints |
|---|---|
| `/api/CharacterSheet` | `GET` (current user's sheets), `POST`, `PUT /{id}`, `DELETE /{id}` |
| `/api/Feat`, `/api/Spell` | `GET`, `GET /{id}` |
| `/api/FeatSheet`, `/api/SpellSheet` | `GET /{sheetId}`, `POST`, `DELETE /{id}` |
| `/api/Classes` | `GET` |
| `/api/UserProfile` | `GET`, `GET /{firebaseUserId}`, `POST` |

With the API running in Development, Swagger UI is at `https://localhost:5001/swagger`.

## Running it locally

### Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Node.js](https://nodejs.org/) and npm
- SQL Server (SQL Server Express works)
- A Firebase project with Email/Password sign-in enabled

### 1. Database

1. Create a database named `Pathfinder` on `localhost\SQLExpress` (or change the connection string in `appsettings.json`).
2. Run the scripts in `PathfindersCompendium/SQL/` in this order:
   1. `Feats Setup.sql`
   2. `Spells Setup.sql`
   3. `Tables.sql` (creates the remaining tables and the foreign keys to `Feat` and `Spell`)

### 2. API

In `PathfindersCompendium/PathfindersCompendium/appsettings.Local.json`, set your Firebase project ID:

```json
{
  "FirebaseProjectId": "your-firebase-project-id"
}
```

Then run:

```bash
cd PathfindersCompendium/PathfindersCompendium
dotnet run
```

The API listens on `https://localhost:5001`.

### 3. Client

Create `PathfindersCompendium/PathfindersCompendium/client/.env`:

```
REACT_APP_API_KEY=your-firebase-web-api-key
```

Then run:

```bash
cd PathfindersCompendium/PathfindersCompendium/client
npm install
npm start
```

The client runs on `http://localhost:3000` and proxies `/api` calls to the API.

## What I learned

- Turning a dense tabletop rulebook into a relational data model, and loading thousands of reference records into it.
- Wiring third-party authentication (Firebase) into an ASP.NET Core API with JWT bearer validation.
- Structuring a .NET API with the repository pattern and dependency injection so controllers stay thin.
- Building a large, form-heavy React UI and keeping it in sync with the API.

## Possible next steps

- Calculate more derived stats (skills, AC, saves) on the server from class, level, and gear.
- Add automated tests for the repositories and controllers.
- Upgrade to a current LTS version of .NET and React.

## Author

**Jerry Robertson** · [GitHub](https://github.com/JerryRobertson) · [LinkedIn](https://www.linkedin.com/in/jrrobertson1)

---

*Pathfinder is a trademark of Paizo Inc. This is an unofficial fan project and is not affiliated with or endorsed by Paizo.*
