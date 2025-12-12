# 📜 Gestão de Bibliotecas #
API criada através de um desafio proposto pela Siemens

# 📋 Tecnologias Utilizadas #
Sobre o projeto, o mesmo foi arquitetado visanso boas praticas aprendias ao longo da minha trajetória profissional.

* Padrões da Clean Architecture
* ASP.NET Core Web Api 8
* Entity Framework Core 8
* Swagger
* SQL Server 2019 local
* AutoMapper
* XUnit e Moq para testes unitários

# 📑 Padrões e Princípios Utilizados #

Todos os padrões e princípios tiveram sua importância para compor o projeto de fácil manutenção e implementação de novas features, segregando suas responsabilidades e deixando o projeto com alta coesão e um aclopamento baixo.

* <b>SOLID:</b> É o principal de todos, está relacionado diretamente a programação orientada a objeto.
  Ele nos ajuda a segregar nossas resposnabilidades do projeto, criar novas features coesas extendendo-as quando for necessário, definir corretamente as heranças entre os objetos, definir os contratos (Interfaces) específicos para cada classe, e desaclopamento entre as camadas e objetos.

  Um repositório que detalha mais sobre os princípios SOLID: <a href="https://github.com/EduardoPires/SOLID">SOLID</a>

  
* <b>KISS:</b> (Keep It Simple, Stupid) Esse é um pattern bastante importante pois traz a simplicidade para o nosso dia dia, a complexidade deve estar somente onde é necessária e nada alem disso. <br/>
  Isso vai além dos códigos em si, na nossa análise e todo processo que engloba o desenvolvimento de software.
  Resolver os problemas de forma simples nós traz eficiência e rápido entendimento daquele contexto por outros profissionais da equipe. <br/>
  Um artigo que detalha mais sobre o pattern: <a href="https://www.interaction-design.org/literature/article/kiss-keep-it-simple-stupid-a-design-principle" target="_blank">KISS</a> 
  
* <b>DRY:</b> (Don't Repeat Yourself) É algo que já esta intricico e que comumente já fazemos no dia dia, e está aqui pra reforçar essa prática.
  Seu proósito é basicamente evitar repetição de código em seu projeto, abstraindo, extendendo, unificando alguns desses pontos.
  Isso denpenderá diretamente em que contexto ele se encontra, a forma como a unificação acontecerá. <br/>

  Um artigo que detalha mais sobre o pattern: <a href="https://medium.com/@rafaelsouzaim/n%C3%A3o-se-repita-dry-dont-repeat-yourself-40da33289bcf">DRY</a> 
  
* <b>AAA:</b> (Arrange, Act e Assert) Esse pattern foi feito para suportar a criação dos nossos testes de unidade. Sua utilização e benefício se da pela organização dos contextos de testes, sendo separando por três conceitos básicos, que são respectivamente:<br/>
	* <b>Arrange:</b> Tudo que eu preciso instanciar para criar o meu teste.<br/>
  	* <b>Act:</b> A chamada do metodo em questão que irei testar passando os meus objetos criados no Arrange.<br/>
  	* <b>Assert:</b> Onde deverá ficar tudo que necessito para validar se aquele retorno foi realmente o esperado. Eles podem ser separados por comentário, isso não é algo ruim pois diferente do codigo em si o teste de unidade possui particularidades que devem ser seguidas como deixa-lo o mais explicito possível.
  <br/>
  Um artigo que detalha amis sobre o pattern: <a href="https://medium.com/@pjbgf/title-testing-code-ocd-and-the-aaa-pattern-df453975ab80">AAA</a>
  <br/><br/>

# 📝 Camadas da Aplicação #

* ### GestaoBiblioteca.Api:
  Essa camada é basicamente a API, nela contém toda a lógica da aplicação em si, é basicamente a porta de entrada do Core. A mesma possui dependência das camadas de Infrastructure, Services, Core e Helpers. <br/>
  A camada em si define classes de configurações de bibliotecas, acessos de serviços externos, como demais configurações que são importantes para o funcionamento correto da mesma.

* ### GestaoBiblioteca.Core:
  Essa camada contém toda a lógica de negócios da API, modelos de domínio, serviços, entidades, interfaces e abstrações que definem o comportamento do sistema atgravés das depedências das outras camadas que a mesma possui referência.
  
* ### GestaoBiblioteca.Helpers:
  Essa camada contém classes de utilitários, classes de ajuda, métodos de extensões que não se enquadram diretamente na lógica de negócios, mas que oferecem suporte para várias operações na aplicação. <br/>
  Essa camada pode ser usada por outras camadas da API para evitar a repetição de código e fornecer funcionalidades reutilizáveis para a mesma.
  
* ### GestaoBiblioteca.Infrastructure.SqlServer:
  Essa camada possio as diretrizes e configurações para acesso ao banco de dados utilizado na API. Em si, contém classes e interfaces que usamos para acessar recursos externos, como sistemas de arquivos, serviços da Web, banco de dados, integrações, e assim por diante.

* ### GestaoBiblioteca.Services:
  Essa camada é basicamente a nossa camada de negócio, através dela implementamos todas as classes e interfaces para posteriores acessos aos repositórios de dados.

* ### GestaoBiblioteca.Tests:
  Por fim, a camada de Tests é onde realizamos todos os cenários de testes do projeto e suas funcionalidades em si, através de testes de unidade usando a biblioteca xUnit.
  <br/><br/>

  # 📝 Instruções Para Testes #
1. Clone o repositório para uma pasta específica do seu computador
2. Abra a pasta do projeto clonada e execute o script contido dentro do diretório /Documents/Scripts Banco Dados/ CriacaoPreparoBancoTabelas.sql, em um banco SQL Server.
3. Dentro da pasta do projeto clonado, navegue até /Source/backend e abra o aquivo de solução "GestaoBiblioteca.sln" pelo Visual Studio 2022 ou anterior.
4. Após abertura, na camada "GestaoBiblioteca.Api", localize o arquivo de appsettings (appsettings.json) e altere a string de conexão com base nos parametros de Servidor, Usuário e Senha.
5. Feito isso, compile o projeto através dos comandos Ctrl +Shift + B ou execute o mesmo para disponibilizar a API no ar.
6. Na mesma pasta do projeto clonado, volte até a pasta /Source/frontend e abra a mesma usando o Visual Studio Code.
7. Após abrir com o mesmo, abra um terminal e execute os comandos: npm run build e logo depois nmp start.
8. Feito isso, a api local estará sendo executada na rota: "https://localhost:7252", e o front end, estará sendo executado na rota "http://localhost:3000/home".
9. Ao abrir a interface do front pela primeira vez, será mostrado um card com contador das entidades cadastradas no banco de dados. Na parte superior direito, conterá um menu com as opções conforme o desafio.
10. Clique sobre cada menu (Autores, Gêneros, Livros) e execute as operações de CRUD conforme o desafio. Ao clicar sobre o menu "Início", o mesmo leva para a página inicial contendo os contadores das entidades.