# recipes-manager-web-client

A web client application for managing recipes, menus, shopping lists, and user profiles. Built with ASP.NET Core Razor Pages, it offers features such as recipe search, creation, editing, user authentication, and menu management.

## Table of Contents

- [Features](#features)
- [Stack](#stack)
- [Installation](#installation)
  - [Prerequisites](#prerequisites)
  - [Setup Instructions](#setup-instructions)
- [Configuration](#configuration)

## Features

- User authentication with login via email or phone.
- Browse, search, and filter recipes by various categories and types.
- View detailed recipe pages including ingredients, cooking time, and calories.
- Create and edit recipes with support for uploading images.
- Save recipes and add them to custom menus.
- Manage multiple menus with notes and recipe collections.
- Create and maintain shopping lists linked to recipes.
- View and edit user profiles with followers and recipe counts.
- Responsive web interface with a clean and intuitive design.
- Integration with GraphQL API for efficient data querying and mutations.
- Custom middleware for securing and managing global user state.
- Error handling with friendly error pages.

## Stack

- **Backend Framework:** ASP.NET Core 7.0 with Razor Pages
- **Language:** C#
- **Frontend:** HTML5, CSS3, JavaScript
- **GraphQL:** GraphQL.Client with Newtonsoft JSON serializer
- **Authentication:** JWT Bearer tokens
- **Client-side libraries:** Bootstrap, jQuery Validation
- **HTTP Client:** HttpClientFactory for REST and GraphQL API calls
- **Development Containers:** Docker-based devcontainer with .NET 7 SDK
- **CI/CD:** GitHub Actions for Azure Web App deployment

## Installation

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- Optional but recommended: Docker, for using the included devcontainer environment
- Node.js and npm (optional, for managing frontend assets if needed)
- Access to the Recipes Manager API backend (hosted or local)

### Setup Instructions

1. **Clone the repository**

```bash
git clone https://github.com/Shchoholiev/recipes-manager-web-client.git
cd recipes-manager-web-client
```

2. **Open in Visual Studio Code**

- Use the included `.devcontainer` configuration to open in a container development environment:
  - Install [Remote - Containers Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)
  - Open the project folder and reopen in container (`Remote-Containers: Reopen in Container` command)

3. **Restore dependencies and build**

```bash
dotnet restore RecipesManagerWebClient.Web/RecipesManagerWebClient.Web.csproj
dotnet build RecipesManagerWebClient.Web/RecipesManagerWebClient.Web.csproj
```

4. **Run the application**

```bash
dotnet run --project RecipesManagerWebClient.Web/RecipesManagerWebClient.Web.csproj
```

5. **Access the web app**

- Open your browser and navigate to `https://localhost:7070` (or the URL specified in the launch settings).

6. **Using Visual Studio Code tasks**

- Build: `Ctrl + Shift + P` > `Tasks: Run Task` > `build`
- Publish: `Tasks: Run Task` > `publish`
- Watch and run: `Tasks: Run Task` > `watch`

## Configuration

Configuration is managed through `appsettings.json` and environment-specific files (`appsettings.Development.json`, `appsettings.Production.json`).

Key settings you need to configure:

- **ApiUrl**: Base URL of the Recipes Manager API backend.

Example (`appsettings.Development.json`):

```json
{
  "ApiUrl": "https://sh-recipes-manager-api-dev.azurewebsites.net/",
  "ObjectStorageUrl": "https://recipes.l7l2.c16.e2-2.dev/"
}
```

- **ObjectStorageUrl**: URL to the storage where recipe images and assets are hosted.

### Environment Variables

- `ASPNETCORE_ENVIRONMENT` — set to `Development` or `Production` to control environment.

### Authentication

- The app uses JWT tokens stored in HTTP cookies (`accessToken` and `refreshToken`) to authenticate API requests.
- Tokens are automatically refreshed if expired via the `AuthenticationService`.

Please ensure your backend API is properly configured to issue and accept tokens for smooth authentication.
