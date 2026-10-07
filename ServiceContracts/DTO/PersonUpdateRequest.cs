
using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;

/// <summary>
/// Represent the DTO class that update the person Details
/// </summary>
public class PersonUpdateRequest
{
    public Guid PersonID{get;set;}
    [Required(ErrorMessage = "Must enter Email Address")]
    [EmailAddress(ErrorMessage = "Email Address Should Valid")]
    public String? Email { get; set; }
    [Required(ErrorMessage = "Must Enter Person Name")]
    public String? PersonName { get; set; }
    public DateTime? BirthOfDate { get; set; }
    public GenderOptions? Gender { get; set; }
    public Guid? CountryID { get; set; }
    public String? Address { get; set; }
    public bool? RecieveNewsLetter { get; set; }
    /// <summary>
    /// Converting the current person data into new person object type
    /// </summary>
    /// <returns></returns>
    
    public Person ToPerson()
    {
        return new Person()
        {
            PersonID=PersonID, 
            Address=Address,
            RecieveNewsLetter=RecieveNewsLetter,
            Email = Email,
            PersonName = PersonName,
            BirthOfDate = BirthOfDate,
            Gender = Gender.ToString(),
            CountryID = CountryID
        };
    }
}
