namespace UMovies.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieFileExplicitDefinition : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieFiles", newName: "MovieFile");
            AlterColumn("dbo.MovieFile", "FileName", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MovieFile", "FileName", c => c.String());
            RenameTable(name: "dbo.MovieFile", newName: "MovieFiles");
        }
    }
}
