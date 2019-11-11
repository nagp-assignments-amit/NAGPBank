FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY AccountAPI/AccountAPI.csproj AccountAPI/
COPY NAGPBank.Data/NAGPBank.Data.csproj NAGPBank.Data/
COPY NAGPBank.CrossCutting/NAGPBank.CrossCutting.csproj NAGPBank.CrossCutting/
RUN dotnet restore AccountAPI/AccountAPI.csproj
COPY . .
WORKDIR /src/AccountAPI
RUN dotnet build AccountAPI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AccountAPI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AccountAPI.dll"]
