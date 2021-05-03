using System;

namespace Common.Economat.Entity
{
    /// <summary>
    /// Recue A Payer Par l'Etudiants
    /// </summary>
    public class StudentBill
    {
        /// <summary>
        /// default
        /// </summary>
        public StudentBill()
        {
            
        }


        /// <summary>
        /// Recue a partir d'une SchoolFee
        /// </summary>
        public StudentBill(SchoolFee schoolFee)
        {
            SchoolFeeGuid  = schoolFee.SchoolFeeGuid;
            StudentGuid    = schoolFee.StudentGuid;
            Designation    = schoolFee.Designation;
            NetAmount      = schoolFee.NetAmount;
            DueDate        = schoolFee.DueDate.GetValueOrDefault();
            IsInstallement = schoolFee.IsInstallement;
        }


        /// <summary>
        /// Le Guid du Receipt
        /// </summary>
        public Guid SchoolFeeGuid { get; set; }


        /// <summary>
        /// Le Guid de l'étudiant concernée
        /// </summary>
        public Guid StudentGuid { get; set; }


        /// <summary>
        /// Motif de ce Payement, ex: FE-Oct-2015-ME, FE-Nov-2015-TR, FE-Dec-2015-SE
        /// </summary>
        public string Designation { get; set; }


        /// <summary>
        /// La Somme a payer
        /// </summary>
        public double NetAmount { get; set; }


        /// <summary>
        /// Date ou La Recue doit etre Payer Au Plus Tard 
        /// </summary>
        public DateTime DueDate { get; set; }


        /// <summary>
        /// IsInstallement
        /// </summary>
        public bool IsInstallement { get; set; }
    }
}
