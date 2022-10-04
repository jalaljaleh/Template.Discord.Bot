using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public enum State
    {
        None,
        OnAnsweringQuestions
    }
    public class User
    {
        public User()
        {
            State = State.None;
            Answers = new List<Answer>();
            QuestionsStatus = new List<QuestionStatus>();
            AnsweredQuestion = new List<Question>();
        }

        public ulong Id { get; set; }
        public State State { get; set; }

        public int CurrentQuestionId { get; set; }
        public int CurrentQuestionItemId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> AnsweredQuestion { get; set; }
        public virtual ICollection<QuestionStatus> QuestionsStatus { get; set; }

        public bool HasAnswered(int itemId)
        {
            return Answers.Any(a => a.QuestionItemId == itemId);
        }
    }
}
