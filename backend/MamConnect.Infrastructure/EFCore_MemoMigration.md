# EF Core - Mémo Migrations & Database Update

## Structure solution
- backend/MamConnect.Api → projet API (Program.cs)
- backend/MamConnect.Domain → entités (Child, User…)
- backend/MamConnect.Infrastructure → DbContext (AppDbContext) + Migrations

---

## Commandes à executer à partir du dossier de la solution

### 1. Ajouter une migration
```powershell
dotnet ef migrations add NomMigration --project ./backend/MamConnect.Infrastructure/MamConnect.Infrastructure.csproj --startup-project ./backend/MamConnect.Api/MamConnect.Api.csproj
```

### 2. Appliquer la migration
```powershell
dotnet ef database update --project ./backend/MamConnect.Infrastructure/MamConnect.Infrastructure.csproj --startup-project ./backend/MamConnect.Api/MamConnect.Api.csproj
```

### 3. Lister les migrations
```powershell
dotnet ef migrations list --project ./backend/MamConnect.Infrastructure/MamConnect.Infrastructure.csproj --startup-project ./backend/MamConnect.Api/MamConnect.Api.csproj
```

---

## Astuce Visual Studio
- Ouvre **Package Manager Console**
- Choisis `MamConnect.Infrastructure` comme projet par défaut
- Commandes :
```powershell
Add-Migration NomMigration
Update-Database
```

---

## Tips
- `--project` = projet avec le DbContext (Infrastructure)
- `--startup-project` = projet API (point d’entrée Program.cs)

### Revenir à une migration précédente
```powershell
dotnet ef database update NomMigration
```

### Supprimer la dernière migration (si pas encore appliquée à la base)
```powershell
dotnet ef migrations remove --project ./backend/MamConnect.Infrastructure/MamConnect.Infrastructure.csproj --startup-project ./backend/MamConnect.Api/MamConnect.Api.csproj
```
