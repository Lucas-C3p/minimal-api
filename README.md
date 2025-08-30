# 🚗 Minimal API - Sistema de Registro de Veículos

Uma API moderna e completa para gerenciamento de veículos, desenvolvida com .NET 7 Minimal APIs, autenticação JWT, Entity Framework e uma suíte abrangente de testes.

## 🚀 Características

- **Minimal APIs**: Arquitetura moderna e performática do .NET 7
- **Autenticação JWT**: Sistema seguro de autenticação e autorização
- **Entity Framework**: ORM robusto com suporte a múltiplos bancos de dados
- **FluentValidation**: Validações expressivas e flexíveis
- **Swagger/OpenAPI**: Documentação interativa completa
- **Testes Abrangentes**: 50+ testes unitários e de integração
- **Containerização**: Suporte completo ao Docker

## 📋 Funcionalidades

### 🔐 Autenticação e Autorização
- Login com JWT
- Dois perfis de usuário: **Administrador** e **Editor**
- Criptografia de senhas com SHA256
- Tokens com expiração configurável

### 🚙 Gestão de Veículos
- **CRUD Completo**: Criar, listar, atualizar e excluir veículos
- **Filtros Avançados**: Busca por nome e marca
- **Paginação**: Controle de resultados por página
- **Validações Robustas**: Dados consistentes e seguros

### 👥 Gestão de Administradores
- CRUD de administradores (apenas para perfil Adm)
- Controle de perfis e permissões
- Validações de email e senha forte

## 🛠️ Tecnologias Utilizadas

- **.NET 7**: Framework principal
- **Entity Framework Core**: ORM e gerenciamento de dados
- **SQLite**: Banco de dados padrão (configurável)
- **JWT Bearer**: Autenticação
- **FluentValidation**: Validações
- **Swagger**: Documentação da API
- **MSTest**: Framework de testes
- **Docker**: Containerização

## 🏃‍♂️ Como Executar

### Pré-requisitos
- .NET 7 SDK
- Git

### 1. Clone o Repositório
```bash
git clone https://github.com/Lucas-C3p/minimal-api.git
cd minimal-api
```

### 2. Restaurar Dependências
```bash
cd Api
dotnet restore
```

### 3. Executar a Aplicação
```bash
dotnet run
```

A API estará disponível em:
- **HTTP**: http://localhost:5000
- **Swagger**: http://localhost:5000 (em desenvolvimento)

### 4. Executar Testes
```bash
cd ../Test
dotnet test
```

## 🐳 Docker

### Construir a Imagem
```bash
docker build -t minimal-api .
```

### Executar o Container
```bash
docker run -p 5000:80 minimal-api
```

## 📚 Documentação da API

### Autenticação

#### Login
```http
POST /administradores/login
Content-Type: application/json

{
  "email": "adm@teste.com",
  "senha": "123456"
}
```

**Resposta:**
```json
{
  "email": "adm@teste.com",
  "perfil": "Adm",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### Veículos

#### Listar Veículos
```http
GET /veiculos?pagina=1&tamanhoPagina=10&nome=civic&marca=honda
Authorization: Bearer {token}
```

#### Criar Veículo
```http
POST /veiculos
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Civic",
  "marca": "Honda",
  "ano": 2022
}
```

#### Atualizar Veículo
```http
PUT /veiculos/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Civic Atualizado",
  "marca": "Honda",
  "ano": 2023
}
```

#### Excluir Veículo
```http
DELETE /veiculos/{id}
Authorization: Bearer {token}
```

### Administradores

#### Listar Administradores (Apenas Adm)
```http
GET /administradores?pagina=1&tamanhoPagina=10
Authorization: Bearer {token}
```

#### Criar Administrador (Apenas Adm)
```http
POST /administradores
Authorization: Bearer {token}
Content-Type: application/json

{
  "email": "novo@admin.com",
  "senha": "MinhaSenh@123",
  "perfil": "Editor"
}
```

## 🔒 Perfis e Permissões

| Operação | Administrador | Editor |
|----------|---------------|--------|
| Login | ✅ | ✅ |
| Listar Veículos | ✅ | ✅ |
| Criar Veículo | ✅ | ✅ |
| Atualizar Veículo | ✅ | ❌ |
| Excluir Veículo | ✅ | ❌ |
| Gerenciar Administradores | ✅ | ❌ |

## ✅ Validações

### Veículos
- **Nome**: 2-150 caracteres, apenas letras, números, espaços e hífens
- **Marca**: 2-100 caracteres, apenas letras, espaços e hífens
- **Ano**: Entre 1950 e ano atual + 1

### Administradores
- **Email**: Formato válido, máximo 255 caracteres
- **Senha**: Mínimo 6 caracteres, deve conter maiúscula, minúscula e número
- **Perfil**: Deve ser "Adm" ou "Editor"

## 🧪 Testes

O projeto inclui uma suíte completa de testes:

- **50+ Testes** executados com sucesso
- **Testes Unitários**: Serviços, validadores, entidades
- **Testes de Integração**: Endpoints, autenticação, autorização
- **Cobertura Completa**: Cenários positivos e negativos

### Executar Testes
```bash
cd Test
dotnet test --verbosity normal
```

## ⚙️ Configuração

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=minimal_api_dev.db"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-aqui-32-caracteres-minimo",
    "Issuer": "MinimalApi",
    "Audience": "MinimalApi",
    "ExpiresInMinutes": 60
  },
  "DatabaseProvider": "Sqlite"
}
```

### Variáveis de Ambiente
- `JWT_KEY`: Chave secreta para JWT
- `DATABASE_PROVIDER`: Sqlite, SqlServer, MySQL
- `CONNECTION_STRING`: String de conexão personalizada

## 🏗️ Arquitetura

```
Api/
├── Dominio/
│   ├── Entidades/          # Modelos de dados
│   ├── DTOs/               # Objetos de transferência
│   ├── Interfaces/         # Contratos de serviços
│   ├── Servicos/           # Lógica de negócio
│   ├── Validadores/        # Validações FluentValidation
│   └── ModelViews/         # Modelos de resposta
├── Infraestrutura/
│   └── Db/                 # Contexto e configurações do banco
└── Program.cs              # Configuração da aplicação

Test/
├── Domain/
│   ├── Entidades/          # Testes de entidades
│   ├── Servicos/           # Testes de serviços
│   └── Validadores/        # Testes de validadores
├── Requests/               # Testes de integração
├── Mocks/                  # Objetos mock para testes
└── Helpers/                # Utilitários de teste
```

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👨‍💻 Autor

**Lucas C3p**
- GitHub: [@Lucas-C3p](https://github.com/Lucas-C3p)

---

⭐ Se este projeto te ajudou, considere dar uma estrela no repositório!

