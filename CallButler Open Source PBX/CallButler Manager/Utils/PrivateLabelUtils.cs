///////////////////////////////////////////////////////////////////////////////////////////////
//
//    This File is Part of the CallButler Open Source PBX (http://www.codeplex.com/callbutler
//
//    Copyright (c) 2005-2008, Jim Heising
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without modification,
//    are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice,
//      this list of conditions and the following disclaimer.
//
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation and/or
//      other materials provided with the distribution.
//
//    * Neither the name of Jim Heising nor the names of its contributors may be
//      used to endorse or promote products derived from this software without specific prior
//      written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
//    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
//    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
//    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
//    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//    POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CallButler.Manager.Utils
{
    class PrivateLabelUtils
    {
        private static string baseName = "CallButler";

        public static string ProductName = null;

        public static void ReplaceProductNameControl(object control)
        {
            if (ProductName != null && ProductName.Length > 0)
            {
                try
                {
                    if (control is MenuStrip)
                    {
                        ((MenuStrip)control).Text = ReplaceProductName(((MenuStrip)control).Text);

                        foreach (ToolStripMenuItem childControl in ((MenuStrip)control).Items)
                        {
                            ReplaceProductNameControl(childControl);
                        }
                    }
                    else if (control is ToolStripMenuItem)
                    {
                        ((ToolStripMenuItem)control).Text = ReplaceProductName(((ToolStripMenuItem)control).Text);

                        foreach (ToolStripItem childControl in ((ToolStripMenuItem)control).DropDownItems)
                        {
                            ReplaceProductNameControl(childControl);
                        }
                    }
                    else if (control is global::Controls.SmoothLabel)
                    {
                        ((global::Controls.SmoothLabel)control).HelpText = ReplaceProductName(((global::Controls.SmoothLabel)control).HelpText);
                        ((global::Controls.SmoothLabel)control).HelpTitle = ReplaceProductName(((global::Controls.SmoothLabel)control).HelpTitle);
                    }
                    else if (control is ViewControls.ViewControlBase)
                    {
                        ((ViewControls.ViewControlBase)control).HeaderTitle = ReplaceProductName(((ViewControls.ViewControlBase)control).HeaderTitle);
                        ((ViewControls.ViewControlBase)control).HeaderCaption = ReplaceProductName(((ViewControls.ViewControlBase)control).HeaderCaption);
                        ((ViewControls.ViewControlBase)control).HelpRTFText = ReplaceProductName(((ViewControls.ViewControlBase)control).HelpRTFText);

                        foreach (Control childControl in ((Control)control).Controls)
                        {
                            ReplaceProductNameControl(childControl);
                        }
                    }
                    /*else if (control is TextBox)
                    {
                    }*/
                    else if (control is Control)
                    {
                        ((Control)control).Text = ReplaceProductName(((Control)control).Text);

                        foreach (Control childControl in ((Control)control).Controls)
                        {
                            ReplaceProductNameControl(childControl);
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        public static void ReplaceProductName(ref string input)
        {
            if (ProductName != null && ProductName.Length > 0)
            {
                input = System.Text.RegularExpressions.Regex.Replace(input, baseName, ProductName);
            }
        }

        public static string ReplaceProductName(string input)
        {
            if (ProductName != null && ProductName.Length > 0)
            {
                return System.Text.RegularExpressions.Regex.Replace(input, baseName, ProductName);
            }

            return input;
        }
    }
}
