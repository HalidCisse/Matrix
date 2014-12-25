using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using DataService.Entities.Pedagogy;
using FirstFloor.ModernUI.Windows.Controls;

namespace Matrix.views.Pedagogy
{
    /// <summary>
    /// Interaction logic for AddAnneeScolaire.xaml
    /// </summary>
    public partial class AddAnneeScolaire
    {
        private readonly List<PeriodeScolaire> _periodeList = new List<PeriodeScolaire>();
        private bool _isFirstTime = true;

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
            ANNEESCOLAIRE_NAME.Text = "Annee Scolaire " + DateTime.Today.Year + "-" + (DateTime.Today.Year + 1);
            DEBUT_ANS.SelectedDate = new DateTime(DateTime.Today.Year, 10, 1);                      
            FIN_ANS.SelectedDate = new DateTime(DateTime.Today.Year, 10, 1).AddMonths(9);
            DEBUT_INS.SelectedDate = DEBUT_ANS.SelectedDate;
            FIN_INS.SelectedDate = DEBUT_INS.SelectedDate.Value.AddMonths(3); 
            PERIODE_LIST.ItemsSource = _periodeList;

            Genperiods();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {       
            if (ChampsValidated() != true) return;

            var newAnneeScolaire = new AnneeScolaire
            {
                AnneeScolaireGuid = Guid.NewGuid(),
                Name = ANNEESCOLAIRE_NAME.Text.Trim(),
                DateDebut = DEBUT_ANS.SelectedDate,
                DateFin = FIN_ANS.SelectedDate,
                DateDebutInscription = DEBUT_INS.SelectedDate,
                DateFinInscription = FIN_INS.SelectedDate
            };

            try
            {
                SavePeriodesScolaire(newAnneeScolaire.AnneeScolaireGuid);

                App.DataS.Pedagogy.AddAnneeScolaire(newAnneeScolaire);
                Close();
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
                Close();
            }
            ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
            Close();                        
        }
       
        private void SavePeriodesScolaire(Guid anneeScolaireId)
        {          
            foreach (var ps in _periodeList)
            {
                ps.AnneeScolaireGuid = anneeScolaireId;
                App.DataS.Pedagogy.AddPeriodeScolaire(ps);
            }                  
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ChampsValidated()
        {
            var ok = true;

            if (string.IsNullOrEmpty(ANNEESCOLAIRE_NAME.Text))
            {
                ANNEESCOLAIRE_NAME.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                ANNEESCOLAIRE_NAME.BorderBrush = Brushes.Blue;
            }


            //todo: Validation Annee Scolaire Superposition

            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
        }

        private void Genperiods()
        {
            _periodeList.Clear();

            var periodeLengh = (int)((FIN_ANS.SelectedDate - DEBUT_ANS.SelectedDate).Value.TotalDays / N_PERIODES.Value);
            
            var lastEndDate = DEBUT_ANS.SelectedDate.GetValueOrDefault();
            var periodType = "Periode ";

            if (N_PERIODES.Value == 2) periodType = "Semestre ";
           
            else if (N_PERIODES.Value == 3) periodType = "Trimestre ";
                                   
            for (var i = 1; i <= N_PERIODES.Value; i++)
            {
                var newPeriodeSco = new PeriodeScolaire
                {
                    PeriodeScolaireGuid = Guid.NewGuid(),
                    Name = periodType + i,
                    StartDate = lastEndDate.AddDays(1),
                    EndDate = lastEndDate.AddDays(periodeLengh)
                };

                if (i == 1) newPeriodeSco.StartDate = DEBUT_ANS.SelectedDate;
               
                else if (i == N_PERIODES.Value) newPeriodeSco.EndDate = FIN_ANS.SelectedDate;
               
                _periodeList.Add(newPeriodeSco);
          
                lastEndDate = newPeriodeSco.EndDate.GetValueOrDefault();
            }

            PERIODE_LIST.ItemsSource = null;
            PERIODE_LIST.ItemsSource = _periodeList;
        }
    
        private void N_PERIODES__ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_isFirstTime){ _isFirstTime = false;  return; }
            Genperiods();       
        }

    }
}
