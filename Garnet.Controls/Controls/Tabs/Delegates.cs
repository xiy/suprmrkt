// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------
using System;
using Pyramid.Garnet.Controls.Tabs;

namespace Pyramid.Garnet.Controls.Tabs
{
    #region TabStripItemClosingEventArgs

    public class TabStripItemClosingEventArgs : EventArgs
    {
        public TabStripItemClosingEventArgs(GarnetTabStripItem item)
        {
            _item = item;
        }

        private bool _cancel = false;
        private GarnetTabStripItem _item;

        public GarnetTabStripItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

    }

    #endregion

    #region TabStripItemChangedEventArgs

    public class TabStripItemChangedEventArgs : EventArgs
    {
        GarnetTabStripItem itm;
        GarnetTabStripItemChangeTypes changeType;

        public TabStripItemChangedEventArgs(GarnetTabStripItem item, GarnetTabStripItemChangeTypes type)
        {
            changeType = type;
            itm = item;
        }

        public GarnetTabStripItemChangeTypes ChangeType
        {
            get { return changeType; }
        }

        public GarnetTabStripItem Item
        {
            get { return itm; }
        }
    }

    #endregion

    public delegate void TabStripItemChangedHandler(TabStripItemChangedEventArgs e);
    public delegate void TabStripItemClosingHandler(TabStripItemClosingEventArgs e);

}
