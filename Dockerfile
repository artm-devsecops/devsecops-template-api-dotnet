FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY . /app
ENTRYPOINT ["dotnet", "DevSecOps.Template.API.DotNet.dll"]
