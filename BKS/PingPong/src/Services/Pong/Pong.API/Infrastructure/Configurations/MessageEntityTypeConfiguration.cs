using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pong.API.Entities;

namespace Pong.API.Infrastructure.Configurations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<PongMessage>
    {
        public void Configure(EntityTypeBuilder<PongMessage> builder)
        {
            builder.Property(x => x.Message)
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .IsRequired(true);
        }
    }
}
