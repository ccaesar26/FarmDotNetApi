using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.FarmAuthorizationService;
using Shared.Models.Events;
using TaskStatus = FarmerTasksService.Models.Enums.TaskStatus;

namespace FarmerTasksService.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController(
    ITaskService taskService,
    IFarmAuthorizationService farmAuthorizationService,
    IPublishEndpoint publishEndpoint,
    ILogger<TasksController> logger
    ) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            var taskId = await taskService.CreateTaskAsync(dto, TODO);
            await publishEndpoint.Publish(new TaskCreatedEvent(taskId));
            return Ok(taskId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating task");
            return StatusCode(500, "An error occurred while creating the task.");
        }
    }
    
    [HttpGet("{id}")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> GetTaskById(Guid id)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        var task = await taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }
    
    [HttpGet]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> GetTasks([FromQuery] TaskFilterDto filter)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        var tasks = await taskService.GetTasksAsync(filter, TODO, TODO, TODO);
        return Ok(tasks);
    }
    
    [HttpPut("{id}")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskDto dto)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.UpdateTaskAsync(id, dto);
            await publishEndpoint.Publish(new TaskUpdatedEvent(id));
            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating task");
            return StatusCode(500, "An error occurred while updating the task.");
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> DeleteTask(Guid id)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.DeleteTaskAsync(id);
            await publishEndpoint.Publish(new TaskDeletedEvent(id));
            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting task");
            return StatusCode(500, "An error occurred while deleting the task.");
        }
    }
    
    [HttpPost("{taskId}/assign/{userId}")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> AssignTask(Guid taskId, Guid userId)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.AssignUsersToTaskAsync(taskId, userId);
            await publishEndpoint.Publish(new TaskAssignedEvent(taskId, userId));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error assigning task");
            return StatusCode(500, "An error occurred while assigning the task.");
        }
    }
    
    [HttpPost("{taskId}/unassign")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> UnassignTask(Guid taskId)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.UnassignUsersFromTaskAsync(taskId, TODO);
            await publishEndpoint.Publish(new TaskUnassignedEvent(taskId));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error unassigning task");
            return StatusCode(500, "An error occurred while unassigning the task.");
        }
    }
    
    [HttpPut("{taskId}/status")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> UpdateTaskStatus(Guid taskId, [FromBody] TaskStatus status)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.UpdateTaskStatusAsync(taskId, status);
            await publishEndpoint.Publish(new TaskStatusUpdatedEvent(taskId, status.ToString()));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating task status");
            return StatusCode(500, "An error occurred while updating the task status.");
        }
    }
    
    [HttpGet("my")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> GetMyTasks()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        var userId = farmAuthorizationService.GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var tasks = await taskService.GetMyTasksAsync(userId.Value, TODO, TODO);
        return Ok(tasks);
    }
    
    [HttpGet("categories")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> GetTaskCategories()
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        var categories = await taskService.GetTaskCategoriesAsync();
        return Ok(categories);
    }
    
    [HttpPost("{taskId}/comments")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> AddComment(Guid taskId, [FromBody] CreateTaskCommentDto dto)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            var commentId = await taskService.AddCommentAsync(dto);
            await publishEndpoint.Publish(new TaskCommentAddedEvent(commentId, taskId));
            return Ok(commentId);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error adding comment");
            return StatusCode(500, "An error occurred while adding the comment.");
        }
    }
    
    [HttpGet("{taskId}/comments")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> GetComments(Guid taskId)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        var comments = await taskService.GetCommentsByTaskIdAsync(taskId);
        return Ok(comments);
    }
}