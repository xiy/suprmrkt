// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using Pyramid.Garnet.Controls.Tabs.Helpers;

namespace Pyramid.Garnet.Controls.Tabs
{
    public class GarnetTabStripItemCollection : CollectionWithEvents
    {
        #region Fields

        [Browsable(false)]
        public event CollectionChangeEventHandler CollectionChanged;

        private int lockUpdate;

        #endregion

        #region Ctor

        public GarnetTabStripItemCollection()
        {
            lockUpdate = 0;
        }

        #endregion

        #region Props

        public GarnetTabStripItem this[int index]
        {
            get
            {
                if (index < 0 || List.Count - 1 < index)
                    return null;

                return (GarnetTabStripItem)List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        [Browsable(false)]
        public virtual int DrawnCount
        {
            get
            {
                int count = Count, res = 0;
                if (count == 0) return 0;
                for (int n = 0; n < count; n++)
                {
                    if (this[n].IsDrawn) 
                        res++;
                }
                return res;
            }
        }

        public virtual GarnetTabStripItem LastVisible
        {
            get
            {
                for (int n = Count - 1; n > 0; n--)
                {
                    if (this[n].Visible)
                        return this[n];
                }

                return null;
            }
        }

        public virtual GarnetTabStripItem FirstVisible
        {
            get
            {
                for (int n = 0; n < Count; n++)
                {
                    if (this[n].Visible)
                        return this[n];
                }

                return null;
            }
        }

        [Browsable(false)]
        public virtual int VisibleCount
        {
            get
            {
                int count = Count, res = 0;
                if (count == 0) return 0;
                for (int n = 0; n < count; n++)
                {
                    if (this[n].Visible) 
                        res++;
                }
                return res;
            }
        }

        #endregion

        #region Methods

        protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        protected virtual void BeginUpdate()
        {
            lockUpdate++;
        }

        protected virtual void EndUpdate()
        {
            if (--lockUpdate == 0)
                OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
        }

        public virtual void AddRange(GarnetTabStripItem[] items)
        {
            BeginUpdate();
            try
            {
                foreach (GarnetTabStripItem item in items)
                {
                    List.Add(item);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        public virtual void Assign(GarnetTabStripItemCollection collection)
        {
            BeginUpdate();
            try
            {
                Clear();
                for (int n = 0; n < collection.Count; n++)
                {
                    GarnetTabStripItem item = collection[n];
                    GarnetTabStripItem newItem = new GarnetTabStripItem();
                    newItem.Assign(item);
                    Add(newItem);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        public virtual int Add(GarnetTabStripItem item)
        {
            int res = IndexOf(item);
            if (res == -1) res = List.Add(item);
            return res;
        }

        public virtual void Remove(GarnetTabStripItem item)
        {
            if (List.Contains(item))
                List.Remove(item);
        }

        public virtual GarnetTabStripItem MoveTo(int newIndex, GarnetTabStripItem item)
        {
            int currentIndex = List.IndexOf(item);
            if (currentIndex >= 0)
            {
                RemoveAt(currentIndex);
                Insert(0, item);

                return item;
            }

            return null;
        }

        public virtual int IndexOf(GarnetTabStripItem item)
        {
            return List.IndexOf(item);
        }

        public virtual bool Contains(GarnetTabStripItem item)
        {
            return List.Contains(item);
        }

        public virtual void Insert(int index, GarnetTabStripItem item)
        {
            if (Contains(item)) return;
            List.Insert(index, item);
        }

        protected override void OnInsertComplete(int index, object item)
        {
            GarnetTabStripItem itm = item as GarnetTabStripItem;
            itm.Changed += new EventHandler(OnItem_Changed);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, item));
        }

        protected override void OnRemove(int index, object item)
        {
            base.OnRemove(index, item);
            GarnetTabStripItem itm = item as GarnetTabStripItem;
            itm.Changed -= new EventHandler(OnItem_Changed);
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, item));
        }

        protected override void OnClear()
        {
            if (Count == 0) return;
            BeginUpdate();
            try
            {
                for (int n = Count - 1; n >= 0; n--)
                {
                    RemoveAt(n);
                }
            }
            finally
            {
                EndUpdate();
            }
        }

        protected virtual void OnItem_Changed(object sender, EventArgs e)
        {
            OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, sender));
        }

        #endregion
    }
}
