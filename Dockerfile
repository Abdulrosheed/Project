FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
WORKDIR /src
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /publish
COPY --from=build-env /publish .
ENTRYPOINT ["dotnet", "Project.dll"]