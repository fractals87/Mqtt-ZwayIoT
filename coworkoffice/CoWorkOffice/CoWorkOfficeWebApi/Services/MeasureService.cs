using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using Microsoft.EntityFrameworkCore;
using MQTTService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoWorkOfficeWebApi.Services
{
    public class MeasureService
    {
        private readonly ApplicationDbContext _context;
        private readonly MqttServiceApi _mqttServiceApi;

        public MeasureService(ApplicationDbContext context, MqttServiceApi mqttServiceApi)
        {
            _context = context;
            _mqttServiceApi = mqttServiceApi;
        }

        public bool ManageNewMeasure(Measure measure)
        {
            
            try
            {
                _context.Measures.AddRange(measure);
                _context.SaveChanges();

                measure = _context.Measures.Include(f => f.IoTDevice).ThenInclude(f=>f.Room).Where(f => f.Id == measure.Id).FirstOrDefault();
                var confortParameters = _context.ConfortParameters.ToList();
                var IoTDevices = _context.IoTDevices.ToList();
                //var reservations = _context.Reservations.Where(f => f.DateTimeStart >= DateTime.Now && f.DateTimeEnd <= DateTime.Now);

                //foreach(var confortParam in confortParameters)
                //{
                    //foreach (var measure in measures)
                    //{
                        var command = new Command();
                        if (measure.IoTDevice.ProbeType == "smoke" && measure.Value == 1)
                        {
                            command = new Command()
                            {
                                IoTDevice_Id = 0,
                                Value = 0,
                                CustomCommand = "SetAllarm"
                            };
                            _mqttServiceApi.Publish("Command/", JsonSerializer.Serialize(command));
                            return true;
                        }

                        var roomService = new RoomService(_context);
                        bool OccupiedRoom = roomService.IsBusy(measure.IoTDevice.Room, DateTime.Now);
                        if (OccupiedRoom)
                        {
                            var conforParameter = confortParameters.Where(f => f.Parameter == measure.IoTDevice.ProbeType).FirstOrDefault();
                            //parametro di confort non monitorato
                            if (conforParameter == null)
                                return true;
                            switch (measure.IoTDevice.ProbeType)
                            {

                                case "temperature":
                                   
                                    if (measure.Value != conforParameter.ConfortValue)
                                    {
                                        var valueToSend = 0;
                                        if (measure.Value > conforParameter.ConfortValue)
                                            valueToSend = 0;
                                        else
                                            valueToSend = 1;

                                        command = new Command()
                                        {
                                            IoTDevice_Id = IoTDevices.Where(f => f.ProbeType == "thermostat_mode" && f.Room_Id == measure.IoTDevice.Room_Id).Select(f => f.Id).First(),
                                            Value = valueToSend
                                        };
                                        _mqttServiceApi.Publish("Command/", JsonSerializer.Serialize(command));                                
                                    }
                                    break;
                                case "luminosity":
                                    if (measure.Value != conforParameter.ConfortValue)
                                    {
                                        //var valueToSend = Convert.ToInt32((254 / confortParameters.Where(f => f.Parameter == measure.IoTDevice.ProbeType).FirstOrDefault().ConfortValue) * measure.Value);
                                        var valueToSend = (254 * (100 - measure.Value)) / 100; 
                                        command = new Command()
                                        {
                                            IoTDevice_Id = IoTDevices.Where(f => f.DeviceType == "LightBulb" && f.Room_Id == measure.IoTDevice.Room_Id).Select(f => f.Id).First(),
                                            Value = valueToSend
                                        };
                                        _mqttServiceApi.Publish("Command/", JsonSerializer.Serialize(command));
                                    }
                                    break;
                            }
                        }
                    //}
                //}

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
