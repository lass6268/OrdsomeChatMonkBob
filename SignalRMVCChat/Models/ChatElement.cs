using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMVCChat.Models
{
    public class ChatElement
    {
        public string User { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Message { get; set; }
    }
}
