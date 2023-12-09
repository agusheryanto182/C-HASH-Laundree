using Laundry.Controller;
using Laundry.Model.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laundry.View
{
    public partial class FrmReport : Form
    {
        private TransactionController tc;

        private CustomerController cc;

        private ServiceController sc;

        private EmployeeController ec;

        public FrmReport()
        {
            InitializeComponent();
            tc = new TransactionController();
            cc = new CustomerController();
            sc = new ServiceController();
            ec = new EmployeeController();
            InisialisasiDataGridView();
            LoadData();
        }


        // deklarasi field untuk meyimpan objek mahasiswa
        private List<Transactions> listOfTransaction = new List<Transactions>();

        private void InisialisasiDataGridView()
        {
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "No.";
            dataGridView1.Columns[0].Width = 35;
            dataGridView1.Columns[1].Name = "ID Transaction";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Name = "Nama Pelanggan";
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Name = "Layanan";
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Name = "Total";
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Name = "Tanggal Pemesanan";
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].Name = "Tanggal pengambilan";
            dataGridView1.Columns[6].Width = 200;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            // panggil method ReadAll dan tampung datanya ke dalam collection
            listOfTransaction = tc.ReadAll();
            // ekstrak objek mhs dari collection
            foreach (var t in listOfTransaction)
            {
                if (t.Status == "LUNAS")
                {
                    var noUrut = dataGridView1.Rows.Count;
                    dataGridView1.Rows.Add(
                        noUrut.ToString(),
                        t.Id,
                        cc.ReadById(t.CustomerId)?.Name,  // Menggunakan ?. untuk menghindari NullReferenceException
                        sc.ReadById(t.ServiceId)?.Name,   // Menggunakan ?. untuk menghindari NullReferenceException
                        t.Total.ToString(),
                        t.Order.ToString(),
                        t.Finish.ToString()
                    );
                }
            }
        }


        class ClsPrint
        {
            #region Variables

            int iCellHeight = 0; //Used to get/set the datagridview cell height
            int iTotalWidth = 0; //
            int iRow = 0;//Used as counter
            bool bFirstPage = false; //Used to check whether we are printing first page
            bool bNewPage = false;// Used to check whether we are printing a new page
            int iHeaderHeight = 0; //Used for the header height
            StringFormat strFormat; //Used to format the grid rows.
            ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
            ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
            private PrintDocument _printDocument = new PrintDocument();
            private DataGridView gw = new DataGridView();
            private string _ReportHeader;

            #endregion

            public ClsPrint(DataGridView gridview, string ReportHeader)
            {
                _printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                _printDocument.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);
                gw = gridview;
                _ReportHeader = ReportHeader;
            }

            public void PrintForm()
            {
                ////Open the print dialog
                //PrintDialog printDialog = new PrintDialog();
                //printDialog.Document = _printDocument;
                //printDialog.UseEXDialog = true;

                ////Get the document
                //if (DialogResult.OK == printDialog.ShowDialog())
                //{
                //    _printDocument.DocumentName = "Test Page Print";
                //    _printDocument.Print();
                //}

                //Open the print preview dialog
                PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
                objPPdialog.Document = _printDocument;
                objPPdialog.ShowDialog();
            }

            private void _printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
            {
                try
                {
                    //Set the left margin
                    int iLeftMargin = e.MarginBounds.Left;
                    //Set the top margin
                    int iTopMargin = e.MarginBounds.Top;
                    //Whether more pages have to print or not
                    bool bMorePagesToPrint = false;
                    int iTmpWidth = 0;

                    //For the first page to print set the cell width and header height
                    if (bFirstPage)
                    {
                        foreach (DataGridViewColumn GridCol in gw.Columns)
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                (double)iTotalWidth * (double)iTotalWidth *
                                ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                            // Save width and height of headers
                            arrColumnLefts.Add(iLeftMargin);
                            arrColumnWidths.Add(iTmpWidth);
                            iLeftMargin += iTmpWidth;
                        }
                    }

                    //Loop till all the grid rows not get printed
                    while (iRow <= gw.Rows.Count - 1)
                    {
                        DataGridViewRow GridRow = gw.Rows[iRow];
                        //Set the cell height
                        iCellHeight = GridRow.Height + 5;
                        int iCount = 0;

                        //Check whether the current page settings allow more rows to print
                        if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                        {
                            bNewPage = true;
                            bFirstPage = false;
                            bMorePagesToPrint = true;
                            break;
                        }
                        else
                        {
                            if (bNewPage)
                            {
                                //Draw Header
                                e.Graphics.DrawString(_ReportHeader,
                                    new Font(gw.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left,
                                    e.MarginBounds.Top - e.Graphics.MeasureString(_ReportHeader,
                                    new Font(gw.Font, FontStyle.Bold),
                                    e.MarginBounds.Width).Height - 13);

                                String strDate = "";
                                //Draw Date
                                e.Graphics.DrawString(strDate,
                                    new Font(gw.Font, FontStyle.Bold), Brushes.Black,
                                    e.MarginBounds.Left +
                                    (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                    new Font(gw.Font, FontStyle.Bold),
                                    e.MarginBounds.Width).Width),
                                    e.MarginBounds.Top - e.Graphics.MeasureString(_ReportHeader,
                                    new Font(new Font(gw.Font, FontStyle.Bold),
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                                //Draw Columns                 
                                iTopMargin = e.MarginBounds.Top;
                                DataGridViewColumn[] _GridCol = new DataGridViewColumn[gw.Columns.Count];
                                int colcount = 0;
                                //Convert ltr to rtl
                                foreach (DataGridViewColumn GridCol in gw.Columns)
                                {
                                    _GridCol[colcount++] = GridCol;
                                }
                                for (int i = 0; i < _GridCol.Count(); i++)
                                {
                                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawRectangle(Pens.Black,
                                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight));

                                    e.Graphics.DrawString(_GridCol[i].HeaderText,
                                        _GridCol[i].InheritedStyle.Font,
                                        new SolidBrush(_GridCol[i].InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                    iCount++;
                                }
                                bNewPage = false;
                                iTopMargin += iHeaderHeight;
                            }

                            iCount = 0;
                            DataGridViewCell[] _GridCell = new DataGridViewCell[GridRow.Cells.Count];
                            int cellcount = 0;
                            //Convert ltr to rtl
                            foreach (DataGridViewCell Cel in GridRow.Cells)
                            {
                                _GridCell[cellcount++] = Cel;
                            }

                            //Draw Columns Contents                
                            for (int i = 0; i < _GridCell.Count(); i++)
                            {
                                if (_GridCell[i].Value != null)
                                {
                                    string cellText = _GridCell[i].FormattedValue.ToString();

                                    // Format the date in the Tanggal Pemesanan and Tanggal Pengambilan columns
                                    if (_GridCell[i].OwningColumn.HeaderText.Contains("Tanggal"))
                                    {
                                        DateTime dateValue;
                                        if (DateTime.TryParse(cellText, out dateValue))
                                        {
                                            // Jika nama kolom mengandung "Pemesanan" atau "Pengambilan", tampilkan hanya tanggal
                                            if (_GridCell[i].OwningColumn.HeaderText.Contains("Pemesanan") ||
                                                _GridCell[i].OwningColumn.HeaderText.Contains("Pengambilan"))
                                            {
                                                cellText = dateValue.ToString("dd/MM/yyyy");
                                            }
                                            else
                                            {
                                                // Jika bukan kolom tanggal, tampilkan format tanggal dan waktu
                                                cellText = dateValue.ToString("dd/MM/yyyy");
                                            }
                                        }
                                    }

                                    e.Graphics.DrawString(cellText,
                                        _GridCell[i].InheritedStyle.Font,
                                        new SolidBrush(_GridCell[i].InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount],
                                        (float)iTopMargin,
                                        (int)arrColumnWidths[iCount], (float)iCellHeight),
                                        strFormat);
                                }

                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight));
                                iCount++;
                            }

                        }

                        iRow++;
                        iTopMargin += iCellHeight;
                    }

                    //If more lines exist, print another page.
                    if (bMorePagesToPrint)
                        e.HasMorePages = true;
                    else
                        e.HasMorePages = false;
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }




            private void _printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
            {
                try
                {
                    strFormat = new StringFormat();
                    strFormat.Alignment = StringAlignment.Center;
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Trimming = StringTrimming.EllipsisCharacter;

                    arrColumnLefts.Clear();
                    arrColumnWidths.Clear();
                    iCellHeight = 0;
                    iRow = 0;
                    bFirstPage = true;
                    bNewPage = true;

                    // Calculating Total Widths
                    iTotalWidth = 0;
                    foreach (DataGridViewColumn dgvGridCol in gw.Columns)
                    {
                        iTotalWidth += dgvGridCol.Width;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void printer()
        {
            ClsPrint _ClsPrint = new ClsPrint(dataGridView1, "Laporan Transaksi");
            _ClsPrint.PrintForm();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printer();
        }
    }
}
