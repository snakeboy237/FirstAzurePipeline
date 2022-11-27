﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Common
{
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Primitives;

    public class CustomFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return null;
        }

        public IChangeToken Watch(string filter)
        {
            return null;
        }
    }
}
