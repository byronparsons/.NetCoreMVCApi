using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Controllers
{
    //[ApiVersion("2.0")] //this has been removed because it was specified in the ConfigureVersioning setup with convention
    [Route("api/companies")]
    //[Route("api/{v:apiversion}/companies")]
    [ApiController]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        //private
        public CompaniesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges: false);

            return Ok(companies);
        }
    }
}