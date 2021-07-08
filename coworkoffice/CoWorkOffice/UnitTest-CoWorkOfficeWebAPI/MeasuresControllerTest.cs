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
    public class MeasuresControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestGetIoTDevices()
        {
            var context = new DbContextFactory().Create();
            var controller = new MeasuresController(context);
            var response = (await controller.GetMeasures(null)).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            var measure = ((List<MeasureDTO>)response.Value).Where(f=>f.data == DateTime.MinValue).FirstOrDefault();
            Assert.AreEqual(999, measure.valore);
        }
    }
}