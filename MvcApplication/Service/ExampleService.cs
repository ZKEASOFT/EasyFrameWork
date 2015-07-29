using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using MvcApplication.Models;

namespace MvcApplication.Service
{
    public class ExampleService : ServiceBase<Example>, IExampleService
    {
        public override void Add(Example item)
        {

            base.Add(item);
        }
    }
}