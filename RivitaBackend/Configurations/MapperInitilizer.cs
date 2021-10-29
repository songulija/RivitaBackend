using AutoMapper;
using RivitaBackend.Models;
using RivitaBackend.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Configurations
{
    // inherit from Profile which is basically automapper
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            // Transportation can map/convert to TransportationDTO, CreateTransportationDTO,UpdateTransformationDTO and vise versa
            CreateMap<Transportation, TransportationDTO>().ReverseMap();
            CreateMap<Transportation, CreateTransportationDTO>().ReverseMap();
            CreateMap<Transportation, UpdateTransportationDTO>().ReverseMap();
            // Wagon can map/convert to CreateWagonDTO, WagonDTO, UpdateWagonDTO
            CreateMap<Wagon, WagonDTO>().ReverseMap();
            CreateMap<Wagon, CreateWagonDTO>().ReverseMap();
            CreateMap<Wagon, UpdateWagonDTO>().ReverseMap();

        }
    }
}
