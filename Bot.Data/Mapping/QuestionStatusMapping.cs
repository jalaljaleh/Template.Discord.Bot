using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data.Mapping
{
    public class QuestionStatusMapping : IEntityTypeConfiguration<QuestionStatus>
    {
        void IEntityTypeConfiguration<QuestionStatus>.Configure(EntityTypeBuilder<QuestionStatus> builder)
        {
            builder.ToTable("QuestionItem.Status", "ref");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Status)
                .HasDefaultValue(QuestionStatusType.NotAnswered);

            builder.HasOne(a => a.Question)
                .WithMany(a => a.UsersStatus)
                .HasForeignKey(a => a.QuestionId);


            builder.HasOne(a => a.User)
                .WithMany(a => a.QuestionsStatus)
                .HasForeignKey(a => a.UserId);



        }
    }
}
