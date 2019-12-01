FROM mcr.microsoft.com/dotnet/core/sdk:latest
EXPOSE 5000
WORKDIR /src
COPY ["Notifications/Notifications.csproj", "Notifications/"]
RUN dotnet restore "Notifications/Notifications.csproj"
COPY . .
WORKDIR "/src/Notifications"
ENTRYPOINT [ "dotnet", "watch", "run", "--no-restore", "--urls", "http://0.0.0.0:5000"]
