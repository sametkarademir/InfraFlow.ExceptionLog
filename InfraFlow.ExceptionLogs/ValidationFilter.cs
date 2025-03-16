using FluentValidation;
using InfraFlow.Domain.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace InfraFlow.ExceptionLogs;

    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Action parametrelerini dolaş
            foreach (var kvp in context.ActionArguments)
            {
                var argument = kvp.Value;
                if (argument == null) continue;

                var argumentType = argument.GetType();
                
                // Bu parametre için validatörleri bul
                var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
                var validators = context.HttpContext.RequestServices.GetServices(validatorType).ToList();
                
                if (!validators.Any()) continue;

                // Her validatör için validasyon yap
                var allErrors = new List<ValidationExceptionModel>();
                
                foreach (var validator in validators)
                {
                    // Validatörü kullanarak validasyon yap
                    var validationResults = await ValidateWithValidator(validator, argument);
                    
                    if (validationResults.Any())
                    {
                        allErrors.AddRange(validationResults);
                    }
                }
                
                // Eğer hata varsa, BadRequest dön
                if (allErrors.Any())
                {
                    throw new AppValidationException(allErrors);
                }
            }

            // Validasyon başarılı, devam et
            await next();
        }
        
        // Validatör ile validasyon yapan yardımcı metot
        private async Task<List<ValidationExceptionModel>> ValidateWithValidator(object validator, object entity)
        {
            var errors = new List<ValidationExceptionModel>();
            
            try
            {
                // ValidateAsync metodunun doğru imzası - IValidator<T> arayüzünden alın
                // FluentValidation ValidateAsync metodu, entity ve CancellationToken parametrelerini alır
                var validatorType = validator.GetType();
                
                // Doğru ValidateAsync metodunu bul
                var validateMethod = validatorType.GetMethods()
                    .FirstOrDefault(m => 
                        m.Name == "ValidateAsync" && 
                        m.GetParameters().Length == 2 && 
                        m.GetParameters()[1].ParameterType == typeof(System.Threading.CancellationToken));
                        
                if (validateMethod == null) return errors;
                
                // Default CancellationToken ile birlikte çağır
                var task = (Task)validateMethod.Invoke(validator, new[] { 
                    entity, 
                    System.Threading.CancellationToken.None 
                });
                
                await task.ConfigureAwait(false);
                
                // ValidationResult'a eriş
                var resultProperty = task.GetType().GetProperty("Result");
                if (resultProperty == null) return errors;
                
                var validationResult = resultProperty.GetValue(task);
                
                // IsValid kontrolü
                var isValidProperty = validationResult.GetType().GetProperty("IsValid");
                if (isValidProperty == null) return errors;
                
                bool isValid = (bool)isValidProperty.GetValue(validationResult);
                if (isValid) return errors;
                
                // Hataları al
                var errorsProperty = validationResult.GetType().GetProperty("Errors");
                if (errorsProperty == null) return errors;
                
                var validationErrors = (IEnumerable<object>)errorsProperty.GetValue(validationResult);
                
                // Hataları grupla
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
                
                // Grupları ValidationErrorModel'e dönüştür
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


// Extension metot ile filtreyi eklemek için
public static class ValidationFilterExtensions
{
    public static IMvcBuilder AddGlobalValidationFilter(this IMvcBuilder builder)
    {
        builder.Services.AddScoped<ValidationFilter>();
        builder.AddMvcOptions(options => { options.Filters.Add<ValidationFilter>(); });

        return builder;
    }
}