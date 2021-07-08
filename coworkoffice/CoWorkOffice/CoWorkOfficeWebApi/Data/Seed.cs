using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWorkOfficeWebApi.Data
{
    public class Seed
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            #region Users
            if (!context.Users.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Users.AddRange(new User() { Id = 1, Name = "Paolo", Surname = "Franzini", Email = "paolo.franzini@3p-ictsoftware.it", Password = "Password1!", Role = "Admin" });
                    context.Users.AddRange(new User() { Id = 2, Name = "User1", Surname = "User1", Email = "user1@user.it", Password = "Password1!", Role ="User" });
                    context.Users.AddRange(new User() { Id = 3, Name = "User2", Surname = "User2", Email = "user2@user.it", Password = "Password1!", Role = "User" });
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");


                    context.Roles.AddRange(new Role() { Id = 1, Description = "Admin" });
                    context.Roles.AddRange(new Role() { Id = 2, Description = "User" });
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Roles OFF");

                    context.UserRoles.AddRange(new UserRole() { Id = 1, UserId = 1, RoleId = 1 });
                    context.UserRoles.AddRange(new UserRole() { Id = 2, UserId = 2, RoleId = 2 });
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.UserRoles ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.UserRoles OFF");

                    transaction.Commit();
                }

            }
            #endregion

            #region Room
            if (!context.Rooms.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    context.Rooms.AddRange(new WaitingRoom() { Id = 1, Description = "Sala Attesa", NMaxCustomer = 10 });
                    context.Rooms.AddRange(new Office() { Id = 2, Description = "Ufficio 1", WaitingRoomId = 1 });
                    context.Rooms.AddRange(new Office() { Id = 3, Description = "Ufficio 2", WaitingRoomId = 1 });


                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Rooms ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Rooms OFF");

                    transaction.Commit();
                }

            }
            #endregion

            #region Reservation
            if (!context.Reservations.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Reservations.AddRange(new Reservation() { Id = 1, DateTimeStart = DateTime.Now.Date.AddHours(8).AddMinutes(0), DateTimeEnd = DateTime.Now.Date.AddHours(20).AddMinutes(0), NCustomerExpected = 5, OfficeId = 2, UserId = 1 });
                    context.Reservations.AddRange(new Reservation() { Id = 2, DateTimeStart = DateTime.Now.Date.AddDays(1).AddHours(9).AddMinutes(0), DateTimeEnd = DateTime.Now.Date.AddDays(1).AddHours(18).AddMinutes(0), NCustomerExpected = 5, OfficeId = 2, UserId = 2 });
                    context.Reservations.AddRange(new Reservation() { Id = 3, DateTimeStart = DateTime.Now.Date.AddDays(1).AddHours(9).AddMinutes(0), DateTimeEnd = DateTime.Now.Date.AddDays(1).AddHours(13).AddMinutes(0), NCustomerExpected = 5, OfficeId = 3, UserId = 2 });
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Reservations ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Reservations OFF");

                    transaction.Commit();
                }

            }
            #endregion

            #region Gateway
            if (!context.Gateways.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Gateways.AddRange(new Gateway() { Id = 1, Description = "Gateway HUE", IP = "127.0.0.1", Port = 8000, Protocol = "ZigBee" });
                    context.Gateways.AddRange(new Gateway() { Id = 2, Description = "Gateway ZWay", IP = "Emulated", Port = 0, Protocol = "ZWave" });
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Gateways ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Gateways OFF");

                    transaction.Commit();
                }
            }
            #endregion

            #region ConfortParameters
            if (!context.ConfortParameters.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.ConfortParameters.AddRange(new ConfortParameter() { Id = 1, Parameter = "temperature", ConfortValue = 20 });
                    context.ConfortParameters.AddRange(new ConfortParameter() { Id = 2, Parameter = "luminosity", ConfortValue = 100 });
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ConfortParameters ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ConfortParameters OFF");

                    transaction.Commit();
                }
            }
            #endregion

            #region IoTDevices
            //https://zwayhomeautomation.docs.apiary.io/#reference/devices/devices-collection/list-all-devices?console=1
            if (!context.IoTDevices.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 1, Description = "Lampadina 1", DeviceType= "LightBulb", RegistrationIdentifier = "1", Gateway_Id = 1,  Room_Id = 1  });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 2, Description = "Sensore temperatura 1", DeviceType = "sensorMultilevel", ProbeType = "temperature", RegistrationIdentifier = "ZWayVDev_zway_11", Gateway_Id = 2, Room_Id = 1 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 3, Description = "Sensore Luminosità 1", DeviceType = "sensorMultilevel", ProbeType = "luminosity", RegistrationIdentifier = "ZWayVDev_zway_12", Gateway_Id = 2, Room_Id = 1 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 4, Description = "Termostato 1", DeviceType = "switchBinary", ProbeType = "thermostat_mode", RegistrationIdentifier = "ZWayVDev_zway_13", Gateway_Id = 2, Room_Id = 1 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 5, Description = "Sensore fumo 1", DeviceType = "SensorBinary", ProbeType = "smoke", RegistrationIdentifier = "ZWayVDev_zway_14", Gateway_Id = 2, Room_Id = 1 });

                    context.IoTDevices.AddRange(new IoTDevice() { Id = 6, Description = "Lampadina 2", DeviceType = "LightBulb", RegistrationIdentifier = "2", Gateway_Id = 1, Room_Id = 2 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 7, Description = "Sensore temperatura 2", DeviceType = "sensorMultilevel", ProbeType = "temperature", RegistrationIdentifier = "ZWayVDev_zway_21", Gateway_Id = 2, Room_Id = 2 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 8, Description = "Sensore Luminosità 2", DeviceType = "sensorMultilevel", ProbeType = "luminosity", RegistrationIdentifier = "ZWayVDev_zway_22", Gateway_Id = 2, Room_Id = 2 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id = 9, Description = "Termostato 2", DeviceType = "switchBinary", ProbeType = "thermostat_mode", RegistrationIdentifier = "ZWayVDev_zway_23", Gateway_Id = 2, Room_Id = 2 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id =10, Description = "Sensore fumo 2", DeviceType = "SensorBinary", ProbeType = "smoke", RegistrationIdentifier = "ZWayVDev_zway_24", Gateway_Id = 2, Room_Id = 2 });

                    context.IoTDevices.AddRange(new IoTDevice() { Id =11, Description = "Lampadina 3", DeviceType = "LightBulb", RegistrationIdentifier = "3",  Gateway_Id = 1, Room_Id = 3 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id =12, Description = "Sensore temperatura 3", DeviceType = "sensorMultilevel", ProbeType = "temperature", RegistrationIdentifier = "ZWayVDev_zway_31", Gateway_Id = 2, Room_Id = 3 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id =13, Description = "Sensore Luminosità 3", DeviceType = "sensorMultilevel", ProbeType = "luminosity", RegistrationIdentifier = "ZWayVDev_zway_32", Gateway_Id = 2, Room_Id = 3 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id =14, Description = "Termostato 3", DeviceType = "switchBinary", ProbeType = "thermostat_mode", RegistrationIdentifier = "ZWayVDev_zway_33", Gateway_Id = 2, Room_Id = 3 });
                    context.IoTDevices.AddRange(new IoTDevice() { Id =15, Description = "Sensore fumo 3", DeviceType = "SensorBinary", ProbeType = "smoke", RegistrationIdentifier = "ZWayVDev_zway_34", Gateway_Id = 2, Room_Id = 3 });

                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.IoTDevices ON");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.IoTDevices OFF");

                    transaction.Commit();
                }

            }
            #endregion

            #region Measure
            if (!context.Measures.Any())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Measures.AddRange(new Measure() {IoTDevice_Id = 1, DateTime = DateTime.MinValue, Value = 999 });
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }

            }
            #endregion

        }
    }
}
