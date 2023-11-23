using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGardens.Dtos.Get.Oficina;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGardens.Controllers;

public class OficinaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OficinaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /* Get all Data from Table */
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Oficina>>> Get()
    {
        var variablePlural = await _unitOfWork.Oficinas.GetAllAsync();
        return _mapper.Map<List<Oficina>>(variablePlural);
    }

    /* Get Data by ID */
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Oficina>> Get(string id)
    {
        var oficina = await _unitOfWork.Oficinas.GetByIdAsync(id);
        if (oficina == null)
        {
            return NotFound();
        }
        return _mapper.Map<Oficina>(oficina);
    }

    /* Get oficinas de españa */
    [HttpGet("GetOficinasEsp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<OficinaDto>>> GetOficinasEsp(string pais = "España")
    {
        var oficinas = await _unitOfWork.Oficinas.GetOficinasEsp(pais);
        if (oficinas == null)
        {
            return NotFound();
        }
        return _mapper.Map<List<OficinaDto>>(oficinas);
    }
}