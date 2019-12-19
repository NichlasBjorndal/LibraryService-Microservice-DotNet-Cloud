FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["BookService/BookService.csproj", "BookService/"]
RUN dotnet restore "BookService/BookService.csproj"
COPY . .
WORKDIR "/src/BookService"
RUN dotnet build "BookService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookService.dll"]