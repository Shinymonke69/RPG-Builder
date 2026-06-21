# RPG-Builder

Sistema web completo para criação e gerenciamento de personagens de RPG (D&D 5e), com geração automática de atributos, sistema de autenticação de contas isoladas e personalização profunda de inventário, feitiços e histórico.

## Funcionalidades

- Sistema completo de autenticação de usuários (Login/Registro) com senhas criptografadas (BCrypt).
- Isolamento de dados: cada usuário tem acesso e controle apenas sobre os seus próprios personagens.
- Criação e edição detalhada de personagens (Nome, nível, raça, classe, antecedentes, etc).
- Gerenciamento de atributos, PV (Pontos de Vida atuais e temporários), XP e lista de perícias.
- Integração com inventário e magias (Armas, Armaduras e anotações gerais).
- Geração aleatória de personagens com apenas um clique.

## Tecnologias utilizadas

- **C# & ASP.NET Core MVC**: Estrutura robusta para o back-end e rotas.
- **Entity Framework Core**: ORM para manipulação do banco de dados (SQLite).
- **Tailwind CSS**: Estilização moderna e responsiva do front-end com suporte a Dark Mode.
- **BCrypt.Net-Next**: Hashing seguro de senhas.

### Instalação e uso

1. Clone este repositório:
```bash
git clone https:
```

2. Instale as dependências e ferramentas necessárias:
- Certifique-se de ter o [.NET SDK](https:
- Restaure os pacotes NuGet do projeto:
```bash
dotnet restore
```

3. Configuração do Banco de Dados:
O projeto já está configurado para utilizar um banco de dados local SQLite (`rpgbuilder.db`). Para criar e popular as tabelas iniciais, rode o comando do Entity Framework:
```bash
dotnet ef database update
```
*(Nota: O banco de dados SQLite será criado automaticamente na pasta raiz do projeto).*

4. Rode o projeto:
Abra o terminal na pasta raiz e execute:
```bash
dotnet run
```
5. Acesse no navegador em `https:

## Dependências externas de API

Durante a inicialização ou sincronização de dados (via `SrdSyncController`), este projeto pode utilizar a API pública [D&D 5e API](https:
1. Endpoints consumidos (exemplo):
- `GET https:
- `GET https:
- O consumo é feito pelo `Dnd5eClient` usando o `HttpClient` nativo do .NET.

## Como contribuir

- Faça um fork do projeto
- Crie uma branch para a nova feature (`git checkout -b feature/minha-feature`)
- Faça commit das suas alterações (`git commit -m 'Adiciona uma nova funcionalidade'`)
- Faça um push para a branch (`git push origin feature/minha-feature`)
- Envie um pull request descrevendo sua alteração detalhadamente.

## Licença

Este projeto está licenciado sob a Licença MIT. Veja mais detalhes no arquivo LICENSE.md.

