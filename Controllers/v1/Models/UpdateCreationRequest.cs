namespace CreationSharingPlatform.Controllers.v1.Models;

public class UpdateCreationRequest
{
    public Guid CreationId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Tags { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string FileName { get; set; } = null!;
}
