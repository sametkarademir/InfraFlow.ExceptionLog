using System.Text.Json;
using InfraFlow.ExceptionLogs.HttpProblemDetails;

namespace InfraFlow.ExceptionLogs.Extensions;

public static class ProblemDetailsExtensions
{
    public static string ToJson<TProblemDetail>(this TProblemDetail details) where TProblemDetail : AppProblemDetail
    {
        return JsonSerializer.Serialize(details);
    }
}