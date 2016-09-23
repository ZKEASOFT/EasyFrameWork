/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.IOC.Unity;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest.Service;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            new UnityRegister(new UnityContainer()).Regist();
        }
        [TestMethod]
        public void TestMethod1()
        {
            var s = ServiceLocator.Current.GetInstance<IExampleService>().Get(m => m.Id == 1);
        }
    }
}
