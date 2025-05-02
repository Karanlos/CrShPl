namespace CreationSharingPlatform.Models;

public class Creation
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string FileName { get; set; } = null!;
}
