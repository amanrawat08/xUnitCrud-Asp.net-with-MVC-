using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    // private field
    private readonly List<Country> _countries;

    public CountriesService(bool initiaze = true)
    {
        _countries = new List<Country>();
        if (initiaze)
        {
            _countries.AddRange(new List<Country>(){
            new Country(){CountryID=Guid.Parse("d775b3ec-f294-4edd-9855-9d3872f9cd75"), CountryName="USA"},
            new Country(){CountryID=Guid.Parse("6fc237ee-fac4-47be-98ab-ea83b5f11364"), CountryName="India"},
            new Country(){CountryID=Guid.Parse("c4d34a2f-6b81-49c4-9828-f437ac751ba0"), CountryName="China"},
            new Country(){CountryID=Guid.Parse("19c5e843-4a90-4e35-b3e2-acae4390d53a"), CountryName="UK"},
            new Country(){CountryID=Guid.Parse("bd9f45ca-5b91-474b-bbe8-e0fc6733ea52"), CountryName="NewLand"},
            new Country(){CountryID=Guid.Parse("23c25a89-a1ac-48ba-a999-ca9fc532919d"), CountryName="England"},
            });
        }
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
        if (_countries.Where((country) => country.CountryName == countryAddRequest.CountryName).Count() > 0)
        {
            throw new ArgumentException("Country name already exists");
        }

        // Convert obj from CountryAddRequest to Country type...
        Country country = countryAddRequest.ToCountry();
        // Generate new GUID;
        country.CountryID = Guid.NewGuid();
        System.Console.WriteLine(country);
        // Add Country obj into _countries...
        _countries.Add(country);

        return country.ToCountryResponse();
    }

    public List<CountryResponse> GetAllCountries()
    {
        return _countries.Select(country => country.ToCountryResponse()).ToList();
    }

    public CountryResponse? GetCountryByCountryID(Guid? countryID)
    {
        if (countryID == null) return null;

        Country? responseFromList = _countries.FirstOrDefault(country => country.CountryID == countryID);
        if (responseFromList == null) return null;


        return responseFromList.ToCountryResponse();
    }
}
