namespace Shared.ResponceDto;

public class CustomResponseDto<T>
{
    public T Data { get; set; }
    
    public int StatusCode { get; set; }

    public List<string> Error { get; set; }
    
    
    public static CustomResponseDto<T> Success(int statusCode, T data)
    {
        return new CustomResponseDto<T>() { Data = data, StatusCode = statusCode };
    }

    public static CustomResponseDto<T> Success(int statusCode)
    {
        return new CustomResponseDto<T>() { StatusCode = statusCode };
    }

    public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
    {
        return new CustomResponseDto<T>() { StatusCode = statusCode, Error = errors };
    }

    public static CustomResponseDto<T> Fail(int statusCode, string error)
    {
        return new CustomResponseDto<T>() { StatusCode = statusCode, Error = new List<string> { error } };
    }

    public static CustomResponseDto<T> Fail(int statusCode)
    {
        return new CustomResponseDto<T>() { StatusCode = statusCode };
    }
}