using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using DataService.Entities;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    /// <summary>
    /// Interaction logic for AddAnneeScolaire.xaml
    /// </summary>
    public partial class AddAnneeScolaire
    {
        private List<PeriodeScolaire> PeriodeList = new List<PeriodeScolaire>();
        private bool isFirstTime = true;

        /// <summary>
        /// Represente Une Annee Scolaire
        /// </summary>
        public AddAnneeScolaire()
        {
            InitializeComponent();
          
            DisplayDefault();
        }

        private void DisplayDefault()
        {
            ANNEESCOLAIRE_NAME_.Text = "Annee Scolaire " + DateTime.Today.Year + "-" + (DateTime.Today.Year + 1);
            DEBUT_ANS_.SelectedDate = new DateTime(DateTime.Today.Year, 10, 1);                      
            FIN_ANS_.SelectedDate = new DateTime(DateTime.Today.Year, 10, 1).AddMonths(9);           
            DEBUT_INS_.SelectedDate = DateTime.Today;
            FIN_INS_.SelectedDate = DateTime.Today.AddMonths(3);
            PERIODE_LIST_.ItemsSource = PeriodeList;

            Genperiods();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {       
            if (ChampsValidated() != true) return;

            var NewAnneeScolaire = new AnneeScolaire
            {
                ANNEE_SCOLAIRE_ID = Guid.NewGuid(),
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
                ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
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


            //todo: Validation Annee Scolaire Superposition

            if (!Ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return Ok;
        }

        private void Genperiods()
        {
            PeriodeList.Clear();

            int periodeLengh = (int)((FIN_ANS_.SelectedDate - DEBUT_ANS_.SelectedDate).Value.TotalDays / N_PERIODES_.Value);
            DateTime lastStartDate = DateTime.Today;
            DateTime lastEndDate = (DateTime)DEBUT_ANS_.SelectedDate;
            var periodType = "Periode ";

            if (N_PERIODES_.Value == 2)
            {
                periodType = "Semestre ";
            }
            else if (N_PERIODES_.Value == 3)
            {
                periodType = "Trimestre ";
            }
                       
            for (var i = 1; i <= N_PERIODES_.Value; i++)
            {
                var newPeriodeSco = new PeriodeScolaire();

                newPeriodeSco.PERIODE_SCOLAIRE_ID = Guid.NewGuid();
                newPeriodeSco.NAME = periodType + i;
                newPeriodeSco.START_DATE = lastEndDate.AddDays(1);
                newPeriodeSco.END_DATE = lastEndDate.AddDays(periodeLengh);
                                
                if (i == 1)
                {
                    newPeriodeSco.START_DATE = DEBUT_ANS_.SelectedDate;
                }
                else if (i == N_PERIODES_.Value)
                {
                    newPeriodeSco.END_DATE = FIN_ANS_.SelectedDate;
                }

                PeriodeList.Add(newPeriodeSco);

                lastStartDate = newPeriodeSco.START_DATE.Value;
                lastEndDate = newPeriodeSco.END_DATE.Value;
            }

            PERIODE_LIST_.ItemsSource = null;
            PERIODE_LIST_.ItemsSource = PeriodeList;
        }
    
        private void N_PERIODES__ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (isFirstTime){ isFirstTime = false;  return; }
            Genperiods();       
        }
    }
}
