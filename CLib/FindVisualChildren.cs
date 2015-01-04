using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CLib
{

    /// <summary>
    /// FindVisual
    /// </summary>
    public class FindVisual
    {  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depObj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T> ( DependencyObject depObj ) where T : DependencyObject
        {
            if(depObj == null) yield break;
            for(var i = 0; i < VisualTreeHelper.GetChildrenCount (depObj); i++)
            {
                var child = VisualTreeHelper.GetChild (depObj, i);
                var children = child as T;
                if(children != null)
                {
                    yield return children;
                }

                foreach(var childOfChild in FindVisualChildren<T> (child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
    
}
