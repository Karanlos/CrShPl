using System.Security.Claims;

namespace CreationSharingPlatform.Models;

public class UserInfo
{
    public Guid UserId { get; set; }
    public List<string> Claims { get; set; } = null!;

    public UserInfo (ClaimsPrincipal user)
    {
        UserId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
    }
}
