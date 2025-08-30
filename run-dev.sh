#!/bin/bash

# Script para executar a aplicação em modo de desenvolvimento

echo "🚀 Iniciando Minimal API em modo de desenvolvimento..."

# Navegar para o diretório da API
cd "$(dirname "$0")/../Api" || exit 1

# Verificar se o .NET está instalado
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET 7 SDK não encontrado. Por favor, instale o .NET 7 SDK."
    exit 1
fi

# Restaurar dependências
echo "📦 Restaurando dependências..."
dotnet restore

# Verificar se a restauração foi bem-sucedida
if [ $? -ne 0 ]; then
    echo "❌ Erro ao restaurar dependências."
    exit 1
fi

# Executar a aplicação
echo "🏃 Executando a aplicação..."
echo "📍 A API estará disponível em: http://localhost:5000"
echo "📚 Documentação Swagger: http://localhost:5000"
echo ""
echo "Para parar a aplicação, pressione Ctrl+C"
echo ""

dotnet run --urls="http://localhost:5000"

