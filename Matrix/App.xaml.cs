using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using DataService;
//using Manager;

namespace Matrix
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class App
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private const string guid = "E6F848DE-6391-4305-8D56-860C7D40F381";

        /// <summary>
        /// 
        /// </summary>
        static public string _currentUser;

        /// <summary>
        /// 
        /// </summary>
        public static DbService DataS { get; private set; }

        /// <summary>
        /// 
        /// </summary>
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
                Current.Shutdown();
            }           
        }
        

        #region StartUp Evenements

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup ( StartupEventArgs e )
        {             
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            #region Single Instance Enforcer

            Mutex mutex;
            if (!CreateMutex(out mutex))
            {
                MessageBox.Show("Another instance is running !!");
                Shutdown();
                return;
            }

            Environment.SetEnvironmentVariable(guid, null, EnvironmentVariableTarget.User);

            #endregion

        }

        static void App_DispatcherUnhandledException ( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show (e.Exception.Message);
            e.Handled = true;
            Current.Shutdown();
        }

        #endregion


        static bool CreateMutex(out Mutex mutex)
        {
            bool createdNew;
            mutex = new Mutex(false, guid, out createdNew);

            if (createdNew)
            {
                var process = Process.GetCurrentProcess();
                var value = process.Id.ToString();

                Environment.SetEnvironmentVariable(guid, value, EnvironmentVariableTarget.User);
            }
            else
            {
                var value = Environment.GetEnvironmentVariable(guid, EnvironmentVariableTarget.User);
                Process process = null;
                var processId = -1;

                if (int.TryParse(value, out processId))
                    process = Process.GetProcessById(processId);

                if (process == null || !SetForegroundWindow(process.MainWindowHandle))
                    MessageBox.Show("Unable to start application. An instance of this application is already running.");
            }

            return createdNew;
        }



    }
}




