FROM mcr.microsoft.com/dotnet/core/sdk:latest
EXPOSE 5000
WORKDIR /src
COPY ["ProcessManagementCenter/ProcessManagementCenter.csproj", "ProcessManagementCenter/"]
RUN dotnet restore "ProcessManagementCenter/ProcessManagementCenter.csproj"
COPY . .
WORKDIR "/src/ProcessManagementCenter"
ENTRYPOINT [ "dotnet", "watch", "run", "--no-restore", "--urls", "http://0.0.0.0:5000"]