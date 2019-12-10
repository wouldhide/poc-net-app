FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Wto.Services.HSProducts.Service/Wto.Services.HSProducts.Service.csproj", "Wto.Services.HSProducts.Service/"]
COPY ["Wto.Library.Core/Wto.Library.Core.csproj", "Wto.Library.Core/"]
COPY ["Wto.Services.Common/Wto.Services.Common.csproj", "Wto.Services.Common/"]
RUN dotnet restore "Wto.Services.HSProducts.Service/Wto.Services.HSProducts.Service.csproj"
COPY . .
WORKDIR "/src/Wto.Services.HSProducts.Service"
RUN dotnet build "Wto.Services.HSProducts.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Wto.Services.HSProducts.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wto.Services.HSProducts.Service.dll"]