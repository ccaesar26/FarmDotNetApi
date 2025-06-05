using FarmerTasksService.Extensions;
using FarmerTasksService.Models.Dtos;
using FarmerTasksService.Services.TaskHub;
using FarmerTasksService.Services.TaskService;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    IHubContext<TaskHub> hubContext,
    ILogger<TasksController> logger
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "ManagerOnly")]
    public async ValueTask<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        var userId = farmAuthorizationService.GetUserId();
        if (farmId.HasValue == false || userId == null || string.IsNullOrEmpty(farmAuthorizationService.GetUserRole()))
        {
            return Unauthorized();
        }

        try
        {
            var taskId = await taskService.CreateTaskAsync(dto, farmId.Value);
            await publishEndpoint.Publish(new TaskCreatedEvent(taskId, farmId.Value, userId.Value, DateTime.UtcNow));
            await hubContext.Clients.All.SendAsync("TaskCreated");
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
    public async ValueTask<IActionResult> GetTasks(
        [FromQuery] TaskFilterDto filter,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = int.MaxValue
    )
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        var tasks = await taskService.GetTasksAsync(filter, pageNumber, pageSize, farmId.Value);
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
            await hubContext.Clients.All.SendAsync("TaskUpdated");
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

    [HttpPost("{taskId}/assign")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> AssignUsersToTaskRequest(Guid taskId, AssignUsersToTaskRequest request)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.AssignUsersToTaskAsync(taskId, request.UserIds);
            request.UserIds.ForEach(async void (userId) =>
            {
                try
                {
                    await publishEndpoint.Publish(new TaskAssignedEvent(taskId, userId));
                }
                catch (Exception _)
                {
                    // ignored
                }
            });
            
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
    public async ValueTask<IActionResult> UnassignTask(Guid taskId, UnassignUsersFromTaskRequest request)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        if (farmId == null)
        {
            return Unauthorized();
        }

        try
        {
            await taskService.UnassignUsersFromTaskAsync(taskId, request.UserIds);
            request.UserIds.ForEach(async void (userId) =>
            {
                try
                {
                    await publishEndpoint.Publish(new TaskUnassignedEvent(taskId, userId));
                }
                catch (Exception _)
                {
                    // ignored
                }
            });
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
            await hubContext.Clients.All.SendAsync("TaskUpdated");
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
    public async ValueTask<IActionResult> GetMyTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = int.MaxValue)
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

        var tasks = await taskService.GetMyTasksAsync(userId.Value, pageNumber, pageSize);
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
        return Ok(categories.Select(c => c.ToDto()));
    }

    [HttpPost("comment")]
    [Authorize(Policy = "ManagerAndWorkers")]
    public async ValueTask<IActionResult> AddComment([FromBody] CreateTaskCommentDto dto)
    {
        var farmId = farmAuthorizationService.GetFarmId();
        var userId = farmAuthorizationService.GetUserId();
        var role = farmAuthorizationService.GetUserRole();
        if (farmId == null || userId == null || string.IsNullOrEmpty(role))
        {
            return Unauthorized();
        }
        
        var comment = new TaskCommentDto(Guid.Empty, dto.TaskId, userId.Value, DateTime.UtcNow, dto.Comment);

        try
        {
            var commentId = await taskService.AddCommentAsync(comment);
            await publishEndpoint.Publish(new TaskCommentAddedEvent(commentId, comment.TaskId));
            await hubContext.Clients.All.SendAsync("CommentAddedToTask", comment.TaskId, commentId);
            await hubContext.Clients.All.SendAsync("TaskUpdated");
            
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