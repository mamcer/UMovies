namespace UMovies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movie", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id);
            
            AddColumn("dbo.Movie", "ThumbnailFileName", c => c.String(maxLength: 255));
            DropColumn("dbo.Movie", "MovieFile");
            DropColumn("dbo.Movie", "ThumbnailFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movie", "ThumbnailFile", c => c.String(maxLength: 255));
            AddColumn("dbo.Movie", "MovieFile", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.MovieFiles", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.MovieFiles", new[] { "Movie_Id" });
            DropColumn("dbo.Movie", "ThumbnailFileName");
            DropTable("dbo.MovieFiles");
        }
    }
}
