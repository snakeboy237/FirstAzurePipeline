﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Session;

    public class CustomSessionStore : ISessionStore
    {
        public bool IsAvailable => true;

        public ISession Create(
            string sessionKey, 
            TimeSpan idleTimeout, 
            TimeSpan ioTimeout, 
            Func<bool> tryEstablishSession, 
            bool isNewSessionKey)
        {
            return new CustomSession();
        }
    }
}
