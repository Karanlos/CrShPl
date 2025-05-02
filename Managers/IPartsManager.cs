using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Managers;

public interface IPartsManager
{
    Task<List<Part>> GetPartsAsync();
    Task<Part> GetPartByIdAsync(Guid partId);
    Task<Part> FindPartByNameAsync(string partName);
}
