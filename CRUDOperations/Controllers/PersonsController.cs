using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.Countries = allCountries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() }
            );
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
                ViewBag.Errors = ModelState.Values.SelectMany((e) => e.Errors).Select((e) => e.ErrorMessage).ToList();
            }

            PersonResponse personResponse = _personService.AddPerson(personAddResquest);
            // Navigate to index action method, it make another get req to "persons/index"
            return RedirectToAction("Index", "Persons");
        }

        [HttpGet]
        [Route("[action]/{personID}")]
        public IActionResult Edit(Guid personID)
        {
            PersonResponse? person = _personService.GetPersonDetailByID(personID);
            if (person == null)
            {
                return RedirectToAction("Index");
            }
            List<CountryResponse> allCountries = _countriesService.GetAllCountries();
            ViewBag.Countries = allCountries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });


            PersonUpdateRequest personUpdateRequest = person.ToPersonUpdateRequest();
            return View(personUpdateRequest);
        }
        [HttpPost]
        [Route("[action]/{personID}")]
        public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personService.GetPersonDetailByID(personUpdateRequest.PersonID);
            if (personResponse == null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                PersonResponse personResponse1 = _personService.GetPersonUpdateRequest(personUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                List<CountryResponse> allCountries = _countriesService.GetAllCountries();
                ViewBag.Countries = allCountries;
                ViewBag.Errors = ModelState.Values.SelectMany((e) => e.Errors).Select((e) => e.ErrorMessage).ToList();
                return View(personResponse.ToPersonUpdateRequest());
            }
        }

        [HttpGet]
        [Route("[action]/{personID}")]
        public IActionResult Delete(Guid personID)
        {
            PersonResponse? personResponse = _personService.GetPersonDetailByID(personID);
            if (personResponse == null) return RedirectToAction("Index");

            return View(personResponse);
        }
        [HttpPost]
        [Route("[action]/{personID}")]
        public IActionResult Delete(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = _personService.GetPersonDetailByID(personUpdateRequest.PersonID);
            if (personResponse == null) return RedirectToAction("Index");

            _personService.DeletePerson(personUpdateRequest.PersonID);
            return RedirectToAction("Index");
        }

    }
}
