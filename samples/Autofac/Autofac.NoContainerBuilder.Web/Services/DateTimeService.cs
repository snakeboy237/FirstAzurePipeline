namespace Autofac.NoContainerBuilder.Web.Services
{
    using System;

    public class DateTimeService : IDateTimeService
    {
        public DateTime GetTime() => DateTime.UtcNow;
    }
}
