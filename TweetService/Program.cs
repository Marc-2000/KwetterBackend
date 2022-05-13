using GreenPipes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TweetService.BLL.Repositories;
using TweetService.BLL.RepositoryInterfaces;
using TweetService.Consumers;
using TweetService.DAL.Context;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<ITweetRepository, TweetRepository>();

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
