using CreationSharingPlatform.Controllers.v1.Models;
using CreationSharingPlatform.Models;

namespace CreationSharingPlatform.Converters;

public static class CreationConverter
{
    public static Creation ToCreationModel(this CreateCreationRequest request)
    {
        return new Creation
        {
            Name = request.Name,
            Description = request.Description,
            FileName = request.FileName,
        };
    }

    public static Creation ToCreationModel(this UpdateCreationRequest request)
    {
        return new Creation
        {
            Name = request.Name,
            Description = request.Description,
            FileName = request.FileName,
        };
    }
}
