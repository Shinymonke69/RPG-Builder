namespace RpgBuilderMvc.Domain.Entities;

public class Background
{
    public int Id { get; set; }

    public string Index { get; set; } = null!;
    public string Name { get; set; } = null!;

    
    public string Feature { get; set; } = "";

    
    public string Skills { get; set; } = "";          
    public string Languages { get; set; } = "";
    public string Tools { get; set; } = "";

    
    public string PersonalityTraits { get; set; } = "";
    public string Ideals { get; set; } = "";
    public string Bonds { get; set; } = "";
    public string Flaws { get; set; } = "";
}