FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ChequeBookAPI/ChequeBookAPI.csproj", "ChequeBookAPI/"]
COPY ["NAGPBank.Data/NAGPBank.Data.csproj", "NAGPBank.Data/"]
COPY ["NAGPBank.CrossCutting/NAGPBank.CrossCutting.csproj", "NAGPBank.CrossCutting/"]
RUN dotnet restore "ChequeBookAPI/ChequeBookAPI.csproj"
COPY . .
WORKDIR "/src/ChequeBookAPI"
RUN dotnet build "ChequeBookAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChequeBookAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChequeBookAPI.dll"]