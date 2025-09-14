namespace tasking_api.Main.Service.Contracts
{
    public interface ICalendarProviderFactory
    {
        public ICalendarProvider GetByKey(string key);
    }
}
