using InfraFlow.ExceptionManagement.Abstractions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfraFlow.ExceptionLogs.AspNetCore.Controllers;

/// <summary>
/// Exception loglarını görüntülemek için admin API'si
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")] // Sadece admin kullanıcılar erişebilir
public class ExceptionLogsController : ControllerBase
{
    private readonly IExceptionLogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    public ExceptionLogsController(IExceptionLogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Exception loglarını sayfalama ve filtreleme ile getirir
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetLogs(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] string source = null,
        [FromQuery] string userId = null)
    {
        var result = await _logger.GetExceptionLogsAsync(
            page,
            pageSize,
            startDate,
            endDate,
            source,
            userId);

        return Ok(result);
    }

    /// <summary>
    /// ID'ye göre log detaylarını getirir
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLogById(string id)
    {
        var log = await _logger.GetExceptionLogByIdAsync(id);
            
        if (log == null)
            return NotFound();
                
        return Ok(log);
    }
}