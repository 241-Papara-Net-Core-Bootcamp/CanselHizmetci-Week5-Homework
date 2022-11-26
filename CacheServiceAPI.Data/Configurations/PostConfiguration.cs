using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheServiceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CacheServiceAPI.Data.Configurations
{
    internal class PostConfiguration:IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(256).HasColumnType("nvarchar");
            builder.Property(c => c.Body).IsRequired().HasColumnType("ntext");
        }
    }

}
