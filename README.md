# WebMangaProject

## O que é?
Projeto de gerenciamento de mangás e animes desenvolvido em **ASP.NET Core 6.0**, utilizando **Arquitetura em Camadas**, separando-as por [bibliotecas de classe](https://learn.microsoft.com/en-us/dotnet/standard/class-libraries), tais como **DataAccessLayer**, **BusinessLogicalLayer**, **WebApi**, **WebMangaProject**, **Entities**, **Shared** e **Test**, com padrões de design modernos(talvez) e comuns de empresas locais estudadas.  
É um catálogo de obras de animação japonesas, onde é possível:  
- Ver informações, salvar, classificar, comentar, montar sua lista de mangás e animes que já viu, está assistindo ou planeja ver.  
- Organizar-se e explorar novas obras por categorias e recomendações.

---

## Arquitetura
📚 [Padrão de Arquitetura](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures) utilizado.  

Para organizar este projeto foi utilizado uma arquitera de multicamadas, também conhecida como Layered, or N-Tier Architecture, padrão de design usado no desenvolvimento de software para organizar código e estruturar aplicativos em camadas distintas, cada uma responsável por um aspecto específico do aplicativo. Essa abordagem promove modularidade, separação de preocupações e manutenibilidade, facilitando o desenvolvimento e gerenciamento, pois, por exemplo, permite mudanças tecnológicas isoladas. Dividindo a estrutura do projeto em várias camadas, cada uma servindo a um propósito diferente. Essas camadas interagem de cima para baixo, onde camadas superiores dependem de camadas inferiores, mas não vice-versa. No nosso caso, temos as seguintes camadas:

### 🔹 **DataAccessLayer (DAL)**  
A camada de acesso a dados, DataAccessLayer(DAL) ou DataAccessObject(DAO) como também é conhecida, é uma camada da arquitetura multicamadas que lida diretamente com a interação com as fontes de dados, com o banco. Aqui ocultamos a lógica detalhada de interação com o banco de dados(encapsulamento). Definimos métodos e operações de acesso a dados que podem ser reutilizados em diferentes partes da aplicação(no BLL em específico), sendo assim implementado a Reusabilidade e Flexibilidade, pois facilita mudanças de banco e na forma como os dados são trados sem impactar outras partes, aqui é usado SQL com EntityFrameworkCore, Azure e UnitOfWork, encontra-se se os [MapConfig.cs](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.ientitytypeconfiguration-1?view=efcore-9.0) para mapear nossas entididades para o banco de dados, estão todos dentro da pasta Mappings, temos as [Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli) na pasta `Migrations`, e temos tratamento de errros específicos relacionados ao banco de dados na classe `UserDbFailed`, onde vemos mais um exemplos de Encapsulamento, separando as responsabilidades ao encapsular a lógica de tratamento de erros relacionadas ao banco de dados de usuários em uma classe específica dentro, vejamos como acontece; 1. Camada de Lógica de Negócios (BLL): A classe UserService faz parte da camada de lógica de negócios e contém um método RegisterUser para registrar um novo usuário. 2. Operação de Banco de Dados: O método RegisterUser tenta adicionar um novo usuário ao repositório de usuários (_userRepository.Add(user)). 3. Tratamento de Exceções: Se uma exceção ocorrer durante a operação de banco de dados, ela é capturada pelo bloco catch. 4. Chamada ao UserDbFailed: A exceção capturada é passada para o método Handle da classe UserDbFailed, que retorna uma resposta apropriada com base no tipo de erro. 5. Resposta Padronizada: A resposta gerada pelo UserDbFailed é retornada ao chamador do método RegisterUser, garantindo que o usuário receba uma mensagem clara e consistente sobre o resultado da operação.

---

### 🔹 **BusinessLogicalLayer (BLL)**  
Contendo as principais regras e lógica de negócios. Foca no domínio do problema e implementa as regras de negócios sem se preocupar com como os dados são armazenados ou apresentados. Aqui temos utilizado Dependency Injection e FluentValidation, nessa biblioteca há uma divisão entre APIConsumer e o restante das operações normais. O APIConsumer é responsável por consumir dados de uma API externa e persistir esses dados localmente, garantindo que a aplicação tenha acesso aos dados mais recentes de animes e mangas, mesmo que a fonte original esteja indisponível, pode não necessariamente ficar no BLL e nem necessariamente persitir localmente, mas foi implementado para o caso da API Externa sair do ar e eu ainta ter os dados para apresentar a aplicação na prática e como a lógica de como esses dados são consumidos, transformados e armazenados faz parte das regras de negócios, faz sentido o APIConsumer estar na camada de negócios. O consumo da API pode ser rodado uma unica vez a quantidade de obras que vai ter para exemplo vai depender do quanto quiser esperar, apenas para conhecer o projeto 2 minutos já basta, para o projeto estar no ar seria legal e o proximo passo implementar uma rotina de atualização ou desacoplar e não persistir localmente.

---

### 🔹 **WebApi**  
Orquestrador de requisições do nosso MangaProject, conectando as possíveis camadas de apresentação (web, mobile) com a camada de negócios (Business Logic Layer - BLL), garantindo a integração e comunicação eficiente entre elas. A WebApi é responsável por expor endpoints que permitem a interação com a aplicação, recebendo requisições HTTP, processando-as e retornando respostas apropriadas. Utilizamos Dependency Injection para gerenciar as dependências entre os componentes, garantindo um código mais modular e testável. Além disso, a WebApi utiliza JWT com Bearer para autenticação e autorização, assegurando que apenas usuários autenticados possam acessar determinados recursos. Para documentação e testes dos endpoints, utilizamos Swagger. A configuração de logs é feita com log4net, permitindo o monitoramento e a rastreabilidade das operações. A estrutura da WebApi inclui controladores que lidam com diferentes entidades (`AnimeController`, `MangaController`, `UserController`), serviços para lógica específica (como TokenService), e configurações de ambiente e segurança (como appsettings.json e Program.cs). Essa camada é essencial para garantir que a aplicação funcione de maneira coesa e segura, integrando todas as partes do sistema de forma eficiente.

---

### 🔹 **WebMangaProject**  
 Nossa camada de apresentação, responsável por interagir diretamente com os usuários finais através de uma interface web. Utilizamos ASP.NET Core MVC para estruturar essa aplicação web, com Azure Redis Cache, Bootstrap, Razor Pages, também utilizando Dependency Injection assim como nas demais camadas, e aqui, diferentemente da APIk, onde a autenticação é baseada em tokens enviados no cabeçalho e a autorização por meio de claims incluídas no payload do JWT, aqui no web MVC o navegador mesmo quem gerencia com cookies, e isso é configurado com AspNetCore.CookiePolicy para manter a autenticação de login dos usuários e Microsoft.AspNetCore.Authorization.Policy para autorização.

---

### 🔹 **Entities**  
Essa camada é essencial para a arquitetura multicamadas como a nossa e programação orientada a objetos, pois serve de fundação sobre a qual as demais camadas (DAL, BLL, API e apresentação) dependem. E contém as classes que representam as entidades/objetos do domínio, como no nosso caso, animes, mangas, usuários, comentários, e afins que surjam. Essas classes são responsáveis por modelar os dados que serão manipulados em todas as outras camadas, garantindo consistência e padronização. Além disso, inclui enums (como `UserRoles`, `UserAnimeStatus`) para representar estados e papéis conforme mencionados anteriormente, e abstrações como a classe Entity, que fornece propriedades e métodos comuns, como controle de acesso para todas as outras entidades, junto com a escolha do Entity Framework, temos a abordagem Code-Firt, que serve para definir o mapeamento entre classes C# e o banco de dados. Ao contrário das abordagens "Database-First" (onde o banco de dados é projetado primeiro e o código é projeto em cima do banco de dados e tem que alterar o código na mao e comparando corretamente em caso de atualizações no esquema de dados) e "Model-First" (onde o modelo conceitual é criado antes), no Code-First o foco e controle está totalmente no código-fonte. Controle total pois tem controle total sobre o modelo de dados diretamente no código. Manutenção Simplificada pois alterações no modelo são rapidamente refletidas no banco de dados através de migrações e com menos trabalho manual e suscetível a falhas já que estão diretamente ligados. E esse conceito todo, assim como nosso projeto, se inicia nessa camada, definindo os objetos.

---

### 🔹 **Shared**  
Biblioteca auxiliar que centraliza componentes e funcionalidades compartilhadas entre as diferentes camadas do sistema, promovendo reusabilidade e padronização. Ela contém interfaces genéricas, modelos de dados auxiliares, constantes, e classes de resposta que são amplamente utilizadas em todo o projeto. Começando pelas **interfaces**, temos `ICRUD` e `ICrudUserItem`, que definem contratos genéricos para operações CRUD (Create, Read, Update, Delete) e operações específicas relacionadas a itens de usuários, como favoritos e recomendações. Essas interfaces são implementadas em outras camadas para garantir consistência nas operações. Em **Models**, encontramos classes como `UserCreate`, `UserLogin`, `UserProfileUpdate`, `MangaCatalog` e `AnimeCatalog`, que servem como modelos de transferência de dados (DTOs) entre camadas, encapsulando informações específicas para operações como criação de usuários, login, ou exibição de catálogos de animes e mangas. A subpasta **Responses** contém classes como `Response`, `SingleResponse`, `DataResponse` e `SingleResponseWToken`, que padronizam as respostas retornadas pelas operações do sistema, permitindo que todas as camadas comuniquem resultados de forma consistente, incluindo mensagens de sucesso, falhas e exceções. A classe `ResponseFactory` é um componente central que atua como uma fábrica para criar instâncias dessas respostas, encapsulando a lógica de construção de mensagens e garantindo que todas as respostas sigam o mesmo formato. Além disso, a camada inclui **LocationConstants**, que define constantes reutilizáveis, como mensagens de validação para apelidos e senhas (`NicknameConstants` e `PasswordConstants`), além de chaves de cache para operações relacionadas a animes e mangas (`AnimeCacheKey` e `MangaCacheKey`). Essas constantes ajudam a evitar duplicação de código e facilitam a manutenção. A camada também utiliza o padrão Singleton em algumas classes, como `ResponseFactory`, para garantir que apenas uma instância seja criada e compartilhada em todo o sistema. Por fim, a camada **Shared** é projetada para ser independente, dependendo apenas da camada **Entities**, conforme indicado no arquivo de projeto (`Shared.csproj`), o que reforça sua função como uma biblioteca de suporte genérica e reutilizável em todo o sistema. Essa estrutura modular e bem organizada torna a camada Shared essencial para a coesão e eficiência do projeto, fornecendo ferramentas e padrões que garantem consistência e simplicidade no desenvolvimento.

---

### 🔹 **Test**  🧪
A pasta Test foi criada para implementar testes básicos no projeto, com o objetivo de aprender os conceitos fundamentais de testes automatizados em .NET e garantir a qualidade do código-fonte implementado e do sistema todo em si, consequentemente. 

Conceitos de Testes Automatizados
Os testes automatizados são uma prática essencial no desenvolvimento de software, pois permitem verificar automaticamente se o código funciona conforme o esperado. Eles ajudam a identificar bugs precocemente, garantem que novas alterações não quebrem funcionalidades existentes (regressão) e aumentam a confiança no sistema.

Tipos de testes implementados ou planejados:

Testes Unitários: Validam partes isoladas do código, como métodos ou classes, garantindo que funcionem corretamente de forma independente.
Testes de Integração: Verificam a interação entre diferentes partes do sistema, como a comunicação entre a camada de negócios e a camada de acesso a dados.
Testes de Aceitação: Garantem que o sistema atenda aos requisitos do usuário final.
Testes em .NET
No .NET, utilizamos frameworks como xUnit, NUnit, ou MSTest para escrever e executar testes. Esses frameworks oferecem ferramentas para criar testes unitários e de integração, além de fornecerem recursos como asserções (Assert) para verificar os resultados esperados. No caso do WebMangaProject, foi escolhido o xUnit por sua simplicidade e integração com o Visual Studio.

Implementação no Projeto
No WebMangaProject, os testes foram implementados de forma básica para validar funcionalidades específicas, como operações de CRUD na camada de acesso a dados (DAL) e respostas padronizadas na camada Shared. Embora a cobertura de testes ainda não seja abrangente, ela serviu como um ponto de partida para não ficar com o conteúdo, os conceitos de testes estudado somente na cabeça e implementar de fato com XUNIT, configurar e ver funcionando. Assim como os demais fontes, não tem métodos muito complextos e nem fontes enormes, pois sua implementação toda é um estudo e modelo, o qual ainda será muito útil para me auxliar em outros projetos reais que trabalharei, seja de anos e com fontes enormes e datados e confusos ou clean ou novos. Adquirindo experiência.

Estrutura de Testes:
DbFixture: Configura o contexto do banco de dados para ser utilizado nos testes, garantindo que as dependências sejam injetadas corretamente.
DataAccessLayerTest: Contém testes para validar operações de CRUD em entidades como Anime, Manga e User. Por exemplo, o teste GetByFavorites_ReturnNotNull verifica se a busca por animes favoritos retorna resultados válidos.
ResponseTest: Testa a consistência das respostas padronizadas, como DataResponse e ResponseTeste, garantindo que as mensagens e os estados sejam gerados corretamente.
Embora os testes atuais sejam limitados, eles já demonstram conceitos importantes, como:

Uso de fixtures para configurar dependências.
Validação de resultados com asserções (Assert.NotNull, Assert.True).
Testes de métodos específicos, como GetByFavorites e UpdateUser.
Próximos Passos para Testes
Expandir a cobertura de testes para incluir a camada de negócios (BLL) e a WebApi.
Implementar testes de integração para validar a comunicação entre as camadas.
Adicionar testes de interface para garantir que a camada de apresentação funcione corretamente.
Utilizar ferramentas como Coverlet para medir a cobertura de código e identificar áreas que precisam de mais testes.


---

## Tecnologias Utilizadas
### 🔷 Backend
- **ASP.NET Core 6.0**
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **Azure Redis Cache**
- **SQL Server**
- **Azure**
- **JSON Web Tokens com Bearer**
- **Cookies**
- **Swagger (Swashbuckle)**
- **Log4Net**

### 🔷 Frontend
- **ASP.NET Core MVC**
- **Razor Pages**
- **Bootstrap**
- **AutoMapper**
- **Newtonsoft.Json**

---

## Funcionalidades Implementadas
### 👤 Gestão de Usuários
- Autenticação e Autorização 
- Perfis de Usuários
- Upload de avatar e imagem de capa

### 📚 Gestão de Conteúdo
- Exploração de obras no formato de um catálogo 
- Sistema de Comentários
- Favoritos
- Recomendações baseadas em preferências

---

## Benefícios da Arquitetura
### 🔗 Desacoplamento
- Cada camada é independente e pode evoluir separadamente.
- Facilita manutenção e testes.
- Permite mudanças tecnológicas isoladas.

### 📈 Reutilização de Código
- Componentes compartilhados entre camadas.
- Padrões de design reutilizáveis.
- Interfaces comuns para operações.

### ⚡ Escalabilidade
- Cada camada pode ser escalada independentemente.
- Cache distribuído com Redis.
- Arquitetura preparada para crescimento.

### 🔒 Segurança
- Autenticação centralizada com JWT.
- Validações em múltiplas camadas.
- Logging estruturado para auditoria.

---

## Como Executar
1. Instale as dependências:
   ```bash
   dotnet restore
   ```
2. Configure o banco de dados nos `appsettings.json`.
3. Execute as migrations:
   ```bash
   dotnet ef database update
   ```
4. Inicie a aplicação:
   ```bash
   dotnet run
   ```

---

## Motivação do Projeto
🎯 Por que este projeto foi criado?

O WebMangaProject foi desenvolvido com o objetivo de consolidar conhecimentos em ASP.NET Core, Entity Framework, e arquitetura em camadas, além de explorar boas práticas de desenvolvimento, como Dependency Injection, FluentValidation, e JWT. A escolha de criar um catálogo de animes e mangás surgiu da paixão por obras japonesas e da necessidade de um sistema organizado e funcional para gerenciar essas obras.

Além disso, o projeto foi uma oportunidade de aprender e aplicar conceitos avançados, como consumo de APIs externas, persistência local de dados, e autenticação baseada em tokens e cookies. A implementação de testes básicos foi motivada pelo desejo de compreender os fundamentos de testes automatizados em .NET, garantindo que o sistema seja confiável e escalável. Este projeto não apenas demonstra habilidades técnicas, mas também reflete o compromisso com aprendizado contínuo e a busca por excelência no desenvolvimento de software.

---

## Próximos Passos
🔄 Evolução do Projeto, aprender +, aplicar +

- Implementar uma rotina de atualização automática para sincronizar dados com a API externa.
- Adicionar suporte a notificações em tempo real usando SignalR.instantaneamente sobre novos animes, mangás ou interações, como comentários em suas postagens.
- Aprender e aplicar conceitos de UX.
- Mobile, aprender .NET MAUI talvez.
- Criar testes automatizados mais abrangentes para garantir a qualidade do código.
- Sistema de Recomendação Avançado: Melhorar o sistema de recomendações, utilizando algoritmos de aprendizado de máquina para sugerir animes e mangás com base no histórico e nas preferências dos usuários.
- Detalhar certinho o tópico como executar. 

--

## Referências
📚 Links Úteis

- [Migrations no Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
- [Code-First](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database)
- [Autenticação e autorização com JWT e Bearer Exemplo com Balta](https://balta.io/blog/autenticacao-e-autorizacao-com-jwt-e-bearer-no-aspnet-7)
- [Azure Redis Cache Doc](https://learn.microsoft.com/en-us/azure/azure-cache-for-redis/cache-overview)
- [Azure Redis Cache Preço](https://azure.microsoft.com/en-us/pricing/details/cache/) (Por isso tive que tirar o site do ar :(, kkkkkk)
- [xUnit Documentation](https://xunit.net/)
- [Common web application architectures](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Iniciando com o Entity Framework Core com Macoratti](https://www.macoratti.net/17/05/efcore_ini1.htm)
- [Automapper](https://automapper.org/)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [Bootstrap](https://getbootstrap.com/)
