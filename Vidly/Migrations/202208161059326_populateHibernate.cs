namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateHibernate : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypesHibernate (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (1,'Pay as you go',0,0,0)");
            Sql("INSERT INTO MembershipTypesHibernate (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (2,'Monthly',30,1,10)");
            Sql("INSERT INTO MembershipTypesHibernate (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (3,'Quartetly',90,3,15)");
            Sql("INSERT INTO MembershipTypesHibernate (Id,Name,SignUpFee,DurationInMonth,Discount) VALUES (4,'Annual',300,12,20)");

            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Andrei',1,4)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Melania',0,1)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Adrian',0,3)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Rebeka',1,2)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Catalin',0,3)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Iulia',1,1)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('George',0,4)");
            Sql("INSERT INTO CostumersHibernate (Name,IsSubscribedToNewsletter,MembershipTypeId) VALUES ('Adina',1,2)");

            Sql("UPDATE CostumersHibernate SET BirthDate='7/9/2001' WHERE Id=1");
            Sql("UPDATE CostumersHibernate SET BirthDate='6/25/1998' WHERE Id=2");
            Sql("UPDATE CostumersHibernate SET BirthDate='6/2/2001' WHERE Id=6");
            Sql("UPDATE CostumersHibernate SET BirthDate='9/7/2001' WHERE Id=4");

        }

        public override void Down()
        {
        }
    }
}
