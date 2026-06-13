using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.DndApi;
using RpgBuilderMvc.Infrastructure.Persistence;
using RpgBuilderMvc.Infrastructure.Sync;
using RpgBuilderMvc.Domain.Entities;


var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<RpgDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient D&D API
builder.Services.AddHttpClient<Dnd5eClient>();

// EquipmentImporter
builder.Services.AddScoped<EquipmentImporter>();

// Srd
builder.Services.AddScoped<SrdImporter>();

// MVC
builder.Services.AddControllersWithViews();

// Authentication
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.Cookie.Name = "RpgBuilderSession";
    });

var app = builder.Build();

var skills = new[]
{
    new Skill { Index = "athletics", Name = "Atletismo", Ability = "STR" },

    new Skill { Index = "acrobatics", Name = "Acrobacia", Ability = "DEX" },
    new Skill { Index = "sleight-of-hand", Name = "Prestidigitação", Ability = "DEX" },
    new Skill { Index = "stealth", Name = "Furtividade", Ability = "DEX" },

    new Skill { Index = "arcana", Name = "Arcanismo", Ability = "INT" },
    new Skill { Index = "history", Name = "História", Ability = "INT" },
    new Skill { Index = "investigation", Name = "Investigação", Ability = "INT" },
    new Skill { Index = "nature", Name = "Natureza", Ability = "INT" },
    new Skill { Index = "religion", Name = "Religião", Ability = "INT" },

    new Skill { Index = "animal-handling", Name = "Adestrar Animais", Ability = "WIS" },
    new Skill { Index = "insight", Name = "Intuição", Ability = "WIS" },
    new Skill { Index = "medicine", Name = "Medicina", Ability = "WIS" },
    new Skill { Index = "perception", Name = "Percepção", Ability = "WIS" },
    new Skill { Index = "survival", Name = "Sobrevivência", Ability = "WIS" },

    new Skill { Index = "deception", Name = "Enganação", Ability = "CHA" },
    new Skill { Index = "intimidation", Name = "Intimidação", Ability = "CHA" },
    new Skill { Index = "performance", Name = "Atuação", Ability = "CHA" },
    new Skill { Index = "persuasion", Name = "Persuasão", Ability = "CHA" },
};
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<RpgDbContext>();

    if (!db.Skills.Any())
    {
        db.Skills.AddRange(skills);
        db.SaveChanges();
    }

    var customBackgrounds = new[]
    {
        new Background
        {
            Index = "acolito",
            Name = "Acólito",
            Feature = "Acesso a templos e serviços religiosos ligados à sua fé.",
            Skills = "Intuição, Religião",
            Languages = "Duas línguas adicionais à sua escolha",
            Tools = "",
            PersonalityTraits = "Profundamente devoto; calmo diante da adversidade.",
            Ideals = "Caridade, fé e serviço aos necessitados.",
            Bonds = "Meu templo e minha fé são mais importantes do que qualquer coisa.",
            Flaws = "Tendo a julgar quem não compartilha da minha fé."
        },
        new Background
        {
            Index = "andarilho",
            Name = "Andarilho",
            Feature = "Sempre encontra comida e abrigo básicos ao viajar por terras selvagens.",
            Skills = "Sobrevivência, Percepção",
            Languages = "Uma língua adicional de povos que encontrou em viagem",
            Tools = "Instrumento musical simples ou ferramenta de navegação simples",
            PersonalityTraits = "Prefere a estrada a ficar parado; observa mais do que fala.",
            Ideals = "Liberdade e curiosidade sobre o mundo.",
            Bonds = "Tem um lugar específico para onde sempre retorna.",
            Flaws = "Dificuldade em confiar em autoridades locais."
        },
        new Background
        {
            Index = "artesao",
            Name = "Artesão",
            Feature = "Acesso a oficinas e guildas de artesãos, facilitando encomendas e reparos.",
            Skills = "Intuição, Persuasão",
            Languages = "",
            Tools = "Ferramentas de artesão à escolha",
            PersonalityTraits = "Perfeccionista com seu trabalho; fala com carinho de suas criações.",
            Ideals = "Trabalho bem feito é uma forma de arte.",
            Bonds = "Deve tudo à guilda ou à oficina onde aprendeu.",
            Flaws = "Relutante em admitir falhas em seu próprio ofício."
        },
        new Background
        {
            Index = "artista",
            Name = "Artista",
            Feature = "Sempre consegue se apresentar para conseguir abrigo simples e comida.",
            Skills = "Atuação, Persuasão",
            Languages = "",
            Tools = "Instrumento musical ou ferramentas de artista",
            PersonalityTraits = "Dramático; vê poesia em tudo.",
            Ideals = "Liberdade artística acima de regras sociais.",
            Bonds = "Devoto a um patrono, mecenas ou público específico.",
            Flaws = "Tende a exagerar histórias e emoções."
        },
        new Background
        {
            Index = "charlatao",
            Name = "Charlatão",
            Feature = "Possui identidades falsas e sempre encontra um jeito de aplicar pequenos golpes.",
            Skills = "Enganação, Prestidigitação",
            Languages = "",
            Tools = "Ferramentas de trapaceiro, kit de disfarce",
            PersonalityTraits = "Charmoso, mas sempre com segundas intenções.",
            Ideals = "Aproveitar qualquer oportunidade, não importa a moral.",
            Bonds = "Sente certo carinho por quem caiu em seus golpes, mas não admite.",
            Flaws = "Não resiste a um 'negócio fácil'."
        },
        new Background
        {
            Index = "criminoso",
            Name = "Criminoso",
            Feature = "Contato confiável no submundo criminoso.",
            Skills = "Furtividade, Enganação",
            Languages = "",
            Tools = "Ferramentas de ladrão, um jogo de azar",
            PersonalityTraits = "Sempre observa saídas; desconfiado de todos.",
            Ideals = "Lealdade ao grupo acima de qualquer lei.",
            Bonds = "Deve um favor perigoso a alguém do submundo.",
            Flaws = "Tendência a resolver tudo de forma ilícita."
        },
        new Background
        {
            Index = "eremita",
            Name = "Eremita",
            Feature = "Descoberta pessoal ou segredo místico que pode ser relevante para o mundo.",
            Skills = "Religião, Medicina",
            Languages = "Uma língua adicional aprendida em estudos solitários",
            Tools = "Kit de herbalismo",
            PersonalityTraits = "Pouco acostumado a convívio social; introspectivo.",
            Ideals = "Conhecimento e contemplação acima de bens materiais.",
            Bonds = "Profundamente ligado ao lugar onde viveu isolado.",
            Flaws = "Tende a se perder em pensamentos e visões."
        },
        new Background
        {
            Index = "escriba",
            Name = "Escriba",
            Feature = "Acesso a bibliotecas e arquivos, facilitando pesquisa de informações.",
            Skills = "História, Arcanismo",
            Languages = "Duas línguas adicionais usadas em textos antigos",
            Tools = "Ferramentas de calígrafo",
            PersonalityTraits = "Anota tudo; curioso sobre fatos e datas.",
            Ideals = "A preservação do conhecimento é sagrada.",
            Bonds = "Protege um conjunto específico de livros ou pergaminhos.",
            Flaws = "Tende a ignorar o perigo quando há algo novo para estudar."
        },
        new Background
        {
            Index = "fazendeiro",
            Name = "Fazendeiro",
            Feature = "Conhecimento de colheitas, animais e clima local; fácil conseguir abrigo simples em vilas rurais.",
            Skills = "Sobrevivência, Animal Handling",
            Languages = "",
            Tools = "Ferramentas de fazendeiro simples",
            PersonalityTraits = "Trabalhador, direto e prático.",
            Ideals = "Vida simples, mas honesta.",
            Bonds = "Família e terras de origem são tudo para ele.",
            Flaws = "Desconfiança de nobres e gente da cidade."
        },
        new Background
        {
            Index = "guarda",
            Name = "Guarda",
            Feature = "Conhece rotinas de segurança, guardas locais e protocolos de patrulha.",
            Skills = "Atletismo, Percepção",
            Languages = "",
            Tools = "Algum emblema ou símbolo de autoridade antigo",
            PersonalityTraits = "Senso forte de dever; atento a comportamentos suspeitos.",
            Ideals = "Ordem e proteção dos inocentes.",
            Bonds = "Ainda sente responsabilidade por um distrito ou local específico.",
            Flaws = "Tendência a ver tudo como potencial ameaça."
        },
        new Background
        {
            Index = "guia",
            Name = "Guia",
            Feature = "Conhece rotas seguras, atalhos e perigos naturais na região onde atua.",
            Skills = "Sobrevivência, Natureza",
            Languages = "Uma língua adicional de um povo local",
            Tools = "Ferramentas de navegação simples",
            PersonalityTraits = "Calmo em situações difíceis; confia em sua experiência.",
            Ideals = "Ajudar viajantes a chegar vivos ao destino.",
            Bonds = "Uma rota ou região específica é seu orgulho.",
            Flaws = "Subestima perigos que já enfrentou muitas vezes."
        },
        new Background
        {
            Index = "marinheiro",
            Name = "Marinheiro",
            Feature = "Sempre encontra passagens em navios ou abrigo em docas e portos.",
            Skills = "Acrobacia, Percepção",
            Languages = "",
            Tools = "Ferramentas de navegador, veículos aquáticos",
            PersonalityTraits = "Brincalhão, supersticioso sobre o mar.",
            Ideals = "Liberdade e aventura nas ondas.",
            Bonds = "Lealdade ao antigo navio ou tripulação.",
            Flaws = "Tende a beber demais quando em terra firme."
        },
        new Background
        {
            Index = "mercador",
            Name = "Mercador",
            Feature = "Rede de contatos comerciais, facilitando trocas e informações de mercado.",
            Skills = "Persuasão, Intuição",
            Languages = "Uma língua adicional útil para comércio",
            Tools = "Ferramentas de comerciante, balança de viagem",
            PersonalityTraits = "Sempre calculando lucro e prejuízo.",
            Ideals = "Oportunidade em qualquer lugar.",
            Bonds = "Tem uma rota de comércio favorita ou um parceiro de longa data.",
            Flaws = "Tende a colocar lucro acima de relações pessoais."
        },
        new Background
        {
            Index = "nobre",
            Name = "Nobre",
            Feature = "Status social elevado que abre portas em círculos aristocráticos.",
            Skills = "História, Persuasão",
            Languages = "Uma língua adicional de cortes ou diplomacia",
            Tools = "Ferramentas de jogo de nobre",
            PersonalityTraits = "Elegante, acostumado a ser ouvido.",
            Ideals = "Responsabilidade sobre os que vivem em suas terras.",
            Bonds = "Família e título são o centro de sua identidade.",
            Flaws = "Tende a subestimar pessoas comuns."
        },
        new Background
        {
            Index = "sabio",
            Name = "Sábio",
            Feature = "Sabe onde e como encontrar informações em bibliotecas e círculos acadêmicos.",
            Skills = "História, Arcanismo",
            Languages = "Duas línguas adicionais estudadas",
            Tools = "",
            PersonalityTraits = "Curioso, faz perguntas demais.",
            Ideals = "Conhecimento por si só é um fim.",
            Bonds = "Devoto a um mestre, ordem ou instituição de estudo.",
            Flaws = "Tende a ser distraído com problemas práticos."
        },
        new Background
        {
            Index = "soldado",
            Name = "Soldado",
            Feature = "Reconhecido em instalações militares, pode requisitar pequenos favores de ex-colegas.",
            Skills = "Atletismo, Intimidação",
            Languages = "",
            Tools = "Ferramentas de jogo, veículos terrestres",
            PersonalityTraits = "Disciplina rígida; postura séria.",
            Ideals = "Honra e lealdade aos companheiros.",
            Bonds = "Nunca abandona um aliado em combate.",
            Flaws = "Tem dificuldade em aceitar ordens de civis ou autoridades fracas."
        }
    };

    var srd = scope.ServiceProvider.GetRequiredService<SrdImporter>();
    await srd.ImportAllAsync();

    // ── StartingEquipment por classe ─────────────────────────
    var barbarianEquip = new[]
    {
        "Pacote de explorador"
    };
    var bardEquip = new[]
    {
        "Pacote de diplomata ou de artista",
        "Um instrumento musical à escolha"
    };
    var clericEquip = new[]
    {
        "Pacote de sacerdote",
        "Símbolo sagrado"
    };
    var druidEquip = new[]
    {
        "Pacote de explorador",
        "Foco druídico"
    };
    var fighterEquip = new[]
    {
        "Pacote de explorador"
    };
    var monkEquip = new[]
    {
        "Pacote de explorador",
        "Roupas de monge"
    };
    var paladinEquip = new[]
    {
        "Pacote de sacerdote",
        "Símbolo sagrado"
    };
    var rangerEquip = new[]
    {
        "Pacote de explorador"
    };
    var rogueEquip = new[]
    {
        "Pacote de ladrão",
        "Kit de ladrão"
    };
    var sorcererEquip = new[]
    {
        "Foco arcano",
        "Pacote de explorador"
    };
    var warlockEquip = new[]
    {
        "Foco arcano",
        "Pacote de estudioso"
    };
    var wizardEquip = new[]
    {
        "Bolsa de componentes",
        "Pacote de estudioso",
        "Livro de magias"
    };

    var classEquipments = new Dictionary<string, string>
    {
        ["barbarian"] = string.Join("\n", barbarianEquip),
        ["bard"] = string.Join("\n", bardEquip),
        ["cleric"] = string.Join("\n", clericEquip),
        ["druid"] = string.Join("\n", druidEquip),
        ["fighter"] = string.Join("\n", fighterEquip),
        ["monk"] = string.Join("\n", monkEquip),
        ["paladin"] = string.Join("\n", paladinEquip),
        ["ranger"] = string.Join("\n", rangerEquip),
        ["rogue"] = string.Join("\n", rogueEquip),
        ["sorcerer"] = string.Join("\n", sorcererEquip),
        ["warlock"] = string.Join("\n", warlockEquip),
        ["wizard"] = string.Join("\n", wizardEquip)
    };

    foreach (var kvp in classEquipments)
    {
        var classIndex = kvp.Key;
        var equipmentText = kvp.Value;

        var cls = db.Classes.FirstOrDefault(c => c.Index == classIndex);
        if (cls != null)
        {
            cls.StartingEquipment = equipmentText;
        }
    }


    var customIndexes = customBackgrounds.Select(b => b.Index).ToHashSet();
    var toRemove = db.Backgrounds.Where(b => !customIndexes.Contains(b.Index)).ToList();
    if (toRemove.Count > 0)
        db.Backgrounds.RemoveRange(toRemove);

    foreach (var bg in customBackgrounds)
    {
        var existing = db.Backgrounds.FirstOrDefault(b => b.Index == bg.Index);
        if (existing == null)
        {
            db.Backgrounds.Add(bg);
        }
        else
        {
            existing.Name = bg.Name;
            existing.Feature = bg.Feature;
            existing.Skills = bg.Skills;
            existing.Languages = bg.Languages;
            existing.Tools = bg.Tools;
            existing.PersonalityTraits = bg.PersonalityTraits;
            existing.Ideals = bg.Ideals;
            existing.Bonds = bg.Bonds;
            existing.Flaws = bg.Flaws;
        }
    }
    db.SaveChanges();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();