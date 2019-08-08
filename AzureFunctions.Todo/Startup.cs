using AzureFunctions.Todo.DataContext;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureFunctions.Todo.Startup))]
namespace AzureFunctions.Todo
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connString = Environment.GetEnvironmentVariable("TodoContext");

            builder.Services.AddDbContext<TodoContext>(options =>
                SqlServerDbContextOptionsExtensions.UseSqlServer(options, connString));
        }
    }
}