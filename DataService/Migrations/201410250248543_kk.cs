namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        CLASSE_ID = c.String(nullable: false, maxLength: 128),
                        NAME = c.String(),
                        LEVEL = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CLASSE_ID);
            
            CreateTable(
                "dbo.Classe_Students",
                c => new
                    {
                        CLASSE_STUDENTS_ID = c.String(nullable: false, maxLength: 128),
                        CLASSE_ID = c.String(),
                        STUDENT_ID = c.String(),
                    })
                .PrimaryKey(t => t.CLASSE_STUDENTS_ID);
            
            CreateTable(
                "dbo.ControlNotes",
                c => new
                    {
                        CONTROLNOTE_ID = c.String(nullable: false, maxLength: 128),
                        MATIERECONTROL_ID = c.String(),
                        STUDENT_ID = c.String(),
                        NOTE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CONTROLNOTE_ID);
            
            CreateTable(
                "dbo.Cours",
                c => new
                    {
                        COURS_ID = c.String(nullable: false, maxLength: 128),
                        MATIERE_ID = c.String(),
                        SALLE = c.Int(nullable: false),
                        CLASSE_ID = c.String(),
                        START_TIME = c.Int(nullable: false),
                        DURATION = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.COURS_ID);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        FILIERE_ID = c.String(nullable: false, maxLength: 128),
                        NAME = c.String(),
                        NIVEAU = c.String(),
                        NIVEAU_ENTREE = c.String(),
                        N_ANNEE = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FILIERE_ID);
            
            CreateTable(
                "dbo.Filiere_Classes",
                c => new
                    {
                        FILIERE_CLASSES_ID = c.String(nullable: false, maxLength: 128),
                        FILIERE_ID = c.String(),
                        CLASSE_ID = c.String(),
                    })
                .PrimaryKey(t => t.FILIERE_CLASSES_ID);
            
            CreateTable(
                "dbo.Filiere_Matieres",
                c => new
                    {
                        FILIERE_MATIERE_ID = c.String(nullable: false, maxLength: 128),
                        FILIERE_ID = c.String(),
                        FILIERE_LEVEL = c.Int(nullable: false),
                        MATIERE_ID = c.String(),
                        HEURE_PAR_SEMAINE = c.String(),
                    })
                .PrimaryKey(t => t.FILIERE_MATIERE_ID);
            
            CreateTable(
                "dbo.Matieres",
                c => new
                    {
                        MATIERE_ID = c.String(nullable: false, maxLength: 128),
                        NAME = c.String(),
                        HEURES_PAR_SEMAINE = c.String(),
                        INSTRUCTEURS_COUNT = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MATIERE_ID);
            
            CreateTable(
                "dbo.MatiereControls",
                c => new
                    {
                        MATIERECONTROL_ID = c.String(nullable: false, maxLength: 128),
                        COURS_ID = c.String(),
                        START_TIME = c.DateTime(),
                        DURATION = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MATIERECONTROL_ID);
            
            CreateTable(
                "dbo.Matiere_Instructeurs",
                c => new
                    {
                        MATIERE_INSTRUCTEURS_ID = c.String(nullable: false, maxLength: 128),
                        MATIERE_ID = c.String(),
                        STAFF_ID = c.String(),
                    })
                .PrimaryKey(t => t.MATIERE_INSTRUCTEURS_ID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        STAFF_ID = c.String(nullable: false, maxLength: 128),
                        POSITION = c.String(),
                        DEPARTEMENT = c.String(),
                        QUALIFICATION = c.String(),
                        HIRED_DATE = c.DateTime(),
                        STATUT = c.String(),
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
                .PrimaryKey(t => t.STAFF_ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        STUDENT_ID = c.String(nullable: false, maxLength: 128),
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
                        STATUT = c.String(),
                    })
                .PrimaryKey(t => t.STUDENT_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Students");
            DropTable("dbo.Staffs");
            DropTable("dbo.Matiere_Instructeurs");
            DropTable("dbo.MatiereControls");
            DropTable("dbo.Matieres");
            DropTable("dbo.Filiere_Matieres");
            DropTable("dbo.Filiere_Classes");
            DropTable("dbo.Filieres");
            DropTable("dbo.Cours");
            DropTable("dbo.ControlNotes");
            DropTable("dbo.Classe_Students");
            DropTable("dbo.Classes");
        }
    }
}
