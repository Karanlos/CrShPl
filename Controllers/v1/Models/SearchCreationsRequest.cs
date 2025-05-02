using System.ComponentModel.DataAnnotations;

namespace CreationSharingPlatform.Controllers.v1.Models;

public class SearchCreationsRequest
{
    [Required]
    public string PartName { get; set; } = null!;
}
