FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/NeverLate-api/NeverLate-api.csproj", "NeverLate-api/"]
COPY ["NeverLate-api.FluentValidation.Extension/NeverLate-api.FluentValidation.Extension.csproj", "NeverLate-api.FluentValidation.Extension/"]
RUN dotnet restore "NeverLate-api/NeverLate-api.csproj"
COPY . .
WORKDIR "/src/NeverLate-api"
RUN dotnet build "NeverLate-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NeverLate-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NeverLate-api.dll"]
