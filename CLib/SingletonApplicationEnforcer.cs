using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace CLib
{
    /// <summary>
    /// 
    /// </summary>
    public class SingletonApplication
    {

        /// <summary>
        /// This class allows restricting the number of executables in execution, to one.
        /// </summary>
        public sealed class SingletonApplicationEnforcer
        {
            readonly Action<IEnumerable<string>> _processArgsFunc;
            readonly string _applicationId;
            Thread _thread;

            /// <summary>
            /// Gets or sets the string that is used to join 
            /// the string array of arguments in memory.
            /// </summary>
            /// <value>The arg delimeter.</value>
            public string ArgDelimeter { get; set; } = "_;;_";

            /// <summary>
            /// Initializes a new instance of the <see cref="SingletonApplicationEnforcer"/> class.
            /// </summary>
            /// <param name="processArgsFunc">A handler for processing command line args 
            /// when they are received from another application instance.</param>
            /// <param name="applicationId">The application id used 
            /// for naming the <seealso cref="EventWaitHandle"/>.</param>
            public SingletonApplicationEnforcer(Action<IEnumerable<string>> processArgsFunc, string applicationId = "DisciplesRock")
            {
                if (processArgsFunc == null)
                {
                    throw new ArgumentNullException("processArgsFunc");
                }
                _processArgsFunc = processArgsFunc;
                _applicationId = applicationId;
            }

            /// <summary>
            /// Determines if this application instance is not the singleton instance.
            /// If this application is not the singleton, then it should exit.
            /// </summary>
            /// <returns><c>true</c> if the application should shutdown, 
            /// otherwise <c>false</c>.</returns>
            public bool ShouldApplicationExit()
            {
                bool createdNew;
                string argsWaitHandleName = "ArgsWaitHandle_" + _applicationId;
                string memoryFileName = "ArgFile_" + _applicationId;

                EventWaitHandle argsWaitHandle = new EventWaitHandle(
                    false, EventResetMode.AutoReset, argsWaitHandleName, out createdNew);

                GC.KeepAlive(argsWaitHandle);

                if (createdNew)
                {
                    /* This is the main, or singleton application. 
                     * A thread is created to service the MemoryMappedFile. 
                     * We repeatedly examine this file each time the argsWaitHandle 
                     * is Set by a non-singleton application instance. */
                    _thread = new Thread(() =>
                    {
                        try
                        {
                            using (MemoryMappedFile file = MemoryMappedFile.CreateOrOpen(memoryFileName, 10000))
                            {
                                while (true)
                                {
                                    argsWaitHandle.WaitOne();
                                    using (MemoryMappedViewStream stream = file.CreateViewStream())
                                    {
                                        var reader = new BinaryReader(stream);
                                        string args;
                                        try
                                        {
                                            args = reader.ReadString();
                                        }
                                        catch (Exception ex)
                                        {
                                            Debug.WriteLine("Unable to retrieve string. " + ex);
                                            continue;
                                        }
                                        string[] argsSplit = args.Split(new[] { ArgDelimeter },
                                                                        StringSplitOptions.RemoveEmptyEntries);
                                        _processArgsFunc(argsSplit);
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Unable to monitor memory file. " + ex);
                        }
                    });

                    _thread.IsBackground = true;
                    _thread.Start();
                }
                else
                {
                    /* Non singleton application instance. 
                     * Should exit, after passing command line args to singleton process, 
                     * via the MemoryMappedFile. */
                    using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting(memoryFileName))
                    {
                        using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                        {
                            var writer = new BinaryWriter(stream);
                            var args = Environment.GetCommandLineArgs();
                            var joined = string.Join(ArgDelimeter, args);
                            writer.Write(joined);
                        }
                    }
                    argsWaitHandle.Set();
                }

                return !createdNew;
            }
        }
    }
}
