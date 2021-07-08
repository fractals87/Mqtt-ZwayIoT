using CoWorkOffice.Data;
using CoWorkOfficeModel.Models.DTO;
using CoWorkOfficeWebApi.Controllers;
using CoWorkOfficeWebApi.Data;
using Microsoft.AspNetCore.Mvc;
using MQTTService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest_CoWorkOfficeWebAPI
{
    public class RoomsControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetRooms()
        {
            var context = new DbContextFactory().Create();
            var controller = new RoomsController(context);
            var response = (await controller.GetRooms()).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("2", ((List<OfficeDTO>)response.Value)[0].Id);
            Assert.AreEqual("3", ((List<OfficeDTO>)response.Value)[1].Id);
        }
    }
}