using SignalRMVCChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMVCChat
{
    public class ChatContext : IChatContext
    {
        public ICollection<Question> QuestionSet { get; set; }
        private readonly Dictionary<Guid, List<ChatElement>> _messageHistory = new Dictionary<Guid, List<ChatElement>>();
        public ChatContext()
        {
            if (QuestionSet == null)
            {

                List<Question> questions = new List<Question>();
                QuestionSet = questions;
                QuestionSet.Add(new Question
                {
                    Id = Guid.NewGuid(),
                    Titel = "Titel 1",
                    Description = "Test 1"
                });
                QuestionSet.Add(new Question
                {
                    Id = Guid.NewGuid(),
                    Titel = "Titel 2",
                    Description = "Test 2"
                });
                QuestionSet.Add(new Question
                {
                    Id = Guid.NewGuid(),
                    Titel = "Titel 2",
                    Description = "Test 2"
                });

            }
        }

        public Task AddMessage(Guid roomId, ChatElement message)
        {
            if (!_messageHistory.ContainsKey(roomId))
            {
                _messageHistory[roomId] = new List<ChatElement>();
            }

            _messageHistory[roomId].Add(message);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ChatElement>> GetMessageHistory(Guid roomId)
        {
            _messageHistory.TryGetValue(roomId, out var messages);
            messages = messages ?? new List<ChatElement>();
            
            return Task.FromResult(messages.AsEnumerable());
        }
    }
}
