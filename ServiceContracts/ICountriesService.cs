using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts;
/// <summary>
/// represent bussiness logic for manipulating country entity
/// /// </summary>
public interface ICountriesService
{
    /// <summary>
    /// Add country object to the list of countries
    /// </summary>
    /// <param name="countryAddRequest">country object to add</param>
    /// <returns>return the country object after adding it including the newly generated countryid</returns>
    CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
    /// <summary>
    /// Returns all countries from the list of countries
    /// </summary>
    /// <returns></returns>
    List<CountryResponse> GetAllCountries();
    /// <summary>
    /// return country obj based on country given id
    /// </summary>
    /// <param name="countryID">Country (guid) to search.</param>
    /// <returns>MAtching country ti return country response object</returns>
    CountryResponse? GetCountryByCountryID(Guid? countryID);
}
