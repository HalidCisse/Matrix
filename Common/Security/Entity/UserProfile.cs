using System;
using Common.Security.Enums;

namespace Common.Security.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class UserProfile 
    {

        /// <summary>
        /// 
        /// </summary>
        public Guid UserProfileGuid { get; set; }


        ///// <summary>
        ///// Admin, Staff, Student
        ///// </summary>
        //public UserSpace UserSpace { get; set; }



        ///// <summary>
        ///// Admin, Staff, Student
        ///// </summary>
        //public UserSpace UserSpace { get; set; }


        /// <summary>
        /// Admin, Staff, Student
        /// </summary>
        public UserSpace UserSpace { get; set; }





    }
}
