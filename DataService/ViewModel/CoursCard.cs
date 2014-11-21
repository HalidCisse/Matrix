using System;
using DataService.Context;
using DataService.Entities;

namespace DataService.ViewModel
{
    public class CoursCard
    {
        public CoursCard ( Cours CurrentCous, DayOfWeek CoursDay )
        {
            TYPE = CurrentCous.TYPE.ToUpper ();
            COURS_ID = CurrentCous.COURS_ID;           
            
            SALLE = CurrentCous.SALLE.ToUpper();

            COURS_DAY = CoursDay;
            START_TIME = CurrentCous.START_TIME.GetValueOrDefault().TimeOfDay.ToString();
            END_TIME   = CurrentCous.END_TIME.GetValueOrDefault ().TimeOfDay.ToString ();

            ResolveData ( CurrentCous.MATIERE_ID, CurrentCous.STAFF_ID);
        }
       
        public Guid COURS_ID { get; set; }

        public string MATIERE_NAME { get; set; }

        public string TYPE { get; set; } 

        public string STAFF_FULL_NAME { get; set; }
       
        public string SALLE { get; set; }
         
        public DayOfWeek COURS_DAY { get; set; }

        public string START_TIME { get; set; }

        public string END_TIME { get; set; }

        private void ResolveData ( Guid MatiereID, string StaffID )
        {           
            using(var Db = new EF ())
            {
                MATIERE_NAME = Db.MATIERE.Find (MatiereID).NAME;

                STAFF_FULL_NAME = Db.STAFF.Find (StaffID).FULL_NAME;
            }
        }


    }
}
