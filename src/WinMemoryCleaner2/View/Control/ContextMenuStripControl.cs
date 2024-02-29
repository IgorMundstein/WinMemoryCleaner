using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Application;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Context Menu Strip Control
    /// </summary>
    /// <seealso cref="ContextMenuStrip" />
    public sealed class ContextMenuStripControl : ContextMenuStrip
    {
        private static readonly Color _darkBackground = ((SolidColorBrush)Application.Current.FindResource("DarkBackground")).ToColor();
        private static readonly Color _darkBorderBrush = ((SolidColorBrush)Application.Current.FindResource("DarkBorderBrush")).ToColor();
        private static readonly Color _darkForeground = ((SolidColorBrush)Application.Current.FindResource("DarkForeground")).ToColor();
        private static readonly Color _darkOver = ((SolidColorBrush)Application.Current.FindResource("DarkOver")).ToColor();

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextMenuStripControl" /> class.
        /// </summary>
        public ContextMenuStripControl()
        {
            BackColor = _darkBackground;
            ForeColor = _darkForeground;
            Renderer = new ToolStripRenderer();
            ShowCheckMargin = false;
            ShowImageMargin = false;

            // Rounded border
            var windowCornerPreference = Constants.Windows.DesktopWindowManager.Value.WindowCornerPreferenceRound;
            NativeMethods.DwmSetWindowAttribute(Handle, Constants.Windows.DesktopWindowManager.Attribute.WindowCornerPreference, ref windowCornerPreference, sizeof(int));

            // Border color
            var borderColor = ColorTranslator.ToWin32(_darkBorderBrush);
            NativeMethods.DwmSetWindowAttribute(Handle, Constants.Windows.DesktopWindowManager.Attribute.BorderColor, ref borderColor, sizeof(int));
        }

        /// <summary>
        /// Tool Strip Renderer
        /// </summary>
        /// <seealso cref="ToolStripProfessionalRenderer" />
        public class ToolStripRenderer : ToolStripProfessionalRenderer
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ToolStripRenderer" /> class.
            /// </summary>
            public ToolStripRenderer()
                : base(new ToolStripColorTable())
            {
                RoundedEdges = true;
            }

            /// <summary>
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                base.OnRenderItemText(e);

                e.Item.ForeColor = e.Item.Selected ? _darkOver : _darkForeground;
            }

            /// <summary>
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
            }

            /// <summary>
            /// </summary>
            /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data.</param>
            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                var toolStripSeparator = e.Item as ToolStripSeparator;

                if (toolStripSeparator == null)
                {
                    base.OnRenderSeparator(e);
                    return;
                }

                using (var pen = new Pen(_darkBorderBrush))
                {
                    e.Graphics.DrawLine(pen, 0, toolStripSeparator.Height / 2, toolStripSeparator.Width, toolStripSeparator.Height / 2);
                }
            }

            /// <summary>
            /// ToolStrip Color Table
            /// </summary>
            /// <seealso cref="ProfessionalColorTable" />
            public class ToolStripColorTable : ProfessionalColorTable
            {
                /// <summary>
                /// Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip" />.
                /// </summary>
                public override Color MenuBorder
                {
                    get { return _darkBorderBrush; }
                }
            }
        }
    }
}