
using System.Threading;
using System.Windows;
using System.Windows.Threading;
//using Manager;
using DataService;

namespace Matrix
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        App ( )
        {
            //Db = new Service ();
            //new Thread(() => Db = new Service()).Start();

            try
            {
                new Thread (( ) => Db = new Service ()) { Name = "DataThread", Priority = ThreadPriority.Highest }.Start ();

            }
            catch(System.Exception e)
            {
                
                MessageBox.Show(e.Message);
            }           
        }

       
        static public string _currentUser;

        public static Service Db { get;
            private set;
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
            //Db = new Service ();
        }

        static void App_DispatcherUnhandledException ( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show (e.Exception.Message);
            e.Handled = true;
        }


#endregion

        
    }
}
