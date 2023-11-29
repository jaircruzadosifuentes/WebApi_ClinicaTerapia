using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public int? NumberOfQuiz { get; set; }
        public string? State { get; set; }
    }
}
