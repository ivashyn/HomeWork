﻿using AutoMapper;
using MatOrderingService.Domain.Models;

namespace MatOrderingService.Config.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Order, OrderInfo>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

            CreateMap<NewOrder, Order>();

            CreateMap<EditOrder, Order>();
        }
    }
}
