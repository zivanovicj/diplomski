﻿using AutoMapper;
using Fuel9.DTO;
using Fuel9.Models;

namespace Fuel9.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleDTO>().ReverseMap()
                                            .ForMember(x => x.FuelType, y => y.MapFrom(z => z.FuelType))
                                            .ForMember(x => x.OdometerType, y => y.MapFrom(z => z.OdometerType))
                                            .ForMember(x => x.VehicleType, y => y.MapFrom(z => z.VehicleType))
                                            .ForMember(x => x.Transmission, y => y.MapFrom(z => z.Transmission));
            CreateMap<FuelLog, FuelLogDTO>().ReverseMap()
                                            .ForMember(x => x.FuelType, y => y.MapFrom(z => z.FuelType));

        }
    }
}