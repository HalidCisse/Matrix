using System;
using Common.Shared.Entity;
using DataService.Context;

namespace DataService.ViewModel.Pedagogy
{
    /// <summary>
    /// Represente Un Etudiant D'Une Classe
    /// </summary>
    public  class ClasseStudentCard
    {
        /// <summary>
        /// 
        /// </summary>
        public ClasseStudentCard( Student myStudent)
        {
            if (myStudent == null) myStudent = new Student();
           
            StudentGuid   = myStudent.StudentGuid;            
            Firstname     = myStudent.Person.FirstName;
            Lastname      = myStudent.Person.LastName;
            PhotoIdentity = myStudent.Person.PhotoIdentity;
            PhoneNumber   = myStudent.Person.PhoneNumber;
            
            using (var db = new SchoolContext())
            {
                Absences = "12 Cours";
                Retards = "26 fois";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Guid StudentGuid { get; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName => Firstname + " " + Lastname;

        /// <summary>
        /// 
        /// </summary>
        public string Firstname { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Lastname { get; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] PhotoIdentity { get; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Absences { get; } 

        /// <summary>
        /// 
        /// </summary>
        public string Retards { get; } 



        //todo Do Absence et Retard dans ClasseStudentsCard

    }

}
