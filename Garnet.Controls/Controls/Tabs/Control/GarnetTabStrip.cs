// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Pyramid.Garnet.Controls.Tabs.BaseClasses;
using Pyramid.Garnet.Controls.Tabs.Design;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pyramid.Garnet.Controls.Tabs
{
    [Designer(typeof (GarnetTabStripDesigner))]
    [DefaultEvent("TabStripItemSelectionChanged")]
    [DefaultProperty("Items")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TabControl))]
    public class GarnetTabStrip : BaseStyledPanel, ISupportInitialize, IDisposable
    {
        #region Static Fields

        internal static int PreferredWidth = 350;
        internal static int PreferredHeight = 200;

        #endregion

        #region Constants

        private const int DEF_HEADER_HEIGHT = 22;
        private const int DEF_GLYPH_WIDTH = 25;

        private int DEF_START_POS = 10;

        #endregion

        #region Events

        public event TabStripItemClosingHandler TabStripItemClosing;
        public event TabStripItemChangedHandler TabStripItemSelectionChanged;
        public event HandledEventHandler MenuItemsLoading;
        public event EventHandler MenuItemsLoaded;
        public event EventHandler TabStripItemClosed;

        #endregion

        #region Fields

        private Rectangle stripButtonRect = Rectangle.Empty;
        private GarnetTabStripItem selectedItem = null;
        private ContextMenuStrip menu = null;
        private GarnetTabStripMenuGlyph menuGlyph = null;
        private GarnetTabStripCloseButton closeButton = null;
        private GarnetTabStripItemCollection tabs;
        private StringFormat sFormat = null;
        private List<GarnetTabStripCloseButton> tabCloseButtonCollection;
        private CloseStyle tabCloseStyle = CloseStyle.OnTab;
        private static Font defaultFont = new Font("Segoe UI", 9.75f);
		private Color tabActivePenColor;

		public Color TabActiveUnderlineColor
		{
			get { return tabActivePenColor; }
			set { tabActivePenColor = value; }
		}

        private bool alwaysShowClose = true;
        private bool isIniting = false;
        private bool alwaysShowMenuGlyph = true;
        private bool menuOpen = false;

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Determines what drawn object is at the given point.
        /// Can be used for both mouse-move and mouse-down events.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public HitTestResult HitTest(Point pt)
        {
            // The central close button has been hit.
            if(closeButton.Bounds.Contains(pt))
                return HitTestResult.CloseButton;
            
            // The menu button has been hit.
            if(menuGlyph.Bounds.Contains(pt))
                return HitTestResult.MenuGlyph;

            // A close button on a tab has been hit.
            if (GetTabItemByPoint(pt) != null && GetTabItemByPoint(pt).CloseButton.Bounds.Contains(pt))
                return HitTestResult.CloseButton;

            // A tab has been hit (doesn't include the bounds of a close button).
            if (GetTabItemByPoint(pt) != null)
                return HitTestResult.TabItem;
            
            // No other result is available.
            return HitTestResult.None;
        }
        
        /// <summary>
        /// Add a <see cref="GarnetTabStripItem"/> to this control without selecting it.
        /// </summary>
        /// <param name="tabItem">The <see cref="GarnetTabStripItem"/> to add.</param>
        public void AddTab(GarnetTabStripItem tabItem)
        {
            AddTab(tabItem, false);
        }
        
        /// <summary>
        /// Add a <see cref="GarnetTabStripItem"/> to this control, with a choice to 
        /// select the new tab page automatically.
        /// </summary>
        /// <param name="tabItem">The <see cref="GarnetTabStripItem"/> to add.</param>
        /// <param name="autoSelect">
        /// A Boolean value indicating whether to automatically select
        /// the newly added <see cref="GarnetTabStripItem"/>.
        /// </param>
        public void AddTab(GarnetTabStripItem tabItem, bool autoSelect)
        {
            tabItem.Dock = DockStyle.Fill;
            Tabs.Add(tabItem);

            if ((autoSelect && tabItem.Visible) || (tabItem.Visible && Tabs.DrawnCount < 1 ))
            {
                SelectedItem = tabItem;
                SelectItem(tabItem);
            }
        }

        /// <summary>
        /// Remove a <see cref="GarnetTabStripItem"/> from the <see cref="GarnetTabStrip" />.
        /// </summary>
        /// <param name="tabItem">The <see cref="GarnetTabStripItem"/> to remove.</param>
        public void RemoveTab(GarnetTabStripItem tabItem)
        {
            int tabIndex = Tabs.IndexOf(tabItem);

            if (tabIndex >= 0)
            {
                UnSelectItem(tabItem);
                Tabs.Remove(tabItem);
            }

            if (Tabs.Count > 0)
            {
                if (RightToLeft == RightToLeft.No)
                {
                    if (Tabs[tabIndex - 1] != null)
                    {
                        SelectedItem = Tabs[tabIndex - 1];
                    }
                    else
                    {
                        SelectedItem = Tabs.FirstVisible;
                    }
                }
                else
                {
                    if (Tabs[tabIndex + 1] != null)
                    {
                        SelectedItem = Tabs[tabIndex + 1];
                    }
                    else
                    {
                        SelectedItem = Tabs.LastVisible;
                    }
                }
            }
        }

		public void SelectNextTab()
		{
			if (this.Tabs.Count > 0 && !IsLastTab)
			{
				this.SelectedItem = this.Tabs[Tabs.IndexOf(this.SelectedItem) + 1];
			}
		}

		public void SelectPreviousTab()
		{
			if (this.Tabs.Count > 0 && tabs.IndexOf(selectedItem) != tabs.Count)
			{

				this.selectedItem = this.tabs[tabs.IndexOf(this.selectedItem) - 1];
			}
		}

		public bool IsLastTab
		{
			get
			{
				if (tabs.IndexOf(selectedItem) < this.tabs.Count || 
					tabs.IndexOf(selectedItem) > this.tabs.Count)
					return false;
				return true;
			}
		}

		public bool IsFirstTab
		{
			get
			{
				if (tabs.IndexOf(selectedItem) > 1)
					return false;
				return true;
			}
		}

        /// <summary>
        /// Get a <see cref="GarnetTabStripItem"/> at the provided System.Drawing.Point.
        /// </summary>
        /// <param name="pt">The <see cref="System.Drawing.Point"/> to check.</param>
        /// <returns><see cref="GarnetTabStripItem"/> if one was found, otherwise returns null.</returns>
        public GarnetTabStripItem GetTabItemByPoint(Point pt)
        {
            GarnetTabStripItem item = null;
            bool found = false;
            
            for (int i = 0; i < Tabs.Count; i++)
            {
                GarnetTabStripItem current = Tabs[i];
                
                if (current.StripRect.Contains(pt) && current.Visible && current.IsDrawn)
                {
                    item = current;
                    found = true;
                }
                
                if(found)
                    break;
            }

            return item;
        }

        /// <summary>
        /// Shows the ContextMenuStrip containing <see cref="GarnetTabStripItem"/>'s.
        /// </summary>
        public virtual void ShowMenu()
        {
            if (menu.Visible == false && menu.Items.Count > 0)
            {
                if (RightToLeft == RightToLeft.No)
                {
                    menu.Show(this, new Point(menuGlyph.Bounds.Left, menuGlyph.Bounds.Bottom));
                }
                else
                {
                    menu.Show(this, new Point(menuGlyph.Bounds.Right, menuGlyph.Bounds.Bottom));
                }

                menuOpen = true;
            }
        }

        /// <summary>
        /// Rounds a single-precision floating point number down to the given decimal place.
        /// </summary>
        /// <param name="f">The System.Float to round.</param>
        /// <param name="decimals">
        /// A System.Integer that specifies the number of decimal places to round to.
        /// </param>
        /// <returns>A rounded down System.Float.</returns>
        public static float RoundFloat(float f, int decimals)
        {
            return (float)Math.Round(f + decimals / 10.0, decimals);
        }


        /// <summary>
        /// Converts a RectangleF (float) to a Rectangle (int).
        /// </summary>
        /// <param name="rectF">The RectangleF to convert to a Rectangle.</param>
        /// <returns>A new Rectangle object with the same dimensions as <paramref name="rectF"/>.</returns>
        internal Rectangle ToRectangle(RectangleF rectF)
        {
            Rectangle rect = new Rectangle((int)rectF.X, (int)rectF.Y, (int)rectF.Width, (int)rectF.Height);
            return rect;
        }

        /// <summary>
        /// Converts a Rectangle (int) to a RectangleF (float).
        /// </summary>
        /// <param name="rect">The Rectangle to convert to a RectangleF.</param>
        /// <returns>A new RectangleF object with the same dimensions as <paramref name="rect"/>.</returns>
        internal RectangleF ToRectangleF(Rectangle rect)
        {
            RectangleF rectF = new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
            return rectF;
        }

        #endregion

        #region Internal

        internal void UnDrawAll()
        {
            for (int i = 0; i < Tabs.Count; i++)
            {
                Tabs[i].IsDrawn = false;
            }
        }

        internal void SelectItem(GarnetTabStripItem tabItem)
        {
            tabItem.Dock = DockStyle.Fill;
            tabItem.Visible = true;
            tabItem.Selected = true;
        }

        internal void UnSelectItem(GarnetTabStripItem tabItem)
        {
            //tabItem.Visible = false;
            tabItem.Selected = false;
        }

        #endregion

        #region Protected

        /// <summary>
        /// Fires <see cref="TabStripItemClosing"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnTabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            if (TabStripItemClosing != null)
                TabStripItemClosing(e);
        }

        /// <summary>
        /// Fires <see cref="TabStripItemClosed"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected internal virtual void OnTabStripItemClosed(EventArgs e)
        {
            if (TabStripItemClosed != null)
                TabStripItemClosed(this, e);
        }

        /// <summary>
        /// Fires <see cref="MenuItemsLoading"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemsLoading(HandledEventArgs e)
        {
            if (MenuItemsLoading != null)
                MenuItemsLoading(this, e);
        }
        /// <summary>
        /// Fires <see cref="MenuItemsLoaded"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemsLoaded(EventArgs e)
        {
            if (MenuItemsLoaded != null)
                MenuItemsLoaded(this, e);
        }

        /// <summary>
        /// Fires <see cref="TabStripItemSelectionChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTabStripItemChanged(TabStripItemChangedEventArgs e)
        {
            if (TabStripItemSelectionChanged != null)
                TabStripItemSelectionChanged(e);
        }

        /// <summary>
        /// Loads menu items based on <see cref="GarnetTabStripItem"/>s currently added
        /// to this control.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMenuItemsLoad(EventArgs e)
        {
            menu.RightToLeft = RightToLeft;
            menu.Items.Clear();

            for (int i = 0; i < Tabs.Count; i++)
            {
                GarnetTabStripItem item = Tabs[i];
                if (!item.Visible)
                    continue;

                ToolStripMenuItem tItem = new ToolStripMenuItem(item.Title);
                tItem.Tag = item;
                tItem.Image = item.Image;
                menu.Items.Add(tItem);
            }

            OnMenuItemsLoaded(EventArgs.Empty);
        }

        #endregion

        #region Overrides

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            UpdateLayout();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SetDefaultSelected();

            // The colour of the control border and under-page line.
            Color lineColor = Color.Silver;

            // Pens for the control border and under-page line.
            Pen borderPen = new Pen(lineColor);
            Pen underlinePen = new Pen(lineColor);

            if (RightToLeft == RightToLeft.No)
            {
                DEF_START_POS = 10;
            }
            else
            {
                DEF_START_POS = stripButtonRect.Right;
            }

            // Adjust the control-border rectangle to make it properly
            // surround the tab control and not be clipped by the window borders.
            PaintBorder(e, borderPen);

            #region Draw Pages

            for (int i = 0; i < Tabs.Count; i++)
            {
                GarnetTabStripItem currentTab = Tabs[i];
                if (!currentTab.Visible && !DesignMode)
                    continue;

                OnCalcTabPage(e.Graphics, currentTab);
                currentTab.IsDrawn = false;

                if (!AllowDraw(currentTab))
                    continue;

                OnDrawTabPage(e.Graphics, currentTab);
            }

            #endregion

            #region Draw UnderPage Line

            if (RightToLeft == RightToLeft.No)
            {
                if (Tabs.DrawnCount == 0 || Tabs.VisibleCount == 0)
                {
                    e.Graphics.DrawLine(underlinePen, new Point(0, DEF_HEADER_HEIGHT),
                                         new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
                else if (SelectedItem != null && SelectedItem.IsDrawn)
                {
                    Point end = new Point((int)SelectedItem.StripRect.Left - 9, DEF_HEADER_HEIGHT);
                    e.Graphics.DrawLine(underlinePen, new Point(0, DEF_HEADER_HEIGHT), end);
                    end.X += (int)SelectedItem.StripRect.Width + 16;
                    e.Graphics.DrawLine(underlinePen, end, new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
            }
            else
            {
                if (Tabs.DrawnCount == 0 || Tabs.VisibleCount == 0)
                {
                    e.Graphics.DrawLine(underlinePen, new Point(0, DEF_HEADER_HEIGHT),
                                        new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
                else if (SelectedItem != null && SelectedItem.IsDrawn)
                {
                    Point end = new Point((int)SelectedItem.StripRect.Left, DEF_HEADER_HEIGHT);
                    e.Graphics.DrawLine(underlinePen, new Point(0, DEF_HEADER_HEIGHT), end);
                    end.X += (int)SelectedItem.StripRect.Width + 20;
                    e.Graphics.DrawLine(underlinePen, end, new Point(ClientRectangle.Width, DEF_HEADER_HEIGHT));
                }
            }

            #endregion

            #region Draw Menu and Close Glyphs

            if (AlwaysShowMenuGlyph || Tabs.DrawnCount > Tabs.VisibleCount)
                menuGlyph.DrawGlyph(e.Graphics);

            //if (AlwaysShowClose || (SelectedItem != null && SelectedItem.CanClose))
            //    closeButton.DrawCross(e.Graphics);

            #endregion
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
                return;

            HitTestResult result = HitTest(e.Location);
            if(result == HitTestResult.MenuGlyph)
            {
                HandledEventArgs args = new HandledEventArgs(false);
                OnMenuItemsLoading(args);
                
                if (!args.Handled)
                    OnMenuItemsLoad(EventArgs.Empty);

                ShowMenu();
            }
            else if (result == HitTestResult.CloseButton)
            {
                if (TabCloseStyle == CloseStyle.None) return;
                if (TabCloseStyle == CloseStyle.OnTab)
                {
                    GarnetTabStripItem hoveredTab = this.GetTabItemByPoint(e.Location);
                    TabStripItemClosingEventArgs args = new TabStripItemClosingEventArgs(hoveredTab);
                    OnTabStripItemClosing(args);
                    if (!args.Cancel && hoveredTab.CanClose)
                    {
                        RemoveTab(hoveredTab);
                        OnTabStripItemClosed(EventArgs.Empty);
                    }
                }
                else if (TabCloseStyle == CloseStyle.OnStrip && SelectedItem != null)
                {
                    TabStripItemClosingEventArgs args = new TabStripItemClosingEventArgs(SelectedItem);
                    OnTabStripItemClosing(args);
                    if (!args.Cancel && SelectedItem.CanClose)
                    {
                        RemoveTab(SelectedItem);
                        OnTabStripItemClosed(EventArgs.Empty);
                    }
                }
            }
            else if(result == HitTestResult.TabItem)
            {
                GarnetTabStripItem item = GetTabItemByPoint(e.Location);
                if (item != null)
                    SelectedItem = item;
            }

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            HitTestResult movingHitResult = HitTest(e.Location);

            if (movingHitResult == HitTestResult.MenuGlyph)
            {
                if (menuGlyph.Bounds.Contains(e.Location))
                {
                    menuGlyph.IsMouseOver = true;
                    Invalidate(menuGlyph.Bounds);
                }
                else
                {
                    if (menuGlyph.IsMouseOver && !menuOpen)
                    {
                        menuGlyph.IsMouseOver = false;
                        Invalidate(menuGlyph.Bounds);
                    }
                }
            }
            else if (movingHitResult == HitTestResult.CloseButton)
            {
                // The mouse is over a close button, so find out which.

                // Is it on the tab?
                GarnetTabStripItem closingTab = this.GetTabItemByPoint(e.Location);
                if (closingTab != null && closingTab.CloseButton.Bounds.Contains(e.Location))
                {
                    // The mouse is over the tab close button, so set that as the TabCloseStyle.
                    TabCloseStyle = CloseStyle.OnTab;
                    closingTab.CloseButton.IsMouseOver = true;
                    Invalidate(closingTab.CloseButton.Bounds);
                }
                else if (closingTab != null && closingTab.CloseButton.IsMouseOver)
                {
                    TabCloseStyle = CloseStyle.None;
                    closingTab.CloseButton.IsMouseOver = false;
                    Invalidate(closingTab.CloseButton.Bounds);
                }

                // Is it on the main close button?
                if (closeButton.Bounds.Contains(e.Location))
                {
                    TabCloseStyle = CloseStyle.OnStrip;
                    closeButton.IsMouseOver = true;
                    Invalidate(closeButton.Bounds);
                }
                else if (closeButton.IsMouseOver)
                {
                    TabCloseStyle = CloseStyle.None;
                    closeButton.IsMouseOver = false;
                    Invalidate(closeButton.Bounds);
                };
            }
            else if (movingHitResult == HitTestResult.TabItem)
            {
                GarnetTabStripItem thisTab = this.GetTabItemByPoint(e.Location);
                
                // This is cheap, but just for now..
                thisTab.CloseButton.IsMouseOver = false;
                Invalidate(thisTab.CloseButton.Bounds);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            menuGlyph.IsMouseOver = false;
            Invalidate(menuGlyph.Bounds);
            
            closeButton.IsMouseOver = false;
            Invalidate(closeButton.Bounds);

            foreach (GarnetTabStripItem item in this.Tabs)
            {
                item.CloseButton.IsMouseOver = false;
                Invalidate(item.CloseButton.Bounds);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (isIniting)
                return;

            UpdateLayout();
        }

        #endregion

        #region Private

        private bool AllowDraw(GarnetTabStripItem item)
        {
            bool result = true;

            if (RightToLeft == RightToLeft.No)
            {
                if (item.StripRect.Right >= stripButtonRect.Width)
                    result = false;
            }
            else
            {
                if (item.StripRect.Left <= stripButtonRect.Left)
                    return false;
            }

            return result;
        }

        private void SetDefaultSelected()
        {
            if (selectedItem == null && Tabs.Count > 0)
                SelectedItem = Tabs[0];

            for (int i = 0; i < Tabs.Count; i++)
            {
                GarnetTabStripItem itm = Tabs[i];
                itm.Dock = DockStyle.Fill;
            }
        }

        private void OnMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            GarnetTabStripItem clickedItem = (GarnetTabStripItem) e.ClickedItem.Tag;
            SelectedItem = clickedItem;
        }

        private void OnMenuVisibleChanged(object sender, EventArgs e)
        {
            if (menu.Visible == false)
            {
                menuOpen = false;
            }
        }

        private void OnCalcTabPage(Graphics g, GarnetTabStripItem currentItem)
        {
            Font currentFont = Font;
            if (currentItem == SelectedItem)
                currentFont = new Font(Font, FontStyle.Regular);

            SizeF textSize = g.MeasureString(currentItem.Title, currentFont, new SizeF(200, 10), sFormat);
            textSize.Width += 20;

            if (RightToLeft == RightToLeft.No)
            {
                RectangleF buttonRect = new RectangleF(DEF_START_POS, 3, textSize.Width + 2, 17);
                currentItem.StripRect = buttonRect;
                DEF_START_POS += (int) textSize.Width + 2;
            }
            else
            {
                RectangleF buttonRect = new RectangleF(DEF_START_POS - textSize.Width + 1, 3, textSize.Width - 1, 17);
                currentItem.StripRect = buttonRect;
                DEF_START_POS -= (int) textSize.Width;
            }
        }

        private void OnDrawTabPage(Graphics g, GarnetTabStripItem currentItem)
        {
            bool isFirstTab = Tabs.IndexOf(currentItem) == 0;
            Font currentFont = Font;

            if (currentItem == SelectedItem)
                currentFont = new Font(Font, FontStyle.Regular);

            SizeF textSize = g.MeasureString(currentItem.Title, currentFont, new SizeF(200, 10), sFormat);
            textSize.Width += 20;
            RectangleF buttonRect = currentItem.StripRect;
            
            GraphicsPath path = new GraphicsPath();
            LinearGradientBrush brush;
            int mtop = 3;

            //TODO: Make TabStyle configurable.

            if (RightToLeft == RightToLeft.No)
            {
                DrawNonRTLTabButton(TabStyle.Garnet, g, currentItem, isFirstTab, currentFont, ref textSize, ref buttonRect, path, mtop);
            }

            if (RightToLeft == RightToLeft.Yes)
            {
                brush = DrawRTLTabButton(TabStyle.Garnet, g, currentItem, isFirstTab, currentFont, ref textSize, ref buttonRect, path, mtop);
            }

            currentItem.IsDrawn = true;
        }

        #region Internal Tab Drawing Functions

        private void PaintBorder(PaintEventArgs e, Pen borderPen)
        {
            Rectangle newRec = ClientRectangle;
            newRec.Height -= DEF_HEADER_HEIGHT;
            newRec.Y += DEF_HEADER_HEIGHT + 1;
            e.Graphics.DrawRectangle(borderPen, newRec);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        #region Non RTL Tab Painting Functions

        private void DrawNonRTLTabButton(TabStyle ts, Graphics g, GarnetTabStripItem tabPage, bool isFirstTab,
                                                Font currentFont, ref SizeF textSize, ref RectangleF tabRect,
                                                GraphicsPath tabPath, int mtop)
        {

            // Adjust the tab-strip position/size a bit to make it look cleaner.
            // This could probably be done from init in the future, it's a bit trashy here.
            tabRect.X += 3;
            tabRect.Height *= 1.2f;

            // TAB DRAWING
            //////////////////////////////////////////////////////////////////////////

            // Hopefully in the future this conditional will choose what to draw depending on
            // what the user has chosen as their tab style in their options. 
            // For now it's just the Visual Studio 2008 style with cleaner colours.
            if (ts == TabStyle.Garnet)
            {
				
                // As we're imitating the VS08 style, we need to 
                if (isFirstTab)
                {
                    tabPath.AddLine(tabRect.Left - 10, tabRect.Bottom - 1, tabRect.Left, mtop + 8);
                }
                else if (tabPage == SelectedItem)
                {
                    tabPath.AddLine(tabRect.Left - 10, tabRect.Bottom - 1, tabRect.Left + (tabRect.Height / 2) - 10, mtop + 8);
                }
                else
                {
                    tabPath.AddLine(tabRect.Left + 1, tabRect.Bottom - 1, tabRect.Left + 1, mtop + 10);
                }

                tabPath.AddLine(tabRect.Left + 1, tabRect.Bottom - (tabRect.Height / 2) - 3, tabRect.Left + (tabRect.Height / 2) - 4, mtop + 2);
                tabPath.AddLine(tabRect.Left + (tabRect.Height / 2), mtop, tabRect.Right - 2, mtop); // TOP RIGHT
                tabPath.AddLine(tabRect.Right, mtop + 2, tabRect.Right, tabRect.Bottom - 1);
                tabPath.CloseFigure();
            }

            LinearGradientBrush tabFillBrush;
            Pen tabOutlinePen = new Pen(Color.Silver);
            Pen tabActivePen = new Pen(this.TabActiveUnderlineColor);

            if (tabPage == SelectedItem)
            {
                tabFillBrush = new LinearGradientBrush(
                    tabRect,
                    Color.White,
                    SelectedGarnetTabEndColor,
                    LinearGradientMode.Vertical);
            }
            else
            {
                tabFillBrush = new LinearGradientBrush(
                    tabRect,
                    Color.White,
                    UnselectedGarnetTabEndColor,
                    LinearGradientMode.Vertical);
            }

            g.FillPath(tabFillBrush, tabPath);
            g.DrawPath(tabOutlinePen, tabPath);

            // TAB STATUS EFFECTS
            //////////////////////////////////////////////////////////////////////////

            // Draw a line underneath the selected tab button.
            // TODO: Change the colour of the line depending on undo state.
            if (tabPage == SelectedItem)
            {
                g.DrawLine(tabActivePen, tabRect.Left - 9, tabRect.Height + 2,
                           tabRect.Left + tabRect.Width + 6, tabRect.Height + 2);
            }

            // TAB TITLE DRAWING
            //////////////////////////////////////////////////////////////////////////

            // The PointF location of the drawn string.
            PointF textLoc = new PointF(tabRect.Left + tabRect.Height - 12, 
                                        currentFont.Size / 1.7f);
            //tabRect.Top + (tabRect.Height / 2) - (textSize.Height / 2) - 3
            
            // The SizeF of the drawn string.
            SizeF textSizeF = g.MeasureString(tabPage.Title, currentFont);

            // Calculate the RectangleF that contains the tab title text.
            // Must be within the RectangleF of the the current tab, and can't intersect close button!
            RectangleF textRect = new RectangleF(textLoc, textSizeF);
            textRect.Y -= 1;

            // Draw the tab title text onto the tab button.
            // NOTE: Tab title drawing needs to be improved to calculate better for different fonts.
            if (tabPage == SelectedItem)
            {
                g.DrawString(tabPage.Title,
                             currentFont,
                             new SolidBrush(Color.Black),
                             textRect,
                             sFormat);
            }
            else
            {
                g.DrawString(tabPage.Title,
                             currentFont,
                             new SolidBrush(Color.Gray),
                             textRect, sFormat);
            }

            // Debug stuff
            //g.DrawRectangle(Pens.Crimson, ToRectangle(tabRect));
			tabPage.CloseButton.DrawCrossOnTab(g, tabRect);
        }

        #endregion

        #region RTL Tab Painting Functions
        private LinearGradientBrush DrawRTLTabButton(TabStyle ts, Graphics g, GarnetTabStripItem currentItem, bool isFirstTab, 
                                                     Font currentFont, ref SizeF textSize, ref RectangleF buttonRect, 
                                                     GraphicsPath path, int mtop)
        {
            LinearGradientBrush brush;
            if (currentItem == SelectedItem || isFirstTab)
            {
                path.AddLine(buttonRect.Right + 10, buttonRect.Bottom - 1,
                             buttonRect.Right - (buttonRect.Height / 2) + 4, mtop + 4);
            }
            else
            {
                path.AddLine(buttonRect.Right, buttonRect.Bottom - 1, buttonRect.Right,
                             buttonRect.Bottom - (buttonRect.Height / 2) - 2);
                path.AddLine(buttonRect.Right, buttonRect.Bottom - (buttonRect.Height / 2) - 3,
                             buttonRect.Right - (buttonRect.Height / 2) + 4, mtop + 3);
            }

            path.AddLine(buttonRect.Right - (buttonRect.Height / 2) - 2, mtop, buttonRect.Left + 3, mtop);
            path.AddLine(buttonRect.Left, mtop + 2, buttonRect.Left, buttonRect.Bottom - 1);
            path.AddLine(buttonRect.Left + 4, buttonRect.Bottom - 1, buttonRect.Right, buttonRect.Bottom - 1);
            path.CloseFigure();

            if (currentItem == SelectedItem)
            {
                brush =
                    new LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Window,
                                            LinearGradientMode.Vertical);
            }
            else
            {
                brush =
                    new LinearGradientBrush(buttonRect, SystemColors.ControlLightLight, SystemColors.Control,
                                            LinearGradientMode.Vertical);
            }

            g.FillPath(brush, path);
            g.DrawPath(SystemPens.ControlDark, path);

            if (currentItem == SelectedItem)
            {
                g.DrawLine(new Pen(brush), buttonRect.Right + 9, buttonRect.Height + 2,
                           buttonRect.Right - buttonRect.Width + 1, buttonRect.Height + 2);
            }

            PointF textLoc = new PointF(buttonRect.Left + 2, buttonRect.Top + (buttonRect.Height / 2) - (textSize.Height / 2) - 2);
            RectangleF textRect = buttonRect;
            textRect.Location = textLoc;
            textRect.Width = buttonRect.Width - (textRect.Left - buttonRect.Left) - 10;
            textRect.Height = textSize.Height + currentFont.Size / 2;

            if (currentItem == SelectedItem)
            {
                textRect.Y -= 1;
                g.DrawString(currentItem.Title, currentFont, new SolidBrush(ForeColor), textRect, sFormat);
            }
            else
            {
                g.DrawString(currentItem.Title, currentFont, new SolidBrush(ForeColor), textRect, sFormat);
            }

            //g.FillRectangle(Brushes.Red, textRect);
            return brush;
        }
        #endregion

        #endregion

        private void UpdateLayout()
        {
            if (RightToLeft == RightToLeft.No)
            {
                sFormat.Trimming = StringTrimming.EllipsisCharacter;
                sFormat.FormatFlags |= StringFormatFlags.NoWrap;
                sFormat.FormatFlags &= StringFormatFlags.DirectionRightToLeft;

                stripButtonRect = new Rectangle(0, 0, ClientSize.Width - DEF_GLYPH_WIDTH - 2, 10);
                menuGlyph.Bounds = new Rectangle(ClientSize.Width - DEF_GLYPH_WIDTH, 2, 16, 16);
                closeButton.Bounds = new Rectangle(ClientSize.Width - 2, 2, 16, 16);
            }
            else
            {
                sFormat.Trimming = StringTrimming.EllipsisCharacter;
                sFormat.FormatFlags |= StringFormatFlags.NoWrap;
                sFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

                stripButtonRect = new Rectangle(DEF_GLYPH_WIDTH + 2, 0, ClientSize.Width - DEF_GLYPH_WIDTH - 15, 10);
                closeButton.Bounds = new Rectangle(4, 2, 16, 16); //ClientSize.Width - DEF_GLYPH_WIDTH, 2, 16, 16);
                menuGlyph.Bounds = new Rectangle(this.ClientSize.Width, 2, 16, 16);
            }

            DockPadding.Top = DEF_HEADER_HEIGHT + 1;
            DockPadding.Bottom = 1;
            DockPadding.Right = 1;
            DockPadding.Left = 1;
        }

        private void OnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            GarnetTabStripItem itm = (GarnetTabStripItem) e.Element;

            if (e.Action == CollectionChangeAction.Add)
            {
                Controls.Add(itm);
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, GarnetTabStripItemChangeTypes.Added));
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                Controls.Remove(itm);
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, GarnetTabStripItemChangeTypes.Removed));
            }
            else
            {
                OnTabStripItemChanged(new TabStripItemChangedEventArgs(itm, GarnetTabStripItemChangeTypes.Changed));
            }

            UpdateLayout();
            Invalidate();
        }

        #endregion

        #endregion

        #region Ctor

        public GarnetTabStrip()
        {
            BeginInit();

            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, true);

            tabCloseButtonCollection = new List<GarnetTabStripCloseButton>();
            TabCloseStyle = CloseStyle.None;

            tabs = new GarnetTabStripItemCollection();
            tabs.CollectionChanged += new CollectionChangeEventHandler(OnCollectionChanged);
            base.Size = new Size(PreferredWidth, PreferredHeight);

            menu = new ContextMenuStrip();
            menu.Renderer = ToolStripRenderer;
            menu.ItemClicked += new ToolStripItemClickedEventHandler(OnMenuItemClicked);
            menu.VisibleChanged += new EventHandler(OnMenuVisibleChanged);

            menuGlyph = new GarnetTabStripMenuGlyph(ToolStripRenderer);
            closeButton = new GarnetTabStripCloseButton(ToolStripRenderer);
            Font = defaultFont;
            sFormat = new StringFormat();

            EndInit();

            UpdateLayout();
        }

        #endregion

        #region Props

        /// <summary>
        /// The end color of the gradient that fills the selected tabs.
        /// </summary>
        private Color SelectedGarnetTabEndColor
        {
            get
            {
                return Color.FromArgb(224, 221, 206);
                //return Color.White;
            }
        }

        /// <summary>
        /// The end color of the gradient that fills unselected tabs.
        /// </summary>
        private Color UnselectedGarnetTabEndColor
        {
            get
            {
                //return Color.FromArgb(217, 214, 196);
                return Color.AliceBlue;
            }
        }

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.All)]
        public GarnetTabStripItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value)
                    return;

                if (value == null && Tabs.Count > 0)
                {
                    GarnetTabStripItem itm = Tabs[0];
                    if (itm.Visible)
                    {
                        selectedItem = itm;
                        selectedItem.Selected = true;
                        selectedItem.Dock = DockStyle.Fill;
                    }
                }
                else
                {
                    selectedItem = value;
                }

                foreach (GarnetTabStripItem itm in Tabs)
                {
                    if (itm == selectedItem)
                    {
                        SelectItem(itm);
                        itm.Dock = DockStyle.Fill;
                        itm.Show();
                    }
                    else
                    {
                        UnSelectItem(itm);
                        itm.Hide();
                    }
                }

                SelectItem(selectedItem);
                Invalidate();

                if (!selectedItem.IsDrawn)
                {
                    Tabs.MoveTo(0, selectedItem);
                    Invalidate();
                }

                OnTabStripItemChanged(
                    new TabStripItemChangedEventArgs(selectedItem, GarnetTabStripItemChangeTypes.SelectionChanged));
            }
        }

        [DefaultValue(true)]
        public bool AlwaysShowMenuGlyph
        {
            get { return alwaysShowMenuGlyph; }
            set
            {
                if (alwaysShowMenuGlyph == value)
                    return;

                alwaysShowMenuGlyph = value;
                Invalidate();
            }
        }

        [DefaultValue(true)]
        public bool AlwaysShowClose
        {
            get { return alwaysShowClose; }
            set
            {
                if (alwaysShowClose == value)
                    return;

                alwaysShowClose = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GarnetTabStripItemCollection Tabs
        {
            get { return tabs; }
        }

        [DefaultValue(typeof (Size), "350,200")]
        public new Size Size
        {
            get { return base.Size; }
            set
            {
                if (base.Size == value)
                    return;

                base.Size = value;
                UpdateLayout();
            }
        }

        /// <summary>
        /// DesignerSerializationVisibility
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ControlCollection Controls
        {
            get { return base.Controls; }
        }

        private List<GarnetTabStripCloseButton> TabCloseButtonCollection
        {
            get { return this.tabCloseButtonCollection; }
        }

        private CloseStyle TabCloseStyle
        {
            get { return this.tabCloseStyle; }
            set { this.tabCloseStyle = value; }
        }

        enum TabStyle
        {
            Orcas,
            Garnet
        }

        enum CloseStyle
        {
            OnTab,
            OnStrip,
            None
        }

        #endregion

        #region ShouldSerialize

        public bool ShouldSerializeFont()
        {
            return Font != null && !Font.Equals(defaultFont);
        }
        
        public bool ShouldSerializeSelectedItem()
        {
            return true;
        }

        public bool ShouldSerializeItems()
        {
            return tabs.Count > 0;
        }

        public new void ResetFont()
        {
            Font = defaultFont;
        }

        #endregion

        #region ISupportInitialize Members

        public void BeginInit()
        {
            isIniting = true;
        }

        public void EndInit()
        {
            isIniting = false;
        }

        #endregion

        #region IDisposable

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                tabs.CollectionChanged -= new CollectionChangeEventHandler(OnCollectionChanged);
                menu.ItemClicked        -= new ToolStripItemClickedEventHandler(OnMenuItemClicked);
                menu.VisibleChanged     -= new EventHandler(OnMenuVisibleChanged);

                foreach (GarnetTabStripItem item in tabs)
                {
                    if (item != null && !item.IsDisposed)
                        item.Dispose();
                }

                if (menu != null && !menu.IsDisposed)
                    menu.Dispose();

                if (sFormat != null)
                    sFormat.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}