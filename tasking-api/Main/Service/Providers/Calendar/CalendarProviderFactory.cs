using tasking_api.Main.Service.Contracts.Providers;

namespace tasking_api.Main.Service.Providers.Calendar
{
    public class CalendarProviderFactory : ICalendarProviderFactory
    {
        private readonly IReadOnlyDictionary<string, ICalendarProvider> _byKey;

        public CalendarProviderFactory(IEnumerable<ICalendarProvider> providers)
        {
            _byKey = providers.ToDictionary(p => p.ProviderKey, StringComparer.OrdinalIgnoreCase);
        }

        public ICalendarProvider GetByKey(string providerKey)
        {
            if (_byKey.TryGetValue(providerKey, out var provider))
                return provider;

            throw new InvalidOperationException($"Unknown calendar provider '{providerKey}'.");
        }
    }
}
