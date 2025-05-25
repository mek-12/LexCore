namespace LexHarvester.Application.Jobs;


public class JobRequest<T> where T : JobRequestBaseModel
{

    public T? Job { get; }
    public DateTime Date { get; }
    
}
public class JobRequest : JobRequest<JobRequestBaseModel>
{
}
public class JobRequestBaseModel
{
}