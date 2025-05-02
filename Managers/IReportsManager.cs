using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Managers;

public interface IReportsManager
{
    Task<bool> ReportCreationAsync(Guid creationId, UserInfo userInfo, string reason);
}
