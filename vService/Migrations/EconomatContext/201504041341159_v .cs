using System.Data.Entity.Migrations;

namespace DataService.Migrations.EconomatContext
{
    public partial class v : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employments",
                c => new
                    {
                        EmploymentGuid = c.Guid(false),
                        StaffGuid = c.Guid(false),
                        Position = c.String(),
                        Category = c.String(),
                        Grade = c.String(),
                        Departement = c.String(),
                        Division = c.String(),
                        Project = c.String(),
                        ReportTo = c.String(),
                        SalaryRecurrence = c.Int(false),
                        PayType = c.Int(false),
                        HourlyPay = c.Double(false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.EmploymentGuid);
            
            CreateTable(
                "dbo.Payrolls",
                c => new
                    {
                        PayrollGuid = c.Guid(false),
                        EmploymentGuid = c.Guid(false),
                        Designation = c.String(),
                        PaycheckDate = c.DateTime(),
                        IsPaid = c.Boolean(false),
                        IsPaidTo = c.Guid(false),
                        DatePaid = c.DateTime(),
                        HoursWorked = c.Time(false, 7),
                        FinalPaycheck = c.Double(false),
                        NumeroReference = c.String(),
                        PaymentMethode = c.Int(false),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.PayrollGuid);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        SalaryGuid = c.Guid(false),
                        EmploymentGuid = c.Guid(false),
                        Designation = c.String(),
                        Remuneration = c.Double(false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.SalaryGuid);
            
            CreateTable(
                "dbo.SchoolFees",
                c => new
                    {
                        SchoolFeeGuid = c.Guid(false),
                        StudentGuid = c.Guid(false),
                        Designation = c.String(),
                        NetAmount = c.Double(false),
                        DueDate = c.DateTime(),
                        NumeroReference = c.String(),
                        IsPaid = c.Boolean(false),
                        IsInstallement = c.Boolean(false),
                        IsPaidBy = c.String(),
                        IsPaidTo = c.Guid(false),
                        DatePaid = c.DateTime(),
                        PaymentMethode = c.Int(false),
                        NumeroVirement = c.String(),
                        Bank = c.String(),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.SchoolFeeGuid);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionGuid = c.Guid(false),
                        TransactionReference = c.String(),
                        Designation = c.String(),
                        PaidToward = c.String(),
                        Amount = c.Double(false),
                        PaymentMethode = c.Int(false),
                        TransactionDate = c.DateTime(),
                        Description = c.String(),
                        AddUserGuid = c.Guid(false),
                        DateAdded = c.DateTime(),
                        LastEditDate = c.DateTime(),
                        LastEditUserGuid = c.Guid(false),
                        IsDeleted = c.Boolean(false),
                        DeleteUserGuid = c.Guid(false),
                        DeleteDate = c.DateTime()
                    })
                .PrimaryKey(t => t.TransactionGuid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transactions");
            DropTable("dbo.SchoolFees");
            DropTable("dbo.Salaries");
            DropTable("dbo.Payrolls");
            DropTable("dbo.Employments");
        }
    }
}
