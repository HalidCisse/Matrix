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
                        AnneeScolaireGuid = c.Guid(nullable: false),
                        Name = c.String(),
                        DateDebut = c.DateTime(),
                        DateFin = c.DateTime(),
                        DateDebutInscription = c.DateTime(),
                        DateFinInscription = c.DateTime(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AnneeScolaireGuid);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClasseGuid = c.Guid(nullable: false),
                        Name = c.String(),
                        FiliereGuid = c.Guid(nullable: false),
                        AnneeScolaireGuid = c.Guid(nullable: false),
                        Level = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ClasseGuid);
            
            CreateTable(
                "dbo.ClasseStudents",
                c => new
                    {
                        ClasseStudentsGuid = c.Guid(nullable: false),
                        ClasseGuid = c.Guid(nullable: false),
                        StudentGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ClasseStudentsGuid);
            
            CreateTable(
                "dbo.ControlNotes",
                c => new
                    {
                        ControlNoteGuid = c.Guid(nullable: false),
                        MatiereControlGuid = c.Guid(nullable: false),
                        StudentGuid = c.Guid(nullable: false),
                        Note = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ControlNoteGuid);
            
            CreateTable(
                "dbo.Cours",
                c => new
                    {
                        CoursGuid = c.Guid(nullable: false),
                        ClasseGuid = c.Guid(nullable: false),
                        StaffGuid = c.Guid(nullable: false),
                        MatiereGuid = c.Guid(nullable: false),
                        Salle = c.String(),
                        RecurrenceDays = c.String(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Type = c.String(),
                        PeriodeScolaireGuid = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CoursGuid);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        FiliereGuid = c.Guid(nullable: false),
                        Name = c.String(),
                        Niveau = c.String(),
                        NiveauEntree = c.String(),
                        NAnnee = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FiliereGuid);
            
            CreateTable(
                "dbo.Inscriptions",
                c => new
                    {
                        InscriptionGuid = c.Guid(nullable: false),
                        StudentGuid = c.Guid(nullable: false),
                        ClasseGuid = c.Guid(nullable: false),
                        AnneeScolaireGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InscriptionGuid);
            
            CreateTable(
                "dbo.InscriptionRules",
                c => new
                    {
                        InscriptionRuleGuid = c.Guid(nullable: false),
                        ClasseGuid = c.Guid(nullable: false),
                        QualificationGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.InscriptionRuleGuid);
            
            CreateTable(
                "dbo.Matieres",
                c => new
                    {
                        MatiereGuid = c.Guid(nullable: false),
                        Name = c.String(),
                        Sigle = c.String(),
                        ClasseGuid = c.Guid(nullable: false),
                        MassHoraire = c.DateTime(),
                        Coefficient = c.Int(nullable: false),
                        Couleur = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.MatiereGuid);
            
            CreateTable(
                "dbo.MatiereControls",
                c => new
                    {
                        MatierecontrolGuid = c.String(nullable: false, maxLength: 128),
                        CoursGuid = c.String(),
                        StartTime = c.DateTime(),
                        Duration = c.DateTime(),
                    })
                .PrimaryKey(t => t.MatierecontrolGuid);
            
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
                        SysGuid = c.Guid(nullable: false),
                        CurrentAnneeScolaireGuid = c.Guid(nullable: false),
                        CurrentPeriodeScolaireGuid = c.Guid(nullable: false),
                        EtablissementName = c.String(),
                        EtablissementTel = c.String(),
                        EtablissementFax = c.String(),
                        EtablissementLogo = c.Binary(),
                        EtablissementCountry = c.String(),
                        EtablissementCity = c.String(),
                        EtablissementAdress = c.String(),
                    })
                .PrimaryKey(t => t.SysGuid);
            
            CreateTable(
                "dbo.PeriodeScolaires",
                c => new
                    {
                        PeriodeScolaireGuid = c.Guid(nullable: false),
                        Name = c.String(),
                        AnneeScolaireGuid = c.Guid(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PeriodeScolaireGuid);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationGuid = c.Guid(nullable: false),
                        Niveau = c.String(),
                        FiliereGuid = c.Guid(nullable: false),
                        Etablissement = c.String(),
                        BacPlus = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.QualificationGuid);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffGuid = c.Guid(nullable: false),
                        StaffId = c.String(),
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
                        RegistrationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StaffGuid);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentGuid = c.Guid(nullable: false),
                        StudentId = c.String(),
                        Title = c.String(),
                        FirstName = c.String(maxLength: 20),
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
                .PrimaryKey(t => t.StudentGuid);
            
            CreateTable(
                "dbo.StudentQualifications",
                c => new
                    {
                        StudentQualificationGuid = c.Guid(nullable: false),
                        StudentGuid = c.Guid(nullable: false),
                        QualificationGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StudentQualificationGuid);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserProfileGuid = c.Guid(nullable: false),
                        UserSpace = c.Int(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        Statut = c.String(),
                    })
                .PrimaryKey(t => t.UserProfileGuid);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserProfileGuid = c.Guid(nullable: false),
                        CanAddStudent = c.Boolean(nullable: false),
                        CanDeleteStudent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileGuid);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserProfileGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileGuid);
            
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
