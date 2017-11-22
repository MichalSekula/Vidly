namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDatasToMovieWithGenre1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Genres", "Name", c => c.String());
            DropColumn("dbo.Genres", "Genre");

            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (1, 'Hangover', 1, 06/11/2009, GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (2, 'Die Hard', 2, 06/01/1989, GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (3, 'The Terminator', 2, 26/10/1984,GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (4, 'Toy Story', 3, 05/04/1996, GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, GenreId, ReleaseDate, DateAdded, NumberInStock) VALUES (5, 'Titanic', 4, 18/11/1997, GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Genres", "Genre", c => c.String());
            DropColumn("dbo.Genres", "Name");
        }
    }
}
