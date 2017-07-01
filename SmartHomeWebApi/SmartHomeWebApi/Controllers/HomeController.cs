using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Serialization;
using System.Web.Mvc;
using MqttLib;

namespace SmartHomeWebApi.Controllers
{
    public class HomeController : Controller
    {
        private static string UUID= "";
        private static string maj = "1";
        private static string min = "3";
        private static string uname = "";
        private static string password = "";
        private string cloudmqtt_connectionString = "";
        private string cloudmqtt_username = "";
        private string cloudmqtt_password = "";
        private string ClientId = "";

       
        public ActionResult GetName() {

            return Content("sumeyye");
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //MqttHelper prog = new MqttHelper(cloudmqtt_connectionString, ClientId, cloudmqtt_username, cloudmqtt_password);
            //prog.Start();
            var regid = "eFp44SZhwL0:APA91bG7TH5vY6TokaOKeln3AYLqoj5ly2-Q12iIETf8Mjl4krSET4Cq1ZJ8nWS1v0mNgpmafkMeLMjyuGxSoVLat5QL5ud6x4Kgzc8wdwb8zmknVHr1PpUtCzD6V3XmZSI-NLj_9nNj";
            string response = SendNotification(regid, "Ayşe");
            return View();
        }
        [HttpPost]
        public ActionResult OpenDoor(string uuid,string major,string minor,string name,string pass)
        {
            bool control=false;
            if (UUID == uuid && maj == major && min == minor && uname == name && password == pass)
            { control = true; }
            else if (UUID == uuid && maj == major && min == minor && uname != null)
            {
                var regid = "fDVuJoJE_4Y:APA91bEPhfpckQCEvJOenzO3Jjw9dAXpRxK7eBQfXFNIx5taisPEW2sHlwhjWVKGlHuzObEKUZZk78to01p1eGr9jBC1pXMlfXRVlb9DUM3oOXmrlzeOEIdrB5QsF1SFjlHE1-Q0cCHv";
                string response = SendNotification(regid, name);
            }

            return Content(control.ToString());
        }
        public string SendNotification(string deviceId, string message)
        {
            string SERVER_API_KEY = "";
            var SENDER_ID = "";
            var value = message;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            var dta = new
            {
                to = deviceId,
                notification = new
                {
                    body = "Kapına "+ message + " geldi. Kapıyı açmak istiyor. Napayım?",
                    title = "Kapıda biri var.",
                    click_action="NotificationActivity"

                },
                data =new {
                page= 1,
                userid = "c54ef4de-3236-4d00-ab63-874840c53010"
                }
        };
            string jsonss = Newtonsoft.Json.JsonConvert.SerializeObject(dta);
            Byte[] byteArray = Encoding.UTF8.GetBytes(jsonss);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();


            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }

        
    }
}
