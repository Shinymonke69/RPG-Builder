namespace RpgBuilderMvc.Domain.Entities;

public class Character
{
    public int Id { get; set; }

    // Nome do personagem
    public string Name { get; set; } = null!;

    // Nível
    public int Level { get; set; }

    // Status simples (ex.: Ativo, Inativo, Favorito)
    public string Status { get; set; } = "Ativo";

    // FK para Classe
    public string ClassIndex { get; set; } = null!;
    public string ClassName { get; set; } = null!;

    // FK para Raça
    public string RaceIndex { get; set; } = null!;
    public string RaceName { get; set; } = null!;

    // FK para Background (Antecedente)
    public string BackgroundIndex { get; set; } = null!;
    public string BackgroundName { get; set; } = null!;

    // Por enquanto, não vamos ligar Traits/Spells diretamente,
    // isso pode vir depois com relacionamentos.
}