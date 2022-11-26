using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheServiceAPI.HangfireService.Abstracts;
using CacheServiceAPI.Service.Concretes;
using Hangfire;

namespace CacheServiceAPI.HangfireService.Concretes
{
    public class HangfireService:IHangfireService
    {
        private readonly RemoteApiService _remoteApiService;

        public HangfireService(RemoteApiService remoteApiService)
        {
            _remoteApiService = remoteApiService;
        }

        public void Run()
        {
            RecurringJob.AddOrUpdate("SaveApiDataToDatabase", () => _remoteApiService.SaveApiDataToDatabase(), "*/5 * * * * *");
        }
    }
}
