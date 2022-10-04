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

    public class QuestionItemMapping : IEntityTypeConfiguration<QuestionItem>
    {

        public void Configure(EntityTypeBuilder<QuestionItem> builder)
        {
            builder.ToTable("QuestionItems", "dbo");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Value)
                .HasMaxLength(4000);

            builder.HasMany(a => a.Answers)
                .WithOne(a => a.QuestionItem)
                .HasForeignKey(a => a.QuestionItemId);

            builder.HasOne(a => a.Parent)
                .WithMany(a => a.Childern)
                .HasForeignKey(a => a.ParentId);

        }
    }
}
