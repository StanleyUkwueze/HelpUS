FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE 9090

# copy .csproj and restore as distinct layers
COPY "HelpUs.API.sln" "HelpUs.API.sln"
COPY "HelpUs.API/HelpUs.API.csproj" "HelpUs.API/HelpUs.API.csproj"

RUN dotnet restore "HelpUs.API.sln"

# copy everything else and build
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

# build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "HelpUs.API.dll" ]