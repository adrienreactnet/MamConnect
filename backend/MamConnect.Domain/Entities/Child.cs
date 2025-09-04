using System;

namespace MamConnect.Domain.Entities;

public class Child
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public DateOnly BirthDate { get; set; }
}
