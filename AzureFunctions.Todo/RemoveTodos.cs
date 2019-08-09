using System;
using AzureFunctions.Todo.DataContext;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Todo
{
    public class RemoveTodos
    {
        private readonly TodoContext _context;

        public RemoveTodos(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// At midnight delete all todos
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
                item.IsDeleted = true;
            }
            await _context.SaveChangesAsync();
        }
    }
}