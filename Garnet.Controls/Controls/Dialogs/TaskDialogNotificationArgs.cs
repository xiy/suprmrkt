// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  Mozilla Public License 1.1 (http://www.mozilla.org/MPL/MPL-1.1.html)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: TaskDialogNotificationArgs.cs 8 2008-03-02 15:54:03Z xiy3x0 $
//
// @title     Garnet Clarity Dialogs for Windows Vista
// @summary   An interop library for managed usage of the Comctl v6
//            dialogs in Windows Vista.
// 
// @credit    Thanks go to Kevin Gre, who originally wrote this great library.
// @links     http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=9659B1CA-27AA-45C9-8589-10536B9355C9
// 
//------------------------------------------------------------------

namespace Pyramid.Garnet.Controls.Dialogs
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Arguments passed to the TaskDialog callback.
    /// </summary>
    public class TaskDialogNotificationArgs
    {
        /// <summary>
        /// What the TaskDialog callback is a notification of.
        /// </summary>
        private TaskDialogNotification notification;

        /// <summary>
        /// The button ID if the notification is about a button. This a DialogResult
        /// value or the ButtonID member of a TaskDialogButton set in the
        /// TaskDialog.Buttons or TaskDialog.RadioButtons members.
        /// </summary>
        private int buttonId;

        /// <summary>
        /// The HREF string of the hyperlink the notification is about.
        /// </summary>
        private string hyperlink;

        /// <summary>
        /// The number of milliseconds since the dialog was opened or the last time the
        /// callback for a timer notification reset the value by returning true.
        /// </summary>
        private uint timerTickCount;

        /// <summary>
        /// The state of the verification flag when the notification is about the verification flag.
        /// </summary>
        private bool verificationFlagChecked;

        /// <summary>
        /// The state of the dialog expando when the notification is about the expando.
        /// </summary>
        private bool expanded;

        /// <summary>
        /// What the TaskDialog callback is a notification of.
        /// </summary>
        public TaskDialogNotification Notification
        {
            get { return this.notification; }
            set { this.notification = value; }
        }

        /// <summary>
        /// The button ID if the notification is about a button. This a DialogResult
        /// value or the ButtonID member of a TaskDialogButton set in the
        /// TaskDialog.Buttons member.
        /// </summary>
        public int ButtonId
        {
            get { return this.buttonId; }
            set { this.buttonId = value; }
        }

        /// <summary>
        /// The HREF string of the hyperlink the notification is about.
        /// </summary>
        public string Hyperlink
        {
            get { return this.hyperlink; }
            set { this.hyperlink = value; }
        }

        /// <summary>
        /// The number of milliseconds since the dialog was opened or the last time the
        /// callback for a timer notification reset the value by returning true.
        /// </summary>
        [CLSCompliant(false)]
        public uint TimerTickCount
        {
            get { return this.timerTickCount; }
            set { this.timerTickCount = value; }
        }

        /// <summary>
        /// The state of the verification flag when the notification is about the verification flag.
        /// </summary>
        public bool VerificationFlagChecked
        {
            get { return this.verificationFlagChecked; }
            set { this.verificationFlagChecked = value; }
        }

        /// <summary>
        /// The state of the dialog expando when the notification is about the expando.
        /// </summary>
        public bool Expanded
        {
            get { return this.expanded; }
            set { this.expanded = value; }
        }
    }
}
