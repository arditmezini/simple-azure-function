# simple-azure-function
This project includes a TodoApi class (HTTP Function) which has 5 functions:
<pre>
	1. GetTodos	| GET	 | http://localhost:7071/api/todo)
	2. GetTodoById	| GET 	 | http://localhost:7071/api/todo/{id})
    	3. CreateTodo	| POST	 | http://localhost:7071/api/todo)
	4. UpdateTodo	| PUT	 | http://localhost:7071/api/todo/{id})
	5. DeleteTodo	| DELETE | http://localhost:7071/api/todo/{id})
</pre>
The ResetTodos class (Timer Function) runs at midnight and sets all todo as deleted.
