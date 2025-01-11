namespace Order_Api.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
