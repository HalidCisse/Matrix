using System.Windows;
using DataService.Entities;

namespace Matrix.views
{
    /// <summary>
    /// Interaction logic for AddClass.xaml
    /// </summary>
    public partial class AddClass 
    {
        private string OpenOption;

        public AddClass (Classe matiereToDisplay = null)
        {
            InitializeComponent ();
         
            FILIERE_.ItemsSource = App.Db.GetAllFilieresNames ();

            


        }

        private void Enregistrer_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void Annuler_Click ( object sender, RoutedEventArgs e )
        {

        }

        private void FILIERE__SelectionChanged ( object sender, System.Windows.Controls.SelectionChangedEventArgs e )
        {
            NIVEAU_.ItemsSource = App.Db.GetFILIERE_LEVELS ();
        }





    }
}
