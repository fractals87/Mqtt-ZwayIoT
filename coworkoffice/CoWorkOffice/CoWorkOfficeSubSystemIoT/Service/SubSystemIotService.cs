using CoWorkOfficeModel.Models;
using CoWorkOfficeModel.Models.Hue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CoWorkOfficeSubSystemIoT.Service
{
    public class SubSystemIotService
    {
        public ICollection<IoTDevice> IoTDevices;
        public ICollection<Gateway> Gateways;

        public SubSystemIotService()
        {
            IoTDevices = new List<IoTDevice>() { };
            Gateways = new List<Gateway>() { };
        }

        public async Task IncomingCommand(Command cmd)
        {


            if(cmd.CustomCommand == "SetAllarm")
            {
                foreach(var bulb in IoTDevices.Where(f => f.DeviceType == "LightBulb"))
                {
                    using (var client = new HttpClient())
                    {
                        var value = new ValuesBulb { bri = 254, sat = 254, hue = 0 };
                        client.BaseAddress = new Uri("http://" + Gateways.Where(f=>f.Id == bulb.Gateway_Id).First().IP + ":" + Gateways.Where(f => f.Id == bulb.Gateway_Id).First().Port + "/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var res = await client.PutAsJsonAsync("api/newdeveloper/lights/" + bulb.RegistrationIdentifier + "/state", value);
                    }
                }
                return;
            }
            var IoTDevice = IoTDevices.Where(f => f.Id == cmd.IoTDevice_Id).FirstOrDefault();
            var Gateway = Gateways.Where(f => f.Id == IoTDevice.Gateway_Id).FirstOrDefault();

            if (Gateway.Id == (int)GatewayEnum.GatewayHUE)
            {
                using (var client = new HttpClient())
                {
                    var value = new ValuesBulb { bri = Convert.ToInt32(cmd.Value), sat = 254, hue = 10992 };
                    client.BaseAddress = new Uri("http://" + Gateway.IP + ":" + Gateway.Port + "/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var res = await client.PutAsJsonAsync("api/newdeveloper/lights/" + IoTDevice.Id + "/state", value);
                }
            }
            else if (Gateway.Id == (int)GatewayEnum.GatewayZWay)
            {
                var doc = XDocument.Load("XMLData.xml");
                var elem = doc.Descendants("key").Where(k => k.Attribute("name").Value.Equals(IoTDevice.RegistrationIdentifier)).First();
                elem.SetAttributeValue("value", cmd.Value);

                doc.Save("XMLData.xml");
            }

        }


        public double GetValue(string RegistrationIdentifier)
        {

            //var test = File.Exists("XMLData.xml");
            var doc = XDocument.Load("XMLData.xml");
            var value = Convert.ToInt32(doc.Descendants("key")
                 .Where(k => k.Attribute("name").Value.Equals(RegistrationIdentifier))
                 .Select(e => e.Attribute("value").Value)
                 .FirstOrDefault());

            return value;
        }
    }

}
