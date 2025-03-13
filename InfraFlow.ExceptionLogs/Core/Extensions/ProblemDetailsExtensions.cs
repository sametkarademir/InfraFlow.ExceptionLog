using System.Text.Json;
using InfraFlow.ExceptionLogs.Infrastructure.HttpProblemDetails;

namespace InfraFlow.ExceptionLogs.Core.Extensions;

public static class ProblemDetailsExtensions
{
    public static string ToJson<TProblemDetail>(this TProblemDetail details) where TProblemDetail : AppProblemDetail
    {
        return JsonSerializer.Serialize(details);
    }
}