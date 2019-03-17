namespace CartTracker.Results
{
    public class QueryResult<TResponseData>
    {
        public TResponseData Data { get; set; }
        public string ErrorMessage { get; set; }

        public QueryResult(TResponseData data)
        {
            Data = data;
            ErrorMessage = null;
        }

        public QueryResult(TResponseData responseData, string errorMessage)
        {
            Data = responseData;
            ErrorMessage = errorMessage;
        }
    }
}
