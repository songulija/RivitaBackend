using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RivitaBackend.IRepository;
using RivitaBackend.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/companies")]
    [ApiController]
    public class CompaniesV2Controller : ControllerBase
    {
        //IUnitOfWork basically a register for every variation of Generic Repository. asigning all models(tables) to GenericRepositories
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CompaniesController> _logger;
        public CompaniesV2Controller(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CompaniesController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _unitOfWork.Companies.GetAll();
            //convert list of Company objects to list of CompanyDTO objects and then return to user
            var results = _mapper.Map<IList<CompanyDTO>>(companies);
            return Ok(results);

        }
    }
}
