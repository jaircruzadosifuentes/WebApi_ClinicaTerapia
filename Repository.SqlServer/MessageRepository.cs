using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class MessageRepository : Repository, IMessageRepository
    {
        public MessageRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<Message> GetMessageDetailUsersMessage(int fromId, string fromTyperUser)
        {
            try
            {
                var messages = new List<Message>();
                var command = CreateCommand("PA_GET_DETAIL_USERS_MESSAGE_MESSAGE");
                command.Parameters.AddWithValue("@v_from_id", fromId);
                command.Parameters.AddWithValue("@v_from_type_user", fromTyperUser);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        messages.Add(new Message
                        {
                            MessageId = Convert.ToInt32(reader["id"].ToString()),
                            ToId = Convert.ToInt32(reader["to_id"].ToString()),
                            FromId = Convert.ToInt32(reader["from_id"].ToString()),
                            MessageContent = reader["message_content"].ToString(),
                            TypeUserFrom = reader["type_user_from"].ToString(),
                            TypeUserTo = reader["type_user_to"].ToString(),
                            Person = new Person()
                            {
                                ProfilePicture = reader["profile_picture"].ToString(),
                                Names = reader["names"].ToString(),
                                Surnames = reader["surnames"].ToString(),
                            },
                            UserName = reader["user_name"].ToString()
                        });
                    }
                }

                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Message> GetMessageForReceptorId(int toId, int fromId, string typeUserTo)
        {
            try
            {
                var messages = new List<Message>();
                var command = CreateCommand("PA_GET_MESSAGE_DETAIL_MESSAGE");
                command.Parameters.AddWithValue("@v_to_id", toId);
                command.Parameters.AddWithValue("@v_from_id", fromId);
                command.Parameters.AddWithValue("@v_type_user_to", typeUserTo);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        messages.Add(new Message
                        {
                            MessageContent = reader["message_content"].ToString(),
                            TypeUserFrom = reader["type_user_from"].ToString(),
                            TypeUserTo = reader["type_user_to"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            Seen = Convert.ToBoolean(reader["seen"].ToString()),
                            IsStaff = Convert.ToBoolean(reader["is_staff"].ToString()),
                            FromId = Convert.ToInt32(reader["from_id"].ToString()),
                            ToId = Convert.ToInt32(reader["to_id"].ToString()),
                            Person = new Person()
                            {
                                ProfilePicture = reader["profile_picture"].ToString()
                            },
                            UserName = reader["user_name"].ToString()
                        });
                    }
                }

                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Message> GetMessagesForUserId(int fromId, string typeFromUser)
        {
            try
            {
                var messages = new List<Message>();
                var command = CreateCommand("PA_GET_MESSAGE_FOR_ID_USER_GET_MESSAGE");
                command.Parameters.AddWithValue("@v_from_id", fromId);
                command.Parameters.AddWithValue("@v_type_user_from", typeFromUser);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        messages.Add(new Message
                        {
                            MessageId = Convert.ToInt32(reader["id"].ToString()),
                            ToId = Convert.ToInt32(reader["to_id"].ToString()),
                            FromId = Convert.ToInt32(reader["from_id"].ToString()),
                            MessageContent = reader["message_content"].ToString(),
                            Person = new Person()
                            {
                                ProfilePicture = reader["profile_picture"].ToString(),
                                Names = reader["names"].ToString(),
                                Surnames = reader["surnames"].ToString(),
                            },
                            Count = Convert.ToInt32(reader["count_msg"].ToString()),
                        });
                    }
                }

                return messages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertaMessage(Message message)
        {
            try
            {
                var command = CreateCommand("PA_REGISTER_MESSAGE_POST_MESSAGE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_message_content", message.MessageContent);
                command.Parameters.AddWithValue("@v_from_id", message.FromId);
                command.Parameters.AddWithValue("@v_to_id", message.ToId);
                command.Parameters.AddWithValue("@v_type_user_to", message.TypeUserTo);
                command.Parameters.AddWithValue("@v_type_user_from", message.TypeUserFrom);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateSeenMessage(int toId, int fromId)
        {
            try
            {
                var command = CreateCommand("PA_SEEN_UPDATE_MESSAGE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_to_id", toId);
                command.Parameters.AddWithValue("@v_from_id", fromId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
