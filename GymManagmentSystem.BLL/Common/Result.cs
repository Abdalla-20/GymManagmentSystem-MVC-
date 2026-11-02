namespace GymManagmentSystem.BLL.Common;

public record Result (bool Success, string? error = null , ResultKind Kind = ResultKind.Ok)
{
    public static Result Ok() => new(true);

    public static Result NotFound(string error = "Not Found") => new(false, error, ResultKind.NotFound);

    public static Result Validation(string error ) => new(false, error, ResultKind.ValidationFailed);

    public static Result Fail(string error , ResultKind kind = ResultKind.Conflict) => new(false, error, kind);

}
public record Result<T>(bool Success, T? Value , string? error = null, ResultKind Kind = ResultKind.Ok)
{
    public static Result<T> Ok(T value) => new(true, value);
    public static Result<T> NotFound(string error = "Not Found") => new(false, default, error, ResultKind.NotFound);
    public static Result<T> Fail(string error, ResultKind kind = ResultKind.Conflict) => new(false, default, error, kind);
}
