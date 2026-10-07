using Entities;
namespace ServiceContracts.DTO;
/// <summary>
/// DTO Class that is used as the return type for most for countru service methods
/// </summary>
public class CountryResponse
{
    public Guid CountryID {get;set;}
    public string? CountryName {get;set;}
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if(obj.GetType()!=typeof(CountryResponse)) return false;
        CountryResponse country_to_compare = (CountryResponse)obj;
        return this.CountryID==country_to_compare.CountryID && this.CountryName == country_to_compare.CountryName;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}

public static class CountryExtensions
{
    public static CountryResponse ToCountryResponse(this Country country)
    {
        // Method convert into country object to country response object
        return new CountryResponse()
        {
            CountryID = country.CountryID,
            CountryName = country.CountryName,
        };
    }
}
