namespace LexHarvester.Application.Contracts.CQRS
{
    public interface ICommandSender
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
