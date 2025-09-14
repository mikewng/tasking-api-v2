using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO;
using tasking_api.Main.Models.DTO.Request;

namespace tasking_api.Main.Service.Contracts
{
    public interface ICalendarService
    {
        // Authentication
        Task<Result<string>> GetAuthUrlAsync(string provider, string redirectUri, CancellationToken cancellationToken = default);
        Task<Result> HandleAuthCallbackAsync(string provider, string authCode, string redirectUri, CancellationToken cancellationToken = default);

        // Calendar Operations
        Task<Result<IEnumerable<CalendarDto>>> GetCalendarsAsync(string provider, CancellationToken cancellationToken = default);
        Task<Result<CalendarDto>> CreateCalendarAsync(string provider, string name, string description, CancellationToken cancellationToken = default);

        // Event Operations
        Task<Result<IEnumerable<CalendarEventDto>>> GetEventsAsync(string provider, string calendarId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        Task<Result<CalendarEventDto>> CreateEventAsync(string provider, string calendarId, CreateCalendarEventRequest eventRequest, CancellationToken cancellationToken = default);
        Task<Result<CalendarEventDto>> UpdateEventAsync(string provider, string calendarId, string eventId, CreateCalendarEventRequest eventRequest, CancellationToken cancellationToken = default);
        Task<Result> DeleteEventAsync(string provider, string calendarId, string eventId, CancellationToken cancellationToken = default);
    }
}
