using System;

namespace MamConnect.Domain.Entities;
using System.Collections.Generic;

public class Child
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public DateOnly BirthDate { get; set; }

    public int? AssistantId { get; set; }
    public User? Assistant { get; set; }
    public ICollection<User> Parents { get; set; } = new List<User>();
    public ICollection<ChildVaccine> ChildVaccines { get; set; } = new List<ChildVaccine>();
}
