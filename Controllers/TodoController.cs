using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private static List<TodoItem> todoItems = new List<TodoItem>
    {
        new TodoItem { Id = 1, Title = "Sample Todo", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Sample Todo2", IsCompleted = false }
    };

    // GET: api/todo
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetTodos()
    {
        return Ok(todoItems);
    }

    // GET: api/todo/{id}
    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetTodoById(int id)
    {
        var todo = todoItems.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    // POST: api/todo
    [HttpPost]
    public ActionResult<TodoItem> CreateTodoItem(TodoItem newTodo)
    {
        newTodo.Id = todoItems.Count + 1; // Simple ID generation
        todoItems.Add(newTodo);
        return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, newTodo);
    }

    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTodoItem(int id, TodoItem updatedTodo)
    {
        var todo = todoItems.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        todo.Title = updatedTodo.Title;
        todo.IsCompleted = updatedTodo.IsCompleted;
        return NoContent();
    }

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTodoItem(int id)
    {
        var todo = todoItems.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            return NotFound();
        }
        todoItems.Remove(todo);
        return NoContent();
    }
}
