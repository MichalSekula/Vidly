namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addToMovies : DbMigration
    {
        public override void Up()
        {
            //Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES (1, 'Hangover', 'Comedy', '06/11/2009', GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            //Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES (2, 'Die Hard', 'Action', '06/01/1989', GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            //Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES (3, 'The Terminator', 'Action', '26/10/1984',GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            //Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES (4, 'Toy Story', 'Family', '05/04/1996', GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
            //Sql("SET IDENTITY_INSERT Movies ON INSERT INTO Movies (Id, Name, Genre, ReleaseDate, DateAdded, NumberInStock) VALUES (5, 'Titanic', 'Romance', '18/11/1997', GETDATE(), 5) SET IDENTITY_INSERT Movies OFF");
        }
        
        public override void Down()
        {
        }
    }
}
