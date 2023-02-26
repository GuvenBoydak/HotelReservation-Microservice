using EventBus.Base.Events;

namespace UnitTest.EventBus.Events.Events;

public class TestsIntegrationEvent:IntegrationEvent
{
    public int Id { get; }

    public TestsIntegrationEvent(int id)
    {
        Id = id;
    }
}