using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Application;
using SolidColorBrush = System.Windows.Media.SolidColorBrush;

namespace WinMemoryCleaner
{
    internal class ContextMenuStripControl : ContextMenuStrip
    {
        private static readonly Color _darkBackground = ((SolidColorBrush)Application.Current.FindResource("DarkBackground")).ToColor();
        private static readonly Color _darkBorderBrush = ((SolidColorBrush)Application.Current.FindResource("DarkBorderBrush")).ToColor();
        private static readonly Color _darkForeground = ((SolidColorBrush)Application.Current.FindResource("DarkForeground")).ToColor();
        private static readonly Color _darkOver = ((SolidColorBrush)Application.Current.FindResource("DarkOver")).ToColor();

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

        internal class ToolStripRenderer : ToolStripProfessionalRenderer
        {
            internal ToolStripRenderer()
                : base(new ToolStripColorTable())
            {
                RoundedEdges = true;
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                base.OnRenderItemText(e);

                e.Item.ForeColor = e.Item.Selected ? _darkOver : _darkForeground;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
            }

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

            internal class ToolStripColorTable : ProfessionalColorTable
            {
                public override Color MenuBorder
                {
                    get { return _darkBorderBrush; }
                }
            }
        }
    }
}