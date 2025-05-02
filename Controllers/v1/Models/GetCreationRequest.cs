namespace CreationSharingPlatform.Controllers.v1.Models;

public class GetCreationRequest
{
    public Guid CreationId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Tags { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public List<Guid> Parts { get; set; } = null!;
}
