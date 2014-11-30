namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AnneeScolaire", newName: "AnneeScolaires");
            RenameTable(name: "dbo.Classe", newName: "Classes");
            RenameTable(name: "dbo.ControlNote", newName: "ControlNotes");
            RenameTable(name: "dbo.Etablissement", newName: "Etablissements");
            RenameTable(name: "dbo.Filiere", newName: "Filieres");
            RenameTable(name: "dbo.Inscription", newName: "Inscriptions");
            RenameTable(name: "dbo.InscriptionRule", newName: "InscriptionRules");
            RenameTable(name: "dbo.Matiere", newName: "Matieres");
            RenameTable(name: "dbo.MatiereControl", newName: "MatiereControls");
            RenameTable(name: "dbo.PeriodeScolaire", newName: "PeriodeScolaires");
            RenameTable(name: "dbo.Qualification", newName: "Qualifications");
            RenameTable(name: "dbo.Salle", newName: "Salles");
            RenameTable(name: "dbo.Setting", newName: "Settings");
            RenameTable(name: "dbo.Staff", newName: "Staffs");
            RenameTable(name: "dbo.Student", newName: "Students");
            RenameTable(name: "dbo.StudentQualification", newName: "StudentQualifications");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.StudentQualifications", newName: "StudentQualification");
            RenameTable(name: "dbo.Students", newName: "Student");
            RenameTable(name: "dbo.Staffs", newName: "Staff");
            RenameTable(name: "dbo.Settings", newName: "Setting");
            RenameTable(name: "dbo.Salles", newName: "Salle");
            RenameTable(name: "dbo.Qualifications", newName: "Qualification");
            RenameTable(name: "dbo.PeriodeScolaires", newName: "PeriodeScolaire");
            RenameTable(name: "dbo.MatiereControls", newName: "MatiereControl");
            RenameTable(name: "dbo.Matieres", newName: "Matiere");
            RenameTable(name: "dbo.InscriptionRules", newName: "InscriptionRule");
            RenameTable(name: "dbo.Inscriptions", newName: "Inscription");
            RenameTable(name: "dbo.Filieres", newName: "Filiere");
            RenameTable(name: "dbo.Etablissements", newName: "Etablissement");
            RenameTable(name: "dbo.ControlNotes", newName: "ControlNote");
            RenameTable(name: "dbo.Classes", newName: "Classe");
            RenameTable(name: "dbo.AnneeScolaires", newName: "AnneeScolaire");
        }
    }
}
