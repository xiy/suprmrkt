// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  Mozilla Public License 1.1 (http://www.mozilla.org/MPL/MPL-1.1.html)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: TaskDialogCommonDialog.cs 8 2008-03-02 15:54:03Z xiy3x0 $
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
    using System.Windows.Forms;

    /// <summary>
    /// TaskDialog wrapped in a CommonDialog class. This is required to work well in
    /// MMC 3.0. In MMC 3.0 you must use the ShowDialog methods on the MMC classes to
    /// correctly show a modal dialog. This class will allow you to do this and keep access
    /// to the results of the TaskDialog.
    /// </summary>
    public class TaskDialogCommonDialog : CommonDialog
    {
        /// <summary>
        /// The TaskDialog we will display.
        /// </summary>
        private TaskDialog taskDialog;

        /// <summary>
        /// The result of the dialog, either a DialogResult value for common push buttons set in the TaskDialog.CommonButtons
        /// member or the ButtonID from a TaskDialogButton structure set on the TaskDialog.Buttons member.
        /// </summary>
        private int taskDialogResult;

        /// <summary>
        /// The verification flag result of the dialog. True if the verification checkbox was checked when the dialog
        /// was dismissed.
        /// </summary>
        private bool verificationFlagCheckedResult;

        /// <summary>
        /// TaskDialog wrapped in a CommonDialog class. THis is required to work well in
        /// MMC 2.1. In MMC 2.1 you must use the ShowDialog methods on the MMC classes to
        /// correctly show a modal dialog. This class will allow you to do this and keep access
        /// to the results of the TaskDialog.
        /// </summary>
        /// <param name="taskDialog">The TaskDialog to show.</param>
        public TaskDialogCommonDialog(TaskDialog taskDialog)
        {
            if (taskDialog == null)
            {
                throw new ArgumentNullException("taskDialog");
            }

            this.taskDialog = taskDialog;
        }

        /// <summary>
        /// The TaskDialog to show.
        /// </summary>
        public TaskDialog TaskDialog
        {
            get { return this.taskDialog; }
        }

        /// <summary>
        /// The result of the dialog, either a DialogResult value for common push buttons set in the TaskDialog.CommonButtons
        /// member or the ButtonID from a TaskDialogButton structure set on the TaskDialog.Buttons member.
        /// </summary>
        public int TaskDialogResult
        {
            get { return this.taskDialogResult; }
        }

        /// <summary>
        /// The verification flag result of the dialog. True if the verification checkbox was checked when the dialog
        /// was dismissed.
        /// </summary>
        public bool VerificationFlagCheckedResult
        {
            get { return this.verificationFlagCheckedResult; }
        }

        /// <summary>
        /// Reset the common dialog.
        /// </summary>
        public override void Reset()
        {
            this.taskDialog.Reset();
        }

        /// <summary>
        /// The required implementation of CommonDialog that shows the Task Dialog.
        /// </summary>
        /// <param name="hwndOwner">Owner window. This can be null.</param>
        /// <returns>If this method returns true, then ShowDialog will return DialogResult.OK.
        /// If this method returns false, then ShowDialog will return DialogResult.Cancel. The
        /// user of this class must use the TaskDialogResult member to get more information.
        /// </returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            this.taskDialogResult = this.taskDialog.Show(hwndOwner, out this.verificationFlagCheckedResult);
            return (this.taskDialogResult != (int)DialogResult.Cancel);
        }
    }
}
