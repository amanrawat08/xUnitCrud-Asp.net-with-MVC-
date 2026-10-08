using System;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace CRUDTests;

public class PersonServiceTest
{
    // 
    private readonly IPersonService _personService;
    private readonly ICountriesService _countriesService;
    private readonly ITestOutputHelper _testOutputHelper1;

    public PersonServiceTest(ITestOutputHelper testOutputHelper)
    {
        _personService = new PersonService(false);
        _countriesService = new CountriesService(false);
        _testOutputHelper1 = testOutputHelper;
    }

    #region Add Person 
    [Fact]
    // when we supply null value, it should throw ArgumentExecption
    public void AddPerson_NullValue()
    {
        // Arrange
        PersonAddResquest? personAddResquest = null;

        // Act
        Assert.Throws<ArgumentNullException>(() => _personService.AddPerson(personAddResquest));

    }
    [Fact]
    // when we supply null value as PeronName, it should throw ArgumentExecption
    public void AddPerson_PersonNameIsNull()
    {
        // Arrange
        PersonAddResquest? personAddResquest = new PersonAddResquest()
        {
            PersonName = null
        };

        // Act
        Assert.Throws<ArgumentException>(() => _personService.AddPerson(personAddResquest));

    }
    [Fact]
    // when we supply Proper person details, it should insert into the person list
    public void AddPerson_PersonDetails()
    {
        // Arrange
        PersonAddResquest? personAddResquest = new PersonAddResquest()
        {
            PersonName = "Aman",
            Address = "Delhi",
            BirthOfDate = DateTime.Now,
            CountryID = Guid.NewGuid(),
            Email = "ar343@gmail.com",
            RecieveNewsLetter = true,
            Gender = ServiceContracts.Enums.GenderOptions.Male,
        };

        // Act
        PersonResponse personResponseFromAdd = _personService.AddPerson(personAddResquest);
        List<PersonResponse> personList = _personService.GetAllPersonList();

        // Assert
        Assert.True(personResponseFromAdd.PersonID != Guid.Empty);

        Assert.Contains(personResponseFromAdd, personList);
    }
    #endregion

    #region Get Person Detail by id
    [Fact]
    public void GetPersonDetail_NullID()
    {
        // arrange
        Guid? personID = null;
        // act
        PersonResponse? personResponse = _personService.GetPersonDetailByID(personID);
        // asserts
        Assert.Null(personResponse);
    }

