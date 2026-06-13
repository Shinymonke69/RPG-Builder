namespace RpgBuilderMvc.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public ICollection<Character> Characters { get; set; } = new List<Character>();
}
