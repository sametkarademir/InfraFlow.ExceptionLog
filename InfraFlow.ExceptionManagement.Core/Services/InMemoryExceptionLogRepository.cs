using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using InfraFlow.ExceptionManagement.Abstractions.Models;

namespace InfraFlow.ExceptionManagement.Core.Services;

 /// <summary>
    /// Bellek içinde exception loglarını tutan basit repository implementasyonu
    /// (Test ve geliştirme için kullanılır)
    /// </summary>
    public class InMemoryExceptionLogRepository : IExceptionStorage
    {
        private readonly List<ExceptionLog> _logs = new();
        private readonly object _lock = new();

        /// <summary>
        /// Exception log kaydeder
        /// </summary>
        public Task AddExceptionLogAsync(ExceptionLog log)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            lock (_lock)
            {
                _logs.Add(log);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// ID'ye göre exception log getirir
        /// </summary>
        public Task<ExceptionLog> GetExceptionLogByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID cannot be null or empty", nameof(id));

            lock (_lock)
            {
                var log = _logs.FirstOrDefault(l => l.Id == id);
                return Task.FromResult(log);
            }
        }

        /// <summary>
        /// Belirli bir sorguya göre exception logları getirir
        /// </summary>
        public Task<ExceptionLogResult> GetExceptionLogsAsync(
            int page = 1, 
            int pageSize = 20, 
            DateTime? startDate = null, 
            DateTime? endDate = null, 
            string source = null, 
            string userId = null)
        {
            // Minimum değerler kontrolü
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            IEnumerable<ExceptionLog> query;

            lock (_lock)
            {
                // Filtreleri uygula
                query = _logs.AsEnumerable();

                if (startDate.HasValue)
                    query = query.Where(l => l.Timestamp >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(l => l.Timestamp <= endDate.Value);

                if (!string.IsNullOrEmpty(source))
                    query = query.Where(l => l.Source == source);

                if (!string.IsNullOrEmpty(userId))
                    query = query.Where(l => l.UserId == userId);

                // Toplam kayıt sayısı
                var totalCount = query.Count();

                // Sıralama ve sayfalama
                var items = query
                    .OrderByDescending(l => l.Timestamp)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Sonuç nesnesi
                var result = new ExceptionLogResult
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
                };

                return Task.FromResult(result);
            }
        }
    }