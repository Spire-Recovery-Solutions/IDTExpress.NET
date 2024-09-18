namespace IDTExpress.NET.Models.Responses
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string ApiRequestId { get; set; }
        public ErrorDetail[] Errors { get; set; }
    }

    public class ErrorDetail
    {
        public int Code { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Field { get; set; }
        public string Source { get; set; }
    }
}
