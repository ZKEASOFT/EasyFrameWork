using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Models
{
    public interface IImage
    {
        string ImageUrl { get; set; }
        string ImageThumbUrl { get; set; }
    }
}
