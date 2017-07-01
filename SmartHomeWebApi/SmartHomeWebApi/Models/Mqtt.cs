using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHomeWebApi.Models
{
    public class Mqtt
    {
        public string Topic { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}