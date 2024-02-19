﻿using Microsoft.Extensions.DependencyInjection;

namespace Module.Application
{
    public class DatabaseInitialization
    {
        private readonly IServiceCollection _services;
        public DatabaseInitialization(IServiceCollection services) {
            _services = services;
        }

        public void Initialize()
        {
            SeedData();
        }

        private void SeedData()
        {

        }
    }
}
