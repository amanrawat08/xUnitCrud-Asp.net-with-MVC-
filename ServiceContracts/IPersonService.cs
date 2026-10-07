using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts;
/// <summary>
/// </summary>
public interface IPersonService
{
    /// <summary>
    ///  Add the new person in the existing list of persons
    /// </summary>
    /// <param name="personAddRequest">data of the person</param>
    /// <returns>new genrated person with new guid</returns>
    PersonResponse AddPerson(PersonAddResquest? personAddRequest);

    /// <summary>
    /// Returns all person data
    /// </summary>
    /// <returns></returns>
    List<PersonResponse> GetAllPersonList();
    /// <summary>
    /// Return the person obj based on the person id
    /// </summary>
    /// <param name="personID">person id to search</param>
    /// <returns>Return person matching obj</returns>
    PersonResponse? GetPersonDetailByID(Guid? personID);
    /// <summary>
    /// we will search the persons data using searchby and searchString
    /// </summary>
    /// <param name="searchBy">Person field</param>
    /// <param name="searchString">the Value of the feild</param>
    /// <returns>list of person response fielter by person feild and value</returns>
    List<PersonResponse> GetFilterPersons(string? searchBy , string? searchString);
     
    /// <summary>
    /// return list of person
    /// </summary>
    /// <param name="allPersons">Represent list id person to sort</param>
    /// <param name="sortBy">Name of property based on sorting is happen</param>
    /// <param name="sortOrder">Asc or Desc</param>
    /// <returns>list of sorted persons</returns>
    List<PersonResponse> GetSortedPerson(List<PersonResponse> allPersons, string sortBy , SortOrderEnum sortOrder);
    /// <summary>
    /// Update the specifies person detail based on the person id
    /// </summary>
    /// <param name="personUpdateRequest">Person details to update even the person id</param>
    /// <returns>Retur person response object after updation</returns>
    PersonResponse GetPersonUpdateRequest(PersonUpdateRequest? personUpdateRequest);
    /// <summary>
    /// Delete the person from the list using person id
    /// </summary>
    /// <param name="PersonID">By person id it will delete.</param>
    /// <returns>Return true or false.</returns>
    Boolean DeletePerson(Guid? PersonID);
}
