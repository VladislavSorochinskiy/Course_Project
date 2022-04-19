using Microsoft.AspNetCore.Identity;

namespace Course_Project.Data
{
    public class User : IdentityUser
    {
        public virtual List<Collection>? Collections { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Like>? Likes { get; set; }
    }
}