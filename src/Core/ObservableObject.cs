using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WinMemoryCleaner
{
    /// <summary>
    /// A base class for objects of which the properties must be observable.
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    public abstract class ObservableObject : INotifyPropertyChanged
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
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                var handler = PropertyChanged;
                var args = new PropertyChangedEventArgs(propertyName);

                // Marshal to UI thread if necessary
                if (Application.Current != null && Application.Current.Dispatcher != null && !Application.Current.Dispatcher.CheckAccess())
                {
                    Application.Current.Dispatcher.Invoke(handler, this, args);
                }
                else
                {
                    handler.Invoke(this, args);
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <typeparam name="T">The type of the property that
        /// changed.</typeparam>
        /// <param name="expression">An expression identifying the property
        /// that changed.</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            var handler = PropertyChanged;

            if (handler != null && expression != null)
            {
                var body = expression.Body as MemberExpression;

                if (body != null)
                {
                    var property = body.Member as PropertyInfo;

                    if (property != null)
                    {
                        var propertyName = property.Name;

                        if (!string.IsNullOrEmpty(propertyName))
                            RaisePropertyChanged(propertyName);
                    }
                }
            }
        }

        #endregion
    }
}
