# RPG-Builder (D&D Vault)

Sistema web completo para criação e gerenciamento de personagens de RPG (D&D 5e), focado em fornecer uma experiência fluida e segura para jogadores. Possui geração automática de atributos, sistema de autenticação de contas isoladas e personalização profunda de inventário, feitiços e histórico.

---

## 🛠️ Tecnologias e Versões Utilizadas

O projeto foi construído utilizando as mais recentes tecnologias do ecossistema .NET e ferramentas modernas de Front-end:

- **Back-end:** C# com ASP.NET Core MVC (.NET 10.0)
- **Banco de Dados:** SQLite
- **ORM:** Entity Framework Core (10.0.7)
- **Estilização Front-end:** Tailwind CSS
- **Segurança (Senhas):** BCrypt.Net-Next (4.2.0)
- **Geração Aleatória:** RandomNameGeneratorNG (2.0.2)

---

## 🏗️ Arquitetura e Organização do Projeto

O projeto segue o padrão arquitetural **MVC (Model-View-Controller)** com separação de responsabilidades em camadas, tornando o código fácil de manter, escalável e com convenções de nomes em inglês no código fonte:

- **`Controllers/`**: Responsável por receber as requisições HTTP, orquestrar a lógica junto ao banco de dados e devolver a View apropriada.
- **`Domain/Entities/`**: Contém os modelos de domínio do banco de dados (ex: `User`, `Character`, `Spell`, etc) com anotações rigorosas de validação de dados (`Data Annotations`).
- **`Views/`**: Interfaces de usuário construídas em Razor Pages (`.cshtml`), com mensagens, formulários e interações traduzidas para o Português (PT-BR) visando acessibilidade para os usuários.
- **`Infrastructure/`**: Contém lógicas auxiliares e de persistência, como o `RpgDbContext` e ferramentas de sincronização com APIs externas (`SrdSync`).
- **`DndApi/`**: Contém os serviços de HttpClient responsáveis por consumir dados externos.

> **Nota de Validação:** O projeto utiliza Data Annotations robustos tanto no Back-end quanto refletidos no Front-end, garantindo a integridade dos dados inseridos pelos usuários.

---

## 🌐 Testes da API (Integração Externa)

Este projeto consome dados da [D&D 5e API](https://www.dnd5eapi.co/) para sincronização do SRD (System Reference Document). 
Como o sistema é uma aplicação MVC pura (que renderiza HTML). O fluxo de teste da API pode ser validado através do **`SrdSyncController`**, que se comunica com a API externa para baixar magias, itens e classes ao iniciar o sistema, para isso coloque no fim do endereço do sistema a rota /SrdSync/Run . (Exemplo: https://localhost:5001/SrdSync/Run).

---

## 🚀 Instalação e Uso Rápido (Para Professores)

O projeto foi preparado para rodar "direto da caixa", sem configurações complexas.

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
Vá até `http://localhost:5000` ou `https://localhost:5001`.

---

## 👤 Usuário de Teste (Pronto para Uso)

Para agilizar o teste da banca sem precisar criar uma conta do zero, o sistema semeia (seed) automaticamente o seguinte usuário administrador ao rodar `dotnet run`:

- **E-mail ou Usuário:** `teste@teste.com`
- **Palavra Secreta (Senha):** `Teste123!`

Você pode usar essas credenciais diretamente na tela de Login.

---

## ✨ Funcionalidades Principais Implementadas

- **Autenticação Segura:** Criação de conta e login com hash BCrypt, garantindo que usuários acessem apenas os seus personagens.
- **Botão Mostrar Senha e Validação (Nova Feature):** A tela de registro agora exibe os erros sem apagar o que o usuário digitou, possui botão "Mostrar Senha" e exige o mínimo de 8 caracteres.
- **Prevenção de Perda de Dados (Nova Feature):** O sistema alerta o usuário caso ele tente sair das páginas de `Criar` ou `Editar` Personagem sem salvar as alterações.
- **Exclusão Segura (Nova Feature):** Um modal customizado moderno é exibido ao tentar excluir um personagem, evitando exclusões acidentais.
- **Gerador Automático:** Geração de nomes, raças e atributos de forma completamente aleatória.

---

## 📝 Licença

Este projeto está licenciado sob a Licença MIT. Veja mais detalhes no arquivo `LICENSE.md`.
