using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public class Question
    {
        public Question()
        {
            Childern = new List<QuestionItem>();
            UsersStatus = new List<QuestionStatus>();
            AnsweredUsers = new List<User>();
        }
        public int Id { get; set; }

        public virtual ICollection<QuestionStatus> UsersStatus { get; set; }
        public virtual ICollection<QuestionItem> Childern { get; set; }
        public virtual ICollection<User> AnsweredUsers { get; set; }


        public QuestionStatus GetStatus(int userId) => UsersStatus.FirstOrDefault(a => a.UserId == userId);

    }
}
