namespace CreationSharingPlatform.Controllers.v1.Models;

public class PutCreationRatingRequest
{
    public Guid CreationId { get; set; }
    public int UniquenessRating { get; set; }
    public int CreativityRating { get; set; }
}
