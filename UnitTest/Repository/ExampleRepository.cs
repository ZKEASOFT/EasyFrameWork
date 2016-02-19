using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using UnitTest.Models;

namespace UnitTest.Repository
{
    public class ExampleRepository : RepositoryBase<Example>, IExampleRepository
    {
    }
}
