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
        private List<PeriodeScolaire> _periodeList = new List<PeriodeScolaire>();
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
            AnneescolaireName.Text = "Annee Scolaire " + DateTime.Today.Year + "-" + (DateTime.Today.Year + 1);
            DebutAns.SelectedDate = new DateTime(DateTime.Today.Year, 10, 1);                      
            FinAns.SelectedDate = new DateTime(DateTime.Today.Year, 10, 1).AddMonths(9);           
            DebutIns.SelectedDate = DateTime.Today;
            FinIns.SelectedDate = DateTime.Today.AddMonths(3);
            PERIODE_LIST_.ItemsSource = _periodeList;

            Genperiods();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {       
            if (ChampsValidated() != true) return;

            var newAnneeScolaire = new AnneeScolaire
            {
                AnneeScolaireId = Guid.NewGuid(),
                Name = AnneescolaireName.Text.Trim(),
                DateDebut = DebutAns.SelectedDate,
                DateFin = FinAns.SelectedDate,
                DateDebutInscription = DebutIns.SelectedDate,
                DateFinInscription = FinIns.SelectedDate
            };

            try
            {
                SavePeriodesScolaire(newAnneeScolaire.AnneeScolaireId);

                App.DataS.Pedagogy.AddAnneeScolaire(newAnneeScolaire);
                ModernDialog.ShowMessage("Success", "Matrix", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "ERREUR", MessageBoxButton.OK);
            }
            Close();                        
        }
       
        private void SavePeriodesScolaire(Guid anneeScolaireId)
        {           
            foreach (var ps in _periodeList)
            {
                ps.AnneeScolaireId = anneeScolaireId;
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

            if (string.IsNullOrEmpty(AnneescolaireName.Text))
            {
                AnneescolaireName.BorderBrush = Brushes.Red;
                ok = false;
            }
            else
            {
                AnneescolaireName.BorderBrush = Brushes.Blue;
            }


            //todo: Validation Annee Scolaire Superposition

            if (!ok) ModernDialog.ShowMessage("Verifier Les Informations !", "Matrix", MessageBoxButton.OK);

            return ok;
        }

        private void Genperiods()
        {
            _periodeList.Clear();

            int periodeLengh = (int)((FinAns.SelectedDate - DebutAns.SelectedDate).Value.TotalDays / NPeriodes.Value);
            DateTime lastStartDate = DateTime.Today;
            DateTime lastEndDate = (DateTime)DebutAns.SelectedDate;
            var periodType = "Periode ";

            if (NPeriodes.Value == 2)
            {
                periodType = "Semestre ";
            }
            else if (NPeriodes.Value == 3)
            {
                periodType = "Trimestre ";
            }
                       
            for (var i = 1; i <= NPeriodes.Value; i++)
            {
                var newPeriodeSco = new PeriodeScolaire();

                newPeriodeSco.PeriodeScolaireId = Guid.NewGuid();
                newPeriodeSco.Name = periodType + i;
                newPeriodeSco.StartDate = lastEndDate.AddDays(1);
                newPeriodeSco.EndDate = lastEndDate.AddDays(periodeLengh);
                                
                if (i == 1)
                {
                    newPeriodeSco.StartDate = DebutAns.SelectedDate;
                }
                else if (i == NPeriodes.Value)
                {
                    newPeriodeSco.EndDate = FinAns.SelectedDate;
                }

                _periodeList.Add(newPeriodeSco);

                lastStartDate = newPeriodeSco.StartDate.Value;
                lastEndDate = newPeriodeSco.EndDate.Value;
            }

            PERIODE_LIST_.ItemsSource = null;
            PERIODE_LIST_.ItemsSource = _periodeList;
        }
    
        private void N_PERIODES__ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_isFirstTime){ _isFirstTime = false;  return; }
            Genperiods();       
        }
    }
}
