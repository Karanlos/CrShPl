using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Managers;

public class ReportsManager : IReportsManager
{
    public Task<bool> ReportCreationAsync(Guid creationId, UserInfo userInfo, string reason)
    {
        return Task.FromResult(true);
    }
}
