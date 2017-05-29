namespace UMovies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Movie", "MovieFilePath", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Movie", "Sinopsis", c => c.String(maxLength: 2500));
            AddColumn("dbo.Movie", "ThumbnailFilePath", c => c.String(maxLength: 255));
            DropColumn("dbo.Movie", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movie", "FilePath", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movie", "ThumbnailFilePath");
            DropColumn("dbo.Movie", "Sinopsis");
            DropColumn("dbo.Movie", "MovieFilePath");
            DropColumn("dbo.Movie", "Year");
        }
    }
}
