using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AzureFunctions.Todo.DataContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AzureFunctions.Todo.Models;
using AzureFunctions.Todo.Models.Dto;
using System;
using AzureFunctions.Todo.Common;

namespace AzureFunctions.Todo
{
    public class TodoApi
    {
        public const string RoutePrefix = "todo";
        private readonly TodoContext _context;

        public TodoApi(TodoContext context)
        {
            _context = context;
        }

        [FunctionName("GetTodos")]
        public async Task<IActionResult> GetTodos([HttpTrigger(AuthorizationLevel.Anonymous, FuncAction.GET, Route = RoutePrefix)]
            HttpRequest req, ILogger log)
        {

            log.LogInformation("Getting todo list items");
            var todos = await _context.Todos.ToListAsync();
            return new OkObjectResult(todos);
        }

        [FunctionName("GetTodoById")]
        public async Task<IActionResult> GetTodoById([HttpTrigger(AuthorizationLevel.Anonymous, FuncAction.GET, Route = RoutePrefix + "/{id}")]
            HttpRequest req, ILogger log, int id)
        {
            log.LogInformation("Getting todo item by id");
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                log.LogInformation($"Item {id} not found");
                return new NotFoundResult();
            }
            return new OkObjectResult(todo);
        }

        [FunctionName("CreateTodo")]
        public async Task<IActionResult> CreateTodo([HttpTrigger(AuthorizationLevel.Anonymous, FuncAction.POST, Route = RoutePrefix)]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new todo list item");
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<TodoCreateModel>(requestBody);
            var todo = new Todos { Description = input.Description, CreatedTime = DateTime.Now };

            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return new OkObjectResult(todo);
        }

        [FunctionName("UpdateTodo")]
        public async Task<IActionResult> UpdateTodo([HttpTrigger(AuthorizationLevel.Anonymous, FuncAction.PUT, Route = RoutePrefix + "/{id}")]
            HttpRequest req, ILogger log, int id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<TodoUpdateModel>(requestBody);
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                log.LogWarning($"Item {id} not found");
                return new NotFoundResult();
            }

            todo.IsCompleted = updated.IsCompleted;
            if (!string.IsNullOrEmpty(updated.Description))
            {
                todo.Description = updated.Description;
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(todo);
        }

        [FunctionName("DeleteTodo")]
        public async Task<IActionResult> DeleteTodo([HttpTrigger(AuthorizationLevel.Anonymous, FuncAction.DELETE, Route = RoutePrefix + "/{id}")]
            HttpRequest req, ILogger log, int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                log.LogWarning($"Item {id} not found");
                return new NotFoundResult();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}