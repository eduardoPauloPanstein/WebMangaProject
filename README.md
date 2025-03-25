# O que é?
Projeto de gerenciamento de mangás e animes desenvolvido em ASP.NET Core 6.0, utilizando [Arquitetura em Camadas](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures), separando-as por [bibliotecas de classe](https://learn.microsoft.com/en-us/dotnet/standard/class-libraries) , tais como:
BusinessLogicalLayer: contendo as principais regras e lógica de negócios. Foca no domínio do problema e implementa as regras de negócios sem se preocupar com como os dados são armazenados ou apresentados.
DataAccessLayer: a camada de acesso a dados, DataAccessLayer(DAL) ou DataAccessObject(DAO), é uma camada da arquitetura multicamadas que lida diretamente com a interação com a fontes de dados, banco. Aqui ocultamos a lógica detalhada de interação com o banco de dados(encapsulamento). Definimos métodos e operações de acesso a dados que podem ser reutilizados em diferentes partes da aplicação(no BLL em específico), sendo assim implementado a Reusabilidade e Flexibilidade, pois facilita mudanças de banco e na forma como os dados são trados sem impactar outras partes, aqui é usado EF e UnitOfWork 
WebApi: orquestrador de requisições do nosso MangaProject, conectando as possíveis camadas de apresentação (web, mobile) com a camada de negócios (Business Logic Layer - BLL), garantindo a integração e comunicação eficiente entre elas.

e padrões de design modernos.
É um catalogo de obras de animação japonesas;
Ver informações, salvar, classificar, comentar, montar sua lista de mangas e animes que já viu, está assistindo ou planeja ver. Se organize e explore novas obras, por categoria e recomendações.

## Tecnologias Utilizadas
🔷 Backend
- ASP.NET Core 6.0
- Entity Framework Core
- AutoMapper
- FluentValidation
- Azure Redis Cache
- SQL Server
- JWT (JSON Web Tokens)
- Swagger (Swashbuckle)
- Log4Net

🔷 Frontend
- ASP.NET Core MVC
- Razor Pages
- Bootstrap
- AutoMapper
- Newtonsoft.Json

## Funcionalidades Implementadas
👤 Gestão de Usuários
- Autenticação e Autorização
- Perfis de Usuários
- Upload de avatar e imagem de capa

📚 Gestão de Conteúdo
- CRUD de Mangás e Animes
- Sistema de Comentários
- Favoritos
- Recomendações baseadas em preferências

## Benefícios da Arquitetura
🔗 Desacoplamento
- Cada camada é independente e pode evoluir separadamente
- Facilita manutenção e testes
- Permite mudanças tecnológicas isoladas

📈 Reutilização de Código
- Componentes compartilhados entre camadas
- Padrões de design reutilizáveis
- Interfaces comuns para operações

⚡ Escalabilidade
- Cada camada pode ser escalada independentemente
- Cache distribuído com Redis
- Arquitetura preparada para crescimento

🔒 Segurança
- Autenticação centralizada com JWT
- Validações em múltiplas camadas
- Logging estruturado para auditoria

## Como Executar
1. Instale as dependências:
```bash
dotnet restore
```
2. Configure o banco de dados no appsettings.json
3. Execute as migrations:
```bash
dotnet ef database update
```
4. Inicie a aplicação:
```bash
dotnet run
```

## Contribuição
Contribuições são bem-vindas! Para contribuir:
1. Faça um fork do projeto
2. Crie uma branch para sua feature
3. Envie um pull request

## Licença
[Inserir licença aqui]
