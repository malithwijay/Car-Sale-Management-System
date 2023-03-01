
namespace Car_Sale
{
    partial class billReport
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_from = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label_date = new System.Windows.Forms.Label();
            this.button_search = new System.Windows.Forms.Button();
            this.textBox_billNo = new System.Windows.Forms.TextBox();
            this.label_salesmanID = new System.Windows.Forms.Label();
            this.DataSet_Bill = new Car_Sale.DataSet_Bill();
            this.BillBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BillTableAdapter = new Car_Sale.DataSet_BillTableAdapters.BillTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Bill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.BillBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Car_Sale.Bill_Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(36, 120);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(923, 464);
            this.reportViewer1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Or";
            // 
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.Location = new System.Drawing.Point(413, 64);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_to.TabIndex = 34;
            // 
            // dateTimePicker_from
            // 
            this.dateTimePicker_from.Location = new System.Drawing.Point(141, 65);
            this.dateTimePicker_from.Name = "dateTimePicker_from";
            this.dateTimePicker_from.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_from.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "To";
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(61, 71);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(30, 13);
            this.label_date.TabIndex = 31;
            this.label_date.Text = "Date";
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(668, 61);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(75, 23);
            this.button_search.TabIndex = 30;
            this.button_search.Text = "Search";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click_1);
            // 
            // textBox_billNo
            // 
            this.textBox_billNo.Location = new System.Drawing.Point(141, 29);
            this.textBox_billNo.Name = "textBox_billNo";
            this.textBox_billNo.Size = new System.Drawing.Size(168, 20);
            this.textBox_billNo.TabIndex = 29;
            // 
            // label_salesmanID
            // 
            this.label_salesmanID.AutoSize = true;
            this.label_salesmanID.Location = new System.Drawing.Point(60, 32);
            this.label_salesmanID.Name = "label_salesmanID";
            this.label_salesmanID.Size = new System.Drawing.Size(37, 13);
            this.label_salesmanID.TabIndex = 28;
            this.label_salesmanID.Text = "Bill No";
            // 
            // DataSet_Bill
            // 
            this.DataSet_Bill.DataSetName = "DataSet_Bill";
            this.DataSet_Bill.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // BillBindingSource
            // 
            this.BillBindingSource.DataMember = "Bill";
            this.BillBindingSource.DataSource = this.DataSet_Bill;
            // 
            // BillTableAdapter
            // 
            this.BillTableAdapter.ClearBeforeFill = true;
            // 
            // billReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker_to);
            this.Controls.Add(this.dateTimePicker_from);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_date);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.textBox_billNo);
            this.Controls.Add(this.label_salesmanID);
            this.Controls.Add(this.reportViewer1);
            this.Name = "billReport";
            this.Text = "billReport";
            this.Load += new System.EventHandler(this.customerReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Bill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_to;
        private System.Windows.Forms.DateTimePicker dateTimePicker_from;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.TextBox textBox_billNo;
        private System.Windows.Forms.Label label_salesmanID;
        private System.Windows.Forms.BindingSource BillBindingSource;
        private DataSet_Bill DataSet_Bill;
        private DataSet_BillTableAdapters.BillTableAdapter BillTableAdapter;
    }
}