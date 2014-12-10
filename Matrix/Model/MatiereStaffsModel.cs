using System;

namespace Matrix.Model
{
    public class MatiereStaffsModel
    {
        
        public string StaffId { get; set; }

        public string FullName { get; set; }

        public byte[] PhotoIdentity { get; set; }

        public string Qualification { get; set; }
       
        public Boolean IsInstructor { get; set; }
        
    }
}
