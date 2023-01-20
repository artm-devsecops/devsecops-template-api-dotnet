FROM mcr.microsoft.com/dotnet/aspnet:7.0.2
WORKDIR /app
COPY . /app
ENTRYPOINT ["dotnet", "poc-net-api.dll"]
