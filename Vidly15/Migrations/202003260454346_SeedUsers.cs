namespace Vidly15.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'71952a8b-891b-42d9-bacc-0bbb135dd094', N'guest@vidly15.com', 0, N'AOTqIXZRuuQvnea6hZWO4SQNWrToAtPybTuYIGJZmUvvMvFGfohURdwGyHnufVHllg==', N'a8514f13-11a6-4994-a48d-8b06fc70e31e', NULL, 0, 0, NULL, 1, 0, N'guest@vidly15.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e2704c75-0792-44b5-935f-a369981cb762', N'admin@vidly15.com', 0, N'AMgFXeWtEj4MNjAmA++n2gnds7j8s/HkFDkGf2vYK+Zxx9AsEwbcQcgUJvF2ZqNxtA==', N'af2dc167-f7af-4c7e-ad26-0a51635fc020', NULL, 0, 0, NULL, 1, 0, N'admin@vidly15.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd818eabf-4c8e-4b3b-980d-61c268cea72a', N'CanManageMovies')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e2704c75-0792-44b5-935f-a369981cb762', N'd818eabf-4c8e-4b3b-980d-61c268cea72a')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
