using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using UnitTest.Models;

namespace UnitTest.Service
{
    public class ExampleItemService : ServiceBase<ExampleItem>, IExampleItemService
    {

    }
}