FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ProcessManagementCenter/ProcessManagementCenter.csproj", "ProcessManagementCenter/"]
RUN dotnet restore "ProcessManagementCenter/ProcessManagementCenter.csproj"
COPY . .
WORKDIR "/src/ProcessManagementCenter"
RUN dotnet build "ProcessManagementCenter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessManagementCenter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessManagementCenter.dll"]