# Movie Search Application

A web application that allows users to search for movies, manage their favorites, and import movies from the OMDB API. The application includes user management with roles (admin/user) and secure authentication via JWT.

## Features

### Backend
- **JWT Authentication**: Securing routes and managing users.
- **SQLite Database**: Using Entity Framework for data persistence.
- **Controllers**:
  - **User**: User management.
  - **Favorite**: Managing favorite movies.
  - **Movie**: Managing movies.
  - **OMDB**: Searching and importing movies from the OMDB API.
- **Services**:
  - **JWT Service**: Handling JWT tokens.
  - **OMDB Service**: Communicating with the OMDB API.
- **Error Handling**: Returning appropriate HTTP codes (200, 404, 500, etc.).
- **Async/Await**: Used for API calls and database access.

### Frontend (Blazor)
- **Authentication**: Login and registration forms.
- **Movie List**: Displayed as cards, with the ability to add/remove favorites.
- **Favorites**: A dedicated page to display the user's favorite movies.
- **Administration**:
  - Importing movies from the OMDB API.
- **Services**:
  - **AuthService**: Handling authentication.
  - **UserService**: Managing users.
  - **FavoriteService**: Managing favorites.
  - **MovieService**: Managing movies.
- **Route Protection**: Restricted access based on roles (admin/user).
- **JWT Token Storage**: Stored in local storage for authenticated requests.

## Technologies Used

### Backend
- **ASP.NET Core**: Framework for API development.
- **Entity Framework Core**: ORM for managing the SQLite database.
- **JWT (JSON Web Tokens)**: For secure authentication.
- **OMDB API**: For searching and importing movies.
- **Dependency Injection**: For clean service management.

### Frontend
- **Blazor**: Framework for UI development.
- **Bootstrap**: For styling and layout.
- **Bootstrap Icons**: For icons.
- **Local Storage**: For storing the JWT token.

## Installation

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0 or higher)

### Steps
1. **Clone the repository**:
   ```bash
   git clone https://gitlab.isima.fr/webapp/filmsapp.git
   ```

2. **Execution Commands**:
   ```bash
   dotnet clean
   dotnet restore
   dotnet build
   dotnet watch
   ```
