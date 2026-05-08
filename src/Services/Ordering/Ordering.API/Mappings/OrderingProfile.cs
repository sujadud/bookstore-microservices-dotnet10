using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Domain.Entities;

namespace Ordering.API.Mappings;

public class OrderingProfile : Profile
{
    public OrderingProfile()
    {
        CreateMap<BasketCheckoutEvent, Order>().ReverseMap();
    }
}
