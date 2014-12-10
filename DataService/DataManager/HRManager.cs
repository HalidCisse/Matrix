using DataService.Context;
using DataService.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Ressources Humaines
    /// </summary>
    public class HrManager
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="myStaff"></param>
       /// <returns></returns>
        public bool AddStaff(Staff myStaff)
        {
            using (var db = new Ef())
            {
                db.Staff.Add(myStaff);
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mStaff"></param>
        /// <returns></returns>
        public bool UpdateStaff(Staff mStaff)
        {
            using (var db = new Ef())
            {
                db.Staff.Attach(mStaff);
                db.Entry(mStaff).State = System.Data.Entity.EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool DeleteStaff(string staffId)
        {
            using (var db = new Ef())
            {
                db.Staff.Remove(db.Staff.Find(staffId));
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public Staff GetStaffById(string staffId)
        {
            using (var db = new Ef())
            {          
                return db.Staff.Find(staffId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstAndLastName"></param>
        /// <returns></returns>
        public Staff GetStaffByFullName(string firstAndLastName)
        {
            using (var db = new Ef())
            {
                var myStaff = db.Staff.SingleOrDefault(s => s.FullName == firstAndLastName);

                return myStaff;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Staff> GetAllStaffs()
        {
            using (var db = new Ef())
            {
                return db.Staff.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllStaffsId()
        {
            var ds = new List<string>();
            using (var db = new Ef())
            {
                ds.AddRange(db.Staff.ToList().Select(s => s.StaffId));
                return ds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerable GetAllStaffNames()
        {
            var names = new List<string>();
            using (var db = new Ef())
            {
                names.AddRange(db.Staff.ToList().Select(s => s.FullName));
                return names;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depName"></param>
        /// <returns></returns>
        public List<Staff> GetDepStaffs(string depName = null)
        {
            using (var db = new Ef())
            {
                return depName == null ? db.Staff.ToList().Where(s => string.IsNullOrEmpty(s.Departement)).ToList() : db.Staff.ToList().Where(s => s.Departement == depName).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public string GetStaffFullName(string staffId)
        {
            using (var db = new Ef())
            {
                return db.Staff.Find(staffId).FullName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool StaffExist(string staffId)
        {
            using (var db = new Ef())
            {
                return db.Staff.Find(staffId) != null;
            }
        }

    }
}
