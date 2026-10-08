using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
    public class PersonsController : Controller
    {   
        // Private Fields
        private readonly IPersonService _personService; 

        
        //
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        } 

        [Route("persons/index")]
        [Route("/")]
        public ActionResult Index(string searchBy, string? searchString , string sortBy=nameof(PersonResponse.PersonName), SortOrderEnum sortOrder = SortOrderEnum.ASC)
        {
            ViewBag.SearchOptions = new Dictionary<string, string>()
            {
              { nameof(PersonResponse.PersonName) , "Person Name" },
              { nameof(PersonResponse.Email) , "Email" },
              { nameof(PersonResponse.BirthOfDate) , "Date Of Birth" },
              { nameof(PersonResponse.CountryID) , "Country" },
              { nameof(PersonResponse.Gender) , "Gender" },
              { nameof(PersonResponse.Address) , "Address" },

            };
            List<PersonResponse> persons =  _personService.GetFilterPersons(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            // Sort
            List<PersonResponse> personResponse_SortOrder =  _personService.GetSortedPerson(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy.ToString();
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            return View(personResponse_SortOrder);
        }

    }
}
