using System;
using System.ComponentModel.DataAnnotations;

namespace DataService.Entities
{
    public class Filiere_Matieres_DEP
    {
        [Key]
        public string FILIERE_MATIERE_ID { get; set; } // FiliereID + FiliereLevel + MatiereID

        public string FILIERE_ID { get; set; }    //ID de la filiere Consernee

        public int FILIERE_LEVEL { get; set; }  // 2 ere Annnee -> 2 eme Annnee

        public string MATIERE_ID { get; set; }   //ID de la matiere Consernee

        public string HEURE_PAR_SEMAINE { get; set; }  // 2 Heures, 2 Heures 30 min
    }
}
