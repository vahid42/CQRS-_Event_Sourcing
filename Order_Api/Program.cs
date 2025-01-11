
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Order_Api.Data;
using Order_Api.Repository;
using Order_Api.Repository.Implementation;
using Order_Api.Utilities;
using StackExchange.Redis;

namespace Order_Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={DbPath}"));
            builder.Services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
            builder.Services.AddScoped<IWriteOrderRepository, WriteOrderRepository>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCommandHandlers(typeof(Program));
            builder.Services.AddQueryHandlers(typeof(Program));

            //builder.Services.AddEventHandlers(typeof(Program));
            builder.Services.AddSingleton<IEventListener>((provider =>
                new EventListener(provider.GetRequiredService<IDatabase>(),
                provider.GetRequiredService<IOptions<RedisConfig>>(),
                provider.GetRequiredService<ILogger<EventListener>>(),
                provider)));

            builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));
            var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetSection("RedisConfig:Url").Value!);
            var redisDatabase = redis.GetDatabase();
            builder.Services.AddSingleton(redisDatabase);

            var app = builder.Build();
            app.ListenForRedisEvents();
            using var serviceScope = app.Services.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        private static string DbPath()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return System.IO.Path.Join(path, "OrderDB.db");
        }
    }
}
