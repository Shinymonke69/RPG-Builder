# RPG-Builder

Sistema web completo para criação e gerenciamento de personagens de RPG (D&D 5e), focado em fornecer uma experiência fluida e segura para jogadores. Possui geração automática de atributos, sistema de autenticação de contas isoladas e personalização profunda de inventário, feitiços e histórico.

---

## Funcionalidades

- Sistema completo de autenticação de usuários (Login/Registro) com senhas criptografadas (BCrypt).
- Isolamento de dados: cada usuário tem acesso e controle apenas sobre os seus próprios personagens.
- Criação e edição detalhada de personagens (Nome, nível, raça, classe, antecedentes, etc).
- Gerenciamento de atributos, PV (Pontos de Vida atuais e temporários), XP e lista de perícias.
- Integração com inventário e magias (Armas, Armaduras e anotações gerais).
- Geração aleatória de personagens com apenas um clique.

---

## Tecnologias e Versões Utilizadas

O projeto foi construído utilizando as mais recentes tecnologias do ecossistema .NET e ferramentas modernas de Front-end:

- **Back-end:** C# com ASP.NET Core MVC (.NET 10.0)
- **Banco de Dados:** SQLite
- **ORM:** Entity Framework Core (10.0.7)
- **Estilização Front-end:** Tailwind CSS
- **Segurança (Senhas):** BCrypt.Net-Next (4.2.0)

---

## Arquitetura e Organização do Projeto

O projeto segue o padrão arquitetural **MVC (Model-View-Controller)** com separação de responsabilidades em camadas, tornando o código fácil de manter, escalável e com convenções de nomes em inglês no código fonte:

- **`Controllers/`**: Responsável por receber as requisições HTTP, orquestrar a lógica junto ao banco de dados e devolver a View apropriada.
- **`Domain/Entities/`**: Contém os modelos de domínio do banco de dados (ex: `User`, `Character`, `Spell`, etc) com anotações rigorosas de validação de dados (`Data Annotations`).
- **`Views/`**: Interfaces de usuário construídas em Razor Pages (`.cshtml`).
- **`Infrastructure/`**: Contém lógicas auxiliares e de persistência, como o `RpgDbContext` e ferramentas de sincronização com APIs externas (`SrdSync`).
- **`DndApi/`**: Contém os serviços de HttpClient responsáveis por consumir dados externos.

---

## Testes da API

Este projeto consome dados da [D&D 5e API](https://www.dnd5eapi.co/) para sincronização do SRD (System Reference Document). 
Como o sistema é uma aplicação MVC pura (que renderiza HTML). O fluxo de teste da API pode ser validado através do **`SrdSyncController`**, que se comunica com a API externa para baixar magias, itens e classes ao iniciar o sistema, para isso coloque no fim do endereço do sistema a rota /SrdSync/Run . (Exemplo: https://localhost:5001/SrdSync/Run).

---

## Instalação e Uso Rápido

O projeto foi preparado para rodar direto, sem configurações complexas.

### Pré-requisitos
- Ter o **.NET SDK 10.0** instalado.
- Terminal / Prompt de Comando.

### Passo a Passo

1. **Clone este repositório:**
```bash
git clone https://github.com/Shinymonke69/RPG-Builder.git
cd RpgBuilderMvc
```

2. **Restaure os pacotes e aplique o banco de dados:**
Não é necessário se preocupar com scripts SQL! O Entity Framework fará todo o trabalho. Execute os comandos abaixo na pasta do projeto:
```bash
dotnet restore
dotnet ef database update
```
*(Um arquivo `rpgbuilder.db` será criado automaticamente na pasta raiz).*

3. **Rode o projeto:**
```bash
dotnet run
```

4. **Acesse no navegador:**
Vá até `http://localhost:5000` e coloque essa rota `/SrdSync/Run` e espere até aparecer a mensagem de sucesso.

---

## Usuário de Teste (Pronto para Uso)

Para agilizar o teste da banca sem precisar criar uma conta do zero, o sistema semeia (seed) automaticamente o seguinte usuário administrador ao rodar `dotnet run`:

- **E-mail ou Usuário:** `teste@teste.com`
- **Palavra Secreta (Senha):** `Teste123!`

Você pode usar essas credenciais diretamente na tela de Login.

---

## Como contribuir

- Faça um fork do projeto
- Crie uma branch para a nova feature (`git checkout -b feature/minha-feature`)
- Faça commit das suas alterações (`git commit -m 'Adiciona uma nova funcionalidade'`)
- Faça um push para a branch (`git push origin feature/minha-feature`)
- Envie um pull request descrevendo sua alteração detalhadamente.

---

## Licença

Este projeto está licenciado sob a Licença MIT. Veja mais detalhes no arquivo `LICENSE.md`.
