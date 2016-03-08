namespace VideosAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VideosAPI.Models.VideoDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VideosAPI.Models.VideoDB context)
        {
            context.Videos.AddOrUpdate(v => v.Title,

            new Models.Video { Title = "MVC4", Length = 120 },
            new Models.Video { Title = "LINQ", Length = 140 }
            );

            context.SaveChanges();
        }
    }
}
