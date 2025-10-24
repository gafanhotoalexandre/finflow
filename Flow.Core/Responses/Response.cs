using System.Text.Json.Serialization;

namespace Flow.Core.Responses;

public class Response<TData>
{
    private int _statusCode = Configuration.DefaultStatusCode;

    [JsonConstructor]
    public Response()
        => _statusCode = Configuration.DefaultStatusCode;

    public Response(
        TData? data,
        int statusCode = Configuration.DefaultStatusCode,
        string? message = null
    )
    {
        Data = data;
        _statusCode = statusCode;
        Message = message;
    }

    public TData? Data { get; set; }
    public string? Message { get; set; }

    [JsonIgnore]
    public bool IsSuccess => _statusCode is >= 200 and <= 299;
}
