using System;
using Common.Shared.Entity;
using Common.Shared.Enums;
using DataService.Context;

namespace DataService.DataManager
{
    /// <summary>
    /// Parametre Systems
    /// </summary>
    public class MatrixSettingsManager
    {

        /// <summary>
        /// Modifier Un Paramettre System
        /// </summary>       
        /// <returns></returns>
        public bool SetMatrixSetting(MatrixSettings systemSetting, object newValue)
        {
            using (var db = new SchoolContext())
            {
                if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null) db.SystemSetting.Add(new MatrixSetting());

                switch (systemSetting)
                {
                    case MatrixSettings.CurrentAnneeScolaire:
                        db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid = (Guid)newValue;
                        break;
                    case MatrixSettings.CurrentPerodeScolaire:
                        db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid = (Guid)newValue;
                        break;
                    case MatrixSettings.EtablissementName:
                        db.SystemSetting.Find(MatrixConstants.SystemGuid()).EtablissementName = (string)newValue;
                        break;
                    case MatrixSettings.EtablissementFax:
                        db.SystemSetting.Find(MatrixConstants.SystemGuid()).EtablissementFax = (string)newValue;
                        break;
                    case MatrixSettings.EtablissementLogo:
                        db.SystemSetting.Find(MatrixConstants.SystemGuid()).EtablissementLogo = (byte[])newValue;
                        break;
                    default:
                        return true;
                }
                
                return db.SaveChanges() > 0;
            }
        }


        /// <summary>
        /// Return La Valeur du Paramettre System
        /// </summary>        
        /// <param name="systemSetting"></param>
        /// <returns></returns>
        public object GetMatrixSetting(MatrixSettings systemSetting)
        {
            using (var db = new SchoolContext())
            {
                if (db.SystemSetting.Find(MatrixConstants.SystemGuid()) == null) db.SystemSetting.Add(new MatrixSetting());

                switch (systemSetting)
                {
                    case MatrixSettings.CurrentAnneeScolaire:
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;

                    case MatrixSettings.CurrentPerodeScolaire:
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;

                    case MatrixSettings.EtablissementName:
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).EtablissementName;

                    case MatrixSettings.EtablissementFax:
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).EtablissementFax;

                    case MatrixSettings.EtablissementLogo:
                        return db.SystemSetting.Find(MatrixConstants.SystemGuid()).EtablissementLogo;

                    default:
                        return null;
                }
            }
        }







    }
}
