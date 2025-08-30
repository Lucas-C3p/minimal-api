# 🤝 Guia de Contribuição

Obrigado por considerar contribuir para o projeto Minimal API! Este documento fornece diretrizes para contribuições.

## 📋 Como Contribuir

### 1. Fork e Clone
```bash
# Fork o repositório no GitHub
# Clone seu fork
git clone https://github.com/SEU_USUARIO/minimal-api.git
cd minimal-api
```

### 2. Configurar Ambiente
```bash
# Instalar dependências
cd Api
dotnet restore

cd ../Test
dotnet restore
```

### 3. Criar Branch
```bash
# Criar uma nova branch para sua feature
git checkout -b feature/nome-da-sua-feature
```

### 4. Fazer Mudanças
- Escreva código limpo e bem documentado
- Siga as convenções de código existentes
- Adicione testes para novas funcionalidades
- Atualize a documentação se necessário

### 5. Executar Testes
```bash
# Executar todos os testes
./scripts/run-tests.sh

# Ou manualmente
cd Test
dotnet test
```

### 6. Commit e Push
```bash
# Adicionar mudanças
git add .

# Commit com mensagem descritiva
git commit -m "feat: adicionar nova funcionalidade X"

# Push para seu fork
git push origin feature/nome-da-sua-feature
```

### 7. Pull Request
- Abra um Pull Request no GitHub
- Descreva suas mudanças claramente
- Referencie issues relacionadas

## 🎯 Tipos de Contribuição

### 🐛 Correção de Bugs
- Reporte bugs através de issues
- Inclua passos para reproduzir
- Forneça informações do ambiente

### ✨ Novas Funcionalidades
- Discuta a funcionalidade em uma issue primeiro
- Mantenha o escopo focado
- Adicione testes abrangentes

### 📚 Documentação
- Melhore README, comentários no código
- Adicione exemplos de uso
- Corrija erros de digitação

### 🧪 Testes
- Adicione testes para código não coberto
- Melhore testes existentes
- Adicione testes de integração

## 📝 Padrões de Código

### C# / .NET
- Use PascalCase para classes e métodos públicos
- Use camelCase para variáveis locais
- Use var quando o tipo é óbvio
- Adicione comentários XML para APIs públicas

```csharp
/// <summary>
/// Busca um veículo pelo ID
/// </summary>
/// <param name="id">ID do veículo</param>
/// <returns>Veículo encontrado ou null</returns>
public Veiculo? BuscaPorId(int id)
{
    return _context.Veiculos.Find(id);
}
```

### Testes
- Use nomes descritivos para métodos de teste
- Siga o padrão Arrange-Act-Assert
- Um teste por cenário

```csharp
[TestMethod]
public void TestarBuscarVeiculoPorIdExistente()
{
    // Arrange
    var veiculo = new Veiculo { Nome = "Civic", Marca = "Honda", Ano = 2022 };
    
    // Act
    var resultado = _servico.BuscaPorId(veiculo.Id);
    
    // Assert
    Assert.IsNotNull(resultado);
    Assert.AreEqual("Civic", resultado.Nome);
}
```

## 🔄 Processo de Review

### Critérios de Aprovação
- [ ] Código compila sem warnings
- [ ] Todos os testes passam
- [ ] Cobertura de testes adequada
- [ ] Documentação atualizada
- [ ] Segue padrões de código
- [ ] Performance não degradada

### Checklist do Revisor
- Funcionalidade funciona conforme esperado
- Código é legível e bem estruturado
- Testes cobrem cenários importantes
- Não introduz vulnerabilidades de segurança
- Documentação está atualizada

## 🚀 Convenções de Commit

Use [Conventional Commits](https://www.conventionalcommits.org/):

```
tipo(escopo): descrição

[corpo opcional]

[rodapé opcional]
```

### Tipos
- `feat`: Nova funcionalidade
- `fix`: Correção de bug
- `docs`: Mudanças na documentação
- `style`: Formatação, ponto e vírgula, etc
- `refactor`: Refatoração de código
- `test`: Adicionar ou modificar testes
- `chore`: Tarefas de manutenção

### Exemplos
```
feat(auth): adicionar autenticação JWT
fix(api): corrigir validação de email
docs(readme): atualizar instruções de instalação
test(vehicles): adicionar testes de integração
```

## 🏷️ Versionamento

- Seguimos [Semantic Versioning](https://semver.org/)
- MAJOR.MINOR.PATCH
- Mudanças breaking são MAJOR
- Novas features são MINOR
- Bug fixes são PATCH

## 📞 Suporte

- 🐛 **Bugs**: Abra uma issue
- 💡 **Ideias**: Discussões no GitHub
- ❓ **Dúvidas**: Issues com label "question"

## 📜 Código de Conduta

- Seja respeitoso e inclusivo
- Aceite feedback construtivo
- Foque no que é melhor para a comunidade
- Mantenha discussões técnicas e profissionais

---

Obrigado por contribuir! 🎉

