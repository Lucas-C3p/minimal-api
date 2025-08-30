#!/bin/bash

# Script para executar todos os testes da aplicação

echo "🧪 Executando testes da Minimal API..."

# Navegar para o diretório de testes
cd "$(dirname "$0")/../Test" || exit 1

# Verificar se o .NET está instalado
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET 7 SDK não encontrado. Por favor, instale o .NET 7 SDK."
    exit 1
fi

# Restaurar dependências
echo "📦 Restaurando dependências de teste..."
dotnet restore

# Verificar se a restauração foi bem-sucedida
if [ $? -ne 0 ]; then
    echo "❌ Erro ao restaurar dependências de teste."
    exit 1
fi

# Executar os testes
echo "🏃 Executando testes..."
dotnet test --verbosity normal --logger "console;verbosity=detailed"

# Verificar o resultado dos testes
if [ $? -eq 0 ]; then
    echo ""
    echo "✅ Todos os testes passaram com sucesso!"
else
    echo ""
    echo "❌ Alguns testes falharam. Verifique os detalhes acima."
    exit 1
fi

