using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonManagementApi.Helpers;
using PersonManagementApi.Models;
using PersonManagementApi.Services;

namespace PersonManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonManagerService _personManagerService;

        public PersonController(ILogger<PersonController> logger,IPersonManagerService personManagerService)
        {
            _logger = logger;
            _personManagerService = personManagerService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var personsList = await _personManagerService.GetPersons();
            return Ok(ResponseBuilder.BuildResponse(personsList, true));
        }
        [HttpPost]
        public async Task<IActionResult> Post(List<PersonModel> personList)
        {
            await _personManagerService.SavePersons(personList);            
            return Created("/get",ResponseBuilder.BuildResponse(personList, true));
        }
    }
}
