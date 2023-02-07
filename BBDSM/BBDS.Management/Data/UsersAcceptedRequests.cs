using System.ComponentModel.DataAnnotations.Schema;

namespace BBDS.Management.Data
{
    public class UsersAcceptedRequests
    {
        [ForeignKey(nameof(Request))]
        public Guid RequestId { get; set; }
        public Request Request { get; set; } = null!;


        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
