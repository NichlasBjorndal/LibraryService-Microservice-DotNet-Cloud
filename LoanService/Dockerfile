FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["LoanService/LoanService.csproj", "LoanService/"]
RUN dotnet restore "LoanService/LoanService.csproj"
COPY . .
WORKDIR "/src/LoanService"
RUN dotnet build "LoanService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoanService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoanService.dll"]