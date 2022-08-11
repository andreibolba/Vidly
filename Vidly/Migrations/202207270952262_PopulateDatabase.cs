namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (1,'Pay as you go',0,0,0)");
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (2,'Monthly',30,1,10)");
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (3,'Quartetly',90,3,15)");
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (4,'Annual',300,12,20)");

            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Andrei',1,4)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Melania',0,1)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Adrian',0,3)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Rebeka',1,2)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Catalin',0,3)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Iulia',1,1)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('George',0,4)");
            Sql("INSERT INTO Costumers (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Adina',1,2)");

            Sql("UPDATE Costumers SET BirthDate='7/9/2001' WHERE Id=1");
            Sql("UPDATE Costumers SET BirthDate='6/25/1998' WHERE Id=2");
            Sql("UPDATE Costumers SET BirthDate='6/2/2001' WHERE Id=6");
            Sql("UPDATE Costumers SET BirthDate='9/7/2001' WHERE Id=4");

            Sql("INSERT INTO Genres (Id,Name) VALUES (1,'Action')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (2,'Drama')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (3,'Comedy')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (4,'Drama')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (5,'Thriller')");
            Sql("INSERT INTO Genres (Id,Name) VALUES (6,'Erotic')");

            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('Miami Bici','02/21/2020','05/22/2020',5,3)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('The Hangover','06/02/2009','01/21/2020',3,3)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('Fast & Furious 9','06/25/2021','07/12/2021',10,1)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('House of Gucci','11/24/2021','12/16/2020',12,2)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('The Mechanic','01/25/2011','11/29/2010',5,1)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('Jaf în stil italian','05/30/2003','09/02/2012',1,1)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('Need for Speed: Începuturi','03/14/2014','08/01/2015',20,5)");
            Sql("INSERT INTO Movies (Name,Added,Released,Stock,GenreId) VALUES ('Baywatch','05/12/2017','01/20/2020',6,1)");
        }
        
        public override void Down()
        {
        }
    }
}
