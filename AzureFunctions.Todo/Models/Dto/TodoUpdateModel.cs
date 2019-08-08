namespace AzureFunctions.Todo.Models.Dto
{
    public class TodoUpdateModel : TodoCreateModel
    {
        public bool IsCompleted { get; set; }
    }
}
