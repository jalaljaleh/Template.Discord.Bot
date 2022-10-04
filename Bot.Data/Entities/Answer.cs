using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public class Answer
    {
        public Answer()
        {
            CreatedAt = DateTime.MinValue;
        }
        public int Id { get; set; }
        public string Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public int QuestionItemId { get; set; }
        public virtual QuestionItem QuestionItem { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
