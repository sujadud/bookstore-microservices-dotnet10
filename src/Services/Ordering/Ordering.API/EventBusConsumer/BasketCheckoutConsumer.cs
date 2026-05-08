using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.API.EventBusConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketCheckoutConsumer> _logger;

    public BasketCheckoutConsumer(IOrderRepository repository, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var orderEntity = _mapper.Map<Order>(context.Message);
        var result = await _repository.AddOrder(orderEntity);

        _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result.Id);
    }
}
