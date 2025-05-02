using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Managers;

public interface ICreationsManager
{
    public Task<List<Creation>> GetCreationsAsync(int amount, int page);
    public Task<Creation> GetCreationByIdAsync(Guid creationId);
    public Task<CreationRatingAggregate> GetCreationRatingAsync(Guid creationId);
    public Task<Creation> CreateCreationAsync(Creation creation, UserInfo user);
    public Task<Creation> UpdateCreationAsync(Creation creation, UserInfo user);
    public Task SetCreationPartsAsync(Guid creationId, List<Guid> partIds, UserInfo user);
    public Task<List<Creation>> FindCreationsByPartAsync(Guid partName, int amount, int page);
    public Task SubmitRatingByIdAsync(Guid creationId, UserInfo user, double uniquenessRating, double creativityRating);
    public Task<Uri> CreatePresignedUrlAsync(Guid creationId, UserInfo user);
    public Task DeleteCreationByIdAsync(Guid creationId, UserInfo user);
    public Task DeleteCreationsByUserIdAsync(UserInfo user);
}
