using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace WinMemoryCleaner
{
    /// <summary>
    /// View Model
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool _isLoading;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ViewModel"/> is isloading.
        /// </summary>
        /// <value>
        ///   <c>true</c> if isloading; otherwise, <c>false</c>.
        /// </value>
        public bool Isloading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("Isloading");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Show/Hide Loading
        /// </summary>
        /// <param name="on">True (ON) / False (OFF)</param>
        protected void Loading(bool on)
        {
            try
            {
                // Multithreading trick
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Isloading = on;
                    Mouse.OverrideCursor = on ? Cursors.Wait : null;
                }));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICommand

        /// <summary>
        /// Relay Command
        /// </summary>
        /// <seealso cref="System.Windows.Input.ICommand" />
        internal class RelayCommand : ICommand
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RelayCommand"/> class.
            /// </summary>
            /// <param name="action">The action.</param>
            public RelayCommand(Action action)
            {
                _action = action;
            }

            /// <summary>
            /// Defines the method to be called when the command is invoked.
            /// </summary>
            /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
            public void Execute(object parameter)
            {
                _action();
            }

            #endregion

            #region Fields

            private readonly Action _action;

            #endregion

            #region Events

#pragma warning disable 67

            /// <summary>
            /// Occurs when changes occur that affect whether or not the command should execute.
            /// </summary>
            public event EventHandler CanExecuteChanged;

#pragma warning restore 67

            #endregion

            #region Methods

            /// <summary>
            /// Defines the method that determines whether the command can execute in its current state.
            /// </summary>
            /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
            /// <returns>
            /// true if this command can be executed; otherwise, false.
            /// </returns>
            public bool CanExecute(object parameter)
            {
                return true;
            }

            #endregion
        }

        #endregion
    }
}