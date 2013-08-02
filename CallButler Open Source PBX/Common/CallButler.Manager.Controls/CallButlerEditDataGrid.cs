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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace CallButler.Manager.Controls
{
    public class DataRowEventArgs : EventArgs
    {
        public DataRow DataRow = null;

        public DataRowEventArgs(DataRow dataRow)
        {
            DataRow = dataRow;
        }
    }

    public class CallButlerEditDataGrid : CallButlerDataGrid
    {
        public event EventHandler<DataRowEventArgs> EditDataRow;
        public event EventHandler<DataRowEventArgs> DeleteDataRow;

        private DataGridViewLinkColumn editColumn = null;
        private DataGridViewLinkColumn deleteColumn = null;
        private DataGridViewImageColumn rowImageColumn = null;
        private Image rowImage = null;

        private bool showEditColumn = true;
        private bool showDeleteColumn = true;

        public CallButlerEditDataGrid()
        {
            this.MultiSelect = false;
        }

        protected override void InitLayout()
        {
            base.InitLayout();

            if (!DesignMode)
            {
                editColumn = new DataGridViewLinkColumn();
                editColumn.ActiveLinkColor = Color.RoyalBlue;
                editColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                editColumn.HeaderText = "";
                editColumn.LinkColor = Color.RoyalBlue;
                editColumn.TrackVisitedState = false;
                editColumn.UseColumnTextForLinkValue = true;
                editColumn.LinkBehavior = LinkBehavior.HoverUnderline;
                editColumn.Text = Properties.LocalizedStrings.Common_Edit;


                deleteColumn = new DataGridViewLinkColumn();
                deleteColumn.ActiveLinkColor = Color.RoyalBlue;
                deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                deleteColumn.HeaderText = "";
                deleteColumn.LinkColor = Color.RoyalBlue;
                deleteColumn.TrackVisitedState = false;
                deleteColumn.UseColumnTextForLinkValue = true;
                deleteColumn.LinkBehavior = LinkBehavior.HoverUnderline;
                deleteColumn.Text = Properties.LocalizedStrings.Common_Delete;

                this.Columns.Add(editColumn);
                this.Columns.Add(deleteColumn);

                this.ColumnAdded += new DataGridViewColumnEventHandler(CallButlerEditDataGrid_ColumnAdded);
            }
        }

        public bool ShowEditColumn
        {
            get
            {
                return showEditColumn;
            }
            set
            {
                showEditColumn = value;

                if(editColumn != null)
                    editColumn.Visible = value;
            }
        }

        public bool ShowDeleteColumn
        {
            get
            {
                return showDeleteColumn;
            }
            set
            {
                showDeleteColumn = value;

                if(deleteColumn != null)
                    deleteColumn.Visible = value;
            }
        }

        [TypeConverter(typeof(ImageConverter)), DefaultValue(typeof(Image), null), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image RowImage
        {
            get
            {
                return rowImage;
            }
            set
            {
                rowImage = value;

                if (!DesignMode)
                {
                    if (rowImage == null && rowImageColumn != null)
                        this.Columns.Remove(rowImageColumn);

                    rowImageColumn = null;

                    if (rowImage != null)
                    {
                        rowImageColumn = new DataGridViewImageColumn(false);
                        rowImageColumn.DisplayIndex = 0;
                        rowImageColumn.Image = rowImage;
                        rowImageColumn.Width = this.RowTemplate.Height;

                        this.Columns.Add(rowImageColumn);
                    }
                }
            }
        }

        void CallButlerEditDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if(rowImageColumn != null)
                rowImageColumn.DisplayIndex = 0;

            editColumn.DisplayIndex = this.ColumnCount - 2;
            deleteColumn.DisplayIndex = this.ColumnCount - 1;
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == editColumn.Index)
            {
                if(EditDataRow != null)
                    EditDataRow(this, new DataRowEventArgs(((DataRowView)this.Rows[e.RowIndex].DataBoundItem).Row));
            }
            else if (e.ColumnIndex == deleteColumn.Index)
            {
                if (DeleteDataRow != null)
                    DeleteDataRow(this, new DataRowEventArgs(((DataRowView)this.Rows[e.RowIndex].DataBoundItem).Row));
            }
            else
            {
                base.OnCellContentClick(e);
            }
        }

        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (EditDataRow != null && e.RowIndex >= 0 && e.RowIndex < this.Rows.Count)
                EditDataRow(this, new DataRowEventArgs(((DataRowView)this.Rows[e.RowIndex].DataBoundItem).Row));

            base.OnCellDoubleClick(e);
        }
    }
}
