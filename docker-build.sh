#!/bin/bash

# Script para construir e executar a aplicação com Docker

echo "🐳 Construindo e executando Minimal API com Docker..."

# Navegar para o diretório raiz do projeto
cd "$(dirname "$0")/.." || exit 1

# Verificar se o Docker está instalado
if ! command -v docker &> /dev/null; then
    echo "❌ Docker não encontrado. Por favor, instale o Docker."
    exit 1
fi

# Parar e remover containers existentes (se houver)
echo "🛑 Parando containers existentes..."
docker-compose down 2>/dev/null || true

# Construir a imagem
echo "🔨 Construindo a imagem Docker..."
docker-compose build

# Verificar se o build foi bem-sucedido
if [ $? -ne 0 ]; then
    echo "❌ Erro ao construir a imagem Docker."
    exit 1
fi

# Executar o container
echo "🚀 Iniciando o container..."
docker-compose up -d

# Verificar se o container está rodando
if [ $? -eq 0 ]; then
    echo ""
    echo "✅ Aplicação iniciada com sucesso!"
    echo "📍 A API está disponível em: http://localhost:5000"
    echo "📚 Documentação Swagger: http://localhost:5000"
    echo ""
    echo "Para ver os logs: docker-compose logs -f"
    echo "Para parar: docker-compose down"
else
    echo "❌ Erro ao iniciar o container."
    exit 1
fi

