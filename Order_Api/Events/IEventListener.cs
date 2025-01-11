namespace Order_Api
{
    public interface IEventListener
    {
        Task Listen(CancellationToken token);
    }
}
