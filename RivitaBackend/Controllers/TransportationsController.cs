using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RivitaBackend.IRepository;
using RivitaBackend.Models;
using RivitaBackend.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RivitaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TransportationsController> _logger;
        private DatabaseContext _context;

        public TransportationsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TransportationsController> logger, DatabaseContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransportations()
        {
            var transportations = await _unitOfWork.Transportations.GetAll(includeProperties: "User");
            var results = _mapper.Map<IList<TransportationDTO>>(transportations);
            return Ok(results);

        }

        [HttpGet("{id:Guid}",Name = "GetTransportation")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransportation(Guid id)
        {
            var transportation = await _unitOfWork.Transportations.Get(w => w.Id == id, includeProperties: "User");

            var result = _mapper.Map<TransportationDTO>(transportation);
            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetForSearch([FromQuery]int transportationNumber=0,[FromQuery]int etsngCargoCode=0,[FromQuery]int gngCargoCode=0,
            [FromQuery]string cargoAcceptanceDate = null, [FromQuery]string movementStartDateInBelarusFrom = null,
            [FromQuery]string movementStartDateInBelarusTo = null, [FromQuery]string movementEndDateInBelarusFrom = null,
            [FromQuery]string movementEndDateInBelarusTo = null, [FromQuery]string companyName = null)
        {
           
            var query = _context.Transportations.AsNoTracking();
            if(transportationNumber != 0)
            {
                query = query.Where(x => x.TransportationNumber == transportationNumber);
            }
            if (etsngCargoCode != 0)
            {
                query = query.Where(x => x.EtsngCargoCode == etsngCargoCode);
            }
            if (gngCargoCode != 0)
            {
                query = query.Where(x => x.GngCargoCode == gngCargoCode);
            }
            if(cargoAcceptanceDate != null)
            {
                var aceptanceDate = DateTime.Parse(cargoAcceptanceDate);
                query = query.Where(x => x.CargoAcceptanceDate.Year == aceptanceDate.Year &&
                x.CargoAcceptanceDate.Month == aceptanceDate.Month &&
                x.CargoAcceptanceDate.Day == aceptanceDate.Day);
                /*DateTime.ParseExact(a.callDate, "yyyyMMddhhmm"*/
            }
            if(movementStartDateInBelarusFrom != null)
            {
                var movementStartInBelarusFromDate = DateTime.Parse(movementStartDateInBelarusFrom);

                query = query.Where(x => x.MovementStartDateInBelarus >= movementStartInBelarusFromDate);
            }
            if(movementStartDateInBelarusTo != null)
            {
                var movementStartInBelarusToDate = DateTime.Parse(movementStartDateInBelarusTo);
                query = query.Where(x => x.MovementStartDateInBelarus <= movementStartInBelarusToDate);

            }
            if(movementEndDateInBelarusFrom != null && movementEndDateInBelarusTo != null)
            {
                var movementEndInBelarusFromDate = DateTime.Parse(movementEndDateInBelarusFrom);

                query = query.Where(x => x.MovementEndDateInBelarus >= movementEndInBelarusFromDate);
            }
            if(movementEndDateInBelarusTo != null)
            {
                var movementEndInBelarusToDate = DateTime.Parse(movementEndDateInBelarusTo);
                query = query.Where(x => x.MovementEndDateInBelarus <= movementEndInBelarusToDate);
            }
            if(companyName != null)
            {
                query = query.Where(x => x.CompanyName == companyName);
            }
            var transportations = await query.ToListAsync();
            var results = _mapper.Map<IList<Transportation>>(transportations);
            return Ok(results);
        }


        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTransportation([FromBody] CreateTransportationDTO transportationDTO)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(InsertTransportation)}");
                return BadRequest("Submited data is invalid");
            }
            transportationDTO.UserId = Guid.Parse(userId);
            var transportation = _mapper.Map<Transportation>(transportationDTO);

            await _unitOfWork.Transportations.Insert(transportation);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetTransportation", new { id = transportation.Id }, transportation);

        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTransportation([FromBody] UpdateTransportationDTO transportationDTO, Guid id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateTransportation)}");
                return BadRequest("Submited data is invalid");
            }

            transportationDTO.UserId = Guid.Parse(userId);
            var transportation = await _unitOfWork.Transportations.Get(w => w.Id == id);
            if (transportation == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateTransportation)}");
                return BadRequest("Submited data is invalid");
            }

            _mapper.Map(transportationDTO, transportation);
            _unitOfWork.Transportations.Update(transportation);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpPut("update")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMany([FromBody] IEnumerable<UpdateTransportationDTO> transportationsDTOs)
        {
            var transportations = _mapper.Map<IList<Transportation>>(transportationsDTOs);
            _unitOfWork.Transportations.UpdateRange(transportations);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTransportation(Guid id)
        {

            var transportation = _unitOfWork.Transportations.Get(w => w.Id == id);
            if (transportation == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteTransportation)}");
                return BadRequest("Submited data is invalid");
            }

            await _unitOfWork.Transportations.DeleteGuid(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
