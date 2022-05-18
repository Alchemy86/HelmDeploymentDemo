FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HelmDeploymentDemo/HelmDeploymentDemo.csproj", "HelmDeploymentDemo/"]
RUN dotnet restore "HelmDeploymentDemo/HelmDeploymentDemo.csproj"
COPY . .
WORKDIR "/src/HelmDeploymentDemo"
RUN dotnet build "HelmDeploymentDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelmDeploymentDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelmDeploymentDemo.dll"]
