# APICatalogo

Este repositório serve como laboratório prático do [**Curso Web API ASP.NET Core Essencial**](https://www.udemy.com/course/curso-web-api-asp-net-core-essencial/), ministrado na Udemy (Macoratti). O objetivo do projeto é construir uma API RESTful completa para gestão de um catálogo de produtos e categorias, aplicando boas práticas de desenvolvimento.

## Tecnologias e Ferramentas

* **[ASP.NET Core 8.0](https://dotnet.microsoft.com/en-us/apps/aspnet)** - Framework
* **[Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)** - ORM
* **[PostgreSQL](https://github.com/jbogard/MediatR)** - Banco de dados
* **[FluentValidation](https://fluentvalidation.net/)** - Regras de validação
* **[Serilog](https://serilog.net/)** - Logs estruturados
* **[Swagger / OpenAPI](https://swagger.io/specification/)** - Documentação da API

## 🏗️ Arquitetura e Conceitos aplicados atualmente

* **Padrão Repository:** Abstração da camada de acesso a dados, permitindo a separação entre a lógica de negócios e a persistência.
* **DTOs:** Utilização de objetos para transferência de dados, evitando a exposição direta das Entidades.
* **Tratamento Global de Erros:** Implementação de Middleware global para captura e tratamento de exceções.
* **Fluent Validation:** Validação de contratos e dados de entrada (DTOs) de forma fluente, antecipando erros de formatação antes de acionar o domínio.
* **Swagger / OpenAPI:** Documentação automática e interativa da API para facilitar testes e integração.
* **Entity Type Configurations:** Configurações das entidades via Fluent API no Entity Framework Core. 
* **Naming Conventions:** Configuração automática das convenções de nomenclatura para o padrão `snake_case` do PostgreSQL.
* **Extensions:** Métodos de extensão para organizar o código do Program.cs.
* **Logging:** Configuração de logs estruturados utilizando **Serilog** (`logs/`).

## 📂 Estrutura do Projeto

```text
APICatalogo
├── Controllers/
├── Data/
├── DTOs/     
├── Entities/
├── Extensions/
├── logs/
├── Middleware/
├── Migrations/
├── Repositories/
├── Validators/
├── appsettings.json
└── Program.cs
```

## 📦 Pacotes NuGet Utilizados

| Pacote | Finalidade |
| :--- | :--- |
| **Npgsql.EntityFrameworkCore.PostgreSQL** | Provedor oficial do Entity Framework Core para integração com o PostgreSQL. |
| **EFCore.NamingConventions** | Aplica automaticamente a convenção `snake_case` em tabelas e colunas, alinhando o EF Core ao padrão nativo do PostgreSQL. |
| **Microsoft.EntityFrameworkCore.Tools** | Ferramentas de linha de comando para gerenciamento de Migrations e atualização de banco de dados (`dotnet ef`). |
| **Serilog.AspNetCore** | Substitui o logging padrão do .NET, permitindo logs estruturados, enriquecimento de contexto e alta performance. |
| **Serilog.Sinks.Console** | Habilita a saída de logs formatados no console da aplicação. |
| **Serilog.Sinks.File** | Permite a persistência de logs em arquivos físicos, com suporte a rotação e retenção automática. |
| **Serilog.Formatting.Compact** | Formatador JSON otimizado (CLEF) para logs em produção, reduzindo armazenamento em disco e facilitando ingestão em ferramentas de observabilidade. |
| **FluentValidation** | Biblioteca para construção de regras de validação complexas, fortemente tipadas e desacopladas das entidades. |
| **Swashbuckle.AspNetCore** | Gera automaticamente a especificação OpenAPI e fornece a interface Swagger UI para documentação e testes. |

## ⚙️ Como Executar

### Pré-requisitos
* [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) instalado.
* [PostgreSQL](https://www.postgresql.org/download/).

### Passos

1.  **Clone o repositório**

2. Atualize o arquivo `appsettings.json` com suas credenciais do banco de dados, ou utilize o `dotnet user-secrets`.

3. Baixe todos os pacotes Nuget necessários para o projeto:
    ```bash
    dotnet restore
    ```
    
4. **Configuração do Banco de Dados:**
   O projeto utiliza um schema personalizado chamado `core`.
   A migration inicial (`IntialCommit`) já contém o comando para criar esse schema automaticamente.
   
   *Nota: Certifique-se de que o usuário do banco de dados definido no `appsettings.json` tenha permissão para criar schemas (CREATE permission).*
5. Na raiz do projeto (onde está o arquivo .csproj), execute:
    ```bash
    dotnet ef database update
    ```

6.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```

7.  **Acesse a URL**

- **ISS**: `https://localhost:44328/docs` para testar os endpoints via Swagger em Dev.
- **HTTP**: `https://localhost:5197/docs` para testar os endpoints via Swagger em Dev.
- **HTTPS**: `https://localhost:7112/docs` para testar os endpoints via Swagger em Dev.
    
>Observação: O launchsettings está configurado para abrir o navegador ao iniciar a aplicação via IIS Express.

---
