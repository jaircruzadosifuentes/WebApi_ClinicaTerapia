using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IMessageService
    {
        IEnumerable<Message> GetMessageForReceptorId(int receptorId, int sendingId, string typeUserTo);
        IEnumerable<Message> GetMessageDetailUsersMessage(int fromId, string typeFromUser);
        bool InsertaMessage(Message message);
        IEnumerable<Message> GetMessagesForUserId(int fromId, string typeFromUser);
        bool UpdateSeenMessage(int toId, int fromId);
    }
    public class MessageService: IMessageService
    {
        private IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Message> GetMessageDetailUsersMessage(int fromId, string fromTyperUser)
        {
            using var context = _unitOfWork.Create();
            var messages = context.Repositories.MessageRepository.GetMessageDetailUsersMessage(fromId, fromTyperUser);
            foreach (var message in messages)
            {
                message.Person.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture: message.Person.ProfilePicture ?? "", "perfil", "bucket-users-photos");
            }
            return messages;
        }

        public IEnumerable<Message> GetMessageForReceptorId(int toId, int fromId, string typeUserTo)
        {
            using var context = _unitOfWork.Create();
            UpdateSeenMessage(toId, fromId);
            var messages = context.Repositories.MessageRepository.GetMessageForReceptorId(toId, fromId, typeUserTo);
            foreach (var message in messages)
            {
                message.Person.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture: message.Person.ProfilePicture ?? "", "perfil", "bucket-users-photos");
            }
            return messages;
        }

        public IEnumerable<Message> GetMessagesForUserId(int fromId, string typeFromUser)
        {
            using var context = _unitOfWork.Create();
            var messages = context.Repositories.MessageRepository.GetMessagesForUserId(fromId, typeFromUser);
            foreach (var message in messages)
            {
                message.Person.ProfilePicture = context.Repositories.CommonRepository.GetUrlImageFromS3(profilePicture: message.Person.ProfilePicture ?? "", "perfil", "bucket-users-photos");
            }
            return messages;
        }

        public bool InsertaMessage(Message message)
        {
            using var context = _unitOfWork.Create();
            bool inserta = context.Repositories.MessageRepository.InsertaMessage(message);
            context.SaveChanges();
            return inserta;
        }

        public bool UpdateSeenMessage(int toId, int fromId)
        {
            using var context = _unitOfWork.Create();
            bool inserta = context.Repositories.MessageRepository.UpdateSeenMessage(toId, fromId);
            context.SaveChanges();
            return inserta;
        }
    }
}
