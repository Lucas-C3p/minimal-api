# Plano de Ação - API de Veículos

## Fase 1: Análise e Configuração Inicial (Em andamento)

- [x] Clonar o repositório Git.
- [x] Analisar a estrutura de arquivos e o código existente (`Program.cs`, `Startup.cs`).
- [x] Atualizar o projeto para a estrutura moderna de Minimal APIs (mover a lógica de `Startup.cs` para `Program.cs`).
- [x] Verificar e atualizar as dependências do NuGet para as versões mais recentes e estáveis.
- [x] Garantir que o projeto compila e executa sem erros após as atualizações estruturais.

## Fase 2: Implementação da Estrutura Base e Modelos

- [x] Revisar e refatorar os modelos `Veiculo` e `Administrador` se necessário.
- [x] Revisar as interfaces de serviço (`IVeiculoServico`, `IAdministradorServico`).

## Fase 3: Configuração do Entity Framework e Banco de Dados

- [x] Revisar a configuração do `DbContexto`.
- [x] Analisar as migrations existentes e o `Seed` do administrador.
- [x] Configurar o banco de dados para ser criado e populado ao iniciar a aplicação (para facilitar o desenvolvimento).

## Fase 4: Implementação da Autenticação e JWT

- [x] Revisar a lógica de geração e validação de token JWT.
- [x] Refatorar o endpoint de login para seguir as melhores práticas.

## Fase 5: Desenvolvimento do CRUD de Veículos

- [x] Revisar e refatorar os endpoints do CRUD de veículos (`POST`, `GET`, `PUT`, `DELETE`).
- [x] Garantir que a lógica de serviço (`VeiculoServico`) está correta.

## Fase 6: Configuração do Swagger e Documentação

- [x] Revisar a configuração do Swagger, incluindo a autenticação JWT.
- [x] Organizar os endpoints com tags e resumos para melhor documentação.

## Fase 7: Implementação de Validações e Autorização

- [x] Revisar a lógica de validação dos DTOs.
- [x] Implementar validação usando FluentValidation para um código mais limpo.
- [x] Verificar se as políticas de autorização (`[Authorize]`) estão aplicadas corretamente nos endpoints.

## Fase 8: Criação de Testes Unitários e de Integração

- [x] Analisar a estrutura do projeto de testes existente.
- [x] Criar testes de unidade para os serviços.
- [x] Criar testes de integração para os endpoints da API.

## Fase 9: Preparação para Deploy e Finalização

- [x] Criar um script ou instruções para o deploy da aplicação (ex: Dockerfile).
- [x] Limpar o código e garantir que tudo está funcionando de ponta a ponta.

## Fase 10: Entrega do Projeto

- [x] Compactar o projeto finalizado e preparar para a entrega.
- [x] Enviar o projeto e as instruções para o usuário.


