FROM mcr.microsoft.com/dotnet/core/sdk:6.0-buster AS build-env
WORKDIR /app

# Copy the published web app
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Run command
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0-buster-slim
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HM.Product.API.dll"]