# AGENTS

## Aperçu du dépôt
- Solution mono-repo combinant une API ASP.NET Core et un front-end React (Vite + MUI).
- Le code .NET est regroupé sous `backend/` avec trois projets : `MamConnect.Api`, `MamConnect.Domain` et `MamConnect.Infrastructure` (fichiers projet dans la solution `MamConnect.sln`).
- Le front-end réside dans `frontend/` (application Vite configurée via `package.json` et `vite.config.js`).

## Détails backend (Program.cs, AppDbContext, entités, contrôleurs)
- `backend/MamConnect.Api/Program.cs` configure les services : contrôleurs JSON, `AppDbContext` sur SQL Server avec stratégie de retry, authentification JWT, autorisation et CORS pour `http://localhost:5173`, ainsi que Swagger.
- `backend/MamConnect.Infrastructure/Data/AppDbContext.cs` expose `DbSet<Child>`, `DbSet<DailyReport>` et `DbSet<User>`, définit les relations (assistants, parents/enfants) et pré-remplit des utilisateurs, enfants et rapports pour le développement.
- Les entités domaine se trouvent dans `backend/MamConnect.Domain/Entities/` (`Child`, `DailyReport`, `User` et l’énumération `UserRole`).
- Les contrôleurs API (`backend/MamConnect.Api/Controllers/`) couvrent l’authentification, la gestion des enfants, des rapports quotidiens, des assistants et des parents.

## Détails frontend (main.jsx, App.jsx, services, composants clés)
- `frontend/src/main.jsx` monte l’application React dans `#root`, applique `ThemeProvider` (MUI), `CssBaseline` et enveloppe `App` dans un `Container`.
- `frontend/src/App.jsx` gère une navigation par hash, conserve l’état d’authentification, affiche une barre d’onglets conditionnelle selon le rôle (admin/assistant/parent) et orchestre les vues principales (`ChildrenList`, `ReportsList`, `AssistantsPage`, `ParentsPage`, `AssignChildren`, `ChildrenRelationsPage`, `LoginPage`).
- Les services HTTP résident dans `frontend/src/services/` (authentification, enfants, rapports, assistants, parents) et se basent sur `apiFetch.js` pour injecter le token JWT et traiter les erreurs.
- Les composants clés sous `frontend/src/components/` couvrent les formulaires d’ajout (`AddChild`, `AddAssistant`, `AddParent`, `AddReports`), les listes (`ChildrenList`, `ReportsList`, `ParentsPage`, `AssistantsPage`), la gestion des affectations (`AssignChildren`, `ChildrenRelationsPage`) et l’authentification (`LoginPage`, `AccountMenu`).

## Bonnes pratiques d’intégration
- Conserver la séparation des responsabilités entre API, domaine et infrastructure côté backend.
- Respecter le modèle de services front-end (`apiFetch.js`) pour centraliser les appels réseau et la gestion des tokens.
- Synchroniser les modifications front/back (DTO, routes) pour éviter les régressions et mettre à jour les données seed si nécessaire.
- Ajouter des tests ou validations pertinentes et exécuter les linters/commandes ci-dessous avant de pousser.

## Commandes utiles
- `dotnet run --project backend/MamConnect.Api`
- `dotnet ef database update --project backend/MamConnect.Infrastructure --startup-project backend/MamConnect.Api`
- `npm run lint`
- `npm run build`
