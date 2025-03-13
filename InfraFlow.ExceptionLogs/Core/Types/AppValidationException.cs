using InfraFlow.ExceptionLogs.Core.Models;

namespace InfraFlow.ExceptionLogs.Core.Types;

public class AppValidationException : AppException
{
    public IEnumerable<ValidationExceptionModel> Errors { get; }

    public AppValidationException() : base("Validation failed.")
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public AppValidationException(string message) : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }
    
    public AppValidationException(string message, string detail) : base(message, detail)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public AppValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> arr = errors.Select(x =>
            $"{Environment.NewLine} -- {x.Property}: {string.Join(Environment.NewLine, values: x.Errors ?? Array.Empty<string>())}"
        );
        return $"Validation failed: {string.Join(string.Empty, arr)}";
    }
}