using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGardens.Dtos.Get.Pedido;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiGardens.Controllers;

public class PedidoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PedidoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /* Get all Data from Table */
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Pedido>>> Get()
    {
        var pedidos = await _unitOfWork.Pedidos.GetAllAsync();
        return _mapper.Map<List<Pedido>>(pedidos);
    }

    /* Get Data by ID */
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pedido>> Get(string id)
    {
        var pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
        if (pedido == null)
        {
            return NotFound();
        }
        return _mapper.Map<Pedido>(pedido);
    }

    /* Get all Estados de Pedido */
    [HttpGet("GetEstadosPedido")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EstadoPedidoDto>>> GetEstadosPedido()
    {
        var pedidos = await _unitOfWork.Pedidos.GetEstadosPedido();
        return _mapper.Map<List<EstadoPedidoDto>>(pedidos);
    }
}