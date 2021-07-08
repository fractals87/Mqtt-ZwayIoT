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
    public class GatewayControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetGateways()
        {
            var context = new DbContextFactory().Create();
            var mqtt = new MqttServiceApi();
            var controller = new GatewaysController(context, mqtt);
            var response = (await controller.GetGateways()).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("1", ((List<GatewayDTO>)response.Value)[0].id);
            Assert.AreEqual("2", ((List<GatewayDTO>)response.Value)[1].id);
        }

        [Test]
        public void TestConfigure()
        {
            var context = new DbContextFactory().Create();
            var mqtt = new MqttServiceApi();
            var controller = new GatewaysController(context, mqtt);
            var response = (controller.Configure()).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("OK", ((ApiMsgDTO)response.Value).code);
        }
    }
}