// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: Progressbar.cs 8 2008-03-02 15:54:03Z xiy3x0 $
// ------------------------------------------------------------------------------
/*
* VISTA CONTROLS FOR .NET 2.0
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
* A copy of this license is available at
* http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Pyramid.Garnet.Controls.Aero
{
    [ToolboxBitmap(typeof(ProgressBar))]
    public class ProgressBar:System.Windows.Forms.ProgressBar
    {
        public ProgressBar()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParams = base.CreateParams;
                //Allows for smooth transition even when progressbar value is subtracted
                cParams.Style |= NativeMethods.PBS_SMOOTHREVERSE;
                return cParams;
            }
        }

        public enum States
        {
            Normal, Error, Paused
        }

        private States ps_ = States.Normal;
        [Description("Gets or sets the ProgressBar state."), Category("Appearance"), DefaultValue(States.Normal)]
        public States ProgressState
        {
            get
            {
                return ps_;
            }
            set
            {
                ps_ = value;
                SetState(ps_);
            }
        }

        public void SetState(States State)
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
            //above required for values to be updated properly, but causes a slight flicker
            switch (State)
            {
                case States.Normal:
                    NativeMethods.SendMessage(this.Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
                    break;
                case States.Error:
                    NativeMethods.SendMessage(this.Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_ERROR, 0);
                    break;
                case States.Paused:
                    NativeMethods.SendMessage(this.Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_PAUSED, 0);
                    break;
                //case States.Partial:
                //The blue progressbar is not available
                //    VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, PBST_PARTIAL, 0);
                //    break;
                default:
                    NativeMethods.SendMessage(this.Handle, NativeMethods.PBM_SETSTATE, NativeMethods.PBST_NORMAL, 0);
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case 15:
                    //Paint event
                    SetState(ps_); //Paint the progressbar properly
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
