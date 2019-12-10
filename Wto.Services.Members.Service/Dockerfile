FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
COPY ["Wto.Services.Members.Service/Wto.Services.Members.Service.csproj", "Wto.Services.Members.Service/"]
COPY ["Wto.Library.Core/Wto.Library.Core.csproj", "Wto.Library.Core/"]
COPY ["Wto.Services.Common/Wto.Services.Common.csproj", "Wto.Services.Common/"]
RUN dotnet restore "Wto.Services.Members.Service/Wto.Services.Members.Service.csproj"
COPY . .
#WORKDIR "/src/Wto.Services.Members.Service"
WORKDIR "/Wto.Services.Members.Service"
RUN dotnet build "Wto.Services.Members.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Wto.Services.Members.Service.csproj" -c Release -o /app

FROM base AS final
#WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wto.Services.Members.Service.dll"]