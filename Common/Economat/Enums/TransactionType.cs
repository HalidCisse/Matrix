using System.ComponentModel;

namespace Common.Economat.Enums {


    /// <summary>
    /// TransactionType
    /// </summary>
    public enum TransactionType {

        /// <summary>
        /// Dépense, Decaissement
        /// </summary>
        [Description("Dépense")]
        Expense = 1,


        /// <summary>
        /// Recettes, Encaissement
        /// </summary>
        [Description("Recettes")]
        Income = 2



    }
}
