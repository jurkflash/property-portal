using Pokok.PropertyPortal.Infrastructure.Extensions;
using Pokok.BuildingBlocks.Cqrs.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCommandHandler<Pokok.PropertyPortal.Application.Commands.CreatePropertyCommand, Guid, Pokok.PropertyPortal.Application.Commands.CreatePropertyCommandHandler>();

ServiceCollectionExtensions.AddOutbox(builder.Services, builder.Configuration);
Pokok.PropertyPortal.Infrastructure.Properties.Extensions.ServiceCollectionExtensions.AddProperties(builder.Services, builder.Configuration);

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
