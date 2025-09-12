namespace MamConnect.Domain.Entities;

public enum UserRole
{
    Parent,
    Assistant,
    Admin
}

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public UserRole Role { get; set; }

    public ICollection<Child> Children { get; set; } = new List<Child>();
    public ICollection<Child> AssignedChildren { get; set; } = new List<Child>();
}