using System.Drawing;
using System.Windows.Forms;

namespace WinMemoryCleaner
{
    /// <summary>
    /// Tray Icon Context Menu Control
    /// </summary>
    /// <seealso cref="ContextMenuStrip" />
    public sealed class TrayIconContextMenuControl : ContextMenuStrip
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrayIconContextMenuControl" /> class.
        /// </summary>
        public TrayIconContextMenuControl()
        {
            BackColor = ThemeManager.SecondaryBackgroundColor;
            ForeColor = ThemeManager.SecondaryForegroundColor;
            Renderer = new ToolStripRenderer();
            ShowCheckMargin = false;
            ShowImageMargin = false;

            // Rounded border
            var windowCornerPreference = Constants.Windows.DesktopWindowManager.Value.WindowCornerPreferenceRound;
            NativeMethods.DwmSetWindowAttribute(Handle, Constants.Windows.DesktopWindowManager.Attribute.WindowCornerPreference, ref windowCornerPreference, sizeof(int));

            // Border color
            var borderColor = ColorTranslator.ToWin32(ThemeManager.SecondaryBorderColor);
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

                e.Item.ForeColor = e.Item.Selected ? ThemeManager.AccentColor : ThemeManager.SecondaryForegroundColor;
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

                using (var pen = new Pen(ThemeManager.SecondaryBorderColor))
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
                    get { return ThemeManager.SecondaryBorderColor; }
                }
            }
        }
    }
}