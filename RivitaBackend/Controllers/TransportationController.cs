using AutoMapper;
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
    public class TransportationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<TransportationController> _logger;

        public TransportationController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TransportationController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransportations()
        {
            var transportations = await _unitOfWork.Transportations.GetAll();
            var results = _mapper.Map<IList<TransportationDTO>>(transportations);
            return Ok(results);

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransportation(Guid id)
        {
            var transportation = await _unitOfWork.Transportations.Get(w => w.Id == id);

            var result = _mapper.Map<TransportationDTO>(transportation);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTransportation([FromBody] TransportationDTO transportationDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid CREATE attempt in {nameof(InsertTransportation)}");
                return BadRequest("Submited data is invalid");
            }
            var transportation = _mapper.Map<Transportation>(transportationDTO);

            await _unitOfWork.Transportations.Insert(transportation);
            await _unitOfWork.Save();
            return CreatedAtRoute("GetTransportation", new { id = transportation.Id }, transportation);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTransportation([FromBody] TransportationDTO transportationDTO, Guid id)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateTransportation)}");
                return BadRequest("Submited data is invalid");
            }

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

        [HttpDelete("{id}")]
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
