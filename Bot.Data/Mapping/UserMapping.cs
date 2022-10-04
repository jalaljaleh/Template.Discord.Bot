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
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .ValueGeneratedNever();

            builder.Property(a => a.State)
                .HasDefaultValue(State.None);

            builder.HasMany(a => a.Answers)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            //builder.HasOne(a => a.CurrentQuestion)
            //    .WithMany()
            //    .IsRequired(false)
            //    .HasForeignKey(a => a.CurrentQuestionId);

            //builder.HasOne(a => a.CurrentQuestionItem)
            //      .WithMany()
            //      .IsRequired(false)
            //      .HasForeignKey(a => a.CurrentQuestionItemId);

            builder.HasMany(a => a.AnsweredQuestion)
                .WithMany(a => a.AnsweredUsers);

            builder.HasMany(a => a.QuestionsStatus)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);
        }
    }
}
