using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
