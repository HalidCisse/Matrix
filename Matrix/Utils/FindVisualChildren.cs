using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Matrix.Utils
{
    class FindVisual
    {  
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
