// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------s
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Pyramid.Garnet.Controls.Tabs
{
  internal class GarnetTabStripMenuGlyph
  {
    #region Fields

    private Rectangle glyphRect = Rectangle.Empty;
    private bool isMouseOver = false;
    private ToolStripProfessionalRenderer renderer;
    private LinearGradientBrush gbrush;

    #endregion

    #region Props

    public bool IsMouseOver
    {
      get { return isMouseOver; }
      set { isMouseOver = value; }
    }

    public Rectangle Bounds
    {
      get { return glyphRect; }
      set { glyphRect = value; }
    }

    #endregion

    #region Ctor

    internal GarnetTabStripMenuGlyph(ToolStripProfessionalRenderer renderer)
    {
      this.renderer = renderer;
    }

    #endregion

    #region Methods

    public void DrawGlyph(Graphics g)
    {
      if (isMouseOver)
      {
        gbrush = new LinearGradientBrush(glyphRect, Color.White, Color.FromArgb(224, 221, 206), LinearGradientMode.Vertical);
        g.FillRectangle(gbrush, glyphRect);
        Rectangle borderRect = glyphRect;

        borderRect.Width--;
        borderRect.Height--;

        g.DrawRectangle(new Pen(Color.Silver), borderRect);
      }

      g.SmoothingMode = SmoothingMode.Default;

      using (Pen pen = new Pen(Color.Gray))
      {
        pen.Width = 2;

        g.DrawLine(pen, new Point(glyphRect.Left + (glyphRect.Width / 3) - 2, glyphRect.Height / 2 - 1),
            new Point(glyphRect.Right - (glyphRect.Width / 3), (glyphRect.Height / 2) -1));
      }

      g.FillPolygon(Brushes.Black, new Point[]{
                new Point(glyphRect.Left + (glyphRect.Width / 3)-2, glyphRect.Height / 2+2),
                new Point(glyphRect.Right - (glyphRect.Width / 3), glyphRect.Height / 2+2),
                new Point(glyphRect.Left + glyphRect.Width / 2-1,glyphRect.Bottom-4)});
    }

    #endregion
  }
}
