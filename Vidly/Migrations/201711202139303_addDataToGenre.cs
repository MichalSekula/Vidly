namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataToGenre : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON INSERT INTO Genres (Id,Genre) VALUES (1,'Comedy')");
            Sql("SET IDENTITY_INSERT Genres ON INSERT INTO Genres (Id,Genre) VALUES (2,'Action')");
            Sql("SET IDENTITY_INSERT Genres ON INSERT INTO Genres (Id,Genre) VALUES (3,'Family')");
            Sql("SET IDENTITY_INSERT Genres ON INSERT INTO Genres (Id,Genre) VALUES (4,'Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
