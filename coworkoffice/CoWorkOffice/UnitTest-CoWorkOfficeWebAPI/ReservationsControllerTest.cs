using CoWorkOffice.Data;
using CoWorkOfficeModel.Models.DTO;
using CoWorkOfficeWebApi.Controllers;
using CoWorkOfficeWebApi.Data;
using CoWorkOfficeWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQTTService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace UnitTest_CoWorkOfficeWebAPI
{
    public class ReservationsControllerTest
    {

        ReservationDTO myReservationDTO = new ReservationDTO() { };

        [SetUp]
        public void Setup()
        {
            myReservationDTO = new ReservationDTO
            {
                da = DateTime.Now.AddDays(10).ToString(),
                a = DateTime.Now.AddDays(10).AddHours(1).ToString(),
                clienti = 5,
                idroom = 2,
            };
        }

        [Test]
        public async Task Test1GetReservations()
        {
            var context = new DbContextFactory().Create();


            //var user = new GenericPrincipal(new ClaimsIdentity("paolo.franzini@3p-ictsoftware.it"), new string[] { "Admin" });
            var user = new GenericPrincipal(new GenericIdentity("paolo.franzini@3p-ictsoftware.it"), new string[] { "Admin" });
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var roomService = new RoomService(context);
            var controller = new ReservationsController(context, roomService);
            controller.ControllerContext = controllerContext;

            var response = (await controller.GetReservations()).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual(1, ((List<ReservationDTO>)response.Value)[0].id);
        }

        [Test]
        public async Task Test2GetReservation()
        {
            var context = new DbContextFactory().Create();
            var roomService = new RoomService(context);
            var controller = new ReservationsController(context, roomService);
            var response = (await controller.GetReservation(1)).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual(1, ((ReservationDTO)response.Value).id);

            response = (await controller.GetReservation(999)).Result as ObjectResult;
            Assert.AreEqual(400, response.StatusCode.Value);
        }

        [Test]
        public async Task Test3PostReservation()
        {
            var context = new DbContextFactory().Create();

            var user = new GenericPrincipal(new GenericIdentity("paolo.franzini@3p-ictsoftware.it"), new string[] { "Admin" });
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var roomService = new RoomService(context);
            var controller = new ReservationsController(context, roomService);
            controller.ControllerContext = controllerContext;

            var response = (await controller.PostReservation(myReservationDTO)).Result as ObjectResult;
            if(response.StatusCode.Value == 200)
            {
                Assert.AreEqual(200, response.StatusCode.Value);
                Assert.NotNull(response.Value);
                Assert.AreEqual("Inserimento avvenuto con successo", ((ApiMsgDTO)response.Value).message);
                myReservationDTO.id = Convert.ToInt32(((ApiMsgDTO)response.Value).code);
            }
            else
            {
                Assert.AreEqual("Ufficio scelto è occupato nella fascia indicata", ((ApiMsgDTO)response.Value).message);
            }

        }

        [Test]
        public async Task Test5PutReservation()
        {
            var context = new DbContextFactory().Create();

            var user = new GenericPrincipal(new GenericIdentity("paolo.franzini@3p-ictsoftware.it"), new string[] { "Admin" });
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var roomService = new RoomService(context);
            var controller = new ReservationsController(context, roomService);
            controller.ControllerContext = controllerContext;

            var response = (await controller.PostReservation(myReservationDTO)).Result as ObjectResult;
            Assert.AreEqual(400, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("Ufficio scelto è occupato nella fascia indicata", ((ApiMsgDTO)response.Value).message);
        }

        [Test]
        public async Task Test6PutReservation()
        {
            var context = new DbContextFactory().Create();

            var user = new GenericPrincipal(new GenericIdentity("paolo.franzini@3p-ictsoftware.it"), new string[] { "Admin" });
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var roomService = new RoomService(context);
            var controller = new ReservationsController(context, roomService);
            controller.ControllerContext = controllerContext;

            var customMyReservationDTO = myReservationDTO;
            customMyReservationDTO.idroom = 3;
            customMyReservationDTO.clienti = 10;


            var response = (await controller.PostReservation(myReservationDTO)).Result as ObjectResult;
            Assert.AreEqual(400, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("Sala d'attesa piena nella fascia indicata, ridurre il numero di clienti o scelgliere altra fascia", ((ApiMsgDTO)response.Value).message);
        }

        [Test]
        public async Task Test7DeleteReservation()
        {
            var context = new DbContextFactory().Create();

            var user = new GenericPrincipal(new GenericIdentity("paolo.franzini@3p-ictsoftware.it"), new string[] { "Admin" });
            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var roomService = new RoomService(context);
            var controller = new ReservationsController(context, roomService);
            controller.ControllerContext = controllerContext;

            var customMyReservationDTO = myReservationDTO;
            customMyReservationDTO.da = DateTime.Now.AddDays(100).ToString();
            customMyReservationDTO.a = DateTime.Now.AddDays(101).ToString();

            var response = (await controller.PostReservation(myReservationDTO)).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("Inserimento avvenuto con successo", ((ApiMsgDTO)response.Value).message);
            myReservationDTO.id = Convert.ToInt32(((ApiMsgDTO)response.Value).code);

            //----START DELETE

            response = (await controller.DeleteReservation(myReservationDTO.id)).Result as ObjectResult;
            Assert.AreEqual(200, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("Cancellazione avvenuta con successo", ((ApiMsgDTO)response.Value).message);

            response = (await controller.DeleteReservation(myReservationDTO.id)).Result as ObjectResult;
            Assert.AreEqual(400, response.StatusCode.Value);
            Assert.NotNull(response.Value);
            Assert.AreEqual("Prenotazione non trovata", ((ApiMsgDTO)response.Value).message);
        }
    }
}