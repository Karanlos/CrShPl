
namespace CreationSharingPlatform.Models;

public class CreationRatingAggregate
{
    public Guid CreationId { get; set; }
    public double AverageUniquenessRating { get; set; }
    public double AverageCreativityRating { get; set; }
}
