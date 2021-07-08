using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTTService
{
    public class MyMqtt
    {
        MqttClient client;

        public MyMqtt()
        {
            // create client instance 
            client = new MqttClient("127.0.0.1");
            // register to message received 
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
        }

        public void Subscribe(List<string> topics)
        {
            foreach (var topic in topics)
            {
                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
        }

        public void Publish(string topic, string message)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string msg = Encoding.UTF8.GetString(e.Message);
            Console.WriteLine($"Topic: {e.Topic}, Message: {msg}");
        }
    }
}
