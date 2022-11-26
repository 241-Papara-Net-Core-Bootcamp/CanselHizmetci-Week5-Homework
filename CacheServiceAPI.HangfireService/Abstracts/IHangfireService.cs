using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;

namespace CacheServiceAPI.HangfireService.Abstracts
{
    public interface IHangfireService
    {
        [JobDisplayName("Fetch and save post every 5 seconds job")]
        void Run();
    }
}
