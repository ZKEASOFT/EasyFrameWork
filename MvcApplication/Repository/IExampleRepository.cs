using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using MvcApplication.Models;

namespace MvcApplication.Repository
{
    public interface IExampleRepository : IRepository<Example>
    {
    }
}
