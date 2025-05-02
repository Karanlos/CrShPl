using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Managers;

public class PartsManager : IPartsManager
{
    public Task<List<Part>> GetPartsAsync()
    {
        var result = new List<Part>
        {
            new Part
            {
                Id = Guid.NewGuid(),
                Name = "Part 1",
                Description = "Description of Part 1"
            },
            new Part
            {
                Id = Guid.NewGuid(),
                Name = "Part 2",
                Description = "Description of Part 2"
            }
        };

        return Task.FromResult(result);
    }

    public Task<Part> FindPartByNameAsync(string partName)
    {
        return Task.FromResult(new Part
        {
            Id = Guid.NewGuid(),
            Name = partName,
            Description = "Part Description"
        });
    }

    public Task<Part> GetPartByIdAsync(Guid partId)
    {
        return Task.FromResult(new Part
        {
            Id = partId,
            Name = "Part Name",
            Description = "Part Description"
        });
    }
}
