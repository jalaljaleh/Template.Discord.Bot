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
    public class QuestionMapping : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("QuestionItem", "dbo");

            builder.HasKey(x => x.Id);
            
            builder.HasMany(x => x.Childern)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId);

            builder.HasMany(a => a.UsersStatus)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.UserId);
        }
    }
}
