using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Matrix.views.Pedagogy
{
    /// <summary>
    /// Interaction logic for AddAnneeScolaire.xaml
    /// </summary>
    public partial class AddAnneeScolaire
    {

        private ObservableCollection<PeriodeScolaire> PeriodeList = new ObservableCollection<PeriodeScolaire>();

        public AddAnneeScolaire()
        {
            InitializeComponent();
          
            DisplayDefault();
        }

        private void DisplayDefault()
        {
            ANNEESCOLAIRE_NAME_.Text = "Annee Scolaire " + DateTime.Today.Year + "-" + (DateTime.Today.Year + 1);
            DEBUT_ANS_.SelectedDate = DateTime.Today;
            FIN_ANS_.SelectedDate = DateTime.Today.AddMonths(9);
            DEBUT_INS_.SelectedDate = DateTime.Today;
            FIN_INS_.SelectedDate = DateTime.Today.AddMonths(3);
            PERIODE_GRID_.ItemsSource = PeriodeList;
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (ChampsValidated() != true) return;

            var NewAnneeScolaire = new AnneeScolaire
            {
                NAME = ANNEESCOLAIRE_NAME_.Text.Trim(),
                DATE_DEBUT = DEBUT_ANS_.SelectedDate,
                DATE_FIN = FIN_ANS_.SelectedDate,
                DATE_DEBUT_INSCRIPTION = DEBUT_INS_.SelectedDate,
                DATE_FIN_INSCRIPTION = FIN_INS_.SelectedDate
            };

            try
            {
                SavePeriodesScolaire(NewAnneeScolaire.ANNEE_SCOLAIRE_ID);

                App.DataS.AddAnneeScolaire(NewAnneeScolaire);
                ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Matrix", MessageBoxButton.OK);
            }
            Close();                        
        }

        private void SavePeriodesScolaire(Guid anneeScolaireID)
        {
            foreach (var PS in PeriodeList)
            {
                PS.ANNEE_SCOLAIRE_ID = anneeScolaireID;
                App.DataS.AddPeriodeScolaire(PS);
            }

            //foreach (var RV in PERIODE_GRID_.Items)
            //{
            //    //RV(1) = "Hi";
            //}
                   
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ChampsValidated()
        {
            var Ok = true;

            if (string.IsNullOrEmpty(ANNEESCOLAIRE_NAME_.Text))
            {
                ANNEESCOLAIRE_NAME_.BorderBrush = Brushes.Red;
                Ok = false;
            }
            else
            {
                ANNEESCOLAIRE_NAME_.BorderBrush = Brushes.Blue;
            }

            if (!Ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return Ok;
        }

        private void N_PERIODES__ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var NumPeriod = sender as IntegerUpDown;

            if (PERIODE_GRID_ != null)PERIODE_GRID_.ItemsSource = null;
            PeriodeList.Clear();

            for (var i = 1; i <= NumPeriod?.Value; i++)
            {
                PeriodeList.Add(new PeriodeScolaire
                {
                    PERIODE_SCOLAIRE_ID = Guid.NewGuid(),
                    NAME = "Periode " + i,
                    END_DATE = DateTime.Today
                                    
                });
            }

            if (PERIODE_GRID_ != null) PERIODE_GRID_.ItemsSource = PeriodeList;
        }
    }
}
