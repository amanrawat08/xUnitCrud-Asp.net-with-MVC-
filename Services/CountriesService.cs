using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    // private field
    private readonly List<Country> _countries;

    public CountriesService()
    {
        _countries = new List<Country>();
    }

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        // Validation: Country add req validation cant be null;
        if(countryAddRequest == null)
        {
            throw new ArgumentNullException(nameof(countryAddRequest));
        }

        // Validation : if countryname is null;
        if(countryAddRequest.CountryName == null)
        {
            throw new ArgumentException(nameof(countryAddRequest.CountryName));
        }
        // Validation : countryname if already exists...
        if(_countries.Where((country)=>country.CountryName==countryAddRequest.CountryName).Count() > 0)
        {
           throw new ArgumentException("Country name already exists"); 
        }

        // Convert obj from CountryAddRequest to Country type...
        Country country =  countryAddRequest.ToCountry();
        // Generate new GUID;
        country.CountryID = Guid.NewGuid();
        // Add Country obj into _countries...
        _countries.Add(country);

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(country=>country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryID(Guid? countryID)
    {
        if (countryID == null) return null;

        Country? responseFromList =  _countries.FirstOrDefault(country => country.CountryID == countryID);
        if (responseFromList==null) return null;

        
        return responseFromList.ToCountryResponse();
    }
}
