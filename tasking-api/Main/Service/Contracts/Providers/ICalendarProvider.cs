using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO;
using tasking_api.Main.Models.DTO.Request;

namespace tasking_api.Main.Service.Contracts.Providers
{
    public interface ICalendarProvider
    {
        string ProviderKey { get; }

        // Authentication
        Task<Result<string>> GetAuthUrlAsync(string redirectUri, CancellationToken cancellationToken = default);
        Task<Result> HandleAuthCallbackAsync(string authCode, string redirectUri, CancellationToken cancellationToken = default);

        // Calendar Operations
        Task<Result<IEnumerable<CalendarDto>>> GetCalendarsAsync(CancellationToken cancellationToken = default);
        Task<Result<CalendarDto>> CreateCalendarAsync(string name, string description, CancellationToken cancellationToken = default);

        // Event Operations
        Task<Result<IEnumerable<CalendarEventDto>>> GetEventsAsync(string calendarId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        Task<Result<CalendarEventDto>> CreateEventAsync(string calendarId, CreateCalendarEventRequest eventRequest, CancellationToken cancellationToken = default);
        Task<Result<CalendarEventDto>> UpdateEventAsync(string calendarId, string eventId, CreateCalendarEventRequest eventRequest, CancellationToken cancellationToken = default);
        Task<Result> DeleteEventAsync(string calendarId, string eventId, CancellationToken cancellationToken = default);
    }
}
