using InfraFlow.ExceptionManagement.Abstractions.Enums;
using InfraFlow.ExceptionManagement.Abstractions.Exceptions;
using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Abstractions.Models;
using InfraFlow.ExceptionManagement.Core.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InfraFlow.ExceptionManagement.Core.Services;

/// <summary>
    /// Exception loglama servisinin temel implementasyonu
    /// </summary>
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly IExceptionStorage _repository;
        private readonly ILogger<ExceptionLogger> _logger;
        private readonly ExceptionLoggingOptions _options;
        private readonly IEnumerable<ILogSink> _sinks;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionLogger(
            IExceptionStorage repository,
            ILogger<ExceptionLogger> logger,
            IOptions<ExceptionLoggingOptions> options,
            IEnumerable<ILogSink> sinks)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _sinks = sinks ?? Array.Empty<ILogSink>();
        }

        /// <summary>
        /// Exception'ı loglar
        /// </summary>
        public async Task LogExceptionAsync(Exception exception, string source, string userId = null, string additionalInfo = null)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            try
            {
                // Exception log kaydını oluştur
                var log = CreateExceptionLog(exception, source, userId, additionalInfo);
                
                // Tüm sinklere yaz
                foreach (var sink in _sinks)
                {
                    try
                    {
                        await sink.WriteAsync(log);
                    }
                    catch (Exception ex)
                    {
                        // Sink hatası olması diğer işlemleri etkilemesin
                        if (_options.LogToStandardLogger)
                        {
                            _logger.LogError(ex, "Failed to write to log sink {SinkType}", sink.GetType().Name);
                        }
                    }
                }
                
                // Veritabanına kaydet
                await _repository.AddExceptionLogAsync(log);
                
                // Standart logger'a da logla
                if (_options.LogToStandardLogger)
                {
                    _logger.LogError(exception, "Exception caught and stored. Source: {Source}, UserId: {UserId}", 
                        source, userId ?? "Anonymous");
                }
            }
            catch (Exception ex)
            {
                // Loglama sırasında hata olması uygulamayı etkilemesin
                _logger.LogError(ex, "Failed to log exception. Original exception: {Message}", 
                    exception.Message);
            }
        }

        /// <summary>
        /// ID'ye göre exception log kaydını getirir
        /// </summary>
        public Task<ExceptionLog> GetExceptionLogByIdAsync(string id)
        {
            return _repository.GetExceptionLogByIdAsync(id);
        }

        /// <summary>
        /// Belirli bir sorgu kriterine göre exception loglarını getirir
        /// </summary>
        public Task<ExceptionLogResult> GetExceptionLogsAsync(
            int page = 1, 
            int pageSize = 20, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string source = null, 
            string userId = null)
        {
            return _repository.GetExceptionLogsAsync(
                page, 
                pageSize, 
                startDate, 
                endDate, 
                source, 
                userId);
        }

        /// <summary>
        /// Exception log kaydı oluşturur
        /// </summary>
        private ExceptionLog CreateExceptionLog(Exception exception, string source, string userId, string additionalInfo)
        {
            var log = new ExceptionLog
            {
                Type = exception.GetType().FullName,
                Message = exception.Message,
                StackTrace = _options.IncludeFullStackTrace ? exception.StackTrace : null,
                Source = source,
                UserId = userId,
                AdditionalInfo = additionalInfo != null && additionalInfo.Length > _options.MaxAdditionalInfoLength
                    ? additionalInfo.Substring(0, _options.MaxAdditionalInfoLength)
                    : additionalInfo
            };

            // Özel exception tiplerini işle
            if (exception is AppExceptionBase appException)
            {
                log.Code = appException.Code;
                log.Detail = appException.Detail;
                log.Severity = appException.Severity;
            }
            else
            {
                log.Code = "SYSTEM_ERROR";
                log.Severity = ExceptionSeverity.Error;
            }

            // İç exception'ları işle
            if (_options.CaptureInnerExceptions && exception.InnerException != null)
            {
                log.InnerExceptionMessage = exception.InnerException.Message;
                log.InnerExceptionStackTrace = _options.IncludeFullStackTrace ? 
                    exception.InnerException.StackTrace : null;
            }

            return log;
        }
    }