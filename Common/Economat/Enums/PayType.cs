using System.ComponentModel;

namespace Common.Economat.Enums
{

	/// <summary>
	/// Methode de Versement des Salaires aux Staffs
	/// </summary>
	public enum PayType
	{

		/// <summary>
		/// Salaire Fixe 
		/// </summary>
		[Description("Salaire Fixe")]
        SalaryOnly,

        /// <summary>
        /// Heures Enseignées
        /// </summary>
        [Description("Heures Enseignées")]
        HoursTaught

        ///// <summary>
        ///// Heures Travailer
        ///// </summary>
        //[Description("Heures Travailées")]
        //HoursWorked = 2



    }
}
