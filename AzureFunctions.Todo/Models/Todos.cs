using Newtonsoft.Json;
using System;

namespace AzureFunctions.Todo.Models
{
    public class Todos
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}