using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public class StudySession
    {
        public int Id { get; set; }
        public int StackId { get; set; }
        public int CorrectAnswers { get; set; }
        public double Score { get; set; }
    }
}
