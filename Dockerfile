FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AdvancedDb_Order_Inventory.csproj", "."]
RUN dotnet restore "./AdvancedDb_Order_Inventory.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AdvancedDb_Order_Inventory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdvancedDb_Order_Inventory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdvancedDb_Order_Inventory.dll"]