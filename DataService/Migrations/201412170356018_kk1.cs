namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "RegistrationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "RegistrationDate", c => c.DateTime());
        }
    }
}
