using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    // private field
    private readonly PersonsDbContext _db;

    public CountriesService(PersonsDbContext personsDbContext)
    {

        _db = personsDbContext;
    }

    public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
    {
        // Validation: Country add req validation cant be null;
        if (countryAddRequest == null)
        {
            throw new ArgumentNullException(nameof(countryAddRequest));
        }

        // Validation : if countryname is null;
        if (countryAddRequest.CountryName == null)
        {
            throw new ArgumentException(nameof(countryAddRequest.CountryName));
        }
        // Validation : countryname if already exists...
        if (_db.Countries.Count((country) => country.CountryName == countryAddRequest.CountryName) > 0)
        {
            throw new ArgumentException("Country name already exists");
        }

        // Convert obj from CountryAddRequest to Country type...
        Country country = countryAddRequest.ToCountry();
        // Generate new GUID;
        country.CountryID = Guid.NewGuid();
        System.Console.WriteLine(country);
        // Add Country obj into _countries...
        _db.Countries.Add(country);
        _db.SaveChanges();
        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _db.Countries
        .Select(country => country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryID(Guid? countryID)
    {
        if (countryID == null) return null;

        Country? responseFromList = _db.Countries.FirstOrDefault(country => country.CountryID == countryID);
        if (responseFromList == null) return null;


        return responseFromList.ToCountryResponse();
    }
}
