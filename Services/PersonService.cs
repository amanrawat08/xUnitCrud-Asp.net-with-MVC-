

using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public class PersonService : IPersonService
{
    private readonly PersonsDbContext _db;
    private readonly ICountriesService _countriesService;
    public PersonService(PersonsDbContext personsDbContext, ICountriesService countriesService)
    {
        _db = personsDbContext;
        _countriesService = countriesService;
       }
    private PersonResponse ConvertPersonIntoPersonObject(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.CountryName = _countriesService.GetCountryByCountryID(personResponse.CountryID)?.CountryName;
        return personResponse;
    }
    public PersonResponse AddPerson(PersonAddResquest? personAddRequest)
    {
        // if personAddREquest is null
        if (personAddRequest == null)
        {
            throw new ArgumentNullException(nameof(personAddRequest));
        }

        // Model Validation
        ValidationHelper.ModelValidation(personAddRequest);

        // convert person add req onto person type
        Person person = personAddRequest.ToPerson();

        // Generate new guid;
        person.PersonID = Guid.NewGuid();

        // Add person to dataset
        _db.Persons.Add(person);
        // Add into persons database
        _db.SaveChanges();

        // Convert the person list to person  obje type
        return ConvertPersonIntoPersonObject(person);

    }

    public List<PersonResponse> GetAllPersonList()
    {
        return _db.Persons.ToList().Select((person) => ConvertPersonIntoPersonObject(person)).ToList();
    }

    public PersonResponse? GetPersonDetailByID(Guid? personID)
    {
        if (personID == null) return null;
        Person? person = _db.Persons.FirstOrDefault((person) => person.PersonID == personID);
        if (person == null) return null;

        return ConvertPersonIntoPersonObject(person);
    }

    public List<PersonResponse> GetFilterPersons(string? searchBy, string? searchString)
    {
        List<PersonResponse> allPerson = GetAllPersonList();
        List<PersonResponse> filterPersons = allPerson;

        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString)) return filterPersons;

        switch (searchBy)
        {
            case nameof(PersonResponse.PersonName):
                filterPersons = allPerson.Where(temp =>
            (!string.IsNullOrEmpty(temp.PersonName) ? temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                break;
            case nameof(PersonResponse.Email):
                filterPersons = allPerson.Where(temp =>
            (!string.IsNullOrEmpty(temp.Email) ? temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                break;
            case nameof(PersonResponse.Address):
                filterPersons = allPerson.Where(temp =>
            (!string.IsNullOrEmpty(temp.Address) ? temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                break;
            case nameof(PersonResponse.Gender):
                filterPersons = allPerson.Where(temp =>
            (!string.IsNullOrEmpty(temp.Gender) ? temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                break;
            case nameof(PersonResponse.CountryID):
                filterPersons = allPerson.Where(temp =>
            (!string.IsNullOrEmpty(temp.CountryName) ? temp.CountryName.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                break;
            case nameof(PersonResponse.BirthOfDate):
                filterPersons = allPerson.Where(temp =>
            (!string.IsNullOrEmpty(temp.BirthOfDate.Value.ToString("dd mm yyyy")) ? temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                break;
            default: filterPersons = allPerson; break;
        }

        return filterPersons;
    }

    public List<PersonResponse> GetSortedPerson(List<PersonResponse> allPersons, string sortBy, SortOrderEnum sortOrder)
    {
        if (string.IsNullOrEmpty(sortBy)) return allPersons;

        List<PersonResponse> sortedPersons = (sortBy, sortOrder) switch
        {
            (nameof(PersonResponse.PersonName), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.PersonName), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Email), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.BirthOfDate), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.BirthOfDate).ToList(),
            (nameof(PersonResponse.BirthOfDate), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.BirthOfDate).ToList(),
            (nameof(PersonResponse.Age), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.Age).ToList(),
            (nameof(PersonResponse.Age), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.Age).ToList(),
            (nameof(PersonResponse.Gender), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Gender), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.CountryName), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.CountryName), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.Address), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
            (nameof(PersonResponse.RecieveNewsLetter), SortOrderEnum.ASC) => allPersons.OrderBy(temp => temp.RecieveNewsLetter).ToList(),
            (nameof(PersonResponse.RecieveNewsLetter), SortOrderEnum.DESC) => allPersons.OrderByDescending(temp => temp.RecieveNewsLetter).ToList(),

            _ => allPersons
        };
        return sortedPersons;
    }

    public PersonResponse GetPersonUpdateRequest(PersonUpdateRequest? personUpdateRequest)
    {
        // ISNull
        if (personUpdateRequest == null) throw new ArgumentNullException(nameof(Person));

        // Validation
        ValidationHelper.ModelValidation(personUpdateRequest);

        // Get matching person object
        List<PersonResponse> allPerson = GetAllPersonList();

        Person? personResponse_Find = _db.Persons.FirstOrDefault((temp) => temp.PersonID == personUpdateRequest.PersonID);

        if (personResponse_Find == null) throw new ArgumentException("No User found");
        // Update all the details
        personResponse_Find.PersonName = personUpdateRequest.PersonName;
        personResponse_Find.Email = personUpdateRequest.Email;
        personResponse_Find.Address = personUpdateRequest.Address;
        personResponse_Find.RecieveNewsLetter = personUpdateRequest.RecieveNewsLetter;
        personResponse_Find.Gender = personUpdateRequest.Gender.ToString();
        personResponse_Find.CountryID = personUpdateRequest.CountryID;
        personResponse_Find.BirthOfDate = personUpdateRequest.BirthOfDate;

        _db.SaveChanges();

        return ConvertPersonIntoPersonObject(personResponse_Find);

    }

    public bool DeletePerson(Guid? personID)
    {
        if (personID == null) throw new ArgumentNullException();

        // Get matching person object
        List<PersonResponse> allPerson = GetAllPersonList();

        Person? personResponse_Find = _db.Persons.FirstOrDefault((temp) => temp.PersonID == personID);

        if (personResponse_Find == null) return false;

        _db.Persons.Remove(
            _db.Persons.First(
                (temp) => temp.PersonID == personID
                )
            );
        _db.SaveChanges();
        return true;

    }
}
