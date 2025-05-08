using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ReportsService.Models.Dtos;
using ReportsService.Models.Events;
using ReportsService.Services.ReportHub;
using ReportsService.Services.ReportService;
using Shared.FarmAuthorizationService;

namespace ReportsService.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ReportsController(
    Supabase.Client supabaseClient,
    IReportService reportService,
    IFarmAuthorizationService authorizationService,
    IHubContext<ReportHub> reportHub,
    ILogger<ReportsController> logger
) : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = "ManagerAndWorkers")] // Both can submit reports
    public async Task<IActionResult> CreateReport([FromBody] CreateReportDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Get User and Farm IDs from JWT claims
        var createdByUserId = authorizationService.GetUserId();
        var farmId = authorizationService.GetFarmId();

        if (!createdByUserId.HasValue || !farmId.HasValue)
        {
            return Unauthorized("User or Farm context missing.");
        }

        try
        {
            var reportId = await reportService.CreateReportAsync(dto, farmId.Value, createdByUserId.Value);
            // Optionally publish ReportCreatedEvent (consider what data is needed)
            // await _publishEndpoint.Publish(new ReportCreatedEvent(reportId, farmId, createdByUserId.Value, DateTime.UtcNow));
            // Notify all clients about the new report
            await reportHub.Clients.All.SendAsync("ReportCreated",
                new ReportCreatedEvent(reportId, farmId.Value, createdByUserId.Value));
            return CreatedAtAction(nameof(GetReport), new { id = reportId }, new { reportId });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating report for Farm {FarmId} by User {UserId}", farmId,
                createdByUserId.Value);
            return StatusCode(500, "An error occurred while creating the report.");
        }
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "ManagerAndWorkers")] // Allow both roles to view reports
    public async Task<IActionResult> GetReport(Guid id)
    {
        var reportDto = await reportService.GetReportByIdAsync(id);
        if (reportDto == null)
        {
            return NotFound();
        }

        return Ok(reportDto);
    }

    [HttpGet]
    [Authorize(Policy = "ManagerOnly")] // Only Managers see all reports by default? Adjust as needed.
    public async Task<IActionResult> GetReports([FromQuery] ReportFilterDto? filter, [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var farmId = authorizationService.GetFarmId();
        if (!farmId.HasValue)
        {
            return BadRequest("Invalid or missing FarmId claim.");
        }

        var reports = await reportService.GetReportsAsync(farmId.Value, filter?.Status, pageNumber, pageSize);
        return Ok(reports);
    }

    [HttpGet("my")]
    [Authorize(Policy = "ManagerAndWorkers")] // Endpoint for users to see reports *they* created
    public async Task<IActionResult> GetMyReports()
    {
        var userId = authorizationService.GetUserId();
        var farmId = authorizationService.GetFarmId();

        if (!userId.HasValue || !farmId.HasValue)
        {
            return Unauthorized("User or Farm context missing.");
        }

        var reports = await reportService.GetMyReportsAsync(userId.Value, farmId.Value);
        return Ok(reports);
    }

    [HttpPut("{id}/status")]
    [Authorize(Policy = "ManagerOnly")] // Only Managers can update status
    public async Task<IActionResult> UpdateReportStatus(Guid id, [FromBody] UpdateReportStatusDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedByUserId = authorizationService.GetUserId();
        if (!updatedByUserId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            await reportService.UpdateReportStatusAsync(id, dto.Status, updatedByUserId.Value);
            // Optionally publish ReportStatusUpdatedEvent
            // await _publishEndpoint.Publish(new ReportStatusUpdatedEvent(id, dto.Status, updatedByUserId.Value, DateTime.UtcNow));
            return NoContent(); // 204 No Content for successful update
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating status for report {ReportId} by User {UserId}", id,
                updatedByUserId.Value);
            return StatusCode(500, "An error occurred while updating the report status.");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManagerOnly")] // Only Managers can delete reports
    public async Task<IActionResult> DeleteReport(Guid id)
    {
        var deletedByUserId = authorizationService.GetUserId();
        if (!deletedByUserId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            await reportService.DeleteReportAsync(id, deletedByUserId.Value);
            // Optionally publish ReportDeletedEvent
            // await _publishEndpoint.Publish(new ReportDeletedEvent(id, deletedByUserId.Value, DateTime.UtcNow));
            return NoContent(); // 204 No Content
        }
        catch (KeyNotFoundException) // Could be thrown by service if desired
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting report {ReportId} by User {UserId}", id, deletedByUserId.Value);
            return StatusCode(500, "An error occurred while deleting the report.");
        }
    }

    // --- Comment Endpoints ---

    [HttpPost("{reportId}/comments")]
    [Authorize(Policy = "ManagerAndWorkers")] // Both can add comments
    public async Task<IActionResult> AddComment(Guid reportId, [FromBody] AddCommentDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = authorizationService.GetUserId();
        if (!userId.HasValue)
        {
            return Unauthorized();
        }

        try
        {
            var commentId = await reportService.AddCommentAsync(dto, reportId, userId.Value);
            // Optionally publish ReportCommentAddedEvent
            // await _publishEndpoint.Publish(new ReportCommentAddedEvent(commentId, reportId, userId.Value, DateTime.UtcNow));
            // Return the location of the new comment (optional but good practice)
            return CreatedAtAction(nameof(GetReport), new { id = reportId },
                commentId); // Point back to the report for simplicity for now
        }
        catch (KeyNotFoundException knfex) // If the report doesn't exist
        {
            logger.LogWarning("Attempted to add comment to non-existent report {ReportId}", reportId);
            return NotFound(knfex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding comment to report {ReportId} by User {UserId}", reportId, userId.Value);
            return StatusCode(500, "An error occurred while adding the comment.");
        }
    }

    [HttpGet("{reportId}/comments")]
    [Authorize(Policy = "ManagerAndWorkers")] // Both can view comments
    public async Task<IActionResult> GetComments(Guid reportId)
    {
        try
        {
            var comments = await reportService.GetCommentsAsync(reportId);
            // Check if the report itself exists implicitly by checking comments (or add an explicit check)
            // if (comments == null) { /* Handle case where report doesn't exist if service returns null */ }
            return Ok(comments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting comments for report {ReportId}", reportId);
            return StatusCode(500, "An error occurred while retrieving comments.");
        }
    }
}