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
using System.Drawing.Text;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CallButler.Manager.Controls
{
    public partial class ScheduleControl : UserControl
    {
        public event EventHandler SelectionChanged;

        private const int topHeaderHeight = 15;
        private const int leftHeaderWidth = 15;

        private int topHeaderItemWidth = 0;
        private int leftHeaderItemHeight = 0;
        private StringFormat topHeaderStringFormat;
        private StringFormat leftHeaderStringFormat;
        private Color selectionColor = Color.Blue;
        private Color borderColor = Color.Gray;
        private bool drawOutsideBorder = false;

        private bool bHaveMouse = false;
        private Point ptOriginal = new Point();
        private Point ptLast = new Point();

        private bool[,] selectionMatrix;
        private Rectangle[,] itemMatrix;

        public ScheduleControl()
        {
            selectionMatrix = new bool[24, 7];
            itemMatrix = new Rectangle[24, 7];

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            topHeaderStringFormat = new StringFormat();
            topHeaderStringFormat.Alignment = StringAlignment.Center;
            topHeaderStringFormat.LineAlignment = StringAlignment.Center;

            leftHeaderStringFormat = new StringFormat();
            leftHeaderStringFormat.Alignment = StringAlignment.Center;
            leftHeaderStringFormat.Alignment = StringAlignment.Center;
            leftHeaderStringFormat.Trimming = StringTrimming.Character;
            leftHeaderStringFormat.FormatFlags = StringFormatFlags.DirectionVertical | StringFormatFlags.NoWrap;
        }

        public bool[,] GetSelectionMatrix()
        {
            return selectionMatrix;
        }

        public bool IsSelected(int day, int hour)
        {
            return selectionMatrix[hour, day];
        }

        public void Select(int day, int hour, bool updateView)
        {
            selectionMatrix[hour, day] = true;

            if (updateView)
                this.Invalidate();
        }

        public void UpdateSelectionView()
        {
            this.Invalidate();
        }

        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate(true);
            }
        }

        public bool DrawOutsideBorder
        {
            get
            {
                return drawOutsideBorder;
            }
            set
            {
                drawOutsideBorder = value;
                this.Invalidate();
            }
        }

        public Color SelectionColor
        {
            get
            {
                return selectionColor;
            }
            set
            {
                selectionColor = value;
                this.Invalidate(true);
            }
        }

        public void SelectAll()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                for (int day = 0; day < 7; day++)
                {
                    selectionMatrix[hour, day] = true;
                }
            }

            this.Invalidate();

            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }

        public void SelectNone()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                for (int day = 0; day < 7; day++)
                {
                    selectionMatrix[hour, day] = false;
                }
            }

            this.Invalidate();

            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }

        public void SelectInverse()
        {
            for (int hour = 0; hour < 24; hour++)
            {
                for (int day = 0; day < 7; day++)
                {
                    selectionMatrix[hour, day] = !selectionMatrix[hour, day];
                }
            }

            this.Invalidate();

            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }

        public string SerializeSelection()
        {
            StringBuilder sb = new StringBuilder();

            for (int day = 0; day < 7; day++)
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    sb.Append(Convert.ToInt16(selectionMatrix[hour, day]).ToString());
                }
            }

            return sb.ToString();
        }

        public void DeserializeSelection(string selectionString)
        {
            int hour = 0;
            int day = 0;

            
            // Reset the selection back to zero
            for (hour = 0; hour < 24; hour++)
            {
                for (day = 0; day < 7; day++)
                {
                    selectionMatrix[hour, day] = false;
                }
            }

            hour = 0;
            day = 0;

            foreach (Char hourChar in selectionString)
            {
                if (hour >= 24)
                {
                    hour = 0;
                    day++;
                }

                if (hourChar == '0')
                    selectionMatrix[hour, day] = false;
                else
                    selectionMatrix[hour, day] = true;

                hour++;
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Color brushColor;

            if (this.Enabled)
                brushColor = this.ForeColor;
            else
                brushColor = SystemColors.InactiveCaptionText;

            using (SolidBrush textBrush = new SolidBrush(brushColor))
            {
                Rectangle headerTextRect = new Rectangle(0, 0, topHeaderItemWidth, itemMatrix[0, 0].Top);
                int hour = 12;

                // Draw the hours in the top header
                for (int index = 0; index < 24; index++)
                {
                    headerTextRect.X = topHeaderItemWidth * index + leftHeaderWidth;
                    e.Graphics.DrawString(hour.ToString(), this.Font, textBrush, headerTextRect, topHeaderStringFormat);

                    hour++;

                    if (hour > 12)
                    {
                        hour = 1;
                    }
                }

                headerTextRect = new Rectangle(0, 0, leftHeaderWidth, leftHeaderItemHeight);

                DayOfWeek day = DayOfWeek.Sunday;

                // Draw the days in the left header
                for (int index = 0; index < 7; index++)
                {
                    headerTextRect.Y = index * leftHeaderItemHeight + topHeaderHeight;
                    e.Graphics.DrawString(day.ToString().Substring(0, 3), this.Font, textBrush, headerTextRect, leftHeaderStringFormat);

                    day += 1;
                }
            }

            // Draw each rectangle
            Color penColor;

            if (this.Enabled)
                penColor = this.BorderColor;
            else
                penColor = SystemColors.InactiveBorder;

            using (Pen rectPen = new Pen(penColor))
            {
                Color selectColor;

                if (this.Enabled)
                    selectColor = selectionColor;
                else
                    selectColor = SystemColors.InactiveCaptionText;

                using (SolidBrush selectionBrush = new SolidBrush(selectColor))
                {
                    for (int hour = 0; hour < 24; hour++)
                    {
                        for (int day = 0; day < 7; day++)
                        {
                            // Draw the selection if this is selected
                            if (selectionMatrix[hour, day])
                            {
                                Rectangle selectionRect = itemMatrix[hour, day];

                                //selectionRect.Width -= 3;
                                //selectionRect.Height -= 3;
                                //selectionRect.X += 2;
                                //selectionRect.Y += 2;

                                e.Graphics.FillRectangle(selectionBrush, selectionRect);
                            }

                            // Draw the outline
                            e.Graphics.DrawRectangle(rectPen, itemMatrix[hour, day]);
                        }
                    }
                }

                if (drawOutsideBorder)
                {
                    e.Graphics.DrawRectangle(rectPen, 0, 0, itemMatrix[23, 6].Right, itemMatrix[23, 6].Bottom);
                }
            }
        }

        private void ScheduleControl_Resize(object sender, EventArgs e)
        {
            topHeaderItemWidth = (this.Width - leftHeaderWidth) / 24;
            leftHeaderItemHeight = (this.Height - topHeaderHeight) / 7;

            ResizeItemRects();

            this.Invalidate(true);
        }

        private void ResizeItemRects()
        {
            int rectSize = Math.Min(topHeaderItemWidth, leftHeaderItemHeight);

            for (int hour = 0; hour < 24; hour++)
            {
                for (int day = 0; day < 7; day++)
                {
                    Rectangle rect = new Rectangle(0, 0, rectSize, rectSize);
                    rect.X = topHeaderItemWidth * hour + ((topHeaderItemWidth - rectSize) / 2) + leftHeaderWidth;
                    rect.Y = leftHeaderItemHeight * day + ((leftHeaderItemHeight - rectSize) / 2) + topHeaderHeight;

                    itemMatrix[hour, day] = rect;
                }
            }
        }

        private void ScheduleControl_MouseDown(object sender, MouseEventArgs e)
        {
            // Make a note that we "have the mouse".
            bHaveMouse = true;
            // Store the "starting point" for this rubber-band rectangle.
            ptOriginal.X = e.X;
            ptOriginal.Y = e.Y;
            // Special value lets us know that no previous
            // rectangle needs to be erased.
            ptLast.X = -1;
            ptLast.Y = -1;
        }

        private void ScheduleControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point ptCurrent = new Point(e.X, e.Y);
            // If we "have the mouse", then we draw our lines.
            if (bHaveMouse)
            {
                // If we have drawn previously, draw again in
                // that spot to remove the lines.
                if (ptLast.X != -1)
                {
                    DrawReversibleRectangle(ptOriginal, ptLast);
                }
                // Update last point.
                ptLast = ptCurrent;
                // Draw new lines.
                DrawReversibleRectangle(ptOriginal, ptCurrent);
            }
        }

        private void ScheduleControl_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle selectionRect = Rectangle.FromLTRB(Math.Min(ptOriginal.X, e.X), Math.Min(ptOriginal.Y, e.Y), Math.Max(ptOriginal.X, e.X), Math.Max(ptOriginal.Y, e.Y));
            CalculateMouseSelection(e.Button, selectionRect);

            // Set internal flag to know we no longer "have the mouse".
            bHaveMouse = false;
            // If we have drawn previously, draw again in that spot
            // to remove the lines.
            if (ptLast.X != -1)
            {
                Point ptCurrent = new Point(e.X, e.Y);
                DrawReversibleRectangle(ptOriginal, ptLast);
            }
            // Set flags to know that there is no "previous" line to reverse.
            ptLast.X = -1;
            ptLast.Y = -1;
            ptOriginal.X = -1;
            ptOriginal.Y = -1;
        }

        private void DrawReversibleRectangle(Point p1, Point p2)
        {
            Rectangle rc = new Rectangle();

            // Convert the points to screen coordinates.
            p1 = PointToScreen(p1);
            p2 = PointToScreen(p2);
            // Normalize the rectangle.
            if (p1.X < p2.X)
            {
                rc.X = p1.X;
                rc.Width = p2.X - p1.X;
            }
            else
            {
                rc.X = p2.X;
                rc.Width = p1.X - p2.X;
            }
            if (p1.Y < p2.Y)
            {
                rc.Y = p1.Y;
                rc.Height = p2.Y - p1.Y;
            }
            else
            {
                rc.Y = p2.Y;
                rc.Height = p1.Y - p2.Y;
            }
            // Draw the reversible frame.
            ControlPaint.DrawReversibleFrame(rc,
                Color.White, FrameStyle.Dashed);
        }

        private void CalculateMouseSelection(MouseButtons button, Rectangle selectionRect)
        {

            for (int hour = 0; hour < 24; hour++)
            {
                for (int day = 0; day < 7; day++)
                {
                    if (selectionRect.IntersectsWith(itemMatrix[hour, day]))
                    {
                        if (itemMatrix[hour, day].Contains(selectionRect))
                        {
                            selectionMatrix[hour, day] = !selectionMatrix[hour, day];
                        }
                        else if (button == MouseButtons.Right)
                        {
                            selectionMatrix[hour, day] = false;
                        }
                        else if (button == MouseButtons.Left)
                        {
                            selectionMatrix[hour, day] = true;
                        }
                    }
                }
            }

            this.Invalidate();

            if (SelectionChanged != null)
            {
                SelectionChanged(this, new EventArgs());
            }
        }

        /*private void toolTip_Popup(object sender, PopupEventArgs e)
        {
            Point mousePoint = this.PointToClient(Cursor.Position);

            //toolTip.RemoveAll();

            for (int hour = 0; hour < 24; hour++)
            {
                for (int day = 0; day < 7; day++)
                {
                    if (itemMatrix[hour, day].Contains(mousePoint))
                    {
                        toolTip.SetToolTip(e.AssociatedControl, ((DayOfWeek)day).ToString());
                    }
                }
            }
        }*/
    }
}
