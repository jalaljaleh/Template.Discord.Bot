using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Data;

namespace Template.Data.Mapping
{
    public class AnswerMapping : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answers", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValue(DateTime.MinValue);

            builder.HasOne(a => a.QuestionItem)
                   .WithMany(a => a.Answers)
                   .HasForeignKey(a => a.QuestionItemId);

            builder.HasOne(a => a.User)
                .WithMany(a => a.Answers)
                .HasForeignKey(a => a.UserId);

        }
    }
}
