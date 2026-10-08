

using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services;

public class PersonService : IPersonService
{
    private readonly List<Person>? _personList;
    private readonly ICountriesService _countriesService;
    public PersonService(bool initiaze = true)
    {
        _personList = new List<Person>();
        _countriesService = new CountriesService();
        if (initiaze)
        {
            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Zeb",
                Address = "Suite 100",
                Email = "zkirckman1@a8.net",
                BirthOfDate = Convert.ToDateTime("2004-07-09"),
                Gender = "Male",
                RecieveNewsLetter = true,
                CountryID=Guid.Parse("bd9f45ca-5b91-474b-bbe8-e0fc6733ea52")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Noellyn",
                Address = "Apt 38",
                Email = "nhaddy2@uiuc.edu",
                BirthOfDate = Convert.ToDateTime("2001-03-26"),
                Gender = "Female",
                RecieveNewsLetter = false,
                CountryID=Guid.Parse("19c5e843-4a90-4e35-b3e2-acae4390d53a")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Robenia",
                Address = "5th Floor",
                Email = "rbedham3@instagram.com",
                BirthOfDate = Convert.ToDateTime("2002-08-29"),
                Gender = "Female",
                RecieveNewsLetter = false,
                CountryID=Guid.Parse("6fc237ee-fac4-47be-98ab-ea83b5f11364")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Angelia",
                Address = "17th Floor",
                Email = "abarnewall4@forbes.com",
                BirthOfDate = Convert.ToDateTime("2003-05-05"),
                Gender = "Female",
                RecieveNewsLetter = false,
                CountryID=Guid.Parse("d775b3ec-f294-4edd-9855-9d3872f9cd75")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Edin",
                Address = "PO Box 30800",
                Email = "emeigh5@rediff.com",
                BirthOfDate = Convert.ToDateTime("2004-02-25"),
                Gender = "Female",
                RecieveNewsLetter = true,
                CountryID=Guid.Parse("23c25a89-a1ac-48ba-a999-ca9fc532919d")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Dar",
                Address = "PO Box 78677",
                Email = "daiskrigg6@biblegateway.com",
                BirthOfDate = Convert.ToDateTime("2004-09-02"),
                Gender = "Male",
                RecieveNewsLetter = false,
                CountryID=Guid.Parse("19c5e843-4a90-4e35-b3e2-acae4390d53a")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Daune",
                Address = "11th Floor",
                Email = "drichardson7@posterous.com",
                BirthOfDate = Convert.ToDateTime("2002-10-16"),
                Gender = "Female",
                RecieveNewsLetter = false,
                CountryID=Guid.Parse("bd9f45ca-5b91-474b-bbe8-e0fc6733ea52")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Levi",
                Address = "PO Box 44931",
                Email = "lkettell8@cbsnews.com",
                BirthOfDate = Convert.ToDateTime("2000-12-14"),
                Gender = "Male",
                RecieveNewsLetter = true,
                CountryID=Guid.Parse("c4d34a2f-6b81-49c4-9828-f437ac751ba0")
            });

            _personList.Add(new Person()
            {
                PersonID = Guid.NewGuid(),
                PersonName = "Drusilla",
                Address = "PO Box 74515",
                Email = "dtesseyman9@forbes.com",
                BirthOfDate = Convert.ToDateTime("2003-02-21"),
                Gender = "Female",
                RecieveNewsLetter = true,
                CountryID=Guid.Parse("6fc237ee-fac4-47be-98ab-ea83b5f11364")
            });
        }
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

        // Add person 
        _personList.Add(person);

        // Convert the person list to person  obje type
        return ConvertPersonIntoPersonObject(person);

    }

    public List<PersonResponse> GetAllPersonList()
    {
        return _personList.Select((person) => ConvertPersonIntoPersonObject(person)).ToList();
    }

    public PersonResponse? GetPersonDetailByID(Guid? personID)
    {
        if (personID == null) return null;
        Person? person = _personList.FirstOrDefault((person) => person.PersonID == personID);
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

        Person? personResponse_Find = _personList.FirstOrDefault((temp) => temp.PersonID == personUpdateRequest.PersonID);

        if (personResponse_Find == null) throw new ArgumentException("No User found");
        // Update all the details
        personResponse_Find.PersonName = personUpdateRequest.PersonName;
        personResponse_Find.Email = personUpdateRequest.Email;
        personResponse_Find.Address = personUpdateRequest.Address;
        personResponse_Find.RecieveNewsLetter = personUpdateRequest.RecieveNewsLetter;
        personResponse_Find.Gender = personUpdateRequest.Gender.ToString();
        personResponse_Find.CountryID = personUpdateRequest.CountryID;
        personResponse_Find.BirthOfDate = personUpdateRequest.BirthOfDate;

        return ConvertPersonIntoPersonObject(personResponse_Find);

    }

    public bool DeletePerson(Guid? personID)
    {
        if (personID == null) throw new ArgumentNullException();

        // Get matching person object
        List<PersonResponse> allPerson = GetAllPersonList();

        Person? personResponse_Find = _personList.FirstOrDefault((temp) => temp.PersonID == personID);

        if (personResponse_Find == null) return false;

        _personList.RemoveAll((temp) => temp.PersonID == personID);

        return true;

    }
}
