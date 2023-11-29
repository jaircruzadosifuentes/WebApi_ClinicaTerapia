using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> GetQuizByIdPatient(int patientId);
        IEnumerable<QuizDetail> GetQuestionsByIdQuiz(int quizId);

    }
}
