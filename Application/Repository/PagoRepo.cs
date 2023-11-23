using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class PagoRepo : GenericRepository<Pago>, IPago
{
    private readonly ApiGardensContext _context;

    public PagoRepo(ApiGardensContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pago>> GetClientesPagos()
    {
        return await (
            from pag in _context.Pagos
            where pag.FechaPago.Year == 2008
            group pag by new { pag.CodigoCliente } into pagClientes
            select new Pago
            {
                CodigoCliente = pagClientes.Key.CodigoCliente
            }
        ).ToListAsync();
    }
}