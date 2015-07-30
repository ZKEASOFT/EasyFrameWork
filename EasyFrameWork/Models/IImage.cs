using Easy.IOC;

namespace Easy.Models
{
    public interface IImage : IEntity
    {
        string ImageUrl { get; set; }
        string ImageThumbUrl { get; set; }
    }
}
