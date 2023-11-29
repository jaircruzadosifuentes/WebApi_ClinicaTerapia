using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IQuizService
    {
        IEnumerable<Quiz> GetQuizByIdPatient(int patientId);
        IEnumerable<QuizDetail> GetQuestionsByIdQuiz(int quizId);
    }
    public class QuizService : IQuizService
    {
        private IUnitOfWork _unitOfWork;

        public QuizService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<QuizDetail> GetQuestionsByIdQuiz(int quizId)
        {
            using var context = _unitOfWork.Create();
            var quizDetails = context.Repositories.QuizRepository.GetQuestionsByIdQuiz(quizId);
            return quizDetails;
        }

        public IEnumerable<Quiz> GetQuizByIdPatient(int patientId)
        {
            using var context = _unitOfWork.Create();
            var listQuiz = context.Repositories.QuizRepository.GetQuizByIdPatient(patientId);
            return listQuiz;
        }
    }
}
