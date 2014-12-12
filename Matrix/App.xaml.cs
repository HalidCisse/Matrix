using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using DataService;
using Matrix.Utils;

//using Manager;

namespace Matrix
{
    
    /// <summary>
    /// 
    /// </summary>
    public partial class App 
    {
        
        /// <summary>
        /// l'utilisateur Actuelle
        /// </summary>
        static public string CurrentUser;

        /// <summary>
        /// Annee Scolaire Actuelle
        /// </summary>
        static public string CurrentAnneeScolaire;

        /// <summary>
        /// Serveur de Donnees
        /// </summary>
        public static DbService DataS { get; private set; }

        /// <summary>
        /// Serveur de Model
        /// </summary>
        public static ModelService ModelS { get; private set; }

        /// <summary>
        /// Verifier Q'une Seule Instance est Lancee
        /// </summary>
        readonly SingletonApplication.SingletonApplicationEnforcer _enforcer = new SingletonApplication.SingletonApplicationEnforcer(DisplayArgs);

        App()
        {

            if (_enforcer.ShouldApplicationExit())
            {
                //MessageBox.Show("Unable to start application. An instance of this application is already running.");
                Shutdown();
            }



            try
            {
                new Thread(() => DataS = new DbService()) { Name = "DataThread", Priority = ThreadPriority.Highest }.Start();
                new Thread(() => ModelS = new ModelService()) { Name = "DataModelThread", Priority = ThreadPriority.Highest }.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Current.Shutdown();
            }

            

            
        }

        


        #region StartUp Events

        /// <summary>
        /// OnStartup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup ( StartupEventArgs e )
        {             
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;            
        }

        static void App_DispatcherUnhandledException ( object sender, DispatcherUnhandledExceptionEventArgs e )
        {
            MessageBox.Show (e.Exception.Message,"Il ya Un Problem le Programme Doit se fermer !!");
            e.Handled = true;
            //Current.Shutdown();
        }

        #endregion



        #region ISingleInstanceApp Members

        public static void DisplayArgs(IEnumerable<string> args)
        {
            var dispatcher = Current.Dispatcher;
            if (dispatcher.CheckAccess())
            {
                ShowArgs();
            }
            else
            {
                dispatcher.BeginInvoke(
                    new Action(ShowArgs));
            }
        }

        private static void ShowArgs()
        {
            // Bring window to foreground
            var mainWindow = Current.MainWindow as MainWindow;
            if (mainWindow.WindowState == WindowState.Minimized)
            {
                mainWindow.WindowState = WindowState.Normal;
            }

            mainWindow.Activate();
        }

        #endregion


    }
}




