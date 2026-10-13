using System.ComponentModel.DataAnnotations;

namespace Entities;
/// <summary>
/// Domian model for storing country details
/// </summary>
public class Country
{
    [Key]
    public Guid CountryID {get;set;}
    public string? CountryName{get;set;}
}
