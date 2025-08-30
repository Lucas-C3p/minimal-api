# Usar a imagem oficial do .NET 7 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY ["Api/mininal-api.csproj", "Api/"]
RUN dotnet restore "Api/mininal-api.csproj"

# Copiar todo o código fonte
COPY . .
WORKDIR "/src/Api"

# Build da aplicação
RUN dotnet build "mininal-api.csproj" -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "mininal-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Usar a imagem runtime do .NET 7 para execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Instalar dependências para SQLite (se necessário)
RUN apt-get update && apt-get install -y sqlite3 && rm -rf /var/lib/apt/lists/*

# Copiar arquivos publicados
COPY --from=publish /app/publish .

# Criar diretório para o banco de dados
RUN mkdir -p /app/data

# Configurar variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/minimal_api.db"

# Expor a porta 80
EXPOSE 80

# Definir o ponto de entrada
ENTRYPOINT ["dotnet", "mininal-api.dll"]

