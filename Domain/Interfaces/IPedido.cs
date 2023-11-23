using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IPedido : IGenericRepository<Pedido>
{
    /* Task<Pedido> GetEstadosPedido(); */
    Task<IEnumerable<Pedido>> GetEstadosPedido();
}