    [Fact]
    // If we supply valid person id, it should return the valid person details..
    public void GetPersonDetailByID_ValidValues()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Canada" };
        CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);
        // acts
        PersonAddResquest person_request = new PersonAddResquest()
        {
            CountryID = countryResponse.CountryID,
            PersonName = "Aman",
            Email = "asas@gmail.com",
            Address = "NEw Delhi",
            BirthOfDate = DateTime.Parse("08-09-2003"),
            Gender = ServiceContracts.Enums.GenderOptions.Male,
            RecieveNewsLetter = false
        };
        PersonResponse personResponse = _personService.AddPerson(person_request);

        PersonResponse? personResponse_fromGetID = _personService.GetPersonDetailByID(personResponse.PersonID);
        // Asserts
        Assert.Equal(personResponse, personResponse_fromGetID);

    }
    #endregion

    #region GetAllPerson
    [Fact]
    public void GetAllPerson_EmptyList()
    {
        // Acts
        List<PersonResponse> personResponses = _personService.GetAllPersonList();
        // Assert
        Assert.Empty(personResponses);
    }
    [Fact]
    // first we will add some persons and thwn call getallperson and check all person are there
    public void GetAllPerson_AddFewPerson()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "India"
        };
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        CountryResponse countryResponse_1 = _countriesService.AddCountry(countryAddRequest);
        CountryResponse countryResponse_2 = _countriesService.AddCountry(countryAddRequest1);

        PersonAddResquest personAddResquest_1 = new PersonAddResquest()
        {
            PersonName = "Aman",
            Email = "asas@gmail.com",
            Address = "NEw Delhi",
            BirthOfDate = DateTime.Parse("08-09-2003"),
            Gender = ServiceContracts.Enums.GenderOptions.Male,
            RecieveNewsLetter = false,
            CountryID = countryResponse_1.CountryID
        };
        PersonAddResquest personAddResquest_2 = new PersonAddResquest()
        {
            PersonName = "Kanishak",
            Email = "kanishkas@gmail.com",
            Address = "Bihar",
            BirthOfDate = DateTime.Parse("08-02-2004"),
            Gender = ServiceContracts.Enums.GenderOptions.Female,
            RecieveNewsLetter = false,
            CountryID = countryResponse_2.CountryID
        };



        List<PersonAddResquest> personAddResquests = new List<PersonAddResquest>()
        {
            personAddResquest_1,personAddResquest_2
        };

        List<PersonResponse> personResponses = new List<PersonResponse>();
        foreach (PersonAddResquest person in personAddResquests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses.Add(personResponse);
        }

        // Print the expected Valuew
        _testOutputHelper1.WriteLine("Expected:");
        foreach (PersonResponse personFromAdd in personResponses)
        {
            _testOutputHelper1.WriteLine(personFromAdd.ToString());
        }

        List<PersonResponse> personResponses_fromGet = _personService.GetAllPersonList();
        // Print the actual Valuew
        _testOutputHelper1.WriteLine("Actual:");
        foreach (PersonResponse personFromGet in personResponses_fromGet)
        {
            _testOutputHelper1.WriteLine(personFromGet.ToString());
        }
        // Act
        foreach (PersonResponse personFromAdd in personResponses)
        {
            Assert.Contains(personFromAdd, personResponses_fromGet);
        }



    }
    #endregion

    #region GetFilterPerson
    [Fact]
    // Get all persons data if the searchBy is personName and searchsting is empty...
    public void GetFilterPerson_EmptyStringValue()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "India"
        };
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        CountryResponse countryResponse_1 = _countriesService.AddCountry(countryAddRequest);
        CountryResponse countryResponse_2 = _countriesService.AddCountry(countryAddRequest1);

        PersonAddResquest personAddResquest_1 = new PersonAddResquest()
        {
            PersonName = "Aman",
            Email = "asas@gmail.com",
            Address = "NEw Delhi",
            BirthOfDate = DateTime.Parse("08-09-2003"),
            Gender = ServiceContracts.Enums.GenderOptions.Male,
            RecieveNewsLetter = false,
            CountryID = countryResponse_1.CountryID
        };
        PersonAddResquest personAddResquest_2 = new PersonAddResquest()
        {
            PersonName = "Kanishak",
            Email = "kanishkas@gmail.com",
            Address = "Bihar",
            BirthOfDate = DateTime.Parse("08-02-2004"),
            Gender = ServiceContracts.Enums.GenderOptions.Female,
            RecieveNewsLetter = false,
            CountryID = countryResponse_2.CountryID
        };



        List<PersonAddResquest> personAddResquests = new List<PersonAddResquest>()
        {
            personAddResquest_1,personAddResquest_2
        };

        List<PersonResponse> personResponses = new List<PersonResponse>();
        foreach (PersonAddResquest person in personAddResquests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses.Add(personResponse);
        }

        // Print the expected Valuew
        _testOutputHelper1.WriteLine("Expected:");
        foreach (PersonResponse personFromAdd in personResponses)
        {
            _testOutputHelper1.WriteLine(personFromAdd.ToString());
        }

        List<PersonResponse> personResponses_fromSearch = _personService.GetFilterPersons(nameof(Person.PersonName), "");
        // Print the actual Valuew
        _testOutputHelper1.WriteLine("Actual:");
        foreach (PersonResponse personFromGet in personResponses_fromSearch)
        {
            _testOutputHelper1.WriteLine(personFromGet.ToString());
        }
        // Act
        foreach (PersonResponse personFromAdd in personResponses)
        {
            Assert.Contains(personFromAdd, personResponses_fromSearch);
        }



    }


    [Fact]
    // First we will add some persons , Get  persons data if the searchBy is personName and searchsting is Some Value...
    public void GetFilterPerson_SearchByPersonName()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "India"
        };
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        CountryResponse countryResponse_1 = _countriesService.AddCountry(countryAddRequest);
        CountryResponse countryResponse_2 = _countriesService.AddCountry(countryAddRequest1);

        PersonAddResquest personAddResquest_1 = new PersonAddResquest()
        {
            PersonName = "Aman",
            Email = "asas@gmail.com",
            Address = "NEw Delhi",
            BirthOfDate = DateTime.Parse("08-09-2003"),
            Gender = ServiceContracts.Enums.GenderOptions.Male,
            RecieveNewsLetter = false,
            CountryID = countryResponse_1.CountryID
        };
        PersonAddResquest personAddResquest_2 = new PersonAddResquest()
        {
            PersonName = "Kanishak",
            Email = "kanishkas@gmail.com",
            Address = "Bihar",
            BirthOfDate = DateTime.Parse("08-02-2004"),
            Gender = ServiceContracts.Enums.GenderOptions.Female,
            RecieveNewsLetter = false,
            CountryID = countryResponse_2.CountryID
        };



        List<PersonAddResquest> personAddResquests = new List<PersonAddResquest>()
        {
            personAddResquest_1,personAddResquest_2
        };

        List<PersonResponse> personResponses = new List<PersonResponse>();
        foreach (PersonAddResquest person in personAddResquests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses.Add(personResponse);
        }

        // Print the expected Valuew
        _testOutputHelper1.WriteLine("Expected:");
        foreach (PersonResponse personFromAdd in personResponses)
        {
            _testOutputHelper1.WriteLine(personFromAdd.ToString());
        }

        List<PersonResponse> personResponses_fromSearch = _personService.GetFilterPersons(nameof(Person.PersonName), "Am");
        // Print the actual Valuew
        _testOutputHelper1.WriteLine("Actual:");
        foreach (PersonResponse personFromGet in personResponses_fromSearch)
        {
            _testOutputHelper1.WriteLine(personFromGet.ToString());
        }
        // Act
        foreach (PersonResponse personFromAdd in personResponses)
        {
            if (personFromAdd.PersonName != null)
            {
                if (personFromAdd.PersonName.Contains("Am", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(personFromAdd, personResponses_fromSearch);
                }
            }
        }



    }
    #endregion

    #region GetSortedPerson
    // When we sort person name in desc it should return person name in desc order...
    public void GetSortedPersons()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest()
        {
            CountryName = "India"
        };
        CountryAddRequest countryAddRequest1 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        CountryResponse countryResponse_1 = _countriesService.AddCountry(countryAddRequest);
        CountryResponse countryResponse_2 = _countriesService.AddCountry(countryAddRequest1);

        PersonAddResquest personAddResquest_1 = new PersonAddResquest()
        {
            PersonName = "Aman",
            Email = "asas@gmail.com",
            Address = "NEw Delhi",
            BirthOfDate = DateTime.Parse("08-09-2003"),
            Gender = ServiceContracts.Enums.GenderOptions.Male,
            RecieveNewsLetter = false,
            CountryID = countryResponse_1.CountryID
        };
        PersonAddResquest personAddResquest_2 = new PersonAddResquest()
        {
            PersonName = "Kanishak",
            Email = "kanishkas@gmail.com",
            Address = "Bihar",
            BirthOfDate = DateTime.Parse("08-02-2004"),
            Gender = ServiceContracts.Enums.GenderOptions.Female,
            RecieveNewsLetter = false,
            CountryID = countryResponse_2.CountryID
        };



        List<PersonAddResquest> personAddResquests = new List<PersonAddResquest>()
        {
            personAddResquest_1,personAddResquest_2
        };

        List<PersonResponse> personResponses_from_add = new List<PersonResponse>();
        foreach (PersonAddResquest person in personAddResquests)
        {
            PersonResponse personResponse = _personService.AddPerson(person);
            personResponses_from_add.Add(personResponse);
        }

        // Print the expected Valuew
        _testOutputHelper1.WriteLine("Expected:");
        foreach (PersonResponse personFromAdd in personResponses_from_add)
        {
            _testOutputHelper1.WriteLine(personFromAdd.ToString());
        }

        List<PersonResponse> allPersons = _personService.GetAllPersonList();
        List<PersonResponse> personResponses_from_sort = _personService.GetSortedPerson(allPersons, nameof(Person.PersonName), SortOrderEnum.ASC);




        // Print the actual Valuew
        _testOutputHelper1.WriteLine("Actual:");
        foreach (PersonResponse personFromGet in personResponses_from_sort)
        {
            _testOutputHelper1.WriteLine(personFromGet.ToString());
        }

        personResponses_from_add = personResponses_from_add.OrderByDescending(temp => temp.PersonName).ToList();

        // Act
        for (int i = 0; i < personResponses_from_add.Count; i++)
        {
            Assert.Equal(personResponses_from_add[i], personResponses_from_sort[i]);
        }



    }
    #endregion

    #region GetPersonUpdate
    [Fact]
    // Supply arg as null, so get ArgnullExecption
    public void GetPersonUpdate_NullValue()
    {
        // Arrange
        PersonUpdateRequest? personUpdateRequest = null;


        // Assert
        Assert.Throws<ArgumentNullException>(() =>
        {   //Act
            _personService.GetPersonUpdateRequest(personUpdateRequest);
        });
    }
    [Fact]
    // Supply Person id is not valid  , we will get ArgnullExecption
    public void GetPersonUpdate_InvalidID()
    {
        // Arrange
        PersonUpdateRequest? personUpdateRequest = new PersonUpdateRequest()
        {
            PersonID = Guid.NewGuid(),
        };


        // Assert
        Assert.Throws<ArgumentException>(() =>
        {   //Act
            _personService.GetPersonUpdateRequest(personUpdateRequest);
        });
    }
    // If the person name is null, it should throw args exception
    [Fact]
    public void GetPersonUpdate_NullPersonName()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };
        CountryResponse countryAddRequest_fromAdd = _countriesService.AddCountry(countryAddRequest);

        PersonAddResquest personAddResquest = new PersonAddResquest() { CountryID = countryAddRequest_fromAdd.CountryID, PersonName = "John", Email = "ar34@gmail.com", Gender = GenderOptions.Male };
        PersonResponse personResponse_fromAdd = _personService.AddPerson(personAddResquest);

        PersonUpdateRequest personUpdateRequest = personResponse_fromAdd.ToPersonUpdateRequest();
        personUpdateRequest.PersonName = null;

        // Assert
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _personService.GetPersonUpdateRequest(personUpdateRequest);
        });


    }
    [Fact]
    // Add new person and tryr to update the same
    public void GetPersonUpdate_Valid()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };
        CountryResponse countryAddRequest_fromAdd = _countriesService.AddCountry(countryAddRequest);

        PersonAddResquest personAddResquest = new PersonAddResquest() { CountryID = countryAddRequest_fromAdd.CountryID, PersonName = "John", Email = "ar34@gmail.com", Address = "NEw Delhi", Gender = GenderOptions.Other };

        PersonResponse personResponse_fromAdd = _personService.AddPerson(personAddResquest);

        PersonUpdateRequest personUpdateRequest = personResponse_fromAdd.ToPersonUpdateRequest();
        personUpdateRequest.PersonName = "Aman Rawat";
        personUpdateRequest.Email = "ar7541147@gmail.com";

        // Act
        PersonResponse personUpdateRequestFromUpdate = _personService.GetPersonUpdateRequest(personUpdateRequest);

        PersonResponse? personResponse_fromGet = _personService.GetPersonDetailByID(personUpdateRequestFromUpdate.PersonID);

        // Assert
        Assert.Equal(personResponse_fromGet, personUpdateRequestFromUpdate);


    }

    #endregion

    #region DeletePerson
    [Fact]
    // Id persn id is null 
    public void DeletePersonID_NullValue()
    {
        Guid? personID = null;

        Assert.Throws<ArgumentNullException>(() =>
        {
            _personService.DeletePerson(personID);
        });
    }
    [Fact]
    // Id person id is Valid
    public void DeletePersonID_validID()
    {
        // Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Japan" };
        CountryResponse countryAddRequest_fromAdd = _countriesService.AddCountry(countryAddRequest);

        PersonAddResquest personAddResquest = new PersonAddResquest() { CountryID = countryAddRequest_fromAdd.CountryID, PersonName = "John", Email = "ar34@gmail.com", Gender = GenderOptions.Male , Address="New Delhi", BirthOfDate = Convert.ToDateTime("08/09/2003"),RecieveNewsLetter=true};
        PersonResponse personResponse_fromAdd = _personService.AddPerson(personAddResquest);


        bool isDelete = _personService.DeletePerson(personResponse_fromAdd.PersonID);

        // Assert
        Assert.True(isDelete);
    }
    [Fact]
    // Id person id is InValid
    public void DeletePersonID_InvalidID()
    {
        bool isDelete = _personService.DeletePerson(Guid.NewGuid());

        // Assert
        Assert.False(isDelete);
    }
    #endregion

}
