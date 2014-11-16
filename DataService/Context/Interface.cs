using System.Collections.Generic;
using DataService.Entities;

namespace DataService.Context
{
    public interface Interface
    {


        #region Student

        bool AddStudent ( Student MyStudent );

        bool UpdateStudent ( Student MyStudent );

        bool DeleteStudent ( string StudentID );       

        Student GetStudentByID ( string STudentID );

        Student GetStudentByFullName ( string FirstANDLastName );

        List<Student> GetAllStudents ( );

        string GetStudentName(string StudentID);

        bool StudentExist(string StudentID);

        #endregion



        #region Patternes DATA

        List<string> GetNATIONALITIES();
        List<string> GetBIRTH_PLACE();
        List<string> GetTITLES();
        List<string> GetSTATUTS();

        #endregion






    }
}
