namespace UMovies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieFolders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movie", "MovieFolder", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Movie", "MovieFile", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Movie", "ThumbnailFile", c => c.String(maxLength: 255));
            DropColumn("dbo.Movie", "MovieFilePath");
            DropColumn("dbo.Movie", "ThumbnailFilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movie", "ThumbnailFilePath", c => c.String(maxLength: 255));
            AddColumn("dbo.Movie", "MovieFilePath", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Movie", "ThumbnailFile");
            DropColumn("dbo.Movie", "MovieFile");
            DropColumn("dbo.Movie", "MovieFolder");
        }
    }
}
