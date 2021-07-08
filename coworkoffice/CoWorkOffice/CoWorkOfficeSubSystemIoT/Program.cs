using CoWorkOfficeModel.Models;
using MQTTService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace CoWorkOfficeSubSystemIoT
{
    class Program
    {
        static public void Main(string[] args)
        {
            var Mqtt = new MqttServiceIoT();
            Timer t = new(20000)
            {
                AutoReset = true
            };
            //t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Elapsed += (sender, e) => OnTimedEvent(sender, e, Mqtt);

            t.Start();
        }

        static void OnTimedEvent(Object source, ElapsedEventArgs e, MqttServiceIoT Mqtt)
        {
            var measures = new List<Measure>() { };
            /*Metodo test per emulazione*/
            /**/
            double value = 0;
            foreach (var sensor in Mqtt.subSystemIotService.IoTDevices.Where(f=>f.Gateway_Id == (int)GatewayEnum.GatewayZWay))
            {
                value = Mqtt.subSystemIotService.GetValue(sensor.RegistrationIdentifier);
                Mqtt.Publish("/20018562/Measures/" + sensor.Room_Id + "/" + sensor.ProbeType + "/" , JsonSerializer.Serialize(new Measure() { DateTime = DateTime.Now, IoTDevice_Id = sensor.Id, Value = value }));
            }

        }

        //static double GetValue(string RegistrationIdentifier)
        //{

        //}

    }
}
