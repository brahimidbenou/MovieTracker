# Application de Recherche de Films

Une application web permettant aux utilisateurs de rechercher des films, de gérer leurs favoris, et d'importer des films depuis l'API OMDB. L'application inclut une gestion des utilisateurs avec des rôles (admin/utilisateur) et une authentification sécurisée via JWT.

## Fonctionnalités

### Backend
- **Authentification JWT** : Sécurisation des routes et gestion des utilisateurs.
- **Base de données SQLite** : Utilisation d'Entity Framework pour la persistance des données.
- **Controllers** :
  - **User** : Gestion des utilisateurs.
  - **Favorite** : Gestion des films favoris.
  - **Movie** : Gestion des films.
  - **OMDB** : Recherche et importation de films depuis l'API OMDB.
- **Services** :
  - **JWT Service** : Gestion des tokens JWT.
  - **OMDB Service** : Communication avec l'API OMDB.
- **Gestion des erreurs** : Retour de codes HTTP appropriés (200, 404, 500, etc.).
- **Async/Await** : Utilisation pour les appels API et l'accès à la base de données.

### Frontend (Blazor)
- **Authentification** : Formulaire de connexion et d'inscription.
- **Liste des films** : Affichage sous forme de cartes, possibilité d'ajouter/retirer des favoris.
- **Favoris** : Page dédiée pour afficher les films favoris de l'utilisateur.
- **Administration** :
  - Importation de films depuis l'API OMDB.
- **Services** :
  - **AuthService** : Gestion de l'authentification.
  - **UserService** : Gestion des utilisateurs.
  - **FavoriteService** : Gestion des favoris.
  - **MovieService** : Gestion des films.
- **Protection des routes** : Accès restreint en fonction des rôles (admin/utilisateur).
- **Stockage du token JWT** : Dans le local storage pour les requêtes authentifiées.

## Technologies Utilisées

### Backend
- **ASP.NET Core** : Framework pour le développement de l'API.
- **Entity Framework Core** : ORM pour la gestion de la base de données SQLite.
- **JWT (JSON Web Tokens)** : Pour l'authentification sécurisée.
- **OMDB API** : Pour la recherche et l'importation de films.
- **Dependency Injection** : Pour une gestion propre des services.

### Frontend
- **Blazor** : Framework pour le développement de l'interface utilisateur.
- **Bootstrap** : Pour le style et la mise en page.
- **Bootstrap Icons** : Pour les icônes.
- **Local Storage** : Pour stocker le token JWT.

## Installation

### Prérequis
- [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0 ou supérieure)

### Étapes
1. **Cloner le dépôt** :
   ```bash
   git clone https://gitlab.isima.fr/webapp/filmsapp.git

2. **Commandes d'éxecution** :
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   dotnet watch

## Problèmes Rencontrés
1. Lors de la suppression du dernier élément de la page des favoris, la liste ne se met pas à jour automatiquement. Il est nécessaire de rafraîchir manuellement la page pour que la liste vide s'affiche correctement.
2. Gestion de la page Admin
