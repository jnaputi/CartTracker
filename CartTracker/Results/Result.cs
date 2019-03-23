namespace CartTracker.Results
{
    public class Result<TResult> : IResult<TResult>
    {
        public bool Successful { get; }
        public TResult Data { get; }

        public Result(bool isSuccessful, TResult data)
        {
            Successful = isSuccessful;
            Data = data;
        }
        
        
    }
}