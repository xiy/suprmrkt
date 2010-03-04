// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: ComboBox.cs 8 2008-03-02 15:54:03Z xiy3x0 $
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
using System.ComponentModel;

namespace Pyramid.Garnet.Controls.Aero
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class ComboBox : System.Windows.Forms.ComboBox
    {
        private string cueBannerText_ = string.Empty;
        [Description("Gets or sets the cue text that is displayed on a ComboBox control."), Category("Appearance"), DefaultValue("")]
        public string CueBannerText
        {
            get
            {
                return cueBannerText_;
            }
            set
            {
                cueBannerText_ = value;
                this.SetCueText();
            }
        }

        private void SetCueText()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.CB_SETCUEBANNER, IntPtr.Zero, cueBannerText_);
        }

        public ComboBox()
        {
            this.FlatStyle = FlatStyle.System;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
