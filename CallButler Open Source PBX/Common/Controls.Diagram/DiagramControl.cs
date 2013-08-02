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
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Controls.Diagram
{
    public partial class DiagramControl : ScrollableControl
    {
        private DiagramShapeControlBase rootShape;
        private DiagramLayoutDirection layout = DiagramLayoutDirection.Vertical;
        private DiagramConnectorType connectorType = DiagramConnectorType.Bezier;
        private bool allowRefresh = true;
        private bool showExpanders = true;

        private bool drawArrows = false;
        private GraphicsPath arrowPath;

        private int childNodeMargin = 50;
        private int peerNodeMargin = 20;

        public DiagramControl()
        {
            InitializeComponent();

            this.AutoScroll = true;
            this.DoubleBuffered = true;

            this.SetAutoScrollMargin(childNodeMargin, childNodeMargin);

            // Create our arrow path
            arrowPath = new GraphicsPath();

            arrowPath.AddLine(0, 0, -5, -3);
            arrowPath.AddLine(-5, -3, -5, 3);
            arrowPath.CloseFigure();
        }

        public DiagramShapeControlBase RootShape
        {
            get
            {
                return rootShape;
            }
            set
            {
                // Clear out all the old controls
                this.Controls.Clear();

                rootShape = value;

                if (rootShape != null)
                {
                    rootShape.Parent = this;
                    rootShape.Visible = true;
                }

                RefreshDiagram();
            }
        }

        public DiagramLayoutDirection LayoutDirection
        {
            get
            {
                return layout;
            }
            set
            {
                layout = value;
                this.AutoScrollPosition = new Point(0, 0);
                RefreshDiagram();
            }
        }

        public DiagramConnectorType ConnectorType
        {
            get
            {
                return connectorType;
            }
            set
            {
                connectorType = value;
                RefreshDiagram();
            }
        }

        public int ChildNodeMargin
        {
            get
            {
                return childNodeMargin;
            }
            set
            {
                childNodeMargin = value;
                this.SetAutoScrollMargin(childNodeMargin, childNodeMargin);
                RefreshDiagram();
            }
        }

        public int PeerNodeMargin
        {
            get
            {
                return peerNodeMargin;
            }
            set
            {
                peerNodeMargin = value;
                RefreshDiagram();
            }
        }

        public bool DrawArrows
        {
            get
            {
                return drawArrows;
            }
            set
            {
                drawArrows = value;
                RefreshDiagram();
            }
        }

        public void RefreshDiagram()
        {
            Size workingSize = new Size(this.ClientSize.Width - childNodeMargin * 2, this.ClientSize.Height - childNodeMargin * 2);

            if (allowRefresh && rootShape != null)
            {
                int maxChildSize = 0;

                if (layout == DiagramLayoutDirection.Vertical)
                {
                    maxChildSize = GetMaxExpandedChildNodesSize(rootShape, layout, rootShape.Width);

                    rootShape.Top = childNodeMargin + AutoScrollPosition.Y;

                    if (workingSize.Width < maxChildSize)
                        rootShape.Left = (maxChildSize - rootShape.Width) / 2 + childNodeMargin + AutoScrollPosition.X;
                    else
                        rootShape.Left = (workingSize.Width - rootShape.Width) / 2 + childNodeMargin + AutoScrollPosition.X;
                        
                    LayoutChildShapes(rootShape, layout);
                }
                else if (layout == DiagramLayoutDirection.Horizontal)
                {
                    maxChildSize = GetMaxExpandedChildNodesSize(rootShape, layout, rootShape.Height);

                    rootShape.Left = childNodeMargin + AutoScrollPosition.X;

                    if (workingSize.Height < maxChildSize)
                        rootShape.Top = (maxChildSize - rootShape.Height) / 2 + childNodeMargin + AutoScrollPosition.Y;
                    else
                        rootShape.Top = (workingSize.Height - rootShape.Height) / 2 + childNodeMargin + AutoScrollPosition.Y;
                    
                    LayoutChildShapes(rootShape, layout);
                }
            }
        }

        private void LayoutChildShapes(DiagramShapeControlBase parentShape, DiagramLayoutDirection direction)
        {
            allowRefresh = false;
            this.SuspendLayout();

            if (parentShape != null && parentShape.Expanded)
            {
                int childPos = 0;

                foreach (DiagramShapeControlBase childShape in parentShape.childShapes)
                {
                    int childSize = 0;

                    // First of all, get the maximum size of the all the expanded child nodes
                    if (direction == DiagramLayoutDirection.Vertical)
                    {
                            childSize = GetMaxExpandedChildNodesSize(childShape, direction, childShape.Width);
                            childShape.newLocation = new Point(childPos + (childSize - childShape.Width) / 2, parentShape.Bottom + childNodeMargin);
                    }
                    else if (direction == DiagramLayoutDirection.Horizontal)
                    {
                            childSize = GetMaxExpandedChildNodesSize(childShape, direction, childShape.Height);
                            childShape.newLocation = new Point(parentShape.Right + childNodeMargin, childPos + (childSize - childShape.Height) / 2);
                    }

                    childPos += childSize + peerNodeMargin;
                }

                childPos -= peerNodeMargin;

                int shift = 0;

                if (direction == DiagramLayoutDirection.Vertical)
                    shift = parentShape.Left + parentShape.Width / 2 - childPos / 2;//(this.Width - childPos) / 2 - (rootShape.Left - parentShape.Left);
                else if (direction == DiagramLayoutDirection.Horizontal)
                    shift = parentShape.Top + parentShape.Height / 2 - childPos / 2;//(this.Width - childPos) / 2 - (rootShape.Left - parentShape.Left);

                // Now center our child nodes
                foreach (DiagramShapeControlBase childShape in parentShape.childShapes)
                {
                    if (direction == DiagramLayoutDirection.Vertical)
                        childShape.newLocation.Offset(shift, 0);
                    else if (direction == DiagramLayoutDirection.Horizontal)
                        childShape.newLocation.Offset(0, shift);

                    childShape.Location = childShape.newLocation;
                    childShape.Visible = true;

                    LayoutChildShapes(childShape, direction);
                }
            }

            if (parentShape != null)
            {
                // Update our expander
                if (direction == DiagramLayoutDirection.Vertical)
                {
                    parentShape.expander.Left = parentShape.Left + (parentShape.Width - parentShape.expander.Width) / 2;
                    parentShape.expander.Top = parentShape.Bottom;
                }
                else if (direction == DiagramLayoutDirection.Horizontal)
                {
                    parentShape.expander.Left = parentShape.Right;
                    parentShape.expander.Top = parentShape.Top + (parentShape.Height - parentShape.expander.Height) / 2;
                }
            }

            this.ResumeLayout(true);

            this.Invalidate(false);
            allowRefresh = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // Draw our connectors
            if (rootShape != null)
            {
                using (Pen connectorPen = new Pen(this.ForeColor))
                {
                    using (SolidBrush connectorBrush = new SolidBrush(this.ForeColor))
                    {
                        DrawConnectors(rootShape, e.Graphics, connectorPen, connectorBrush, layout, connectorType);
                    }
                }
            }
        }

        public void ExpandAll()
        {
            allowRefresh = false;

            foreach (Control control in this.Controls)
            {
                DiagramShapeControlBase shapeControl = control as DiagramShapeControlBase;

                if (shapeControl != null)
                {
                    shapeControl.Expanded = true;
                }
            }

            allowRefresh = true;
            RefreshDiagram();
        }

        public void CollapseAll()
        {
            allowRefresh = false;

            foreach (Control control in this.Controls)
            {
                DiagramShapeControlBase shapeControl = control as DiagramShapeControlBase;

                if (shapeControl != null)
                {
                    shapeControl.Expanded = false;
                }
            }

            allowRefresh = true;
            RefreshDiagram();
        }

        public bool ShowExpanders
        {
            get
            {
                return showExpanders;
            }
            set
            {
                showExpanders = value;

                allowRefresh = false;
                foreach (Control control in this.Controls)
                {
                    DiagramShapeControlBase shapeControl = control as DiagramShapeControlBase;

                    if (shapeControl != null)
                    {
                        shapeControl.UpdateExpander();
                    }
                }

                allowRefresh = true;
                RefreshDiagram();
            }
        }

        private void DrawConnectors(DiagramShapeControlBase parentShape, Graphics g, Pen pen, Brush brush, DiagramLayoutDirection direction, DiagramConnectorType connectorType)
        {
            if (parentShape.Expanded)
            {
                Point startPoint = new Point();
                Point endPoint = new Point();

                if (showExpanders)
                {
                    startPoint.X = parentShape.expander.Left + parentShape.expander.Width / 2;
                    startPoint.Y = parentShape.expander.Top + parentShape.expander.Height / 2;
                }
                else
                {
                    if (direction == DiagramLayoutDirection.Vertical)
                    {
                        startPoint.X = parentShape.Left + parentShape.Width / 2;
                        startPoint.Y = parentShape.Bottom;
                    }
                    else if (direction == DiagramLayoutDirection.Horizontal)
                    {
                        startPoint.Y = parentShape.Top + parentShape.Height / 2;
                        startPoint.X = parentShape.Right;
                    }
                }

                foreach (DiagramShapeControlBase shape in parentShape.childShapes)
                {
                    if (direction == DiagramLayoutDirection.Vertical)
                    {
                        endPoint.Y = shape.Top;
                        endPoint.X = shape.Left + (shape.Width / 2);
                    }
                    else if (direction == DiagramLayoutDirection.Horizontal)
                    {
                        endPoint.Y = shape.Top + (shape.Height / 2);
                        endPoint.X = shape.Left;
                    }

                    switch (connectorType)
                    {
                        case DiagramConnectorType.Standard:
                            g.DrawLine(pen, startPoint, endPoint);

                            break;

                        case DiagramConnectorType.Bezier:
                            Point pt1 = Point.Empty;
                            Point pt2 = Point.Empty;

                            if (direction == DiagramLayoutDirection.Vertical)
                            {
                                pt1 = new Point(startPoint.X, startPoint.Y + 20);
                                pt2 = new Point(endPoint.X, endPoint.Y - 20);
                            }
                            else if (direction == DiagramLayoutDirection.Horizontal)
                            {
                                pt1 = new Point(startPoint.X + 20, startPoint.Y);
                                pt2 = new Point(endPoint.X - 20, endPoint.Y);
                            }

                            g.DrawBezier(pen, startPoint, pt1, pt2, endPoint);

                            break;
                    }

                    // Draw arrows
                    if (drawArrows)
                    {
                        Point anglePoint = new Point(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
                        float angle = (float)(Math.Atan2(anglePoint.Y, anglePoint.X) * (180.0 / Math.PI));

                        if (connectorType == DiagramConnectorType.Bezier)
                        {
                            if (direction == DiagramLayoutDirection.Horizontal)
                                angle *= 0.5f;
                            else
                                angle = 90.0f + (angle - 90.0f) * 0.5f;
                        }

                        g.TranslateTransform(endPoint.X, endPoint.Y);
                        g.RotateTransform(angle);

                        g.FillPath(brush, arrowPath);

                        g.ResetTransform();
                    }

                    DrawConnectors(shape, g, pen, brush, direction, connectorType);
                }
            }
        }

        private int GetMaxExpandedChildNodesSize(DiagramShapeControlBase parentShape, DiagramLayoutDirection direction, int maxExpandedSize)
        {
            // If the parent node is expanded get the number of child nodes to this node
            int maxChildSize = 0;

            if (parentShape.Expanded)
            {
                foreach (DiagramShapeControlBase childShape in parentShape.childShapes)
                {
                    if (direction == DiagramLayoutDirection.Vertical)
                        maxChildSize += GetMaxExpandedChildNodesSize(childShape, direction, childShape.Width);
                    else if (direction == DiagramLayoutDirection.Horizontal)
                        maxChildSize += GetMaxExpandedChildNodesSize(childShape, direction, childShape.Height);

                    maxChildSize += peerNodeMargin;
                }

                maxChildSize -= peerNodeMargin;
            }

            return Math.Max(maxChildSize, maxExpandedSize);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RefreshDiagram();
        }

        public void AddRandomShape(DiagramShapeControlBase shape)
        {
            Random rand = new Random();

            int shapeControls = 0;

            foreach (Control control in Controls)
            {
                if (control is DiagramShapeControlBase)
                    shapeControls++;
            }

            int randomShapeIndex = rand.Next(shapeControls);
            shapeControls = 0;

            foreach (Control control in Controls)
            {
                if (control is DiagramShapeControlBase)
                {
                    if (shapeControls == randomShapeIndex)
                    {
                        ((DiagramShapeControlBase)control).AddChildShape(shape);
                        return;
                    }

                    shapeControls++;
                }
            }
        }
    }
}
