using CoWorkOffice.Data;
using CoWorkOfficeModel.Models.DTO;
using CoWorkOfficeWebApi.Controllers;
using CoWorkOfficeWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using MQTTService;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest_CoWorkOfficeWebAPI
{
    public class IoTDevicesControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetIoTDevices()
        {
            var context = new DbContextFactory().Create();
            var controller = new IoTDevicesController(context);
            var response = (await controller.GetIoTDevices(1)).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("Lampadina 1", ((List<IotDeviceDTO>)response.Value)[0].description);
            Assert.AreEqual("Lampadina 2", ((List<IotDeviceDTO>)response.Value)[1].description);
            Assert.AreEqual("Lampadina 3", ((List<IotDeviceDTO>)response.Value)[2].description);
        }
    }
}