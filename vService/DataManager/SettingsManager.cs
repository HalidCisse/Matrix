using System;
using Common.Shared.Enums;
using DataService.Context;

namespace DataService.DataManager
{
    /// <summary>
    /// Gestion Des Parametres
    /// </summary>
    public class SettingsManager
    {
        
        /// <summary>
        /// Modifier Un Parametre
        /// </summary>
        /// <returns></returns>
        public bool SetSetting(Guid userProfileId, Settings mySetting, string newValue)
        {
            using (var db = new SchoolContext())
            {
                //var firstOrDefault = db.Setting.FirstOrDefault(s => s.UserProfileId == userProfileId && s.SettingNum == (int)mySetting);

                //if (firstOrDefault != null)
                //{
                //    firstOrDefault.SettingValue = newValue;
                //}
                //else
                //{
                //    var newSetting = new Setting
                //    {
                //        SettingId = Guid.NewGuid(),
                //        UserProfileId = userProfileId,
                //        SettingNum = (int)mySetting,
                //        SettingValue = newValue
                //    };
                //    AddSetting(newSetting);
                //}

                return db.SaveChanges() > 0;
            }
        }

        ///// <summary>
        ///// Ajouter Un Paramettre Pour Un Utilisateur
        ///// </summary>       
        ///// <returns></returns>
        //public bool AddSetting(UserSetting newUserSetting)
        //{
        //    using (var db = new SchoolContext())
        //    {
        //        //db.Setting.Add(newSetting);
        //        return db.SaveChanges() > 0;
        //    }
        //}
       
         /// <summary>
         /// Return Le Paramettre Pour Un Utilisateur
         /// </summary>
         /// <param name="userProfileId"></param>
         /// <param name="mySetting"></param>
         /// <returns></returns>
         public string GetSetting(Guid userProfileId, Settings mySetting)
        {
            using (var db = new SchoolContext())
            {
                //return db.Setting.FirstOrDefault(s => s.UserProfileId == userProfileId && s.SettingNum == (int)mySetting)?.SettingValue;
                return null;
            }
        }



    }
}
