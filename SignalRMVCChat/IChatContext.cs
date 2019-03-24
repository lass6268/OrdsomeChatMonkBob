using SignalRMVCChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMVCChat
{
    public interface IChatContext
    {
        ICollection<Question> QuestionSet { get; set; }
        Task AddMessage(Guid roomId, ChatElement message);
        Task<IEnumerable<ChatElement>> GetMessageHistory(Guid roomId);
    }
}
