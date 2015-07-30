using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using MvcApplication.Models;

namespace MvcApplication.Service
{
    public interface IExampleService : IService<Example>
    {
    }
}
