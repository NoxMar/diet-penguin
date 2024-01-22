using DietPenguin.Application;
using DietPenguin.Core;
using DietPenguin.Domain;
using DietPenguin.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(cfg
    =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(DietPenguin.Core.Register));
    cfg.RegisterServicesFromAssemblyContaining(typeof(DietPenguin.Domain.Register));
    cfg.RegisterServicesFromAssemblyContaining(typeof(DietPenguin.Application.Register));
    cfg.RegisterServicesFromAssemblyContaining(typeof(DietPenguin.Infrastructure.Register));
});

builder.Services
    .RegisterCoreServices()
    .RegisterDomainServices()
    .RegisterApplicationServices()
    .RegisterInfrastructureServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();