namespace LexHarvester.Application.Contracts.CQRS
{
    public interface IQuerySender
    {
        Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}
