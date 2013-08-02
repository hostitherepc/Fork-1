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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CallButler.Manager.Controls
{
  public partial class CallFlowItem : global::Controls.Diagram.DiagramShapeControlBase
  {
    public CallFlowItem()
    {
      InitializeComponent();

      UpdateControl();
    }

    private void UpdateControl()
    {
      this.SuspendLayout();

      int newHeight = pnlHeader.Height + roundedCornerPanel1.ShadowOffset + roundedCornerPanel1.Padding.Top + roundedCornerPanel1.Padding.Bottom;

      if (pnlActions.Controls.Count > 0)
      {
        dividerLine1.Visible = true;
        newHeight += dividerLine1.Height + 16;
      }
      else
      {
        dividerLine1.Visible = false;
      }

      if (lblCaption.Text != null && lblCaption.Text.Length > 0)
      {
        newHeight += lblCaption.Height;
        lblCaption.Visible = true;
      }
      else
      {
        lblCaption.Visible = false;
      }

      this.Height = newHeight;
      lblCaption.Invalidate(true);

      this.ResumeLayout(true);
    }

    public Image Icon
    {
      get
      {
        return pbIcon.Image;
      }
      set
      {
        pbIcon.Image = value;
      }
    }

    public string Title
    {
      get
      {
        return lblTitle.Text;
      }
      set
      {
        lblTitle.Text = value;
      }
    }

    public string Caption
    {
      get
      {
        return lblCaption.Text;
      }
      set
      {
        lblCaption.Text = value;
        UpdateControl();
      }
    }

    public Control AddActionControl(Control control)
    {
      pnlActions.Controls.Add(control);

      UpdateControl();

      return control;
    }
  }
}
