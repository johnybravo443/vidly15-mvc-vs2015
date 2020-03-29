namespace Vidly15.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberAvailableToMovieModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Byte());

            Sql("UPDATE Movies SET NumberAvailable = NumberInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
