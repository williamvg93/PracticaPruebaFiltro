using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class OficinaRepo : GenericRepository<Oficina>, IOficina
{
    private readonly ApiGardensContext _context;

    public OficinaRepo(ApiGardensContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Oficina>> GetOficinasEsp(string pais)
    {

        return await (
            from ofi in _context.Oficinas
            where ofi.Pais == pais
            select ofi
        ).ToListAsync();


        /* return await (from cou in _context.Countries
                      join reg in _context.Regions
                      on cou.Id equals reg.IdCountryFk
                      join cit in _context.Cities
                      on reg.Id equals cit.IdRegionFk
                      join adr in _context.Addresses
                      on cit.Id equals adr.IdCityFk
                      join cli in _context.Clients
                      on adr.Id equals cli.IdAddressFk
                      where cou.Name == name
                      select new CountryClient
                      {
                          Name = cli.Name,
                          Country = cou.Name
                      }
        ).ToListAsync();
 */
    }
}