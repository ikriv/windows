using System;
using System.Windows;
using System.Windows.Forms.Integration;

namespace IKriv.Windows.Integration
{
    public class SizeToContentWinFormsHost : WindowsFormsHost
    {
        private readonly System.Windows.Forms.UserControl _control;

        public SizeToContentWinFormsHost(System.Windows.Forms.UserControl control)
        {
            _control = control;
            Child = control;
            control.SizeChanged += OnControlSizeChanged;
        }

        void OnControlSizeChanged(object sender, EventArgs e)
        {
            if (Visibility != Visibility.Visible) return;
            InvalidateMeasure();
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var defaultSize = base.MeasureOverride(constraint);
            var source = PresentationSource.FromVisual(this);
            if (source == null) return defaultSize;

            var transformFromDevice = source.CompositionTarget.TransformFromDevice;
            var vector = new Vector(_control.Size.Width, _control.Size.Height);
            var controlSizeVector = transformFromDevice.Transform(vector);
            var controlSize = new Size(controlSizeVector.X, controlSizeVector.Y);
            return controlSize;
        }
    }
}
