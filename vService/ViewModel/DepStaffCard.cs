using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Shared.Entity;
using DataService.Context;

namespace DataService.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class DepStaffCard
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="departement"></param>
        public DepStaffCard (string departement)
        {            
            DepartementName = departement.ToUpper ();            
            GetSTAFFS_LIST();
            StaffCount = StaffsList.Count;
            //GetSTAFF_COUNT();
            //STAFF_COUNT = STAFFS_LIST.Count();
        }
        

        /// <summary>
        /// 
        /// </summary>
        public string DepartementName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Staff> StaffsList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StaffCount { get; set; }


        #region HELPERS

        private void GetSTAFFS_LIST ( )
        {
            StaffsList = new List<Staff> ();

            using (var db = new SchoolContext())
                StaffsList = string.IsNullOrEmpty(DepartementName)
                    ? db.Staffs.Where(s => !s.Person.IsDeleted && string.IsNullOrEmpty(s.DepartementPrincipale)).Include(s => s.Person).ToList()
                    : db.Staffs.Where(s => !s.Person.IsDeleted && s.DepartementPrincipale == DepartementName).Include(s=> s.Person).ToList();
        }

        private void GetSTAFF_COUNT ( )
        {
            using (var db = new SchoolContext())
                StaffCount = string.IsNullOrEmpty(DepartementName)
                    ? db.Staffs.Count(s => string.IsNullOrEmpty(s.DepartementPrincipale))
                    : db.Staffs.Count(s => s.DepartementPrincipale == DepartementName);
        }

        #endregion

    }
}
