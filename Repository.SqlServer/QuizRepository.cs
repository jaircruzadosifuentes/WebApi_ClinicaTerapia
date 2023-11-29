using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class QuizRepository : Repository, IQuizRepository
    {
        public QuizRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<QuizDetail> GetQuestionsByIdQuiz(int quizId)
        {
            try
            {
                var quizDetails = new List<QuizDetail>();
                var command = CreateCommand("PA_QUESTIONS_GET_BY_ID_QUIZ");
                command.Parameters.AddWithValue("@v_quiz_id", quizId);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        quizDetails.Add(new QuizDetail
                        {
                            QuizDetailId = Convert.ToInt32(reader["id"].ToString()),
                            Quiz = new Quiz()
                            {
                                QuizId = Convert.ToInt32(reader["quiz_id"].ToString())
                            },
                            Name = reader["name_question"].ToString(),
                            TypeOfQuestion = reader["type_of_question"].ToString(),
                            NroQuestion = Convert.ToInt32(reader["nro_question"].ToString())
                        });
                    }
                }

                return quizDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Quiz> GetQuizByIdPatient(int patientId)
        {
            try
            {
                var listQuiz = new List<Quiz>();
                var command = CreateCommand("PA_QUIZ_GET_BY_ID_PATIENT");
                command.Parameters.AddWithValue("@v_patient_id", patientId);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listQuiz.Add(new Quiz
                        {
                            QuizId = Convert.ToInt32(reader["id"].ToString()),
                            Name = reader["name"].ToString(),
                            Duration = Convert.ToInt32(reader["duration"].ToString()),
                            NumberOfQuiz = Convert.ToInt32(reader["number_of_quiz"].ToString()),
                            State = reader["state"].ToString(),
                        });
                    }
                }

                return listQuiz;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
