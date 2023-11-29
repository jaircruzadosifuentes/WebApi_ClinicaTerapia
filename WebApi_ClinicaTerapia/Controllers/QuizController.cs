using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }
      
        [HttpGet("GetQuizByIdPatient/{patientId}")]
        public ActionResult<IEnumerable<Quiz>> GetQuizByIdPatient(int patientId)
        {
            var listQuiz = _quizService.GetQuizByIdPatient(patientId);
            return Ok(listQuiz);
        }
        [HttpGet("GetQuestionsByIdQuiz/{quizId}")]
        public ActionResult<IEnumerable<QuizDetail>> GetQuestionsByIdQuiz(int quizId)
        {
            var quizDetails = _quizService.GetQuestionsByIdQuiz(quizId);
            return Ok(quizDetails);
        }
    }
}
