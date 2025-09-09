using System;

namespace MamConnect.Domain.Entities;

public class Child
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public DateOnly BirthDate { get; set; }

    public int? AssistantId { get; set; }
    public User? Assistant { get; set; }
    public ICollection<User> Parents { get; set; } = new List<User>();
}
