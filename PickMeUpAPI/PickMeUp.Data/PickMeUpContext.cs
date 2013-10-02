using System;
using System.Data.Entity;
using System.Linq;
using PickMeUp.Models;

namespace PickMeUp.Data
{
    public class PickMeUpContext:DbContext

    {
        public PickMeUpContext()
            :base("PickMeUpSystem")
        {
        }
    
        public DbSet <Thought>Thoughts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}

