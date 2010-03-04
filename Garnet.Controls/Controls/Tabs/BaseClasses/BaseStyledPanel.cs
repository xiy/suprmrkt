// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Pyramid.Garnet.Controls.Tabs.BaseClasses
{
    [ToolboxItem(false)]
    public class BaseStyledPanel : ContainerControl
    {
        #region Fields

        private static ToolStripProfessionalRenderer renderer;

        #endregion

        #region Events

        public event EventHandler ThemeChanged;

        #endregion

        #region Ctor

        static BaseStyledPanel()
        {
            renderer = new ToolStripProfessionalRenderer();
        }

        public BaseStyledPanel()
        {
            // Set painting style for better performance.
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        #endregion

        #region Methods

        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            UpdateRenderer();
            Invalidate();
        }

        protected virtual void OnThemeChanged(EventArgs e)
        {
            if (ThemeChanged != null)
                ThemeChanged(this, e);
        }

        private void UpdateRenderer()
        {
            if (!UseThemes)
            {
                renderer.ColorTable.UseSystemColors = true;
            }
            else
            {
                renderer.ColorTable.UseSystemColors = false;
            }
        }

        #endregion

        #region Props

        [Browsable(false)]
        public ToolStripProfessionalRenderer ToolStripRenderer
        {
            get { return renderer; }
        }

        [DefaultValue(true)]
        [Browsable(false)]
        public bool UseThemes
        {
            get
            {
                return VisualStyleRenderer.IsSupported && VisualStyleInformation.IsSupportedByOS && Application.RenderWithVisualStyles;
            }
        }

        #endregion
    }
}
