using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class PedidoRepo : GenericRepository<Pedido>, IPedido
{
    private readonly ApiGardensContext _context;

    public PedidoRepo(ApiGardensContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> GetEstadosPedido()
    {
        return await (
            from ped in _context.Pedidos
            group ped by ped.Estado into pedEstados
            select new Pedido
            {
                Estado = pedEstados.Key
            }
        ).ToListAsync();
    }
}