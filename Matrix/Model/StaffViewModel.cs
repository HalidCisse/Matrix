using System.Collections.Generic;
using DataService.Entities;

namespace Matrix.Model
{
    public class StaffViewModel
    {
        public StaffViewModel()
        {
            StaffsList = new List<Staff> ();
        }

        public string DepartementName { get; set; }

        public List<Staff> StaffsList { get; set; }

        public int StaffCount  { get; set; }
        
    }
}
