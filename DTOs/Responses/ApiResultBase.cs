namespace DTOs.Responses;

public class ApiResultBase<T>
{
    public bool IsSuccess { get; set; } = true;
    public string Errors { get; set; } = string.Empty;
    public T? Body { get; set; }
}