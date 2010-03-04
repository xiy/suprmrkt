// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Pyramid.Garnet.Controls.Tabs;

namespace Pyramid.Garnet.Controls.Tabs.Design
{
    public class garnetTabStripItemDesigner : ParentControlDesigner
    {
        #region Fields

        GarnetTabStripItem TabStrip;

        #endregion

        #region Init & Dispose

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            TabStrip = component as GarnetTabStripItem;
        }

        #endregion

        #region Overrides

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            properties.Remove("Dock");
            properties.Remove("AutoScroll");
            properties.Remove("AutoScrollMargin");
            properties.Remove("AutoScrollMinSize");
            properties.Remove("DockPadding");
            properties.Remove("DrawGrid");
            properties.Remove("Font");
            properties.Remove("Padding");
            properties.Remove("MinimumSize");
            properties.Remove("MaximumSize");
            properties.Remove("Margin");
            properties.Remove("ForeColor");
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("RightToLeft");
            properties.Remove("GridSize");
            properties.Remove("ImeMode");
            properties.Remove("BorderStyle");
            properties.Remove("AutoSize");
            properties.Remove("AutoSizeMode");
            properties.Remove("Location");
        }

        public override SelectionRules SelectionRules
        {
            get
            {
                return 0;
            }
        }

        public override bool CanBeParentedTo(IDesigner parentDesigner)
        {
            return (parentDesigner.Component is GarnetTabStrip);
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            if (TabStrip != null)
            {
                using (Pen p = new Pen(SystemColors.ControlDark))
                {
                    p.DashStyle = DashStyle.Dash;
                    pe.Graphics.DrawRectangle(p, 0, 0, TabStrip.Width - 1, TabStrip.Height - 1);
                }
            }
        }

        #endregion
    }
}
