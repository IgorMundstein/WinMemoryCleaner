using System;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Observable Item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ObservableObject" />
    public class ObservableItem<T> : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItem{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="getter">The getter.</param>
        /// <param name="setter">The setter.</param>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        public ObservableItem(string name, Func<T> getter, Action<T> setter, bool isEnabled = true)
        {
            Getter = getter;
            IsEnabled = isEnabled;
            Name = name;
            Setter = setter;
        }

        /// <summary>
        /// Gets the getter.
        /// </summary>
        /// <value>
        /// The getter.
        /// </value>
        public Func<T> Getter { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the setter.
        /// </summary>
        /// <value>
        /// The setter.
        /// </value>
        public Action<T> Setter { get; private set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value
        {
            get { return Getter != null ? Getter() : default(T); }
            set
            {
                if (Setter != null)
                {
                    Setter(value);
                    RaisePropertyChanged();
                }
            }
        }
    }
}
