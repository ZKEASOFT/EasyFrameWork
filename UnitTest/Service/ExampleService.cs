/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using UnitTest.Models;
using UnitTest.Repository;

namespace UnitTest.Service
{
    public class ExampleService : ServiceBase<Example>, IExampleService
    {
    }
}