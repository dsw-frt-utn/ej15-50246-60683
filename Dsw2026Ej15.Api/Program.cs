
using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data.Implementations;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

        builder.Services.AddHealthChecks();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.MapHealthChecks("/health-check");

        app.Run();

    }
}