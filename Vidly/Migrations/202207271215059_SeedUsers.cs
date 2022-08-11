namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'15bee814-d565-499b-83f2-aaabc72f1123', N'andrei@yahoo.com', 0, N'AK+9Q5bz8BLrYwQnMdGagjpRvZVF/pZBzRct0bw3H8hu8m0DlXl1M/WpJZoYeAxmqQ==', N'fa0937aa-3a59-489d-ab2a-56970f8ee027', NULL, 0, 0, NULL, 1, 0, N'andrei@yahoo.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ddb0a69f-6d1a-46e5-ade6-3fbd6bbdbf89', N'admin@vidly.ro', 0, N'AHmh6BnkBto4D64JXMVEM3bh73YYBzwOMcL1B9a0N7luPM/blrxcqTkAfnFB1hPyCw==', N'fb7f330a-6e38-4963-b086-74318a80bf11', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.ro')

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'28dae3a1-36ba-4977-b015-e328e55fa2b2', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ddb0a69f-6d1a-46e5-ade6-3fbd6bbdbf89', N'28dae3a1-36ba-4977-b015-e328e55fa2b2')

");
        }
        
        public override void Down()
        {
        }
    }
}
