

namespace DataService.DataManager {

    /// <summary>
    /// Gestion Du Budget
    /// </summary>
    public sealed class EconomatManager {


        /// <summary>
        /// Gestion Des Recettes et depenses
        /// </summary>
        public TreasuryManager Treasury = new TreasuryManager();


        /// <summary>
        /// Gestion Des Ressources Financiere
        /// </summary>
        public StudentsFinanceManager StudentsFinance = new StudentsFinanceManager();


        /// <summary>
        /// Gestion Des Salaire
        /// </summary>
        public PayrollManager PayRoll = new PayrollManager();


    }
}
