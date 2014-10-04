using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;

namespace DataAccess
{
    public interface CommonInterface
    {
        
        List<Student> GetAllStudents ( );

        Student GetStudentByGUID ( Guid guid );

        void SaveStudent ( Student student );

        void AddStudent ( Student student );

        void UpdateStudent ( Student student );

        











    }
}
