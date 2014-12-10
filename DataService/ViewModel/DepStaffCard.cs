using System.Collections.Generic;
using System.Linq;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    public class DepStaffCard
    {


        public DepStaffCard (string departement)
        {            
             DepartementName = departement.ToUpper ();                       
            GetSTAFFS_LIST();
            //GetSTAFF_COUNT();
            //STAFF_COUNT = STAFFS_LIST.Count();
        }
        

        public string DepartementName { get; set; }

        public List<Staff> StaffsList { get; set; }

        public int StaffCount  { get; set; }


        #region HELPERS

        private void GetSTAFFS_LIST ( )
        {
            StaffsList = new List<Staff> ();

            using(var db = new Ef ())
            {
                StaffsList = string.IsNullOrEmpty(DepartementName)? db.Staff.Where (s => string.IsNullOrEmpty (s.Departement)).ToList () : db.Staff.Where (s => s.Departement == DepartementName).ToList ();
            }
        }

        public void GetSTAFF_COUNT ( )
        {
            using(var db = new Ef ())
            {
                StaffCount = string.IsNullOrEmpty(DepartementName) ? db.Staff.Count(s => string.IsNullOrEmpty (s.Departement)) : db.Staff.Count(s => s.Departement == DepartementName);
            }
        }

        #endregion

    }
}
