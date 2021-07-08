using CoWorkOfficeModel.Models;
using CoWorkOfficeSubSystemIoT.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using static uPLibrary.Networking.M2Mqtt.MqttClient;

namespace MQTTService
{
    public class MqttServiceIoT
    {
        private MqttClient client;
        private string rootTopic = "/20018562/";

        public SubSystemIotService subSystemIotService;

        public MqttServiceIoT()
        {
            client = new MqttClient("127.0.0.1");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            client.Subscribe(new string[] { rootTopic + "Configuration/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { rootTopic + "Command/" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            subSystemIotService = new SubSystemIotService();
        }

        public void Publish(string topic, string message)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //string[] msg = Encoding.UTF8.GetString(e.Message).Split("||");

            switch (e.Topic)
            {
                case "/20018562/Configuration/IoTDevice/":
                    subSystemIotService.IoTDevices.Clear();
                    subSystemIotService.IoTDevices = JsonSerializer.Deserialize<ICollection<IoTDevice>>(Encoding.UTF8.GetString(e.Message));
                    break;
                case "/20018562/Configuration/Gateway/":
                    subSystemIotService.Gateways.Clear();
                    subSystemIotService.Gateways = JsonSerializer.Deserialize<ICollection<Gateway>>(Encoding.UTF8.GetString(e.Message));
                    break;
                case "/20018562/Command/":
                    #pragma warning disable CS4014 
                    subSystemIotService.IncomingCommand(JsonSerializer.Deserialize<Command>(Encoding.UTF8.GetString(e.Message)));
                    #pragma warning restore CS4014 
                    break;
            }

            //Console.WriteLine($"Topic: {e.Topic}, Message: {msg}");
        }
    }
}
