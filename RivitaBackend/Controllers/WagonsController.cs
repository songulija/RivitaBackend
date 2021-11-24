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
    public class WagonsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<WagonsController> _logger;

        public WagonsController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<WagonsController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWagons()
        {
            var wagons = await _unitOfWork.Wagons.GetAll();
            var results = _mapper.Map<IList<WagonDTO>>(wagons);
            return Ok(results);

        }

        [HttpGet("{id:Guid}", Name = "GetWagon")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWagon(Guid id)
        {
            var wagon = await _unitOfWork.Wagons.Get(w => w.Id == id, includeProperties: "Transportation");

            var result = _mapper.Map<WagonDTO>(wagon);
            return Ok(result);
        }

        [HttpGet("transportation/{id:Guid}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWagonsByTransportation(Guid id)
        {
            var wagons = await _unitOfWork.Wagons.GetAll(w => w.TransportationId == id, includeProperties: "Transportation");
            var results = _mapper.Map<IList<Wagon>>(wagons);
            return Ok(results);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertWagon([FromBody] WagonDTO wagonDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(InsertWagon)}");
                return BadRequest("Submited data is invalid");
            }
            var wagon = _mapper.Map<Wagon>(wagonDTO);

            await _unitOfWork.Wagons.Insert(wagon);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetWagon", new { id = wagon.Id }, wagon);

        }

        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateWagon([FromBody] UpdateWagonDTO wagonDTO, Guid id)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWagon)}");
                return BadRequest("Submited data is invalid");
            }

            var wagon = await _unitOfWork.Wagons.Get(w => w.Id == id);
            if (wagon == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateWagon)}");
                return BadRequest("Submited data is invalid");
            }

            _mapper.Map(wagonDTO, wagon);
            _unitOfWork.Wagons.Update(wagon);
            await _unitOfWork.Save();

            return NoContent();
        }
        [HttpPut("update")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMany([FromBody]IEnumerable<UpdateWagonDTO> wagonsDTOs)
        {
            var wagons = _mapper.Map<IList<Wagon>>(wagonsDTOs);
            _unitOfWork.Wagons.UpdateRange(wagons);
            await _unitOfWork.Save();

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWagon(Guid id)
        {

            var wagon = _unitOfWork.Wagons.Get(w => w.Id == id);
            if (wagon == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteWagon)}");
                return BadRequest("Submited data is invalid");
            }

            await _unitOfWork.Wagons.DeleteGuid(id);
            await _unitOfWork.Save();

            return NoContent();
        }
    }
}
