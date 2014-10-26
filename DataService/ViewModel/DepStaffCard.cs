using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    public class DepStaffCard
    {


        public DepStaffCard (string Departement)
        {            
             DEPARTEMENT_NAME = Departement.ToUpper ();                       
            GetSTAFFS_LIST();
            //GetSTAFF_COUNT();
            //STAFF_COUNT = STAFFS_LIST.Count();
        }
        

        public string DEPARTEMENT_NAME { get; set; }

        public List<Staff> STAFFS_LIST { get; set; }

        public int STAFF_COUNT  { get; set; }


        #region HELPERS

        private void GetSTAFFS_LIST ( )
        {
            STAFFS_LIST = new List<Staff> ();

            using(var Db = new EF ())
            {
                STAFFS_LIST = string.IsNullOrEmpty(DEPARTEMENT_NAME)? Db.STAFF.Where (S => string.IsNullOrEmpty (S.DEPARTEMENT)).ToList () : Db.STAFF.Where (S => S.DEPARTEMENT == DEPARTEMENT_NAME).ToList ();
            }
        }

        public void GetSTAFF_COUNT ( )
        {
            using(var Db = new EF ())
            {
                STAFF_COUNT = string.IsNullOrEmpty(DEPARTEMENT_NAME) ? Db.STAFF.Count(S => string.IsNullOrEmpty (S.DEPARTEMENT)) : Db.STAFF.Count(S => S.DEPARTEMENT == DEPARTEMENT_NAME);
            }
        }

        #endregion

    }
}
