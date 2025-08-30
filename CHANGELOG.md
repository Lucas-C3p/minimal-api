# Changelog

Todas as mudanças notáveis neste projeto serão documentadas neste arquivo.

## [2.0.0] - 2024-08-30

### 🎉 Versão Completa - Refatoração Total

#### ✨ Adicionado
- **Arquitetura Moderna**: Migração completa para .NET 7 Minimal APIs
- **Autenticação Robusta**: Sistema JWT com criptografia SHA256
- **Validações Avançadas**: FluentValidation com regras personalizadas
- **Testes Abrangentes**: 50+ testes unitários e de integração
- **Documentação Swagger**: Interface interativa completa
- **Containerização**: Suporte completo ao Docker
- **Scripts de Automação**: Scripts para desenvolvimento e deploy

#### 🔧 Melhorado
- **Estrutura do Projeto**: Organização em camadas bem definidas
- **Segurança**: Criptografia de senhas e validação de tokens
- **Performance**: Otimizações na consulta de dados
- **Usabilidade**: Filtros e paginação nos endpoints
- **Manutenibilidade**: Código limpo e bem documentado

#### 🗂️ Estrutura de Arquivos
```
minimal-api/
├── Api/                          # Aplicação principal
│   ├── Dominio/
│   │   ├── Entidades/           # Modelos de dados
│   │   ├── DTOs/                # Objetos de transferência
│   │   ├── Interfaces/          # Contratos de serviços
│   │   ├── Servicos/            # Lógica de negócio
│   │   ├── Validadores/         # Validações FluentValidation
│   │   └── ModelViews/          # Modelos de resposta
│   ├── Infraestrutura/
│   │   └── Db/                  # Contexto e configurações do banco
│   ├── Migrations/              # Migrações do banco de dados
│   ├── Program.cs               # Configuração da aplicação
│   └── appsettings.json         # Configurações
├── Test/                        # Projeto de testes
│   ├── Domain/                  # Testes de domínio
│   ├── Requests/                # Testes de integração
│   ├── Mocks/                   # Objetos mock
│   └── Helpers/                 # Utilitários de teste
├── scripts/                     # Scripts de automação
├── Dockerfile                   # Configuração Docker
├── docker-compose.yml           # Orquestração de containers
└── README.md                    # Documentação completa
```

#### 🚀 Funcionalidades Implementadas

##### Autenticação e Autorização
- [x] Login com JWT
- [x] Dois perfis: Administrador e Editor
- [x] Criptografia de senhas
- [x] Tokens com expiração configurável
- [x] Middleware de autorização

##### Gestão de Veículos
- [x] CRUD completo (Create, Read, Update, Delete)
- [x] Filtros por nome e marca
- [x] Paginação configurável
- [x] Validações robustas
- [x] Controle de permissões por perfil

##### Gestão de Administradores
- [x] CRUD de administradores (apenas para Adm)
- [x] Validação de email e senha forte
- [x] Controle de perfis

##### Documentação e Testes
- [x] Swagger/OpenAPI completo
- [x] 50+ testes automatizados
- [x] Cobertura de cenários positivos e negativos
- [x] Testes de integração com autenticação

##### DevOps e Deploy
- [x] Dockerfile otimizado
- [x] Docker Compose para orquestração
- [x] Scripts de automação
- [x] Configurações para produção

#### 🔒 Segurança
- Senhas criptografadas com SHA256
- Tokens JWT seguros
- Validação de entrada rigorosa
- Controle de acesso baseado em perfis
- Headers de segurança configurados

#### 📊 Métricas de Qualidade
- **Testes**: 50+ testes (100% de sucesso)
- **Cobertura**: Cenários críticos cobertos
- **Performance**: Otimizada para produção
- **Segurança**: Práticas recomendadas implementadas

## [1.0.0] - Versão Base

### Funcionalidades Básicas
- Estrutura inicial com Startup.cs
- CRUD básico de veículos
- Autenticação JWT simples
- Testes básicos

---

## 🏷️ Versionamento

Este projeto segue o [Semantic Versioning](https://semver.org/):
- **MAJOR**: Mudanças incompatíveis na API
- **MINOR**: Funcionalidades adicionadas de forma compatível
- **PATCH**: Correções de bugs compatíveis

