using CreationSharingPlatform.Common;
using CreationSharingPlatform.Managers;
using CreationSharingPlatform.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreationSharingPlatform.Controllers.v1.Admin;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.AdminPermission)]
[Route("api/v{apiVersion:apiVersion}/Admin/[controller]")]
public class CreationsController : ControllerBase
{
    private readonly ICreationsManager _creationsManager;
    private readonly IPartsManager _partsManager;

    public CreationsController(
        ICreationsManager creationsManager,
        IPartsManager partsManager)
    {
        _creationsManager = creationsManager;
        _partsManager = partsManager;
    }

    [HttpDelete("user/{userId}")]
    public async Task<IActionResult> DeleteCreationsByUserIdAsync(Guid userId)
    {
        var userInfo = new UserInfo(User);
        await _creationsManager.DeleteCreationsByUserIdAsync(userInfo);
        return Ok();
    }

}
