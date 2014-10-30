using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Context;
using DataService.Entities;
using DataService.Model;
using DataService.ViewModel;
using System;

namespace DataService
{
    public class ModelService
    {
        private readonly DbService DS = new DbService ();

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

        public List<FiliereLevelCard> GetFiliereMatieresCards ( string FiliereID )
        {
            var MatiereCardList = new List<FiliereLevelCard> ();

            foreach(int Level in DS.GetFILIERE_NIVEAUX (FiliereID))
            {
                MatiereCardList.Add (new FiliereLevelCard (FiliereID, Level));
            }
            return MatiereCardList;
        }
       
        public List<DepStaffCard> GetDepStaffsCard ( )
        {
            var DepStaffCardList = new List<DepStaffCard> { new DepStaffCard ("") };

            Parallel.ForEach (DS.GetDEPARTEMENTS (), Dep =>
            {
                DepStaffCardList.Add (new DepStaffCard (Dep));
            });

            return DepStaffCardList;
        }

        public List<FiliereClassCard> GetFiliereClassCards ( )
        {
            var ClassCardList = new List<FiliereClassCard> ();

            foreach(var Fil in DS.GetAllFilieres ())
            {
                ClassCardList.Add (new FiliereClassCard (Fil));
            }
            return ClassCardList;
        }
        
        public List<MatiereCard> GetClassMatieresCards ( Guid ClassID )
        {
            var FiliereID = DS.GetClasseByID (ClassID).FILIERE_ID;
            var FiliereYear = DS.GetClasseByID (ClassID).LEVEL;

            var MATIERES_LIST = new List<MatiereCard> ();

            using(var Db = new EF ())
            {
                var MatieresIDs = Db.FILIERE_MATIERE.Where (F => F.FILIERE_ID == FiliereID && F.FILIERE_LEVEL == FiliereYear).Select (F => F.MATIERE_ID).ToList ();

                foreach(var M in MatieresIDs.Select (M => Db.MATIERE.Find (M)))
                {
                    MATIERES_LIST.Add (new MatiereCard (FiliereID, FiliereYear, M));
                }
                return MATIERES_LIST;
            }
        }


        public List<ClassCard> GetFiliereClassCards(string FiliereID)
        {
            //return new FiliereClassCard(DS.GetFiliereByID(FiliereID));

            var Class_List = new List<ClassCard>();

            using (var Db = new EF())
            {               
                foreach (var C in Db.CLASSE.Where(C => C.FILIERE_ID == FiliereID))
                {
                    Class_List.Add(new ClassCard(C));
                }
                return Class_List;
            }
        }


        public List<Staff> GetClassStaffCards(string filiereId, int level)
        {
            throw new System.NotImplementedException();
        }

        public List<Student> GetClassStudentCards(string filiereId, int level)
        {

            throw new System.NotImplementedException ();
        }
    }

    
}
