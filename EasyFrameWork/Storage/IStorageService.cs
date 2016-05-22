using Easy.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Storage
{
    public interface IStorageService:IDependency
    {
        string CreateFolder(string folder);
        string DeleteFolder(string folder);
        string SaveFile(string file);
        string DeleteFile(string file);
    }
}
