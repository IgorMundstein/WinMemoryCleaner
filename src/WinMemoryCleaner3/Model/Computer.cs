using CommunityToolkit.Mvvm.ComponentModel;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Computer
    /// </summary>
    public partial class Computer : ObservableObject
    {
        private Memory memory;
        private OperatingSystem operatingSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="Computer" /> class.
        /// </summary>
        public Computer()
        {

        }

        /// <summary>
        /// Memory
        /// </summary>
        public Memory Memory
        {
            get => memory;
            set{ SetProperty(ref memory, value);}
            
        }

        /// <summary>
        /// Operating System
        /// </summary>
        public OperatingSystem OperatingSystem
        {
            get => operatingSystem;
            set => SetProperty(ref operatingSystem, value);
        }

    }
}
