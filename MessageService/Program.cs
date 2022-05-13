using GreenPipes;
using MassTransit;
using MessageService.BLL.Repositories;
using MessageService.BLL.RepositoryInterfaces;
using MessageService.Consumers;
using MessageService.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting Message Microservice");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, logConfig) =>
{
    logConfig.WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration)
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("Version", 1);
});

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
    {
        cur.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cur.ReceiveEndpoint("userQueue", oq =>
        {
            oq.PrefetchCount = 20;
            oq.UseMessageRetry(r => r.Interval(2, 100));
            oq.ConfigureConsumer<UserConsumer>(provider);
        });
    }));
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Kwetter_db_string"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
