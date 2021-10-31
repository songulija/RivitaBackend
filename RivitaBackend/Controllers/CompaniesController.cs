using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RivitaBackend.IRepository;
using RivitaBackend.Models;
using RivitaBackend.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        //IUnitOfWork basically a register for every variation of Generic Repository. asigning all models(tables) to GenericRepositories
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CompaniesController> _logger;
        public CompaniesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompaniesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _unitOfWork.Companies.GetAll();
            //convert list of Company objects to list of CompanyDTO objects and then return to user
            var results = _mapper.Map<IList<CompanyDTO>>(companies);
            return Ok(results);

        }
        /// <summary>
        /// HTTP GET request to api/companies/{id} provide id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name= "GetCompany")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _unitOfWork.Companies.Get(c => c.Id == id);
            //convert Company object that get from db to CompnayDTO obj
            var result = _mapper.Map<CompanyDTO>(company);
            return Ok(result);
        }
        /// <summary>
        /// HTTP POST request to api/companies route. In request body have to provide companyDTO json object
        /// </summary>
        /// <param name="companyDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertCompany([FromBody]CompanyDTO companyDTO)
        {
            //checking if provided companyDTO model fields from POST request body are valid.
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(InsertCompany)}");
                return BadRequest("Submited data is invalid");
            }
            //convert companyDTO object from POST request body to Company model object that we will add to datanase
            var company = _mapper.Map<Company>(companyDTO);
            await _unitOfWork.Companies.Insert(company);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetCompany", new { id = company.Id }, company);

        }
        /// <summary>
        /// HTTP PUT request to api/companies/{id}. provide id, as PUT request body provide companyDTO json object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyDTO"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDTO companyDTO,int id)
        {
            //check if companyDTO object from body, its fields are valid
            if (!ModelState.IsValid || id > 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCompany)}");
                return BadRequest("Submited data is invalid");
            }
            //check if record exist only then you can update something
            var company = await _unitOfWork.Companies.Get(c => c.Id == id);
            if(company == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateCompany)}");
                return BadRequest("Submited data is invalid");
            }
            //map/convert companyDTO into company object. basically put what is in companyDTO to company object
            _mapper.Map(companyDTO, company);
            _unitOfWork.Companies.Update(company);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            //find if record exist. then delete it
            var company = _unitOfWork.Companies.Get(c => c.Id == id);
            if(company == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteCompany)}");
                return BadRequest("Submited data is invalid");
            }
            await _unitOfWork.Companies.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
