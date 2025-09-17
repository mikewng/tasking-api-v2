namespace tasking_api.Main.Service.Contracts.Providers
{
    public interface ICalendarProviderFactory
    {
        public ICalendarProvider GetByKey(string key);
    }
}
