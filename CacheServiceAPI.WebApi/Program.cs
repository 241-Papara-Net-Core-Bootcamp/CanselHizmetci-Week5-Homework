using CacheServiceAPI.Data.Abstracts;
using CacheServiceAPI.Data.Concretes;
using CacheServiceAPI.Data.Configurations;
using CacheServiceAPI.Data.Context;
using CacheServiceAPI.HangfireService.Abstracts;
using CacheServiceAPI.HangfireService.Concretes;
using CacheServiceAPI.Service.Abstracts;
using CacheServiceAPI.Service.Concretes;
using CacheServiceAPI.Service.Mapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CacheServiceAPI.WebApi
{
    public class Program
    {
        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContextConStr")));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.Configure<CacheConfiguration>(builder.Configuration.GetSection("CacheConfiguration"));
            builder.Services.AddScoped<IMemoryCache, MemoryCache>();
            builder.Services.AddScoped<ICacheService, MemoryCacheService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<RemoteApiService, RemoteApiService>();
            builder.Services.AddScoped<IHangfireService, HangfireService.Concretes.HangfireService>();
            builder.Services.AddHangfire(c =>
                c.UseSqlServerStorage(builder.Configuration.GetConnectionString("AppDbContextConStr")));


            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddMemoryCache();

            builder.Services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));
            builder.Services.AddHangfireServer();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs");

            app.MapControllers();
            var serviceScopeFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var _service = (IHangfireService)scope.ServiceProvider.GetService(typeof(IHangfireService));
                _service.Run();
                // …
            }
            //var postServ = app.Services.GetService<IPostService>();
            //RecurringJob.AddOrUpdate();
            app.Run();
        }
    }
}