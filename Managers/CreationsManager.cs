using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Managers;

public class CreationsManager : ICreationsManager
{
    public CreationsManager()
    {
        // Get EF context here
    }

    public Task<List<Creation>> FindCreationsByPartAsync(Guid partName, int amount, int page)
    {
        var result = new List<Creation>();

        result.Add(new Creation
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            FileName = "Test"
        });

        result.Add(new Creation
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            FileName = "Test"
        });

        return Task.FromResult(result);
    }

    public Task<Creation> GetCreationByIdAsync(Guid creationId)
    {
        return Task.FromResult(new Creation
        {
            Id = creationId,
            OwnerId = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            FileName = "Test"
        });
    }

    public Task<CreationRatingAggregate> GetCreationRatingAsync(Guid creationId)
    {
        return Task.FromResult(new CreationRatingAggregate
        {
            CreationId = creationId,
            AverageUniquenessRating = 0,
            AverageCreativityRating = 0,
        });
    }

    public Task<List<Creation>> GetCreationsAsync(int amount, int page)
    {
        var result = new List<Creation>();

        result.Add(new Creation
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            FileName = "Test"
        });

        result.Add(new Creation
        {
            Id = Guid.NewGuid(),
            OwnerId = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            FileName = "Test"
        });

        return Task.FromResult(result);
    }

    public Task<Creation> CreateCreationAsync(Creation creation, UserInfo user)
    {
        // Get Creation and check if user is owner, if not throw exception
        creation.OwnerId = user.UserId;
        return Task.FromResult(creation);
    }

    public Task<Creation> UpdateCreationAsync(Creation creation, UserInfo user)
    {
        // Get Creation and check if user is owner, if not throw exception
        creation.OwnerId = user.UserId;
        return Task.FromResult(creation);
    }

    public Task SetCreationPartsAsync(Guid creationId, List<Guid> partIds, UserInfo user)
    {
        return Task.FromResult(new Creation
        {
            Id = creationId,
            OwnerId = user.UserId,
            Name = "Test",
            Description = "Test",
            FileName = "Test"
        });
    }

    public Task SubmitRatingByIdAsync(Guid creationId, UserInfo user, double uniquenessRating, double creativityRating)
    {
        return Task.CompletedTask;
    }

    public Task DeleteCreationByIdAsync(Guid creationId, UserInfo user)
    {
        return Task.CompletedTask;
    }

    public Task<Uri> CreatePresignedUrlAsync(Guid creationId, UserInfo user)
    {
        return Task.FromResult(new Uri("https://example.com"));
    }

    public Task DeleteCreationsByUserIdAsync(UserInfo user)
    {
        // Get all creations by userId and delete them
        return Task.CompletedTask;
    }

}
