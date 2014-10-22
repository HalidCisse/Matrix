using System.Collections.Generic;
using DataService.Entities;

namespace Matrix.Model
{
    public class StaffViewModel
    {
        public StaffViewModel()
        {
            STAFFS_LIST = new List<Staff> ();
        }

        public string DEPARTEMENT_NAME { get; set; }

        public List<Staff> STAFFS_LIST { get; set; }

        public int STAFF_COUNT  { get; set; }
        
    }
}
