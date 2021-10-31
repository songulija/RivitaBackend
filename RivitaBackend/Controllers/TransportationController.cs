﻿using AutoMapper;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace RivitaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransportations()
        {
            var transportations = await _unitOfWork.Transportations.GetAll();
            var results = _mapper.Map<IList<TransportationDTO>>(transportations);
            return Ok(results);

        }

        [HttpGet("{id:Guid}",Name = "GetTransportation")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransportation(Guid id)
        {
            var transportation = await _unitOfWork.Transportations.Get(w => w.Id == id);

            var result = _mapper.Map<TransportationDTO>(transportation);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTransportation([FromBody] TransportationDTO transportationDTO)
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
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTransportation([FromBody] TransportationDTO transportationDTO, Guid id)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
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
