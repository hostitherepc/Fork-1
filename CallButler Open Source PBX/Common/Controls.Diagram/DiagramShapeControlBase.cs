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

namespace Controls.Diagram
{
    public partial class DiagramShapeControlBase : UserControl
    {
        internal List<DiagramShapeControlBase> childShapes;

        private DiagramShapeControlBase parentShape;

        internal bool expanded = false;
        internal PictureBox expander;
        internal Point newLocation;

        private Image expanderImagePlus = Properties.Resources.plus;
        private Image expanderImageMinus = Properties.Resources.minus;

        public DiagramShapeControlBase()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            childShapes = new List<DiagramShapeControlBase>();

            expander = new PictureBox();
            expander.SizeMode = PictureBoxSizeMode.AutoSize;
            expander.BackColor = Color.Transparent;
            expander.Click += new EventHandler(expander_Click);
            expander.Cursor = Cursors.Hand;
            expander.DoubleClick += new EventHandler(expander_Click);
            
            UpdateExpander();
        }

        void expander_Click(object sender, EventArgs e)
        {
            this.Expanded = !this.Expanded;
        }

        [Browsable(false)]
        public bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                expanded = value;

                foreach (DiagramShapeControlBase shape in childShapes)
                {
                    shape.Visible = expanded;
                }

                UpdateExpander();

                if (childShapes.Count > 0 && Parent is DiagramControl)
                    ((DiagramControl)Parent).RefreshDiagram();
            }
        }

        [TypeConverter(typeof(ImageConverter)), DefaultValue(typeof(Image), null), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image ExpanderImagePlus
        {
            get
            {
                return expanderImagePlus;
            }
            set
            {
                expanderImagePlus = value;
                UpdateExpander();
            }
        }

        [TypeConverter(typeof(ImageConverter)), DefaultValue(typeof(Image), null), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image ExpanderImageMinus
        {
            get
            {
                return expanderImageMinus;
            }
            set
            {
                expanderImageMinus = value;
                UpdateExpander();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (!this.Visible)
            {
                foreach (DiagramShapeControlBase shape in childShapes)
                {
                    shape.Visible = false;
                }
            }

            UpdateExpander();
        }

        public DiagramShapeControlBase AddChildShape(DiagramShapeControlBase childShape)
        {
            // If the child already belongs to another parent, make sure you remove it from that parent
            if (childShape.parentShape != null)
            {
                childShape.ParentShape.RemoveChildShape(childShape);
            }
            
            this.childShapes.Add(childShape);
            childShape.parentShape = this;            

            AddShapeToDiagram(childShape);

            UpdateExpander();

            if (Parent is DiagramControl)
                ((DiagramControl)Parent).RefreshDiagram();

            return childShape;
        }

        public DiagramShapeControlBase InsertChildShape(int index, DiagramShapeControlBase childShape)
        {
            // If the child already belongs to another parent, make sure you remove it from that parent
            if (childShape.parentShape != null)
            {
                childShape.ParentShape.RemoveChildShape(childShape);
            }

            this.childShapes.Insert(index, childShape);
            childShape.parentShape = this;

            AddShapeToDiagram(childShape);

            UpdateExpander();

            if (Parent is DiagramControl)
                ((DiagramControl)Parent).RefreshDiagram();

            return childShape;
        }

        private void AddShapeToDiagram(DiagramShapeControlBase shape)
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Add(shape);

                foreach (DiagramShapeControlBase childShape in shape.childShapes)
                {
                    AddShapeToDiagram(childShape);
                }
            }
        }

        public DiagramShapeControlBase[] ChildShapes
        {
            get
            {
                return childShapes.ToArray();
            }
        }

        public void RemoveChildShape(DiagramShapeControlBase childShape)
        {
            if (this.childShapes.Contains(childShape))
            {
                RemoveShapeFromDiagram(childShape);

                this.childShapes.Remove(childShape);
                childShape.parentShape = null;

                UpdateExpander();

                if (Parent is DiagramControl)
                    ((DiagramControl)Parent).RefreshDiagram();
            }
        }

        private void RemoveShapeFromDiagram(DiagramShapeControlBase shape)
        {
            if (this.Parent != null)
            {
                this.Parent.Controls.Remove(shape);

                foreach (DiagramShapeControlBase childShape in shape.childShapes)
                {
                    RemoveShapeFromDiagram(childShape);
                }
            }
        }

        public void ClearChildShapes()
        {
            while(childShapes.Count > 0)
            {
                childShapes[0].ClearChildShapes();
                this.Parent.Controls.Remove(childShapes[0]);
                childShapes.RemoveAt(0);
            }

            if (Parent is DiagramControl)
                ((DiagramControl)Parent).RefreshDiagram();
        }

        [Browsable(false)]
        public DiagramShapeControlBase ParentShape
        {
            get
            {
                return parentShape;
            }
        }

        internal void UpdateExpander()
        {          
            expander.Parent = this.Parent;

            if (Parent is DiagramControl && !((DiagramControl)Parent).ShowExpanders)
            {
                expander.Visible = false;
            }
            else if (this.Visible && this.childShapes.Count > 0)
            {
                if (expanded)
                    expander.Image = expanderImageMinus;
                else
                    expander.Image = expanderImagePlus;

                expander.Visible = true;
            }
            else
                expander.Visible = false;
        }
    }
}
