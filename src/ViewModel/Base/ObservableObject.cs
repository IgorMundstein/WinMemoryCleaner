using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WinMemoryCleaner
{
    /// <summary>
    /// A base class for objects of which the properties must be observable.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    internal abstract class ObservableObject : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="propertyExpression">An expression identifying the property
        /// that changed.</param>
        internal void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                MemberExpression body = propertyExpression.Body as MemberExpression;

                if (body != null)
                {
                    PropertyInfo property = body.Member as PropertyInfo;

                    if (property != null)
                    {
                        string propertyName = property.Name;

                        if (!string.IsNullOrEmpty(propertyName))
                            RaisePropertyChanged(propertyName);
                    }
                }
            }
        }

        #endregion
    }
}
