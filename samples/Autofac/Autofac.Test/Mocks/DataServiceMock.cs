﻿namespace Autofac.Test.Mocks
{
    using Web.Services;

    public class DataServiceMock : IDataService
    {
        public string GetData() => "Test Data";
    }
}
