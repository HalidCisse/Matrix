namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnneeScolaires",
                c => new
                    {
                        ANNEE_SCOLAIRE_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        DATE_DEBUT = c.DateTime(),
                        DATE_FIN = c.DateTime(),
                        DATE_DEBUT_INSCRIPTION = c.DateTime(),
                        DATE_FIN_INSCRIPTION = c.DateTime(),
                        DESCRIPTION = c.String(),
                    })
                .PrimaryKey(t => t.ANNEE_SCOLAIRE_ID);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        CLASSE_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        FILIERE_ID = c.Guid(nullable: false),
                        LEVEL = c.Int(nullable: false),
                        DESCRIPTION = c.String(),
                        ANNEE_SCOLAIRE_ID = c.Guid(nullable: false),
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
                        COURS_ID = c.Guid(nullable: false),
                        CLASSE_ID = c.Guid(nullable: false),
                        STAFF_ID = c.String(),
                        MATIERE_ID = c.Guid(nullable: false),
                        SALLE = c.String(),
                        RECURRENCE_DAYS = c.String(),
                        START_TIME = c.DateTime(),
                        END_TIME = c.DateTime(),
                        START_DATE = c.DateTime(),
                        END_DATE = c.DateTime(),
                        TYPE = c.String(),
                        PERIODE_SCOLAIRE_ID = c.Guid(nullable: false),
                        DESCRIPTION = c.String(),
                    })
                .PrimaryKey(t => t.COURS_ID);
            
            CreateTable(
                "dbo.Etablissements",
                c => new
                    {
                        ETABLISSEMENT_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        LOGO = c.Binary(),
                        COUNTRY = c.String(),
                        ADRESSE = c.String(),
                        PHONE = c.String(),
                        FAX = c.String(),
                        DESCRIPTION = c.String(),
                    })
                .PrimaryKey(t => t.ETABLISSEMENT_ID);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        FILIERE_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        NIVEAU = c.String(),
                        NIVEAU_ENTREE = c.String(),
                        N_ANNEE = c.Int(nullable: false),
                        DESCRIPTION = c.String(),
                    })
                .PrimaryKey(t => t.FILIERE_ID);
            
            CreateTable(
                "dbo.Inscriptions",
                c => new
                    {
                        INSCRIPTION_ID = c.Guid(nullable: false),
                        STUDENT_ID = c.String(),
                        CLASSE_ID = c.Guid(nullable: false),
                        ANNEE_SCOLAIRE_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.INSCRIPTION_ID);
            
            CreateTable(
                "dbo.InscriptionRules",
                c => new
                    {
                        INSCRIPTION_RULE_ID = c.Guid(nullable: false),
                        CLASSE_ID = c.Guid(nullable: false),
                        QUALIFICATION_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.INSCRIPTION_RULE_ID);
            
            CreateTable(
                "dbo.Matieres",
                c => new
                    {
                        MATIERE_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        SIGLE = c.String(),
                        CLASSE_ID = c.Guid(nullable: false),
                        COEFFICIENT = c.Int(nullable: false),
                        COULEUR = c.String(),
                        DESCRIPTION = c.String(),
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
                        MATIERE_INSTRUCTEURS_ID = c.Guid(nullable: false),
                        MATIERE_ID = c.Guid(nullable: false),
                        STAFF_ID = c.String(),
                    })
                .PrimaryKey(t => t.MATIERE_INSTRUCTEURS_ID);
            
            CreateTable(
                "dbo.PeriodeScolaires",
                c => new
                    {
                        PERIODE_SCOLAIRE_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        ANNEE_SCOLAIRE_ID = c.Guid(nullable: false),
                        START_DATE = c.DateTime(),
                        END_DATE = c.DateTime(),
                    })
                .PrimaryKey(t => t.PERIODE_SCOLAIRE_ID);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QUALIFICATION_ID = c.Guid(nullable: false),
                        NIVEAU = c.String(),
                        FILIERE_ID = c.Guid(nullable: false),
                        ETABLISSEMENT = c.String(),
                        BAC_PLUS = c.Int(nullable: false),
                        DESCRIPTION = c.String(),
                    })
                .PrimaryKey(t => t.QUALIFICATION_ID);
            
            CreateTable(
                "dbo.Salles",
                c => new
                    {
                        SALLE_ID = c.Guid(nullable: false),
                        NAME = c.String(),
                        ADRESSE = c.String(),
                    })
                .PrimaryKey(t => t.SALLE_ID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SETTING_ID = c.Guid(nullable: false),
                        USER_ID = c.Guid(nullable: false),
                        SETTING_NAME = c.String(),
                        SETTING_VALUE = c.String(),
                    })
                .PrimaryKey(t => t.SETTING_ID);
            
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
            
            CreateTable(
                "dbo.StudentQualifications",
                c => new
                    {
                        STUDENT_QUALIFICATION_ID = c.Guid(nullable: false),
                        STUDENT_ID = c.String(),
                        QUALIFICATION_ID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.STUDENT_QUALIFICATION_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentQualifications");
            DropTable("dbo.Students");
            DropTable("dbo.Staffs");
            DropTable("dbo.Settings");
            DropTable("dbo.Salles");
            DropTable("dbo.Qualifications");
            DropTable("dbo.PeriodeScolaires");
            DropTable("dbo.Matiere_Instructeurs");
            DropTable("dbo.MatiereControls");
            DropTable("dbo.Matieres");
            DropTable("dbo.InscriptionRules");
            DropTable("dbo.Inscriptions");
            DropTable("dbo.Filieres");
            DropTable("dbo.Etablissements");
            DropTable("dbo.Cours");
            DropTable("dbo.ControlNotes");
            DropTable("dbo.Classe_Students");
            DropTable("dbo.Classes");
            DropTable("dbo.AnneeScolaires");
        }
    }
}
