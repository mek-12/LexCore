namespace LexHarvester.Application.Jobs;

public interface IJob {
    Task Run(JobRequest jobRequest, DateTime date);
}