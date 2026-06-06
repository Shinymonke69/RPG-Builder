namespace RpgBuilderMvc.Services;

public static class NameGenerator
{
    private static readonly string[] FirstNames =
    {
        "Aldric", "Elora", "Thorin", "Lyria", "Draven",
        "Kael", "Seraphina", "Garruk", "Isolde", "Roderic"
    };

    private static readonly string[] LastNames =
    {
        "Stormborn", "Nightbreeze", "Ironfist", "Dawnguard",
        "Shadowbane", "Brightwood", "Ravenscar", "Dragonsong"
    };

    private static readonly Random random = new();

    public static string GenerateFullName()
    {
        var first = FirstNames[random.Next(FirstNames.Length)];
        var last = LastNames[random.Next(LastNames.Length)];
        return $"{first} {last}";
    }
}
