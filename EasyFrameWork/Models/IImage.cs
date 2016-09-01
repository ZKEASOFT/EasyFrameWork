using Easy.IOC;
using System;

namespace Easy.Models
{
    public interface IImage : IEntity
    {
        string ImageUrl { get; set; }
        string ImageThumbUrl { get; set; }
    }
}
