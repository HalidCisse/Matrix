using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using DataService;
//using Manager;

namespace Matrix
{
    

     

    public partial class App
    {
        static public string _currentUser;
        public static DbService DataS { get; private set; }
        public static ModelService ModelS { get; private set; }

        App ( )
        {
            //Db = new Service ();
            //new Thread(() => Db = new Service()).Start();

            try
            {
                new Thread (( ) => DataS = new DbService ()) { Name = "DataThread", Priority = ThreadPriority.Highest }.Start ();
                new Thread (( ) => ModelS = new ModelService ()) { Name = "DataModelThread", Priority = ThreadPriority.Highest }.Start ();                
            }
            catch(Exception e)
            {
                MessageBox.Show (e.Message);                
            }           
        }

       
       


        /// <summary>
        /// Le business de l'application
        /// </summary>
        //static private Manager.Manager _Manager = new Manager.Manager ();

        //public static Manager.Manager Manager
        //{
        //    get { return _Manager; }
        //    set { _Manager = value; }
        //}



#region StartUp Evenements


        protected override void OnStartup ( StartupEventArgs e )
        {
            base.OnStartup (e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;            
        }

        static void App_DispatcherUnhandledException ( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show (e.Exception.Message);
            e.Handled = true;
        }


#endregion

        
    }
}
