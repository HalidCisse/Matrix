using System;
using System.Reflection;

namespace CLib
{
    /// <summary>
    /// 
    /// </summary>
    public static class Res
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathInApplication"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetRes ( string pathInApplication, Assembly assembly = null )
        {
            if(assembly == null)
            {
                assembly = Assembly.GetCallingAssembly ();
            }

            if(pathInApplication[0] == '/')
            {
                pathInApplication = pathInApplication.Substring (1);
            }
            return(new Uri (@"pack://application:,,,/" + assembly.GetName ().Name + ";component/" + pathInApplication, UriKind.Absolute).ToString());
        }
    }
}
