﻿namespace Course_Project.Data
{
    public class Like
    {
        public string UserId { get; private set; }
        public User User { get; private set; }
        public int CommentId { get; private set; }
        public Comment Comment { get; private set; }

        public Like(User user, Comment comment)
        {
            User = user;
            UserId = user.Id;
            Comment = comment;
            CommentId = comment.Id;
        }
    }
}