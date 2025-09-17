using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;
using tasking_api.Main.Service.Contracts.Providers;

namespace tasking_api.Main.Service
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarProviderFactory _providerFactory;
        private readonly ILogger<CalendarService> _logger;

        public CalendarService(ICalendarProviderFactory providerFactory, ILogger<CalendarService> logger)
        {
            _providerFactory = providerFactory;
            _logger = logger;
        }

        public async Task<Result<string>> GetAuthUrlAsync(string provider, string redirectUri, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.GetAuthUrlAsync(redirectUri, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result<string>.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting auth URL for provider: {Provider}", provider);
                return Result<string>.Fail("Failed to get authentication URL");
            }
        }

        public async Task<Result> HandleAuthCallbackAsync(string provider, string authCode, string redirectUri, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.HandleAuthCallbackAsync(authCode, redirectUri, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling auth callback for provider: {Provider}", provider);
                return Result.Fail("Authentication failed");
            }
        }

        public async Task<Result<IEnumerable<CalendarDto>>> GetCalendarsAsync(string provider, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.GetCalendarsAsync(cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result<IEnumerable<CalendarDto>>.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting calendars for provider: {Provider}", provider);
                return Result<IEnumerable<CalendarDto>>.Fail("Failed to get calendars");
            }
        }

        public async Task<Result<CalendarDto>> CreateCalendarAsync(string provider, string name, string description, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.CreateCalendarAsync(name, description, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result<CalendarDto>.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating calendar for provider: {Provider}", provider);
                return Result<CalendarDto>.Fail("Failed to create calendar");
            }
        }

        public async Task<Result<IEnumerable<CalendarEventDto>>> GetEventsAsync(string provider, string calendarId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.GetEventsAsync(calendarId, startDate, endDate, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result<IEnumerable<CalendarEventDto>>.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting events for provider: {Provider}", provider);
                return Result<IEnumerable<CalendarEventDto>>.Fail("Failed to get events");
            }
        }

        public async Task<Result<CalendarEventDto>> CreateEventAsync(string provider, string calendarId, CreateCalendarEventRequest eventRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.CreateEventAsync(calendarId, eventRequest, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result<CalendarEventDto>.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating event for provider: {Provider}", provider);
                return Result<CalendarEventDto>.Fail("Failed to create event");
            }
        }

        public async Task<Result<CalendarEventDto>> UpdateEventAsync(string provider, string calendarId, string eventId, CreateCalendarEventRequest eventRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.UpdateEventAsync(calendarId, eventId, eventRequest, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result<CalendarEventDto>.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating event for provider: {Provider}", provider);
                return Result<CalendarEventDto>.Fail("Failed to update event");
            }
        }

        public async Task<Result> DeleteEventAsync(string provider, string calendarId, string eventId, CancellationToken cancellationToken = default)
        {
            try
            {
                var calendarProvider = _providerFactory.GetByKey(provider);
                return await calendarProvider.DeleteEventAsync(calendarId, eventId, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Invalid calendar provider: {Provider}", provider);
                return Result.Fail($"Unsupported calendar provider: {provider}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting event for provider: {Provider}", provider);
                return Result.Fail("Failed to delete event");
            }
        }
    }
}
