using System;
using System.Collections.Generic;
using System.Text;

namespace ImageShareLikes.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateCreated { get; set; }
        public int Likes { get; set; }
    }
}
