using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO;
/// <summary>
/// Acts as DTO for inserting a person
/// </summary>
public class PersonAddResquest
{


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
    /// Converting the person data into new person object
    /// </summary>
    /// <returns></returns>
    public Person ToPerson()
    {
        return new Person()
        {
            Email = Email,
            Address = Address,
            PersonName = PersonName,
            BirthOfDate = BirthOfDate,
            Gender = Gender.ToString(),
            RecieveNewsLetter = RecieveNewsLetter,
            CountryID = CountryID
        };
    }
}
