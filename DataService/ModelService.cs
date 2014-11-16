using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Model;
using DataService.ViewModel;

namespace DataService
{
    public class ModelService
    {
        //private readonly DbService DS = new DbService ();

        public List<FiliereCard> GetAllFilieresCards ( )
        {
            using(var Db = new EF ())
            {
                var FL = new List<FiliereCard> ();

                Parallel.ForEach (Db.FILIERE, F =>
                {
                    FL.Add (new FiliereCard (F));
                });
                return FL;
            }
        }

        public List<FiliereLevelCard> GetFiliereMatieresCards ( Guid FiliereID )
        {
            
            var MatiereCardList = new List<FiliereLevelCard> ();
            var Ds = new DbService();
           
            foreach(int Level in Ds.GetFILIERE_NIVEAUX (FiliereID))
            {
                MatiereCardList.Add (new FiliereLevelCard (FiliereID, Level));
            }
            return MatiereCardList;                       
        }
       
        public List<DepStaffCard> GetDepStaffsCard ( )
        {
            var DepStaffCardList = new List<DepStaffCard> { new DepStaffCard ("") };
            var Ds = new DbService ();

            Parallel.ForEach (Ds.GetDEPARTEMENTS (), Dep =>
            {
                DepStaffCardList.Add (new DepStaffCard (Dep));
            });

            return DepStaffCardList;
        }

        public List<FiliereClassCard> GetFiliereClassCards ( )
        {
            var ClassCardList = new List<FiliereClassCard> ();
            var Ds = new DbService ();

            Parallel.ForEach(Ds.GetAllFilieres(), Fil =>
            {
                ClassCardList.Add(new FiliereClassCard(Fil));
            });

            //foreach(var Fil in Ds.GetAllFilieres ())
            //{
            //    ClassCardList.Add (new FiliereClassCard (Fil));
            //}
            return ClassCardList;
        }
        
        public List<MatiereCard> GetClassMatieresCards ( Classe MyClasse )
        {            
            var MATIERES_LIST = new List<MatiereCard> ();

            using(var Db = new EF ())
            {                
                foreach(var M in Db.MATIERE.Where (M => M.FILIERE_ID == MyClasse.FILIERE_ID && M.FILIERE_LEVEL == MyClasse.LEVEL))
                {
                    MATIERES_LIST.Add (new MatiereCard (M));
                }
                return MATIERES_LIST;
            }
        }

        public List<ClassCard> GetFiliereClassCards ( Guid FiliereID )
        {                        
            using (var Db = new EF())
            {
                var Class_List = new List<ClassCard> ();

                Parallel.ForEach(Db.CLASSE.Where(C => C.FILIERE_ID == FiliereID), C =>
                    {
                        Class_List.Add (new ClassCard (C));
                    });

                //foreach (var C in Db.CLASSE.Where(C => C.FILIERE_ID == FiliereID))
                //{
                //    Class_List.Add(new ClassCard(C));
                //}
                return Class_List;
            }
        }












        
    }

    
}
