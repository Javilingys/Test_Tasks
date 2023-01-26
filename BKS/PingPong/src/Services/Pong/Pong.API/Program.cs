using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pong.API.Extensions;
using Pong.API.Helpers;
using Pong.API.Infrastructure;
using Pong.API.Infrastructure.MappingProfiles;
using Pong.API.Models.Dtos.Responses;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
});

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.InvalidModelStateResponseFactory = (actionContext) =>
        {
            var errors = actionContext.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            var errorResponse = new ApiValidationErrorResponse
            {
                Errors = errors
            };

            return new BadRequestObjectResult(errorResponse);
        };
    });
builder.Services.AddDbContext<PongDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Миграция базы данных автоматическая.
await MigrateDatabaseHelper.MigrateDatabase(app);

await app.RunAsync();


