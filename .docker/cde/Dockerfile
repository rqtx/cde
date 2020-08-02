# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY src .
RUN dotnet restore -r linux-x64

# copy and publish app and libraries
COPY . .
RUN dotnet publish Cde.Api -c release -o /app -r linux-x64 --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Cde.Api.dll"]
