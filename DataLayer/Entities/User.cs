using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class User : IdentityUser
    {
        public bool IsBlocked { get; set; }
        public virtual List<Collection>? Collections { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Like>? Likes { get; set; }
    }
}