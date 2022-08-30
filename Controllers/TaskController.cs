using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers;

[Controller]
[Route("api/[controller]")]
public class TaskController : Controller
{

    private readonly MongoDBService _mongoDBService;

    public TaskController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<TaskModel>> Get()
    {
        return await _mongoDBService.GetAsync();
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TaskModel taskInstance)
    {
        await _mongoDBService.CreateAsync(taskInstance);
        return CreatedAtAction(nameof(Get), new { id = taskInstance.Id }, taskInstance);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> updateAsync(string id, [FromBody] TaskModel task)
    {
        await _mongoDBService.updateAsync(id, task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }

}