﻿using Google.Apis.Services;
using System;

namespace YoutubeAPI.Integration.Infra.ExternalServices.GoogleAPI.Services
{
    public class GoogleServiceManager<TService> : IGoogleServiceManager<TService> where TService : BaseClientService
    {
        public TService GetService()
        {
            return this.CreateService();
        }

        private TService CreateService()
        {
            var service = (TService)Activator.CreateInstance(typeof(TService), new object[] { new BaseClientService.Initializer() });
            return service;
        }
    }
}
