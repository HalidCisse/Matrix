using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataService.Entities;

namespace Matrix.Model
{
    public class MatiereStaffsModel
    {
        
        public string STAFF_ID { get; set; }

        public string FULL_NAME { get; set; }

        public byte[] PHOTO_IDENTITY { get; set; }

        public string QUALIFICATION { get; set; }
       
        public Boolean IsINSTRUCTOR { get; set; }
        
    }
}
