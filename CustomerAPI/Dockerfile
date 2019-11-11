FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 6001

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["CustomerAPI/CustomerAPI.csproj", "CustomerAPI/"]
COPY ["NAGPBank.CrossCutting/NAGPBank.CrossCutting.csproj", "NAGPBank.CrossCutting/"]
COPY ["NAGPBank.Data/NAGPBank.Data.csproj", "NAGPBank.Data/"]
RUN dotnet restore "CustomerAPI/CustomerAPI.csproj"
COPY . .
WORKDIR "/src/CustomerAPI"
RUN dotnet build "CustomerAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CustomerAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CustomerAPI.dll"]