using System.Diagnostics.CodeAnalysis;
using Markel.Application.Abstractions.Errors;

namespace Markel.Application.Abstractions.Results;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public Error Error { get; }
    
    public bool IsFailure => !IsSuccess;
    
    public static Result Success() => new(true, Error.None);
    
    public static Result Failure(Error error) => new(false, error);
    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Errors.Error.None);
    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);
    protected static Result<TValue> Create<TValue>(TValue? value) => 
                                        value is not null ? Success(value) : Failure<TValue>(Errors.Error.NullValue);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    public Result(TValue? value, bool isSuccess, Error error)
        :base(isSuccess, error)
    {
        _value = value;
    }
    
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");
    
    // needed to convert the response to a type Result<Response> in the handlers
    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}