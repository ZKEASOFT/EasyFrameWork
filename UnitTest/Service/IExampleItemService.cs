using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using UnitTest.Models;

namespace UnitTest.Service
{
    public interface IExampleItemService : IService<ExampleItem>
    {
    }
}
