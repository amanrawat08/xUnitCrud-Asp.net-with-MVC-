
using ServiceContracts;
using Entities;
using Services;
using ServiceContracts.DTO;
using Xunit.Sdk;
namespace CRUDTests;

public class CountriesServiceTest
{
    private readonly ICountriesService _countriesService;
    public CountriesServiceTest()
    {
        _countriesService = new CountriesService(false);

    }
    #region Add Country
    // when CoutryAddRequest is null, it should thorw argumentnullexaption
    [Fact]
    public void AddCountry_NullCountry()
    {
        //Arrange
        CountryAddRequest? request = null;
        //Asserts
        Assert.Throws<ArgumentNullException>(() =>
        {
            // Act
            _countriesService.AddCountry(request);
        });
    }
    // when the country is null, it should throw argumentexception
    [Fact]
    public void AddCountry_NullCountryName()
    {
        // Arrange
        CountryAddRequest? request = new CountryAddRequest()
        {
            CountryName = null
        };
        // Asserts
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countriesService.AddCountry(request);
        });
    }
    // when the company name is duplicate, it should throw argument exeption
    [Fact]
    public void AddCountry_DuplicateCountryName()
    {
        // Arrange
        CountryAddRequest? request1 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        CountryAddRequest? request2 = new CountryAddRequest()
        {
            CountryName = "USA"
        };
        // Asserts
        Assert.Throws<ArgumentException>(() =>
        {
            // Act
            _countriesService.AddCountry(request1);
            _countriesService.AddCountry(request2);
        });
    }
    // when you supply proper country name m it should insertx` the country name in existing list of countries
    [Fact]
    public void AddCountry_ProperCountryDetails()
    {
        //Arrange
        CountryAddRequest? request = new CountryAddRequest()
        {
            CountryName = "Japan"
        };
        
        //Act
        CountryResponse response = _countriesService.AddCountry(request);

        List<CountryResponse> contriesFromGetAllcountries = _countriesService.GetAllCountries();

        //Asserts 
        Assert.True(response.CountryID != Guid.Empty); 
        Assert.Contains(response, contriesFromGetAllcountries);

    }
    #endregion 

    #region Get All coutries
    [Fact]
    // The list of the country is empty bydefault
    public void GetAllCountries_EmptyList()
    {
        // Act
        List<CountryResponse> actualCountriesResponseList = _countriesService.GetAllCountries();

        // asserts
        Assert.Empty(actualCountriesResponseList);
    }
    
    [Fact]
    public void GetAllCountries_AllFewCountries()
    {
        // Arrange
         List<CountryAddRequest> country_AddList = new List<CountryAddRequest>
         {
            new CountryAddRequest(){CountryName="USA"},  
            new CountryAddRequest(){CountryName="UK"},  
         };

        //  Act
        List<CountryResponse> countriesListFromAddCoutnries = new List<CountryResponse>();
        foreach(CountryAddRequest country_request in country_AddList)
        {
            countriesListFromAddCoutnries.Add(_countriesService.AddCountry(country_request));
        }
        List<CountryResponse> actualCountryResponseList =  _countriesService.GetAllCountries();

        // read each ele from countriesListFromAddCoutnries;
        foreach (CountryResponse expected_country in countriesListFromAddCoutnries)
        {
            Assert.Contains(expected_country, actualCountryResponseList);
        }
    }
    #endregion

    #region GetCountryByCountryID
    [Fact]
    // If the country id is null
    public void GetCountryByCountryID_NullableID()
    {
        // Arrage
        Guid? id = null;
        // act
        CountryResponse? countryResponseFromGetMethod =  _countriesService.GetCountryByCountryID(id);
        // Assert
        Assert.Null(countryResponseFromGetMethod);
    }
    [Fact]
    // If the country id is Valid so it should returrn the valid country detail as the country response
    public void GetCountryByCountryID_ValidID()
    {
        // Arrage
        CountryAddRequest? countryAddRequest = new CountryAddRequest()
        {
            CountryName="China"
        };
        CountryResponse countryResponseFromAdd = _countriesService.AddCountry(countryAddRequest);

        // act
        CountryResponse countryResponseFromGetID =  _countriesService.GetCountryByCountryID(countryResponseFromAdd.CountryID);
        // Assert
        Assert.Equal(countryResponseFromAdd, countryResponseFromGetID);
    }
    #endregion
}
