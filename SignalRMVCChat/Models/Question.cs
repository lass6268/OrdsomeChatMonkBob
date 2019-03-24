using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMVCChat.Models
{
    public class Question
    {
        public string Titel { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Answered { get; set; }
        public ChatElement RightAnswer { get; set; }

    }
}
