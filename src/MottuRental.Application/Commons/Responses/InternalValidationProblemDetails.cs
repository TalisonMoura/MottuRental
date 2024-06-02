namespace MottuRental.Application.Commons.Responses;

public class InternalValidationProblemDetails
{
    public string Title { get; set; }
    public IEnumerable<ErrorResponse> Errors { get; set; }

    public InternalValidationProblemDetails(IDictionary<string, string[]> errors)
    {
        Title = "One or more validation errors occurred.";
        Errors = errors.Select(x => new ErrorResponse { Key = x.Key, Value = x.Value.FirstOrDefault() });
    }

    public class ErrorResponse
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}