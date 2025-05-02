using CreationSharingPlatform.Controllers.v1.Models;
using CreationSharingPlatform.Converters;
using CreationSharingPlatform.Managers;
using CreationSharingPlatform.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreationSharingPlatform.Controllers.v1;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v{apiVersion:apiVersion}/[controller]")]
public class CreationsController : ControllerBase
{
    private readonly ILogger<CreationsController> _logger;
    private readonly ICreationsManager _creationsManager;
    private readonly IReportsManager _reportsManager;
    private readonly IPartsManager _partsManager;

    public CreationsController(ILogger<CreationsController> logger, ICreationsManager creationsManager, IReportsManager reportsManager, IPartsManager partsManager)
    {
        _logger = logger;
        _creationsManager = creationsManager;
        _reportsManager = reportsManager;
        _partsManager = partsManager;
    }

    [HttpGet("{creationId}")]
    public async Task<IActionResult> GetCreationAsync(Guid creationId)
    {
        _logger.LogInformation("GetCreationAsync called with creationId: {creationId}", creationId);
        var creation = await _creationsManager.GetCreationByIdAsync(creationId);
        if (creation == null)
        {
            return NotFound();
        }
        return Ok(creation);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCreationAsync(CreateCreationRequest request)
    {
        var userInfo = new UserInfo(User);
        _logger.LogInformation("CreateCreationAsync called with request: {@request}", request);
        var creation = await _creationsManager.CreateCreationAsync(request.ToCreationModel(), userInfo);
        return CreatedAtAction(nameof(GetCreationAsync), new { creationId = creation.Id }, creation);
    }

    [HttpGet]
    public async Task<IActionResult> GetCreationsAsync([FromQuery] int amount = 10, [FromQuery] int page = 1)
    {
        _logger.LogInformation("GetCreationsAsync called with amount: {amount}, page: {page}", amount, page);
        var creations = await _creationsManager.GetCreationsAsync(amount, page);
        return Ok(creations);
    }

    [HttpPut("{creationId}")]
    public async Task<IActionResult> UpdateCreationAsync(UpdateCreationRequest request)
    {
        _logger.LogInformation("UpdateCreationAsync called with request: {@request}", request);

        var userInfo = new UserInfo(User);

        var creation = await _creationsManager.GetCreationByIdAsync(request.CreationId);

        if (creation == null)
        {
            return NotFound();
        }

        if (creation.OwnerId != userInfo.UserId)
        {
            return Forbid();
        }

        var updatedCreation = await _creationsManager.UpdateCreationAsync(request.ToCreationModel(), userInfo);

        return Ok(updatedCreation);
    }

    [HttpGet("search")]
    public async Task<IActionResult> FindCreationsByPartAsync(SearchCreationsRequest request, [FromQuery] Guid partId, [FromQuery] int amount = 10, [FromQuery] int page = 1)
    {
        _logger.LogInformation("FindCreationsByPartAsync called with partId: {partId}, amount: {amount}, page: {page}", partId, amount, page);
        var part = await _partsManager.FindPartByNameAsync(request.PartName);
        var creations = await _creationsManager.FindCreationsByPartAsync(part.Id, amount, page);
        return Ok(creations);
    }

    [HttpPut("{creationId}/parts")]
    public async Task<IActionResult> UpdateCreationPartsAsync(Guid creationId, UpdateCreationPartsRequest request)
    {
        _logger.LogInformation("UpdateCreationPartsAsync called with creationId: {creationId}, request: {@request}", creationId, request);

        var userInfo = new UserInfo(User);

        var creation = await _creationsManager.GetCreationByIdAsync(request.CreationId);

        if (creation == null)
        {
            return NotFound();
        }

        if (creation.OwnerId != userInfo.UserId)
        {
            return Forbid();
        }

        await _creationsManager.SetCreationPartsAsync(creationId, request.PartIds, userInfo);

        return Ok();
    }

    [HttpDelete("{creationId}")]
    public async Task<IActionResult> DeleteCreationAsync(Guid creationId)
    {
        _logger.LogInformation("DeleteCreationAsync called with creationId: {creationId}", creationId);
        var userInfo = new UserInfo(User);
        var creation = await _creationsManager.GetCreationByIdAsync(creationId);
        if (creation == null)
        {
            return NotFound();
        }
        if (creation.OwnerId != userInfo.UserId)
        {
            return Forbid();
        }
        await _creationsManager.DeleteCreationByIdAsync(creationId, userInfo);

        return Ok();
    }

    [HttpPut("{creationId}/rating")]
    public async Task<IActionResult> SubmitCreationRatingAsync(Guid creationId, PutCreationRatingRequest request)
    {
        _logger.LogInformation("UpdateCreationRatingAsync called with creationId: {creationId}, request: {@request}", creationId, request);
        var userInfo = new UserInfo(User);
        var creation = await _creationsManager.GetCreationByIdAsync(creationId);
        if (creation == null)
        {
            return NotFound();
        }

        await _creationsManager.SubmitRatingByIdAsync(creationId, userInfo, request.UniquenessRating, request.CreativityRating);

        return Ok();
    }

    [HttpPost("{creationId}/report")]
    public async Task<IActionResult> ReportCreationAsync(Guid creationId, ReportCreationRequest request)
    {
        _logger.LogInformation("ReportCreationAsync called with creationId: {creationId}, request: {@request}", creationId, request);
        var userInfo = new UserInfo(User);

        await _reportsManager.ReportCreationAsync(creationId, userInfo, request.Reason);

        return Ok();
    }
}
