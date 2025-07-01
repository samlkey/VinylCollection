# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FRONTEND.dll"]

# Use the official .NET SDK image for build and dev
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /src
COPY . .
EXPOSE 8080
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080"] 