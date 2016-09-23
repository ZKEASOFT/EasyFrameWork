/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using MvcApplication.Models;
using MvcApplication.Repository;

namespace MvcApplication.Service
{
    public class ExampleService : ServiceBase<Example>, IExampleService
    {
        private readonly IExampleRepository _exampleRepository;

        public ExampleService(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }
    }
}