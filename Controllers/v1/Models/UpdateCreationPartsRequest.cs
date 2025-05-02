using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Controllers.v1.Models;

public class UpdateCreationPartsRequest
{
    public Guid CreationId { get; set; }
    public List<Guid> PartIds { get; set; } = new();
}
