using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using DataService;
using CLib;

namespace Matrix
{
    
    /// <summary>
    /// Demarage
    /// </summary>
    public partial class App 
    {

        App()
        {
            if (_enforcer.ShouldApplicationExit()) Shutdown();

            try
            {
                new Thread(() => DataS = new DbService()) { Priority = ThreadPriority.Highest }.Start();
                new Thread(() => ModelS = new ModelService()) { Priority = ThreadPriority.Highest }.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Current.Shutdown();
            }

            // todo : Login Form Here
        }



        #region DATA SERVICES
       
        /// <summary>
        /// Serveur de Donnees
        /// </summary>
        public static DbService DataS { get; private set; }

        /// <summary>
        /// Serveur de Model
        /// </summary>
        public static ModelService ModelS { get; private set; }


        #endregion



        #region START UP EVENTS

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
            MessageBox.Show (e.Exception.Message,"Not Handled Exception");
            e.Handled = true;
            //Current.Shutdown();
        }

        #endregion



        #region SINGLE INSTANCE MEMBERS

        /// <summary>
        /// Verifier Q'une Seule Instance est Lancee
        /// </summary>
        readonly SingletonApplication.SingletonApplicationEnforcer _enforcer = new SingletonApplication.SingletonApplicationEnforcer(DisplayArgs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void DisplayArgs(IEnumerable<string> args)
        {
            var dispatcher = Current.Dispatcher;

            if (dispatcher.CheckAccess()) ShowArgs();         
            else dispatcher.BeginInvoke(new Action(ShowArgs));
           
        }

        private static void ShowArgs()
        {            
            var mainWindow = Current.MainWindow as MainWindow;
            if (mainWindow?.WindowState == WindowState.Minimized)
            {
                mainWindow.WindowState = WindowState.Normal;
            }
            mainWindow?.Activate();            
        }

        #endregion


    }
}




