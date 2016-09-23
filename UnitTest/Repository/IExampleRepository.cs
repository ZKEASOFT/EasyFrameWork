/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.RepositoryPattern;
using UnitTest.Models;

namespace UnitTest.Repository
{
    public interface IExampleRepository : IRepository<Example>
    {
    }
}
