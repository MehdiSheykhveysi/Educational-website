using System;

namespace Site.Core.Infrastructures.DTO
{
    public class CommentDTO
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public string UserAvatar { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
