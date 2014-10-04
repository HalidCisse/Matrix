using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;

namespace DataAccess
{
    public class EFR: CommonInterface
    {


        #region



        //Entities DB = new Entities ();
        


        #endregion





        #region Implementations

        public List<Student> GetAllStudents ( )
        {
            throw new NotImplementedException ();
        }

        public Student GetStudentByGUID ( Guid guid )
        {
            throw new NotImplementedException ();
        }

        public void SaveStudent ( Student MyStudent )
        {
            var DB = new MatrixDBEntities ();

            if (DB.ETUDIANTS.Find(MyStudent.GUID) != null)
                UpdateStudent(MyStudent);
            else
                AddStudent(MyStudent);
        }

        public void AddStudent(ETUDIANT MyStudent)
        {
            ETUDIANT newStudent = MyStudent;
            //var newStudent = FromStudent (MyStudent);
            using(var DB = new MatrixDBEntities ())
            {
                
                newStudent.GUID = Guid.NewGuid();
                DB.ETUDIANTS.Add (newStudent);
                DB.SaveChanges ();               
            }
        }

        public void UpdateStudent(Student MyStudent)
        {
            using(var DB = new MatrixDBEntities ())
            {
                var Query = DB.ETUDIANTS.Where (S => S.GUID == MyStudent.GUID);
                // ReSharper disable once NotAccessedVariable
                var ST = Query.Single ();
                ST = FromStudent (MyStudent);               
                DB.SaveChanges ();
            }            
        }



        #endregion




        #region Helpers


        internal ETUDIANT FromStudent( Student MyStudent)
        {
            var OutStudent = new ETUDIANT
            {
                //OutStudent.PHOTO_IDENTITE = MyStudent.PHOTO_IDENTITE,
                GUID = MyStudent.GUID,
                CIVILITE = MyStudent.CIVILITE,
                NOM = MyStudent.NOM,
                PRENOM = MyStudent.PRENOM,
                MATRICULE = MyStudent.MATRICULE,
                NUMERO_ID = MyStudent.NUMERO_IDENTITE,
                DATE_NAISSANCE = MyStudent.DATE_NAISSANCE,
                NATIONALITE = MyStudent.NATIONALITE,
                LIEU_NAISSANCE = MyStudent.LIEU_NAISSANCE,
                NUMERO_TEL = MyStudent.NUMERO_TEL,
                ADRESS_EMAIL = MyStudent.ADRESS_EMAIL,
                ADRESS_DOMICILE = MyStudent.ADRESS_DOMICILE,
                STATUT = MyStudent.STATUT
            };                           
            return OutStudent;
        }



        #endregion












        public void AddStudent ( Student student )
        {
            throw new NotImplementedException ();
        }
    }
}
