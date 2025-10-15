namespace MamConnect.Domain.Entities;

public class Vaccine
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Liste des âges (en mois) où le vaccin est prévu (ex: "2,4,11").
    /// </summary>
    public string AgesInMonths { get; set; } = string.Empty;
}