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
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace CallButler.Manager.Utils
{
    public class ReportPrinter
    {
        private int currentPageIndex;
        private List<Stream> streams;
        private IWin32Window parent;
        private LocalReport report;

        public delegate void PrintPageSelectionEventHandler(object source, PrintPageEventArgs e);

        public event PrintPageSelectionEventHandler OnPrintPageSelectionEvent;

        public ReportPrinter(string dataSourceName, object dataSource, string embeddedResource, IWin32Window parent)
        {
            this.parent = parent;


            Report = new LocalReport();
            ReportDataSource r = new ReportDataSource();

            r.Name = dataSourceName;
            r.Value = dataSource;

            report.DataSources.Add(r);
            report.ReportEmbeddedResource = embeddedResource;

            CurrentPageIndex = 0;
            Export(report);
        }

        public ReportPrinter(LocalReport report)
        {
            Report = report;
            CurrentPageIndex = 0;
            Export(report);
        }

        private LocalReport Report
        {
            get
            {
                return report;
            }
            set
            {
                report = value;
            }
        }

        public object DataSource
        {
            get
            {
                if (Report.DataSources.Count > 0)
                {
                    return Report.DataSources[0];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (Report.DataSources.Count > 0)
                {
                    Report.DataSources[0].Value = value;
                }
                Export(Report);
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.PageSettings.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                PrintPageSelectionEventHandler eh = OnPrintPageSelectionEvent;
                if (eh != null)
                {
                    OnPrintPageSelectionEvent(this, e);
                }
            }
            else
            {
                FinalizePrint(e);
            }
        }

        private int CurrentPageIndex
        {
            get
            {
                return currentPageIndex;
            }
            set
            {
                currentPageIndex = value;
            }
        }
        private List<Stream> Streams
        {
            get
            {
                if (streams == null)
                {
                    streams = new List<Stream>();
                }
                return streams;
            }
            set
            {
                streams = value;
            }
        }

        public void FinalizePrint(PrintPageEventArgs e)
        {
            Metafile pageImage = new Metafile(Streams[CurrentPageIndex]);
            e.Graphics.DrawImage(pageImage, e.PageBounds);
            CurrentPageIndex++;
            e.HasMorePages = (CurrentPageIndex < Streams.Count);
        }

        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            Streams.Add(stream);
            return stream;
        }

        // Export the given report as an EMF (Enhanced Metafile) file.
        private void Export(LocalReport report)
        {
            StringBuilder deviceInfo = new StringBuilder();
            deviceInfo.Append("<DeviceInfo>");
            deviceInfo.Append("  <OutputFormat>EMF</OutputFormat>");
            deviceInfo.Append("  <PageWidth>8.5in</PageWidth>");
            deviceInfo.Append("  <PageHeight>11in</PageHeight>");
            deviceInfo.Append("  <MarginTop>0.25in</MarginTop>");
            deviceInfo.Append("  <MarginLeft>0.25in</MarginLeft>");
            deviceInfo.Append("  <MarginRight>0.25in</MarginRight>");
            deviceInfo.Append("  <MarginBottom>0.25in</MarginBottom>");
            deviceInfo.Append("</DeviceInfo>");
            string di = deviceInfo.ToString();

            Warning[] warnings;
            Streams = new List<Stream>();
            report.Render("Image", di, CreateStream, out warnings);

            foreach (Stream stream in Streams)
            {
                stream.Position = 0;
            }
        }



        public void Print()
        {
            PrintDocument printDocument = new PrintDocument();

            PrintDialog dlg = new PrintDialog();
            dlg.AllowCurrentPage = false;
            dlg.AllowPrintToFile = false;
            dlg.AllowSelection = true;
            dlg.AllowSomePages = false;
            dlg.PrintToFile = false;
            dlg.ShowHelp = false;
            dlg.ShowNetwork = false;

            if (parent != null)
            {

                if (dlg.ShowDialog(parent) != DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            printDocument.PrinterSettings = dlg.PrinterSettings;
            printDocument.DefaultPageSettings = dlg.PrinterSettings.DefaultPageSettings;

            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);

            try
            {
                printDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ReportPrinter_UnableToPrint) + e.Message, CallButler.Manager.Utils.PrivateLabelUtils.ReplaceProductName(Properties.LocalizedStrings.ReportPrinter_PrintError), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public void Dispose()
        {
            if (streams != null)
            {
                foreach (Stream stream in Streams)
                {
                    try
                    {
                        stream.Close();
                    }
                    catch { }
                }
                streams = null;
            }
        }
    }
}
