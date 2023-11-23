using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGardens.Dtos.Get.Pago;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGardens.Controllers;

public class PagoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PagoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /* Get all Data from Table */
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Pago>>> Get()
    {
        var pedidos = await _unitOfWork.Pagos.GetAllAsync();
        return _mapper.Map<List<Pago>>(pedidos);
    }

    /* Get Data by ID */
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pago>> Get(string id)
    {
        var pedido = await _unitOfWork.Pagos.GetByIdAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        return _mapper.Map<Pago>(pedido);
    }


    /* Get Clientes que han realizado Pagos */
    [HttpGet("GetClientesPagos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClientePagoDto>>> GetClientesPagos()
    {
        var pedidos = await _unitOfWork.Pagos.GetClientesPagos();
        return _mapper.Map<List<ClientePagoDto>>(pedidos);
    }
}



