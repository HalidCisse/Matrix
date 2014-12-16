using System;
using DataService.Context;
using DataService.Entities;
using DataService.Enum;

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
            using (var db = new Ef())
            {
                if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null) db.MatrixSetting.Add(new MatrixSetting());

                switch (systemSetting)
                {
                    case MatrixSettings.CurrentAnneeScolaire:
                        db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid = (Guid)newValue;
                        break;
                    case MatrixSettings.CurrentPerodeScolaire:
                        db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid = (Guid)newValue;
                        break;
                    case MatrixSettings.EtablissementName:
                        db.MatrixSetting.Find(MatrixConstants.SystemGuid()).EtablissementName = (string)newValue;
                        break;
                    case MatrixSettings.EtablissementFax:
                        db.MatrixSetting.Find(MatrixConstants.SystemGuid()).EtablissementFax = (string)newValue;
                        break;
                    case MatrixSettings.EtablissementLogo:
                        db.MatrixSetting.Find(MatrixConstants.SystemGuid()).EtablissementLogo = (byte[])newValue;
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
            using (var db = new Ef())
            {
                if (db.MatrixSetting.Find(MatrixConstants.SystemGuid()) == null) db.MatrixSetting.Add(new MatrixSetting());

                switch (systemSetting)
                {
                    case MatrixSettings.CurrentAnneeScolaire:
                        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentAnneeScolaireGuid;

                    case MatrixSettings.CurrentPerodeScolaire:
                        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).CurrentPeriodeScolaireGuid;

                    case MatrixSettings.EtablissementName:
                        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).EtablissementName;

                    case MatrixSettings.EtablissementFax:
                        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).EtablissementFax;

                    case MatrixSettings.EtablissementLogo:
                        return db.MatrixSetting.Find(MatrixConstants.SystemGuid()).EtablissementLogo;

                    default:
                        return null;
                }
            }
        }







    }
}
