namespace LexHarvester.Application.Seeding;

public interface ITableSync
{
    /// <summary>
    /// Represents the order in which the table sync should be executed.
    /// Lower values indicate higher priority.
    /// Higher values indicate lower priority.
    /// This is used to determine the sequence of execution for multiple sync operations.
    /// If two syncs have the same order, they will be executed in the order they were registered.
    /// For example, if you have two syncs with orders 1 and 2, the one with order 1 will be executed first.
    /// If you have two syncs with the same order, they will be executed in the order they were registered.
    /// This is useful for ensuring that certain tables are synced before others, or that certain data is available before other operations are performed.
    /// </summary>
    int Order { get; }
    virtual string Name { get => this.GetType().Name; }   
    Task SyncAsync(CancellationToken cancellationToken = default);
}
