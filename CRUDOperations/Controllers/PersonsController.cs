using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
    [Route("[controller]")]
    public class PersonsController : Controller
    {
        // Private Fields
        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;


        //
        public PersonsController(IPersonService personService, ICountriesService countriesService)
        {
            _personService = personService;
            _countriesService = countriesService;
        }

        [Route("[action]")]
        [Route("/")]
        public ActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderEnum sortOrder = SortOrderEnum.ASC)
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
            List<PersonResponse> persons = _personService.GetFilterPersons(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            // Sort
            List<PersonResponse> personResponse_SortOrder = _personService.GetSortedPerson(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy.ToString();
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            return View(personResponse_SortOrder);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            List<CountryResponse> allCountries = _countriesService.GetAllCountries();
            ViewBag.Countries = allCountries;
            return View();
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PersonAddResquest personAddResquest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> allCountries = _countriesService.GetAllCountries();
                ViewBag.Countries = allCountries;
                ViewBag.Errors =  ModelState.Values.SelectMany((e)=>e.Errors).Select((e)=>e.ErrorMessage).ToList();
            }

            PersonResponse personResponse = _personService.AddPerson(personAddResquest);
            // Navigate to index action method, it make another get req to "persons/index"
            return RedirectToAction("Index", "Persons");
        }
    }
}
