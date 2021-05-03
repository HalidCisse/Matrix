using System;

namespace Common.Pedagogy.Entity {

    /// <summary>
    /// 
    /// </summary>
    public class CoursEvent {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="study"></param>
        /// <param name="eventDate"></param>
        public CoursEvent (Study study, DateTime eventDate)
        {
            CoursGuid = study.StudyGuid;
            EventDate = eventDate;
            StartTime = study.StartTime;
            EndTime   = study.EndTime;
        }

        /// <summary>
        /// ID du Cours
        /// </summary>
        public Guid CoursGuid { get; set; }

        /// <summary>
        /// Le jours de l'evenement
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Heure de debut du cours
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Heure de fin du cours
        /// </summary>
        public TimeSpan EndTime { get; set; }


    }
}
