﻿namespace MyTested.AspNetCore.Mvc.Test.Setups.Startups
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class CustomStartupWithDefaultBuildProvider
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IInjectedService, ReplaceableInjectedService>();
            
            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app) => app.UseMvcWithDefaultRoute();
    }
}
