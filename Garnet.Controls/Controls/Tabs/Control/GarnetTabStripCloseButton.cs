// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  Mozilla Public License 1.1 (http://www.mozilla.org/MPL/MPL-1.1.html)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: GarnetTabStripCloseButton.cs 8 2008-03-02 15:54:03Z xiy3x0 $
// ------------------------------------------------------------------------------
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace Pyramid.Garnet.Controls.Tabs
{
    internal class GarnetTabStripCloseButton
    {
        #region Fields

        private Rectangle crossRect;
        private RectangleF tabCrossRect;
        private bool isMouseOver = false;
        private ToolStripProfessionalRenderer renderer;

        #endregion

        #region Props

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        public Rectangle Bounds
        {
            get { return crossRect; }
            set { crossRect = value; }
        }

        public RectangleF TabCrossBounds
        {
            get { return tabCrossRect; }
            set { tabCrossRect = value; }
        }

        #endregion

        #region Ctor

        internal GarnetTabStripCloseButton(ToolStripProfessionalRenderer renderer)
        {
            this.renderer = renderer;
        }

        #endregion

        #region Methods

        public void DrawCross(Graphics g)
        {
            if (this.Bounds == null)
                throw new System.ArgumentException("Bounds cannot be null.");

            if (isMouseOver)
            {
                Color fill = renderer.ColorTable.ButtonSelectedHighlight;
                LinearGradientBrush gbrush = new LinearGradientBrush(crossRect, 
                                                                    Color.White, 
                                                                    Color.FromArgb(224, 221, 206), 
                                                                    LinearGradientMode.Vertical);
                g.FillRectangle(gbrush, crossRect);

                Rectangle borderRect = crossRect;

                borderRect.Width--;
                borderRect.Height--;

                g.DrawRectangle(new Pen(Color.Silver), borderRect);
            }

            using (Pen pen = new Pen(Color.Black, 1.6f))
            {
                g.DrawLine(pen, crossRect.Left + 3, crossRect.Top + 3,
                    crossRect.Right - 5, crossRect.Bottom - 4);

                g.DrawLine(pen, crossRect.Right - 5, crossRect.Top + 3,
                    crossRect.Left + 3, crossRect.Bottom - 4);
            }
        }

        public void DrawCrossOnTab(Graphics g, RectangleF tabRect)
        {
            Rectangle xBounds = Rectangle.Empty;
            // Create the Rectangle to contain the close glyph.
            xBounds.Height = 10;
            xBounds.Width = 10;
            xBounds.X = (int)tabRect.X + (int)tabRect.Width - 14;
            xBounds.Y = (int)tabRect.Y + 5;
            this.Bounds = xBounds;

            //g.DrawRectangle(Pens.CornflowerBlue, xBounds);
            
            // Determine whether the cross glyph is being drawn with the mouse over it.
            float penWidth = 1.6f;
            Pen closePen = isMouseOver ? closePen = new Pen(Color.Brown, penWidth) : closePen = new Pen(Color.Silver, penWidth);

            // Draw the cross onto the tab inside the xBounds Rectangle.
            using (closePen)
            {
                g.DrawLine(closePen, Bounds.Left + 2, Bounds.Top + 2,
                        Bounds.Right - 2, Bounds.Bottom - 2);

                g.DrawLine(closePen, Bounds.Right - 2, Bounds.Top + 2,
                    Bounds.Left + 2, Bounds.Bottom - 2);
            }
        }

        #endregion
    }
}
