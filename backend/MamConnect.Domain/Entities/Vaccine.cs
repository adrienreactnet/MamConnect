namespace MamConnect.Domain.Entities;

public class Vaccine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Âge (en mois) auquel cette dose de vaccin est prévue.
    /// </summary>
    public int AgeInMonths { get; set; }
    public ICollection<ChildVaccine> ChildVaccines { get; set; } = new List<ChildVaccine>();
}