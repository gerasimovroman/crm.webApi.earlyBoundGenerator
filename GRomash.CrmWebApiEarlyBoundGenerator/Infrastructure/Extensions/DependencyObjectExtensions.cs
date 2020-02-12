using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GRomash.CrmWebApiEarlyBoundGenerator.Infrastructure.Extensions
{
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Gets the type of the visual parent of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startObject">The start object.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Parent of type {typeof(T)} could not be found</exception>
        public static DependencyObject GetVisualParentOfType<T>(this DependencyObject startObject)
        {
            var parent = startObject;

            while (IsNotNullAndNotOfType<T>(parent))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent is T ? parent : throw new Exception($"Parent of type {typeof(T)} could not be found");
        }

        /// <summary>
        /// Determines whether [is not null and not of type] [the specified object].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///   <c>true</c> if [is not null and not of type] [the specified object]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNotNullAndNotOfType<T>(this DependencyObject obj)
        {
            return obj != null && !(obj is T);
        }

        /// <summary>
        /// Finds the visual child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static T FindVisualChild<T>(this DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T dependencyObject)
                    return dependencyObject;
                else
                {
                    var childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
