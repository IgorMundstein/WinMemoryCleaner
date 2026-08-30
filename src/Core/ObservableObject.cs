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
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property. Resolved automatically via CallerMemberName.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event. Alias for <see cref="OnPropertyChanged(string)"/> kept for backwards compatibility.
        /// </summary>
        /// <param name="propertyName">Name of the property. Resolved automatically via CallerMemberName.</param>
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Raises the PropertyChanged event for the given property expression.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="expression">An expression identifying the property that changed.</param>
        public void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            if (expression?.Body is MemberExpression body && body.Member is PropertyInfo property)
            {
                var propertyName = property.Name;

                if (!string.IsNullOrEmpty(propertyName))
                    OnPropertyChanged(propertyName);
            }
        }

        #endregion
    }
}