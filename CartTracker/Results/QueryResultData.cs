namespace CartTracker.Results
{
    public class QueryResultData<TResponseData>
    {
        public TResponseData ResponseData { get; }
        public string Message { get; }

        public QueryResultData(TResponseData responseData, string message)
        {
            ResponseData = responseData;
            Message = message;
        }
    }
}