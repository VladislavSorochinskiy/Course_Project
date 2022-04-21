namespace DataLayer.Entities
{
    public class Like
    {
        public string? UserId { get; private set; }
        public virtual User? User { get; private set; }
        public int? CommentId { get; private set; }
        public virtual Comment? Comment { get; private set; }

        public Like()
        {

        }
        public Like(User user, Comment comment)
        {
            User = user;
            UserId = user.Id;
            Comment = comment;
            CommentId = comment.Id;
        }
    }
}