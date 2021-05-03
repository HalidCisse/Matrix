using System;
using System.Collections.Generic;
using CLib;
using Common.Economat.Entity;
using Common.Economat.Enums;
using Common.Pedagogy.Entity;
using DataService.DataManager;

namespace DataService.Helpers
{
    internal static class SchoolFeeHelper
    {

        /// <summary>
        /// Algorithme pour générer les frais de scolarité d une inscription
        /// </summary>
        /// <param name ="myEnrollement">Inscription</param>
        /// <returns>La list des factures</returns>
        internal static HashSet<StudentBill> GenerateInscriptionReceipts(Enrollement myEnrollement) {    
                    
            var theAnneeScolaire              = PedagogyManager.StaticGetAnneeScolaireByGuid
                                                             (myEnrollement.SchoolYearGuid);
            var theClasse                     = ClassesManager.StaticGetClasseById(myEnrollement.ClasseGuid);
            var debutAnnee                    = theAnneeScolaire.DateDebut.GetValueOrDefault();
            var finAnnee                      = theAnneeScolaire.DateFin.GetValueOrDefault();

            var yearTotalMonths               = DateTimeHelper.MonthDifference(debutAnnee, finAnnee);
            var periodLenght                  = (int) myEnrollement.InstallmentRecurrence;
            var totalInstalls                 = yearTotalMonths / periodLenght;
            if ((yearTotalMonths % periodLenght) != 0) totalInstalls++;
            var nextPayOff                    = debutAnnee.AddSeconds(1);

            double installAmount = 0;
            switch (myEnrollement.InstallmentRecurrence)
            {
            case InstallmentRecurrence.Monthly:
                installAmount   = myEnrollement.IsScholar ? myEnrollement.InstallementAmount : 
                                                                      theClasse.MonthlyAmount;
                break;
            case InstallmentRecurrence.Quarterly:
                installAmount   = myEnrollement.IsScholar ? myEnrollement.InstallementAmount : 
                                                                    theClasse.QuarterlyAmount;
                break;
            case InstallmentRecurrence.HalfYearly:
                installAmount   = myEnrollement.IsScholar ? myEnrollement.InstallementAmount : 
                                                                   theClasse.HalfYearlyAmount;
                break;
            case InstallmentRecurrence.Yearly:
                installAmount   = myEnrollement.IsScholar ? myEnrollement.InstallementAmount : 
                                                                       theClasse.YearlyAmount;
                break;
            }

            var billsList = new HashSet<StudentBill>{
                new StudentBill{
                    SchoolFeeGuid = Guid.NewGuid(),
                    StudentGuid = myEnrollement.StudentGuid,
                    Designation = "Inscription " + theAnneeScolaire.Name,
                    NetAmount = myEnrollement.IsScholar ? myEnrollement.InscriptionAmount : 
                                                               theClasse.InscriptionAmount,
                    DueDate = debutAnnee
                }
            };

            if(myEnrollement.InstallmentRecurrence==InstallmentRecurrence.Once)
                return new HashSet<StudentBill>
                {new StudentBill{
                    SchoolFeeGuid = Guid.NewGuid(),
                    StudentGuid = myEnrollement.StudentGuid,
                    Designation = "Frais Etudes",
                    DueDate = debutAnnee,
                    NetAmount = theClasse.YearlyAmount
                }};

            for (var i = 0; i < totalInstalls; i++){
                billsList.Add(new StudentBill{
                    SchoolFeeGuid             = Guid.NewGuid(),
                    StudentGuid               = myEnrollement.StudentGuid,
                    Designation               = nextPayOff.ToString("MMM-yy") + " Installement " 
                                                + myEnrollement.InstallmentRecurrence.GetEnumDescription(),
                    NetAmount                 = installAmount,
                    DueDate                   = nextPayOff,
                    IsInstallement            = true
                });
                nextPayOff                    = new DateTime(nextPayOff.AddMonths(periodLenght).Year, 
                                                nextPayOff.AddMonths(periodLenght).Month, 1);
            }
            return billsList;
        }
    }
}

