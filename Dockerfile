FROM mcr.microsoft.com/dotnet/aspnet:6.0.1
WORKDIR /app
COPY . /app
ENTRYPOINT ["dotnet", "poc-net-api.dll"]
