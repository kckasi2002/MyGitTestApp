using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OAuth_Project.Controllers;

namespace MyFirstUnitTest
{
    [TestClass]
    public class RoleControllerTests
    {
        [TestMethod]
        public void GetUnitTestPurpose_CheckResponse_ReturnOK()
        {
            RoleController roleController = new RoleController();

            int response = roleController.GetUnitTestPurpose();

            Assert.AreEqual("1", response.ToString());
        }
    }
}
