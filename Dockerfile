FROM mcr.microsoft.com/dotnet/aspnet:6.0.13
WORKDIR /app
COPY . /app
ENTRYPOINT ["dotnet", "poc-net-api.dll"]
