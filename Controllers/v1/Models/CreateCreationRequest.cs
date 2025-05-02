namespace CreationSharingPlatform.Controllers.v1.Models;

public class CreateCreationRequest
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public List<Guid> Parts { get; set; } = null!;
}
