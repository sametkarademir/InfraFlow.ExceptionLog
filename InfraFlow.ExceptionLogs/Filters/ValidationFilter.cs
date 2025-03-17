using FluentValidation;
using InfraFlow.Domain.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionLogs.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var kvp in context.ActionArguments)
        {
            var argument = kvp.Value;
            if (argument == null) continue;

            var argumentType = argument.GetType();

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
            var validators = context.HttpContext.RequestServices.GetServices(validatorType).ToList();

            if (!validators.Any())
                continue;

            var allErrors = new List<ValidationExceptionModel>();

            foreach (var validator in validators)
            {
                if (validator == null) continue;

                var validationResults = await ValidateWithValidator(validator, argument);

                if (validationResults.Any())
                {
                    allErrors.AddRange(validationResults);
                }
            }

            if (allErrors.Any())
            {
                throw new AppValidationException(allErrors);
            }
        }

        await next();
    }

    private async Task<List<ValidationExceptionModel>> ValidateWithValidator(object validator, object entity)
    {
        var errors = new List<ValidationExceptionModel>();

        try
        {
            var validatorType = validator.GetType();

            // Doğru ValidateAsync metodunu bul
            var validateMethod = validatorType.GetMethods()
                .FirstOrDefault(m =>
                    m.Name == "ValidateAsync" &&
                    m.GetParameters().Length == 2 &&
                    m.GetParameters()[1].ParameterType == typeof(System.Threading.CancellationToken));

            if (validateMethod == null) return errors;

            var task = (Task)validateMethod.Invoke(validator, new[]
            {
                entity,
                System.Threading.CancellationToken.None
            });

            await task.ConfigureAwait(false);

            var resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty == null) return errors;

            var validationResult = resultProperty.GetValue(task);

            var isValidProperty = validationResult.GetType().GetProperty("IsValid");
            if (isValidProperty == null) return errors;

            bool isValid = (bool)isValidProperty.GetValue(validationResult);
            if (isValid) return errors;

            var errorsProperty = validationResult.GetType().GetProperty("Errors");
            if (errorsProperty == null) return errors;

            var validationErrors = (IEnumerable<object>)errorsProperty.GetValue(validationResult);

            var groupedErrors = new Dictionary<string, List<string>>();

            foreach (var error in validationErrors)
            {
                var propertyNameProp = error.GetType().GetProperty("PropertyName");
                var errorMessageProp = error.GetType().GetProperty("ErrorMessage");

                if (propertyNameProp != null && errorMessageProp != null)
                {
                    string propertyName = (string)propertyNameProp.GetValue(error);
                    string errorMessage = (string)errorMessageProp.GetValue(error);

                    if (!groupedErrors.ContainsKey(propertyName))
                        groupedErrors[propertyName] = new List<string>();

                    groupedErrors[propertyName].Add(errorMessage);
                }
            }

            foreach (var group in groupedErrors)
            {
                errors.Add(new ValidationExceptionModel
                {
                    Property = group.Key,
                    Errors = group.Value
                });
            }
        }
        catch (Exception ex)
        {
            throw new AppBusinessException(ex.Message, "500");
        }

        return errors;
    }
}