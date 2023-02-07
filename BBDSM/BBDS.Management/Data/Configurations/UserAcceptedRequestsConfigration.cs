using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BBDS.Management.Data.Configurations
{
    public class UserAcceptedRequestsConfigration : IEntityTypeConfiguration<UsersAcceptedRequests>
    {
        public void Configure(EntityTypeBuilder<UsersAcceptedRequests> builder)
        {
            builder.HasKey(t => new { t.UserId, t.RequestId });
            builder.HasOne(t => t.Request).WithMany(e => e.UsersAcceptedRequest).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.User).WithMany(e => e.UsersAcceptedRequest).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
