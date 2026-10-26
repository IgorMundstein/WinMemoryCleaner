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
        private bool _isEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableItem{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="getter">The getter.</param>
        /// <param name="setter">The setter.</param>
        /// <param name="isEnabled">if set to <c>true</c> [is enabled].</param>
        /// <param name="toolTip">The tooltip.</param>
        public ObservableItem(string name, Func<T> getter, Action<T> setter, bool isEnabled = true, string toolTip = null)
        {
            Getter = getter;
            _isEnabled = isEnabled;
            Name = name;
            Setter = setter;
            ToolTip = toolTip;
        }

        /// <summary>
        /// Gets the getter.
        /// </summary>
        /// <value>
        /// The getter.
        /// </value>
        public Func<T> Getter { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

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
        /// Gets the tooltip.
        /// </summary>
        /// <value>
        /// The tooltip.
        /// </value>
        public string ToolTip { get; private set; }

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
