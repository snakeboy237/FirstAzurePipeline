﻿namespace MyTested.AspNetCore.Mvc.Test.BuildersTests.DataTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal.Caching;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Setups;
    using Setups.Controllers;
    using Xunit;

    public class MemoryCacheBuilderTests : IDisposable
    {
        public MemoryCacheBuilderTests()
        {
            MyApplication
                .StartsFrom<DefaultStartup>()
                .WithServices(services =>
                {
                    services.AddMemoryCache();

                    services
                        .AddMvc()
                        .PartManager
                        .ApplicationParts
                        .Add(new AssemblyPart(this.GetType().Assembly));
                });
        }

        [Fact]
        public void WithEntryShouldSetCorrectValues()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry("Normal", "NormalValid")
                    .AndAlso()
                    .WithEntry("Another", "AnotherValid"))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel("Normal"));
        }

        [Fact]
        public void WithCacheOptionsShouldSetCorrectValues()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry("FullEntry", "FullEntryValid", new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    }))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new MemoryCacheEntryMock("FullEntry")
                    {
                        Value = "FullEntryValid",
                        AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    }));
        }

        [Fact]
        public void WithCacheBuilderShouldSetCorrectValues()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntry(entry => entry
                        .WithKey("FullEntry")
                        .AndAlso()
                        .WithValue("FullEntryValid")
                        .AndAlso()
                        .WithAbsoluteExpiration(new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)))
                        .AndAlso()
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(1))
                        .AndAlso()
                        .WithPriority(CacheItemPriority.High)
                        .AndAlso()
                        .WithSlidingExpiration(TimeSpan.FromMinutes(5))))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new MemoryCacheEntryMock("FullEntry")
                    {
                        Value = "FullEntryValid",
                        AbsoluteExpiration = new DateTimeOffset(new DateTime(2016, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(5)
                    }));
        }

        [Fact]
        public void WithCacheBuilderWithoutKeyShouldThrowException()
        {
            Test.AssertException<InvalidOperationException>(() =>
            {
                MyController<MemoryCacheController>
                    .Instance()
                    .WithMemoryCache(memoryCache => memoryCache
                        .WithEntry(entry => entry.WithKey(null)))
                    .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                    .ShouldReturn()
                    .Ok();
            },
            "Cache entry key must be provided. 'WithKey' method must be called with а non-null value.");
        }

        [Fact]
        public void WithCacheEntriesShouldSetCorrectValues()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .Calling(c => c.FullMemoryCacheAction(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }));
        }

        [Fact]
        public void WithoutMemoryCacheShouldReturnEmptyCache()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .WithoutMemoryCache()
                .Calling(c => c.GetCount(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok.WithModel(0));
        }

        [Fact]
        public void WithoutMemoryCacheShouldReturnEmptyCacheWhenClearingAlreadyEmptyCache()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(new Dictionary<object, object>()))
                .WithoutMemoryCache()
                .Calling(c => c.GetCount(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok.WithModel(0));
        }

        [Fact]
        public void WithoutMemoryEntryCacheShouldReturnCorrectCacheData()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .WithoutMemoryCache(cache => cache.WithoutEntry("second"))
                .Calling(c => c.GetAllEntities(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["third"] = "thirdValue"
                    }));
        }

        [Fact]
        public void WithoutMemoryCacheByKeyShouldReturnCorrectCacheData()
        {
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["second"] = "secondValue",
                        ["third"] = "thirdValue"
                    }))
                .WithoutMemoryCache("second")
                .Calling(c => c.GetAllEntities(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue",
                        ["third"] = "thirdValue"
                    }));
        }

        [Fact]
        public void WithoutMemoryCacheByKeysShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<object, object>
            {
                ["first"] = "firstValue",
                ["second"] = "secondValue",
                ["third"] = "thirdValue"
            };

            var entriesToDelete = entities.Select(x => x.Key).ToList();
            entriesToDelete.RemoveAt(0);

            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache.WithEntries(entities))
                .WithoutMemoryCache(entriesToDelete)
                .Calling(c => c.GetAllEntities(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue"
                    }));
        }

        [Fact]
        public void WithoutMemoryCacheByNonExistingKeysShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<object, object>
            {
                ["first"] = "firstValue",
                ["second"] = "secondValue",
                ["third"] = "thirdValue"
            };

            var entitiesToDelete = new List<string> { "key1", "key2" };
            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(entities))
                .WithoutMemoryCache(entitiesToDelete)
                .Calling(c => c.GetAllEntities(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(entities));
        }

        [Fact]
        public void WithoutMemoryCacheByNonExistingKeyShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<object, object>
            {
                ["first"] = "firstValue",
                ["second"] = "secondValue",
                ["third"] = "thirdValue"
            };

            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache
                    .WithEntries(entities))
                .WithoutMemoryCache("firstValue")
                .Calling(c => c.GetAllEntities(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(entities));
        }

        [Fact]
        public void WithoutMemoryCacheByParamKeysShouldReturnCorrectCacheData()
        {
            var entities = new Dictionary<object, object>
            {
                ["first"] = "firstValue",
                ["second"] = "secondValue",
                ["third"] = "thirdValue"
            };

            var entriesToDelete = entities.Select(x => x.Key).ToList();
            entriesToDelete.RemoveAt(0);

            MyController<MemoryCacheController>
                .Instance()
                .WithMemoryCache(memoryCache => memoryCache.WithEntries(entities))
                .WithoutMemoryCache(entriesToDelete[0], entriesToDelete[1])
                .Calling(c => c.GetAllEntities(From.Services<IMemoryCache>()))
                .ShouldReturn()
                .Ok(ok => ok
                    .WithModel(new Dictionary<object, object>
                    {
                        ["first"] = "firstValue"
                    }));
        }

        public void Dispose() => MyApplication.StartsFrom<DefaultStartup>();
    }
}
