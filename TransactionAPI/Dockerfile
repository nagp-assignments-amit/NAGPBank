FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TransactionAPI/TransactionAPI.csproj", "TransactionAPI/"]
COPY ["NAGPBank.Data/NAGPBank.Data.csproj", "NAGPBank.Data/"]
COPY ["NAGPBank.CrossCutting/NAGPBank.CrossCutting.csproj", "NAGPBank.CrossCutting/"]
RUN dotnet restore "TransactionAPI/TransactionAPI.csproj"
COPY . .
WORKDIR "/src/TransactionAPI"
RUN dotnet build "TransactionAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TransactionAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TransactionAPI.dll"]