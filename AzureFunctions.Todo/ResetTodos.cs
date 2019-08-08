using System;
using AzureFunctions.Todo.DataContext;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Todo
{
    public class ResetTodos
    {
        private readonly TodoContext _context;

        public ResetTodos(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// At midnight set all daily todos completed
        /// </summary>
        /// <param name="myTimer"></param>
        /// <param name="log"></param>
        [FunctionName("ResetTodos")]
        public async void Run([TimerTrigger("0 0 0 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var todos = await _context.Todos.ToListAsync();
            foreach (var item in todos)
            {
                item.IsCompleted = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}