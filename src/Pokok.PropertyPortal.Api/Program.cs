using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.BuildingBlocks.Cqrs.Events;
using Pokok.BuildingBlocks.Cqrs.Extensions;
using Pokok.BuildingBlocks.Messaging.Abstractions;
using Pokok.BuildingBlocks.Messaging.RabbitMQ;
using Pokok.PropertyPortal.Application.Commands.CreateProperty;
using Pokok.PropertyPortal.Application.Commands.CreatePropertyUnit;
using Pokok.PropertyPortal.Application.Commands.RemovePropertyUnit;
using Pokok.PropertyPortal.Application.Queries.GetPropertyById;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddCommandHandler<CreatePropertyCommand, PropertyId, CreatePropertyCommandHandler>();
builder.Services.AddCommandHandler<CreatePropertyUnitCommand, PropertyUnitId, CreatePropertyUnitCommandHandler>();
builder.Services.AddCommandHandler<RemovePropertyUnitCommand, bool, RemovePropertyUnitCommandHandler>();

builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddQueryHandler<GetPropertyByIdQuery, Property, GetPropertyByIdQueryHandler>();
builder.Services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
builder.Services.AddSingleton<IMessagePublisher, RabbitMQMessagePublisher>();
builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection("RabbitMQ"));

ServiceCollectionExtensions.AddOutbox(builder.Services, builder.Configuration);
Pokok.PropertyPortal.Infrastructure.Properties.Extensions.ServiceCollectionExtensions.AddProperties(builder.Services, builder.Configuration);


//**********
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
////builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//// Register UnitOfWork
//builder.Services.AddScoped<IUnitOfWork, UnitOfWorkBase>(sp =>
//{
//    var context = sp.GetRequiredService<PropertyDbContext>();
//    var dispatcher = sp.GetRequiredService<IDomainEventDispatcher>();
//    return new UnitOfWorkBase(context, dispatcher);
//});
//*********



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
