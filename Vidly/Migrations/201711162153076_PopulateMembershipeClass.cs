namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMembershipeClass : DbMigration
    {
        // tutaj mo¿emy inicjalizowac swoje tabele o wartosci jakie chcemy, zazwyczaj bêdzie to robione za pomoca CRUD 
        //lecz jesli potrzebujemy wgrac na sztywno np. jakies slowniki mozemy wykorzystac migracje
        //add-migration dodajCos ------- i w tej klasie w metodzie Up za pomoca zmiennej Sql("podajemy zapytanie w jezyku T-SQL/SQL ") 
        //update-database
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (1, 0, 0, 0)");
            Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (2, 30, 1, 10)");
            Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (3, 90, 3, 15)");
            Sql("INSERT INTO MembershipTypes (Id, SignUpFee, DurationInMonths, DiscountRate) VALUES (4, 300, 12, 20)");
        }
        
        public override void Down()
        {
        }
    }
}
