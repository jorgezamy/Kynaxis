﻿namespace Kynaxis.Models
{
    public class Story
    {
        public string Title { get; set; } = "";
        public string Uri { get; set; } = "";
        public string PostedBy { get; set; } = "";
        public string Time { get; set; } = "";
        public int Score { get; set; }
        public int CommentCount { get; set; }
    }
}
