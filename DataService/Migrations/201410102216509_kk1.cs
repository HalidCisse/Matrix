namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Staff", newName: "Staffs");
            RenameTable(name: "dbo.Student", newName: "Students");
            DropTable("dbo.Person");
        }
        
        public override void Down()
        {
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
            
            RenameTable(name: "dbo.Students", newName: "Student");
            RenameTable(name: "dbo.Staffs", newName: "Staff");
        }
    }
}
