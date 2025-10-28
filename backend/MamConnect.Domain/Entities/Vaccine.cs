using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MamConnect.Domain.Entities;

public class Vaccine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Age (en mois) auquel cette dose de vaccin est prevue.
    /// </summary>
    public int AgeInMonths { get; set; }

    [JsonIgnore]
    public ICollection<ChildVaccine> ChildVaccines { get; set; } = new List<ChildVaccine>();
}
