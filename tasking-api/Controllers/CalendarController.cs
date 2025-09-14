using Microsoft.AspNetCore.Mvc;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly ICalendarService _calendarService;

        public CalendarController(ILogger<CalendarController> logger, ICalendarService calendarService)
        {
            _logger = logger;
            _calendarService = calendarService;
        }

        [HttpGet("{provider}/auth")]
        public async Task<IActionResult> GetAuthUrl(string provider, [FromQuery] string redirectUri, CancellationToken ct)
        {
            var result = await _calendarService.GetAuthUrlAsync(provider, redirectUri, ct);
            
            return result.Success
                ? Ok(new { authUrl = result.Value })
                : BadRequest(result.Error);
        }

        [HttpPost("{provider}/auth/callback")]
        public async Task<IActionResult> HandleAuthCallback(string provider, [FromQuery] string code, [FromQuery] string redirectUri, CancellationToken ct)
        {
            var result = await _calendarService.HandleAuthCallbackAsync(provider, code, redirectUri, ct);
            
            return result.Success
                ? Ok(new { message = "Authentication successful" })
                : BadRequest(result.Error);
        }

        [HttpGet("{provider}/calendars")]
        public async Task<IActionResult> GetCalendars(string provider, CancellationToken ct)
        {
            var result = await _calendarService.GetCalendarsAsync(provider, ct);
            
            return result.Success
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPost("{provider}/calendars")]
        public async Task<IActionResult> CreateCalendar(string provider, [FromBody] CreateCalendarRequest request, CancellationToken ct)
        {
            var result = await _calendarService.CreateCalendarAsync(provider, request.Name, request.Description, ct);
            
            return result.Success
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("{provider}/calendars/{calendarId}/events")]
        public async Task<IActionResult> GetEvents(string provider, string calendarId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, CancellationToken ct)
        {
            var result = await _calendarService.GetEventsAsync(provider, calendarId, startDate, endDate, ct);
            
            return result.Success
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPost("{provider}/calendars/{calendarId}/events")]
        public async Task<IActionResult> CreateEvent(string provider, string calendarId, [FromBody] CreateCalendarEventRequest request, CancellationToken ct)
        {
            var result = await _calendarService.CreateEventAsync(provider, calendarId, request, ct);
            
            return result.Success
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPut("{provider}/calendars/{calendarId}/events/{eventId}")]
        public async Task<IActionResult> UpdateEvent(string provider, string calendarId, string eventId, [FromBody] CreateCalendarEventRequest request, CancellationToken ct)
        {
            var result = await _calendarService.UpdateEventAsync(provider, calendarId, eventId, request, ct);
            
            return result.Success
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpDelete("{provider}/calendars/{calendarId}/events/{eventId}")]
        public async Task<IActionResult> DeleteEvent(string provider, string calendarId, string eventId, CancellationToken ct)
        {
            var result = await _calendarService.DeleteEventAsync(provider, calendarId, eventId, ct);
            
            return result.Success
                ? Ok(new { message = "Event deleted successfully" })
                : BadRequest(result.Error);
        }
    }

    public class CreateCalendarRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}