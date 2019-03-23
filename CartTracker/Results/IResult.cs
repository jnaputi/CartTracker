namespace CartTracker.Results
{
    public interface IResult<out TData>
    {
        bool Successful { get; }
        TData Data { get; }
    }
}