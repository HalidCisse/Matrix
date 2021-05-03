using System.Data.Entity.Migrations;

namespace DataService.Migrations.SchoolContext
{
    public partial class v : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbsenceTickets",
                c => new
                    {
                        AbsenceTicketGuid = c.Guid(false),
                        PersonGuid = c.Guid(false),
                        CoursGuid = c.Guid(false),
                        CoursDate = c.DateTime(),
                        IsPresent = c.Boolean(false),
                        RetardTime = c.Time(false, 7)
                    })
                .PrimaryKey(t => t.AbsenceTicketGuid)
                .ForeignKey("dbo.People", t => t.PersonGuid, true)
                .Index(t => t.PersonGuid);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonGuid = c.Guid(false),
                        Title = c.Int(false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhotoIdentity = c.Binary(),
                        HealthState = c.Int(false),
                        Nationality = c.String(),
                        IdentityNumber = c.String(),
                        BirthDate = c.DateTime(),
                        BirthPlace = c.String(),
                        PhoneNumber = c.String(),
                        EmailAdress = c.String(),
                        HomeAdress = c.String(),
                        RegistrationDate = c.DateTime(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime(),
                        Message_MessageGuid = c.Guid()
                    })
                .PrimaryKey(t => t.PersonGuid)
                .ForeignKey("dbo.Messages", t => t.Message_MessageGuid)
                .Index(t => t.Message_MessageGuid);
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatGuid = c.Guid(false),
                        Subject = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.ChatGuid);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageGuid = c.Guid(false),
                        SenderGuid = c.Guid(false),
                        ChatGuid = c.Guid(false),
                        Body = c.String(),
                        AttachementGuid = c.Guid(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.MessageGuid)
                .ForeignKey("dbo.Documents", t => t.AttachementGuid)
                .ForeignKey("dbo.Chats", t => t.ChatGuid, true)
                .ForeignKey("dbo.People", t => t.SenderGuid, true)
                .Index(t => t.SenderGuid)
                .Index(t => t.ChatGuid)
                .Index(t => t.AttachementGuid);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentGuid = c.Guid(false),
                        PersonGuid = c.Guid(false),
                        DocumentName = c.String(),
                        Description = c.String(),
                        FileType = c.Int(false),
                        DataBytes = c.Binary(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.DocumentGuid)
                .ForeignKey("dbo.People", t => t.PersonGuid, true)
                .Index(t => t.PersonGuid);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClasseGuid = c.Guid(false),
                        Name = c.String(),
                        Sigle = c.String(),
                        Session = c.String(),
                        MaxEffectif = c.Int(false),
                        FiliereGuid = c.Guid(false),
                        ClassGrade = c.Int(false),
                        Description = c.String(),
                        InscriptionAmount = c.Double(false),
                        MonthlyAmount = c.Double(false),
                        QuarterlyAmount = c.Double(false),
                        HalfYearlyAmount = c.Double(false),
                        YearlyAmount = c.Double(false),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.ClasseGuid)
                .ForeignKey("dbo.Filieres", t => t.FiliereGuid, true)
                .Index(t => t.FiliereGuid);
            
            CreateTable(
                "dbo.Studies",
                c => new
                    {
                        StudyGuid = c.Guid(false),
                        ClasseGuid = c.Guid(false),
                        ProffGuid = c.Guid(false),
                        GraderGuid = c.Guid(),
                        SupervisorGuid = c.Guid(),
                        SubjectGuid = c.Guid(false),
                        Room = c.String(),
                        RecurrenceDays = c.String(),
                        StartTime = c.Time(false, 7),
                        EndTime = c.Time(false, 7),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Type = c.Int(false),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.StudyGuid)
                .ForeignKey("dbo.Classes", t => t.ClasseGuid, true)
                .ForeignKey("dbo.Subjects", t => t.SubjectGuid, true)
                .ForeignKey("dbo.Staffs", t => t.GraderGuid)
                .ForeignKey("dbo.Staffs", t => t.ProffGuid, true)
                .ForeignKey("dbo.Staffs", t => t.SupervisorGuid)
                .Index(t => t.ClasseGuid)
                .Index(t => t.ProffGuid)
                .Index(t => t.GraderGuid)
                .Index(t => t.SupervisorGuid)
                .Index(t => t.SubjectGuid);
            
            CreateTable(
                "dbo.StudyExceptions",
                c => new
                    {
                        StudyExceptionGuid = c.Guid(false),
                        StudyGuid = c.Guid(false),
                        ExceptionDate = c.DateTime()
                    })
                .PrimaryKey(t => t.StudyExceptionGuid)
                .ForeignKey("dbo.Studies", t => t.StudyGuid, true)
                .Index(t => t.StudyGuid);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffGuid = c.Guid(false),
                        PersonGuid = c.Guid(false),
                        Matricule = c.String(),
                        PositionPrincipale = c.String(),
                        DepartementPrincipale = c.String(),
                        Division = c.String(),
                        Qualification = c.String(),
                        Diploma = c.String(),
                        DiplomaLevel = c.String(),
                        Experiences = c.Int(false),
                        FormerJob = c.String(),
                        Grade = c.String(),
                        HiredDate = c.DateTime(),
                        Statut = c.Int(false)
                    })
                .PrimaryKey(t => t.StaffGuid)
                .ForeignKey("dbo.People", t => t.PersonGuid, true)
                .Index(t => t.PersonGuid);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectGuid = c.Guid(false),
                        Name = c.String(),
                        Sigle = c.String(),
                        Discipline = c.String(),
                        TypeMatiere = c.String(),
                        Module = c.String(),
                        StudyLanguage = c.String(),
                        WeeklyHours = c.Time(false, 7),
                        TempsPrescrit = c.Time(false, 7),
                        HourlyPay = c.Double(false),
                        Coefficient = c.Int(false),
                        Couleur = c.String(),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.SubjectGuid);
            
            CreateTable(
                "dbo.StudentGrades",
                c => new
                    {
                        StudentGradeGuid = c.Guid(false),
                        CoursGuid = c.Guid(false),
                        StudentGuid = c.Guid(false),
                        DateTaken = c.DateTime(),
                        Mark = c.Double(false),
                        Coefficient = c.Int(false),
                        Barem = c.Double(false),
                        Appreciation = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.StudentGradeGuid)
                .ForeignKey("dbo.Students", t => t.StudentGuid, true)
                .ForeignKey("dbo.Studies", t => t.CoursGuid, true)
                .Index(t => t.CoursGuid)
                .Index(t => t.StudentGuid);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentGuid = c.Guid(false),
                        Matricule = c.String(),
                        Statut = c.Int(false),
                        Guardian_PersonGuid = c.Guid(),
                        Person_PersonGuid = c.Guid()
                    })
                .PrimaryKey(t => t.StudentGuid)
                .ForeignKey("dbo.People", t => t.Guardian_PersonGuid)
                .ForeignKey("dbo.People", t => t.Person_PersonGuid)
                .Index(t => t.Guardian_PersonGuid)
                .Index(t => t.Person_PersonGuid);
            
            CreateTable(
                "dbo.Enrollements",
                c => new
                    {
                        EnrollementGuid = c.Guid(false),
                        StudentGuid = c.Guid(false),
                        ClasseGuid = c.Guid(false),
                        SchoolYearGuid = c.Guid(false),
                        EnrollementNum = c.String(),
                        EnrollementStatus = c.Int(false),
                        Description = c.String(),
                        IsScholar = c.Boolean(false),
                        InstallmentRecurrence = c.Int(false),
                        InscriptionAmount = c.Double(false),
                        InstallementAmount = c.Double(false),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.EnrollementGuid)
                .ForeignKey("dbo.Classes", t => t.ClasseGuid, true)
                .ForeignKey("dbo.SchoolYears", t => t.SchoolYearGuid, true)
                .ForeignKey("dbo.Students", t => t.StudentGuid, true)
                .Index(t => t.StudentGuid)
                .Index(t => t.ClasseGuid)
                .Index(t => t.SchoolYearGuid);
            
            CreateTable(
                "dbo.SchoolYears",
                c => new
                    {
                        SchoolYearGuid = c.Guid(false),
                        Name = c.String(),
                        Session = c.String(),
                        BaremDesNotes = c.Double(false),
                        DateDebut = c.DateTime(),
                        DateFin = c.DateTime(),
                        DateDebutInscription = c.DateTime(),
                        DateFinInscription = c.DateTime(),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.SchoolYearGuid);
            
            CreateTable(
                "dbo.SchoolPeriods",
                c => new
                    {
                        SchoolPeriodGuid = c.Guid(false),
                        Name = c.String(),
                        SchoolYearGuid = c.Guid(false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime()
                    })
                .PrimaryKey(t => t.SchoolPeriodGuid)
                .ForeignKey("dbo.SchoolYears", t => t.SchoolYearGuid, true)
                .Index(t => t.SchoolYearGuid);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        FiliereGuid = c.Guid(false),
                        Name = c.String(),
                        Sigle = c.String(),
                        TypeFormation = c.String(),
                        Cycle = c.String(),
                        Diplome = c.String(),
                        Departement = c.String(),
                        Admission = c.String(),
                        Scolarite = c.Int(false),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.FiliereGuid);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        MessageGuid = c.Guid(false),
                        SenderGuid = c.Guid(),
                        RecipientGuid = c.Guid(),
                        RecipientEmail = c.String(),
                        Subject = c.String(),
                        Content = c.String(),
                        AttachementGuid = c.Guid(),
                        MessageType = c.Int(false),
                        IsRead = c.Boolean(false),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.MessageGuid)
                .ForeignKey("dbo.Documents", t => t.AttachementGuid)
                .ForeignKey("dbo.People", t => t.SenderGuid)
                .Index(t => t.SenderGuid)
                .Index(t => t.AttachementGuid);
            
            CreateTable(
                "dbo.MatrixSettings",
                c => new
                    {
                        SysGuid = c.Guid(false),
                        CurrentAnneeScolaireGuid = c.Guid(false),
                        CurrentPeriodeScolaireGuid = c.Guid(false),
                        EtablissementName = c.String(),
                        EtablissementTel = c.String(),
                        EtablissementFax = c.String(),
                        EtablissementLogo = c.Binary(),
                        EtablissementCountry = c.String(),
                        EtablissementCity = c.String(),
                        EtablissementAdress = c.String()
                    })
                .PrimaryKey(t => t.SysGuid);
            
            CreateTable(
                "dbo.ChatPersons",
                c => new
                    {
                        Chat_ChatGuid = c.Guid(false),
                        Person_PersonGuid = c.Guid(false)
                    })
                .PrimaryKey(t => new { t.Chat_ChatGuid, t.Person_PersonGuid })
                .ForeignKey("dbo.Chats", t => t.Chat_ChatGuid, true)
                .ForeignKey("dbo.People", t => t.Person_PersonGuid, true)
                .Index(t => t.Chat_ChatGuid)
                .Index(t => t.Person_PersonGuid);
            
            CreateTable(
                "dbo.SubjectStaffs",
                c => new
                    {
                        Subject_SubjectGuid = c.Guid(false),
                        Staff_StaffGuid = c.Guid(false)
                    })
                .PrimaryKey(t => new { t.Subject_SubjectGuid, t.Staff_StaffGuid })
                .ForeignKey("dbo.Subjects", t => t.Subject_SubjectGuid, true)
                .ForeignKey("dbo.Staffs", t => t.Staff_StaffGuid, true)
                .Index(t => t.Subject_SubjectGuid)
                .Index(t => t.Staff_StaffGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "SenderGuid", "dbo.People");
            DropForeignKey("dbo.Conversations", "AttachementGuid", "dbo.Documents");
            DropForeignKey("dbo.Classes", "FiliereGuid", "dbo.Filieres");
            DropForeignKey("dbo.Studies", "SupervisorGuid", "dbo.Staffs");
            DropForeignKey("dbo.StudentGrades", "CoursGuid", "dbo.Studies");
            DropForeignKey("dbo.StudentGrades", "StudentGuid", "dbo.Students");
            DropForeignKey("dbo.Students", "Person_PersonGuid", "dbo.People");
            DropForeignKey("dbo.Enrollements", "StudentGuid", "dbo.Students");
            DropForeignKey("dbo.SchoolPeriods", "SchoolYearGuid", "dbo.SchoolYears");
            DropForeignKey("dbo.Enrollements", "SchoolYearGuid", "dbo.SchoolYears");
            DropForeignKey("dbo.Enrollements", "ClasseGuid", "dbo.Classes");
            DropForeignKey("dbo.Students", "Guardian_PersonGuid", "dbo.People");
            DropForeignKey("dbo.Studies", "ProffGuid", "dbo.Staffs");
            DropForeignKey("dbo.Studies", "GraderGuid", "dbo.Staffs");
            DropForeignKey("dbo.SubjectStaffs", "Staff_StaffGuid", "dbo.Staffs");
            DropForeignKey("dbo.SubjectStaffs", "Subject_SubjectGuid", "dbo.Subjects");
            DropForeignKey("dbo.Studies", "SubjectGuid", "dbo.Subjects");
            DropForeignKey("dbo.Staffs", "PersonGuid", "dbo.People");
            DropForeignKey("dbo.StudyExceptions", "StudyGuid", "dbo.Studies");
            DropForeignKey("dbo.Studies", "ClasseGuid", "dbo.Classes");
            DropForeignKey("dbo.ChatPersons", "Person_PersonGuid", "dbo.People");
            DropForeignKey("dbo.ChatPersons", "Chat_ChatGuid", "dbo.Chats");
            DropForeignKey("dbo.Messages", "SenderGuid", "dbo.People");
            DropForeignKey("dbo.People", "Message_MessageGuid", "dbo.Messages");
            DropForeignKey("dbo.Messages", "ChatGuid", "dbo.Chats");
            DropForeignKey("dbo.Messages", "AttachementGuid", "dbo.Documents");
            DropForeignKey("dbo.Documents", "PersonGuid", "dbo.People");
            DropForeignKey("dbo.AbsenceTickets", "PersonGuid", "dbo.People");
            DropIndex("dbo.SubjectStaffs", new[] { "Staff_StaffGuid" });
            DropIndex("dbo.SubjectStaffs", new[] { "Subject_SubjectGuid" });
            DropIndex("dbo.ChatPersons", new[] { "Person_PersonGuid" });
            DropIndex("dbo.ChatPersons", new[] { "Chat_ChatGuid" });
            DropIndex("dbo.Conversations", new[] { "AttachementGuid" });
            DropIndex("dbo.Conversations", new[] { "SenderGuid" });
            DropIndex("dbo.SchoolPeriods", new[] { "SchoolYearGuid" });
            DropIndex("dbo.Enrollements", new[] { "SchoolYearGuid" });
            DropIndex("dbo.Enrollements", new[] { "ClasseGuid" });
            DropIndex("dbo.Enrollements", new[] { "StudentGuid" });
            DropIndex("dbo.Students", new[] { "Person_PersonGuid" });
            DropIndex("dbo.Students", new[] { "Guardian_PersonGuid" });
            DropIndex("dbo.StudentGrades", new[] { "StudentGuid" });
            DropIndex("dbo.StudentGrades", new[] { "CoursGuid" });
            DropIndex("dbo.Staffs", new[] { "PersonGuid" });
            DropIndex("dbo.StudyExceptions", new[] { "StudyGuid" });
            DropIndex("dbo.Studies", new[] { "SubjectGuid" });
            DropIndex("dbo.Studies", new[] { "SupervisorGuid" });
            DropIndex("dbo.Studies", new[] { "GraderGuid" });
            DropIndex("dbo.Studies", new[] { "ProffGuid" });
            DropIndex("dbo.Studies", new[] { "ClasseGuid" });
            DropIndex("dbo.Classes", new[] { "FiliereGuid" });
            DropIndex("dbo.Documents", new[] { "PersonGuid" });
            DropIndex("dbo.Messages", new[] { "AttachementGuid" });
            DropIndex("dbo.Messages", new[] { "ChatGuid" });
            DropIndex("dbo.Messages", new[] { "SenderGuid" });
            DropIndex("dbo.People", new[] { "Message_MessageGuid" });
            DropIndex("dbo.AbsenceTickets", new[] { "PersonGuid" });
            DropTable("dbo.SubjectStaffs");
            DropTable("dbo.ChatPersons");
            DropTable("dbo.MatrixSettings");
            DropTable("dbo.Conversations");
            DropTable("dbo.Filieres");
            DropTable("dbo.SchoolPeriods");
            DropTable("dbo.SchoolYears");
            DropTable("dbo.Enrollements");
            DropTable("dbo.Students");
            DropTable("dbo.StudentGrades");
            DropTable("dbo.Subjects");
            DropTable("dbo.Staffs");
            DropTable("dbo.StudyExceptions");
            DropTable("dbo.Studies");
            DropTable("dbo.Classes");
            DropTable("dbo.Documents");
            DropTable("dbo.Messages");
            DropTable("dbo.Chats");
            DropTable("dbo.People");
            DropTable("dbo.AbsenceTickets");
        }
    }
}
