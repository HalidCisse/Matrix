using DataService.Context;
using DataService.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion des Ressources Humaines
    /// </summary>
    public class HRManager
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="MyStaff"></param>
       /// <returns></returns>
        public bool AddStaff(Staff MyStaff)
        {
            using (var Db = new EF())
            {
                Db.STAFF.Add(MyStaff);
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MStaff"></param>
        /// <returns></returns>
        public bool UpdateStaff(Staff MStaff)
        {
            using (var Db = new EF())
            {
                Db.STAFF.Attach(MStaff);
                Db.Entry(MStaff).State = System.Data.Entity.EntityState.Modified;

                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public bool DeleteStaff(string StaffID)
        {
            using (var Db = new EF())
            {
                Db.STAFF.Remove(Db.STAFF.Find(StaffID));
                return Db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public Staff GetStaffByID(string StaffID)
        {
            using (var Db = new EF())
            {          
                return Db.STAFF.Find(StaffID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FirstANDLastName"></param>
        /// <returns></returns>
        public Staff GetStaffByFullName(string FirstANDLastName)
        {
            using (var Db = new EF())
            {
                var MyStaff = Db.STAFF.SingleOrDefault(S => S.FULL_NAME == FirstANDLastName);

                return MyStaff;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Staff> GetAllStaffs()
        {
            using (var Db = new EF())
            {
                return Db.STAFF.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllStaffsID()
        {
            var IDs = new List<string>();
            using (var Db = new EF())
            {
                IDs.AddRange(Db.STAFF.ToList().Select(S => S.STAFF_ID));
                return IDs;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerable GetAllStaffNames()
        {
            var Names = new List<string>();
            using (var Db = new EF())
            {
                Names.AddRange(Db.STAFF.ToList().Select(S => S.FULL_NAME));
                return Names;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DepName"></param>
        /// <returns></returns>
        public List<Staff> GetDepStaffs(string DepName = null)
        {
            using (var Db = new EF())
            {
                return DepName == null ? Db.STAFF.ToList().Where(S => string.IsNullOrEmpty(S.DEPARTEMENT)).ToList() : Db.STAFF.ToList().Where(S => S.DEPARTEMENT == DepName).ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public string GetStaffFullName(string StaffID)
        {
            using (var Db = new EF())
            {
                return Db.STAFF.Find(StaffID).FULL_NAME;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StaffID"></param>
        /// <returns></returns>
        public bool StaffExist(string StaffID)
        {
            using (var Db = new EF())
            {
                return Db.STAFF.Find(StaffID) != null;
            }
        }

    }
}
