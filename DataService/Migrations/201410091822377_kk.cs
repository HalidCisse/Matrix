namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Person", newName: "Staff");
            DropPrimaryKey("dbo.Staff");
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PERSON_ID = c.String(nullable: false, maxLength: 128),
                        TITLE = c.String(),
                        FIRSTNAME = c.String(),
                        LASTNAME = c.String(),
                        PHOTO_IDENTITY = c.Binary(),
                        NATIONALITY = c.String(),
                        IDENTITY_NUMBER = c.String(),
                        BIRTH_DATE = c.DateTime(),
                        BIRTH_PLACE = c.String(),
                        PHONE_NUMBER = c.String(),
                        EMAIL_ADRESS = c.String(),
                        HOME_ADRESS = c.String(),
                        REGISTRATION_DATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.PERSON_ID);
            
            AlterColumn("dbo.Staff", "STAFF_ID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Staff", "HIRED_DATE", c => c.DateTime());
            AddPrimaryKey("dbo.Staff", "STAFF_ID");
            DropColumn("dbo.Staff", "PERSON_ID");
            DropColumn("dbo.Staff", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staff", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Staff", "PERSON_ID", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Staff");
            AlterColumn("dbo.Staff", "HIRED_DATE", c => c.String());
            AlterColumn("dbo.Staff", "STAFF_ID", c => c.String());
            DropTable("dbo.Person");
            AddPrimaryKey("dbo.Staff", "PERSON_ID");
            RenameTable(name: "dbo.Staff", newName: "Person");
        }
    }
}
