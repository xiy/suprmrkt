// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: ListView.cs 8 2008-03-02 15:54:03Z xiy3x0 $
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
    [ToolboxBitmap(typeof(ListView))]
    public class ListView : System.Windows.Forms.ListView
    {
        //Imports the UXTheme DLL
        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);
        private Boolean elv = false;

        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case 15:
                    //Paint event
                    if (!elv)
                    {
                        //1-time run needed
                        SetWindowTheme(this.Handle, "explorer", null); //Explorer style
                        NativeMethods.SendMessage(this.Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, NativeMethods.LVS_EX_DOUBLEBUFFER, NativeMethods.LVS_EX_DOUBLEBUFFER); //Blue selection, keeps other extended styles
                        elv = true;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
