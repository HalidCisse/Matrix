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
                        AnneeScolaireId = c.Guid(nullable: false),
                        Name = c.String(),
                        DateDebut = c.DateTime(),
                        DateFin = c.DateTime(),
                        DateDebutInscription = c.DateTime(),
                        DateFinInscription = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AnneeScolaireId);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClasseId = c.Guid(nullable: false),
                        Name = c.String(),
                        FiliereId = c.Guid(nullable: false),
                        Level = c.Int(nullable: false),
                        Description = c.String(),
                        AnneeScolaireId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ClasseId);
            
            CreateTable(
                "dbo.ClasseStudents",
                c => new
                    {
                        ClasseStudentsId = c.String(nullable: false, maxLength: 128),
                        ClasseId = c.String(),
                        StudentId = c.String(),
                    })
                .PrimaryKey(t => t.ClasseStudentsId);
            
            CreateTable(
                "dbo.ControlNotes",
                c => new
                    {
                        ControlnoteId = c.String(nullable: false, maxLength: 128),
                        MatierecontrolId = c.String(),
                        StudentId = c.String(),
                        Note = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ControlnoteId);
            
            CreateTable(
                "dbo.Cours",
                c => new
                    {
                        CoursId = c.Guid(nullable: false),
                        ClasseId = c.Guid(nullable: false),
                        StaffId = c.String(),
                        MatiereId = c.Guid(nullable: false),
                        Salle = c.String(),
                        RecurrenceDays = c.String(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Type = c.String(),
                        PeriodeScolaireId = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CoursId);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        FiliereId = c.Guid(nullable: false),
                        Name = c.String(),
                        Niveau = c.String(),
                        NiveauEntree = c.String(),
                        NAnnee = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FiliereId);
            
            CreateTable(
                "dbo.Inscriptions",
                c => new
                    {
                        InscriptionId = c.Guid(nullable: false),
                        StudentId = c.String(),
                        ClasseId = c.Guid(nullable: false),
                        AnneeScolaireId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InscriptionId);
            
            CreateTable(
                "dbo.InscriptionRules",
                c => new
                    {
                        InscriptionRuleId = c.Guid(nullable: false),
                        ClasseId = c.Guid(nullable: false),
                        QualificationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InscriptionRuleId);
            
            CreateTable(
                "dbo.Matieres",
                c => new
                    {
                        MatiereId = c.Guid(nullable: false),
                        Name = c.String(),
                        Sigle = c.String(),
                        ClasseId = c.Guid(nullable: false),
                        Coefficient = c.Int(nullable: false),
                        Couleur = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MatiereId);
            
            CreateTable(
                "dbo.MatiereControls",
                c => new
                    {
                        MatierecontrolId = c.String(nullable: false, maxLength: 128),
                        CoursId = c.String(),
                        StartTime = c.DateTime(),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatierecontrolId);
            
            CreateTable(
                "dbo.MatiereInstructeurs",
                c => new
                    {
                        MatiereInstructeursId = c.Guid(nullable: false),
                        MatiereId = c.Guid(nullable: false),
                        StaffId = c.String(),
                    })
                .PrimaryKey(t => t.MatiereInstructeursId);
            
            CreateTable(
                "dbo.MatrixSettings",
                c => new
                    {
                        SysId = c.Guid(nullable: false),
                        CurrentAnneeScolaireGuid = c.Guid(nullable: false),
                        CurrentPeriodeScolaireGuid = c.Guid(nullable: false),
                        EtablissementName = c.String(),
                        EtablissementTel = c.String(),
                        EtablissementFax = c.String(),
                        EtablissementLogo = c.Binary(),
                    })
                .PrimaryKey(t => t.SysId);
            
            CreateTable(
                "dbo.PeriodeScolaires",
                c => new
                    {
                        PeriodeScolaireId = c.Guid(nullable: false),
                        Name = c.String(),
                        AnneeScolaireId = c.Guid(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PeriodeScolaireId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationId = c.Guid(nullable: false),
                        Niveau = c.String(),
                        FiliereId = c.Guid(nullable: false),
                        Etablissement = c.String(),
                        BacPlus = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.QualificationId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.String(nullable: false, maxLength: 128),
                        Position = c.String(),
                        Departement = c.String(),
                        Qualification = c.String(),
                        HiredDate = c.DateTime(),
                        Statut = c.String(),
                        Title = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhotoIdentity = c.Binary(),
                        Nationality = c.String(),
                        IdentityNumber = c.String(),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(),
                        PhoneNumber = c.String(),
                        EmailAdress = c.String(),
                        HomeAdress = c.String(),
                    })
                .PrimaryKey(t => t.StaffId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhotoIdentity = c.Binary(),
                        Nationality = c.String(),
                        IdentityNumber = c.String(),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(),
                        PhoneNumber = c.String(),
                        EmailAdress = c.String(),
                        HomeAdress = c.String(),
                        Statut = c.String(),
                        RegistrationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentQualifications",
                c => new
                    {
                        StudentQualificationId = c.Guid(nullable: false),
                        StudentId = c.String(),
                        QualificationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StudentQualificationId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileId = c.Guid(nullable: false),
                        UserSpace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserProfileId = c.Guid(nullable: false),
                        CanAddStudent = c.Boolean(nullable: false),
                        CanDeleteStudent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserProfileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSettings");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.StudentQualifications");
            DropTable("dbo.Students");
            DropTable("dbo.Staffs");
            DropTable("dbo.Qualifications");
            DropTable("dbo.PeriodeScolaires");
            DropTable("dbo.MatrixSettings");
            DropTable("dbo.MatiereInstructeurs");
            DropTable("dbo.MatiereControls");
            DropTable("dbo.Matieres");
            DropTable("dbo.InscriptionRules");
            DropTable("dbo.Inscriptions");
            DropTable("dbo.Filieres");
            DropTable("dbo.Cours");
            DropTable("dbo.ControlNotes");
            DropTable("dbo.ClasseStudents");
            DropTable("dbo.Classes");
            DropTable("dbo.AnneeScolaires");
        }
    }
}
