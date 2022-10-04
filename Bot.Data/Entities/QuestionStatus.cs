using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{

    public class QuestionStatus
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public QuestionStatusType Status { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
