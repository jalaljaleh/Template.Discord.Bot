using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public class QuestionItem
    {
        public QuestionItem()
        {
            Answers = new List<Answer>();
        }
        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public int ParentId { get; set; }
        public virtual Question Parent { get; set; }

        public bool HasUserAnswered(int userId)
        {
            return Answers.Any(x => x.UserId == userId);
        }

    }
}
