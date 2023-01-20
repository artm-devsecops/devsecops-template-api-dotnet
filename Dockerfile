FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy
WORKDIR /app
COPY . /app
ENTRYPOINT ["dotnet", "DevSecOps.Template.API.DotNet.dll"]
