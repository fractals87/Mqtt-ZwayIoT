using CoWorkOffice.Data;
using CoWorkOfficeModel.Models;
using CoWorkOfficeWebApi.Data;
using CoWorkOfficeWebApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTService
{
    public class MqttServiceApi
    {
        private MqttClient client;
        private string rootTopic = "/20018562/";


        public MqttServiceApi(/*ApplicationDbContext context*/)
        {
            //_context = context;
            // create client instance 
            client = new MqttClient("127.0.0.1");
            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
            client.Subscribe(new string[] { rootTopic + "Measures/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public void Publish(string topic, string message)
        {
            client.Publish( rootTopic + topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        //public void Publish(string topic, object obj)
        //{
        //    client.Publish(rootTopic + topic, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj)), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        //}

        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //string[] msg = Encoding.UTF8.GetString(e.Message).Split("||");

            var dbFactory = new DbContextFactory();
            var measureService = new MeasureService(dbFactory.Create(), this);
            switch (e.Topic.Substring(0,19))
            {
                case "/20018562/Measures/":
                    measureService.ManageNewMeasure(JsonSerializer.Deserialize<Measure>(Encoding.UTF8.GetString(e.Message)));
                    break;
            }
        }
    }
}
