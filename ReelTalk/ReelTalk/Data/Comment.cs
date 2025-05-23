﻿using Microsoft.AspNetCore.Identity;

namespace ReelTalk.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ProductionId { get; set; }
        public virtual Production? Production { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public Comment()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
