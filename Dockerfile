FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project.csproj", "."]
RUN dotnet restore "./Project.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Project.csproj" -c Release -o /app/build
RUN dotnet tool install --global dotnet-ef
FROM build AS publish
RUN dotnet publish "Project.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/ .
#ENTRYPOINT ["dotnet", "Project.dll"]
CMD ["dotnet", "Project.dll"]
ENTRYPOINT ["dotnet", "Project.dll", "--environment=Production", "--migration=true"]