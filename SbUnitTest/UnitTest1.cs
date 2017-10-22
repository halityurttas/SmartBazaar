using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartBazaar.Data;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using SmartBazaar.Data.Entities;
using System.Web.Mvc;
using System.Dynamic;

namespace SbUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SmartBazaar.Web.App_Start.MapperConfig.RegisterMaps();
            SmartBazaar.Web.Business.Layers.SettingsLayer.RegisterAppSettings();
            SmartBazaar.Web.Controllers.ProductController productController = new SmartBazaar.Web.Controllers.ProductController();
            JsonResult result = productController.JSearch("henry") as JsonResult;
            Assert.IsNotNull(result);
        }
    }
}
