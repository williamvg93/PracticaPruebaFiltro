using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.Repository;

public class OficinaRepo : GenericRepository<Oficina>, IOficina
{
    private readonly ApiGardensContext _context;

    public OficinaRepo(ApiGardensContext context) : base(context)
    {
        _context = context;
    }
}