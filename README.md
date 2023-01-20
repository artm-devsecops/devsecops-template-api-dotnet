# ARTM - DevSecOps - Modèle REST API en DotNet 

Ce projet sert de référence pour la création d'une API REST en DotNet. Il comprend des exemples de pour les points de terminaison REST incluant une authorization basée sur les spécifications OpenID Connect et OAuth 2.0.

De plus, il démontre une intégration continue avec Github Actions.

## Grande lignes

- Visual Studio 2022 Community ou Visual Studio Code
- .Net 6 (C#, ASP.NET Core)
- OpenAPI Specification (Swagger)
- OpenID Connect & OAuth 2.0
- Tests unitaires
    - Xunit
    - Moq
- Github Actions
    Workflows:
    - Build
    - Test et publication des résultats
    - Docker
        - Build
        - Push vers GitHub Container Registry
    Pull Requests:
    - CodeQL
    - Dependabot
    
## Configuration

L'Application utilise Azure Active Directory pour l'authentification et l'authorization des points de terminaison. Pour ce faire, il faut créer une application Azure Active Directory et configurer les paramètres de l'application dans le fichier `appsettings.json`:

