namespace Tutoring.Common.Primitives.Envelopes;

public class Envelope
{
    public int StatusCode { get; set; }
    public bool IsSuccess => StatusCode is >= 200 and < 300;
    public object? Data { get; set; }
    public string? Error { get; set; }
}