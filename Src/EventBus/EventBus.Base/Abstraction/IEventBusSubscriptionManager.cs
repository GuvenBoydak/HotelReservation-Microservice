using EventBus.Base.Events;

namespace EventBus.Base.Abstraction;

public interface IEventBusSubscriptionManager
{
    bool IsEmpty { get; }
    event EventHandler<string>
        OnEventRemoved;
    void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>;
    void RemoveSubscription<T, TH>() where TH : IIntegrationEventHandler<T> where T : IntegrationEvent;
    bool HasSubscriptionForEvent(string eventName); 
    Type GetEventTypeByName(string eventName); 
    void Clear();

    IEnumerable<SubscriptionInfo>
        GetHandlersForEvent(
            string eventName); 
}