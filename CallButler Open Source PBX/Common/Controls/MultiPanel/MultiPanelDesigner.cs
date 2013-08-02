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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace Controls.MultiPanel
{
	/// <summary>
	/// Summary description for MultiPanelDesigner.
	/// </summary>
	public class MultiPanelDesigner : ParentControlDesigner
	{
		#region old WndProc
		//		/// <summary>
		//		/// Overrides the handling of Mouse clicks to allow back-next to work in the designer
		//		/// </summary>
		//		/// <param name="msg"></param>
		//		protected override void WndProc(ref Message msg)
		//		{
		//			const int WM_LBUTTONDOWN = 0x0201;
		//			const int WM_LBUTTONDBLCLK = 0x0203;
		//			//When the user left clicks
		//			if (msg.Msg == WM_LBUTTONDOWN || msg.Msg == WM_LBUTTONDBLCLK)
		//			{
		//				// Get the control under the mouse
		//				ISelectionService ss = (ISelectionService)GetService(typeof(ISelectionService));
		//				
		//				if (ss.PrimarySelection is Controls.MultiPanel.MultiPanel)
		//				{
		//					 Controls.MultiPanel.MultiPanel MultiPanel =  (Controls.MultiPanel.MultiPanel) ss.PrimarySelection;
		//					// Extract the mouse position
		//					int xPos = (short)((uint)msg.LParam & 0x0000FFFF);
		//					int yPos = (short)(((uint)msg.LParam & 0xFFFF0000) >> 16);
		//
		//					// Pass on the mouse message
		//					MultiPanel.ClickButtons(msg.HWnd, new Point(xPos, yPos));
		//					
		//					//Don't pass the Message on (i.e. Consume it)
		//					return;
		//				}
		//			}
		//
		//			base.WndProc(ref msg);
		//		}
		#endregion

		/// <summary>
		/// Prevents the grid from being drawn on the MultiPanel
		/// </summary>
		protected override bool DrawGrid
		{
			get 
			{ 
				return base.DrawGrid && _allowGrid;
			}
		}
		private bool _allowGrid = true;

		/// <summary>
		/// Simple way to ensure <see cref="MultiPanelPage"/>s only contained here
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		public override bool CanParent(Control control)
		{
			if (control is MultiPanelPage)
				return true;
			return false;
		}
		public override bool CanParent(ControlDesigner controlDesigner)
		{
			if (controlDesigner is MultiPanelPageDesigner)
				return true;
			return false;
		}


		protected override bool GetHitTest(Point point)
		{
            /*Rectangle rect = Rectangle.FromLTRB(5, 5, Control.Width - 5, Control.Height - 5);

            MessageBox.Show(point.ToString());

            if (rect.Contains(point))
                return false;
            else
			    return true;*/

            return false;
		}

		public override DesignerVerbCollection Verbs
		{
			get
			{
				DesignerVerbCollection verbs = new DesignerVerbCollection();
				verbs.Add(new DesignerVerb("Add Page", new EventHandler(handleAddPage)));

				return verbs;
			}
		}

		private void handleAddPage(object sender, EventArgs e)
		{
			MultiPanel wiz = this.Control as MultiPanel;

			IDesignerHost h  = (IDesignerHost) GetService(typeof(IDesignerHost));
			IComponentChangeService c = (IComponentChangeService) GetService(typeof (IComponentChangeService));

			DesignerTransaction dt = h.CreateTransaction("Add Page");
			MultiPanelPage page = (MultiPanelPage) h.CreateComponent(typeof(MultiPanelPage));
			c.OnComponentChanging(wiz, null);
    
			//Add a new page to the collection
			wiz.Pages.Add(page);
			wiz.Controls.Add(page);
			wiz.ActivatePage(page);

			c.OnComponentChanged(wiz, null, null, null);
			dt.Commit();
		}	

		protected override void OnPaintAdornments(PaintEventArgs pe)
		{
			_allowGrid = false;
			base.OnPaintAdornments (pe);
			_allowGrid = true;

//			if (base.DrawGrid)
//			{
//				MultiPanel wiz = this.Control as MultiPanel;
//				Brush brush = new HatchBrush(HatchStyle.Percent10,SystemColors.ControlText, wiz.BackColor);
//
//				pe.Graphics.FillRectangle(brush,0,0,wiz.Width,8);
//				pe.Graphics.FillRectangle(brush,0,0,8,wiz.pnlButtons.Top);
//				pe.Graphics.FillRectangle(brush,0,wiz.pnlButtons.Top-8,wiz.Width,8);
//				pe.Graphics.FillRectangle(brush,wiz.Width-8,0,8,wiz.pnlButtons.Top);
//			}		
		}

	}
}
