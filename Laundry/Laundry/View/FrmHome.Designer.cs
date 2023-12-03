namespace Laundry.View
{
    partial class FrmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHome));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTransaksi = new System.Windows.Forms.Button();
            this.btnLayanan = new System.Windows.Forms.Button();
            this.btnLaporan = new System.Windows.Forms.Button();
            this.btnKaryawan = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox1.Controls.Add(this.btnKaryawan);
            this.groupBox1.Controls.Add(this.btnLaporan);
            this.groupBox1.Controls.Add(this.btnLayanan);
            this.groupBox1.Controls.Add(this.btnTransaksi);
            this.groupBox1.Location = new System.Drawing.Point(-1, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnTransaksi
            // 
            this.btnTransaksi.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnTransaksi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTransaksi.BackgroundImage")));
            this.btnTransaksi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTransaksi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransaksi.ForeColor = System.Drawing.SystemColors.Window;
            this.btnTransaksi.Location = new System.Drawing.Point(98, 21);
            this.btnTransaksi.Name = "btnTransaksi";
            this.btnTransaksi.Size = new System.Drawing.Size(138, 140);
            this.btnTransaksi.TabIndex = 0;
            this.btnTransaksi.Text = "Transaksi";
            this.btnTransaksi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTransaksi.UseVisualStyleBackColor = false;
            // 
            // btnLayanan
            // 
            this.btnLayanan.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLayanan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLayanan.ForeColor = System.Drawing.SystemColors.Window;
            this.btnLayanan.Image = ((System.Drawing.Image)(resources.GetObject("btnLayanan.Image")));
            this.btnLayanan.Location = new System.Drawing.Point(255, 21);
            this.btnLayanan.Name = "btnLayanan";
            this.btnLayanan.Size = new System.Drawing.Size(138, 140);
            this.btnLayanan.TabIndex = 1;
            this.btnLayanan.Text = "Layanan";
            this.btnLayanan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLayanan.UseVisualStyleBackColor = false;
            // 
            // btnLaporan
            // 
            this.btnLaporan.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLaporan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaporan.ForeColor = System.Drawing.SystemColors.Window;
            this.btnLaporan.Image = ((System.Drawing.Image)(resources.GetObject("btnLaporan.Image")));
            this.btnLaporan.Location = new System.Drawing.Point(413, 22);
            this.btnLaporan.Name = "btnLaporan";
            this.btnLaporan.Size = new System.Drawing.Size(138, 140);
            this.btnLaporan.TabIndex = 2;
            this.btnLaporan.Text = "Laporan";
            this.btnLaporan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLaporan.UseVisualStyleBackColor = false;
            // 
            // btnKaryawan
            // 
            this.btnKaryawan.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnKaryawan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKaryawan.ForeColor = System.Drawing.SystemColors.Window;
            this.btnKaryawan.Image = ((System.Drawing.Image)(resources.GetObject("btnKaryawan.Image")));
            this.btnKaryawan.Location = new System.Drawing.Point(571, 24);
            this.btnKaryawan.Name = "btnKaryawan";
            this.btnKaryawan.Size = new System.Drawing.Size(138, 140);
            this.btnKaryawan.TabIndex = 3;
            this.btnKaryawan.Text = "Karyawan";
            this.btnKaryawan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnKaryawan.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(254, 212);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(296, 212);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmHome";
            this.Text = "FrmHome";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnKaryawan;
        private System.Windows.Forms.Button btnLaporan;
        private System.Windows.Forms.Button btnLayanan;
        private System.Windows.Forms.Button btnTransaksi;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}