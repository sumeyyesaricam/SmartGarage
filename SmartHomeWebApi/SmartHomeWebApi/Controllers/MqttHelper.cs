using MqttLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHomeWebApi.Controllers
{
    public class MqttHelper
    {

        public IMqtt _client;
        public MqttHelper(string connectionString, string clientId, string username, string password)
        {
            // Instantiate client using MqttClientFactor

            _client = MqttClientFactory.CreateClient(connectionString, clientId);

            // Setup some useful client delegate callbacks
            _client.Connected += new ConnectionDelegate(client_Connected);
            _client.ConnectionLost += new ConnectionDelegate(_client_ConnectionLost);
            _client.PublishArrived += new PublishArrivedDelegate(client_PublishArrived);
        }

        public void Start()
        {
            // Connect to broker in 'CleanStart' mode
            Console.WriteLine("Client connecting\n");
            _client.Connect(true);
        }

        public void Stop()
        {
            if (_client.IsConnected)
            {
                Console.WriteLine("Client disconnecting\n");
                _client.Disconnect();
                Console.WriteLine("Client disconnected\n");
            }
        }

        public void client_Connected(object sender, EventArgs e)
        {
            Console.WriteLine("Client connected\n");
            RegisterOurSubscriptions();
            PublishSomething();
        }

        public void _client_ConnectionLost(object sender, EventArgs e)
        {
            Console.WriteLine("Client connection lost\n");
        }

        public void RegisterOurSubscriptions()
        {
            Console.WriteLine("Subscribing to mqttdotnet/subtest/#\n");
            _client.Subscribe("mqttdotnet/subtest/#", QoS.BestEfforts);
        }

        public void PublishSomething()
        {
            Console.WriteLine("Publishing on mqttdotnet/pubtest\n");
            _client.Publish("mqttdotnet/pubtest", "Hello MQTT World", QoS.BestEfforts, false);
        }

        public bool client_PublishArrived(object sender, PublishArrivedArgs e)
        {
            Console.WriteLine("Received Message");
            Console.WriteLine("Topic: " + e.Topic);
            Console.WriteLine("Payload: " + e.Payload);
            Console.WriteLine();
            return true;
        }
    }
}