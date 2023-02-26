using EventBus.Base.Abstraction;
using UnitTest.EventBus.Events.Events;

namespace UnitTest.EventBus.Events.EventHandlers;

public class TestsIntegrationEventHandler: IIntegrationEventHandler<TestsIntegrationEvent>
{
    public Task Handle(TestsIntegrationEvent @event)
    {
        Console.WriteLine("Handler method work id: " + @event.Id);
        return Task.CompletedTask;
    }
}