using System;
using System.Linq;
using DataService.Context;
using DataService.Entities;
using DataService.Enum;

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
            using (var db = new Ef())
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

        /// <summary>
        /// Ajouter Un Paramettre Pour Un Utilisateur
        /// </summary>       
        /// <returns></returns>
        public bool AddSetting(Setting newSetting)
        {
            using (var db = new Ef())
            {
                //db.Setting.Add(newSetting);
                return db.SaveChanges() > 0;
            }
        }
       
         /// <summary>
         /// Return Le Paramettre Pour Un Utilisateur
         /// </summary>
         /// <param name="userProfileId"></param>
         /// <param name="mySetting"></param>
         /// <returns></returns>
         public string GetSetting(Guid userProfileId, Settings mySetting)
        {
            using (var db = new Ef())
            {
                //return db.Setting.FirstOrDefault(s => s.UserProfileId == userProfileId && s.SettingNum == (int)mySetting)?.SettingValue;
                return null;
            }
        }



    }
}
