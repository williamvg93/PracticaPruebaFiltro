using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGardens.Dtos.Get.Oficina;
using ApiGardens.Dtos.Get.Pago;
using ApiGardens.Dtos.Get.Pedido;
using AutoMapper;
using Domain.Entities;

namespace ApiGardens.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Oficina, OficinaDto>()
        .ReverseMap();

        CreateMap<Pedido, EstadoPedidoDto>()
        .ReverseMap();

        CreateMap<Pago, ClientePagoDto>()
        .ReverseMap();
    }
}