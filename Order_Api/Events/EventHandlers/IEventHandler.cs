namespace Order_Api.Events.EventHandlers
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }

}
