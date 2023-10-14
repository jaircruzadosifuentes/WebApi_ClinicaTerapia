using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessageForReceptorId(int toId, int fromId, string typeUserTo);
        IEnumerable<Message> GetMessageDetailUsersMessage(int fromId, string fromTyperUser);
        IEnumerable<Message> GetMessagesForUserId(int fromId, string typeFromUser);
        bool InsertaMessage(Message message);
        bool UpdateSeenMessage(int toId, int fromId);

    }
}
