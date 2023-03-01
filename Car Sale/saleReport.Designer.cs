
namespace Car_Sale
{
    partial class saleReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.SaleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet_Sale = new Car_Sale.DataSet_Sale();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label_salesmanID = new System.Windows.Forms.Label();
            this.textBox_salesmanID = new System.Windows.Forms.TextBox();
            this.button_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label_date = new System.Windows.Forms.Label();
            this.dateTimePicker_from = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_to = new System.Windows.Forms.DateTimePicker();
            this.SaleTableAdapter = new Car_Sale.DataSet_SaleTableAdapters.SaleTableAdapter();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SaleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Sale)).BeginInit();
            this.SuspendLayout();
            // 
            // SaleBindingSource
            // 
            this.SaleBindingSource.DataMember = "Sale";
            this.SaleBindingSource.DataSource = this.DataSet_Sale;
            // 
            // DataSet_Sale
            // 
            this.DataSet_Sale.DataSetName = "DataSet_Sale";
            this.DataSet_Sale.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.SaleBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Car_Sale.Sale_Report.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(24, 101);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(936, 486);
            this.reportViewer1.TabIndex = 0;
            // 
            // label_salesmanID
            // 
            this.label_salesmanID.AutoSize = true;
            this.label_salesmanID.Location = new System.Drawing.Point(72, 29);
            this.label_salesmanID.Name = "label_salesmanID";
            this.label_salesmanID.Size = new System.Drawing.Size(67, 13);
            this.label_salesmanID.TabIndex = 1;
            this.label_salesmanID.Text = "Salesman ID";
            // 
            // textBox_salesmanID
            // 
            this.textBox_salesmanID.Location = new System.Drawing.Point(153, 26);
            this.textBox_salesmanID.Name = "textBox_salesmanID";
            this.textBox_salesmanID.Size = new System.Drawing.Size(168, 20);
            this.textBox_salesmanID.TabIndex = 3;
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(680, 58);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(75, 23);
            this.button_search.TabIndex = 5;
            this.button_search.Text = "Search";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "To";
            // 
            // label_date
            // 
            this.label_date.AutoSize = true;
            this.label_date.Location = new System.Drawing.Point(73, 68);
            this.label_date.Name = "label_date";
            this.label_date.Size = new System.Drawing.Size(30, 13);
            this.label_date.TabIndex = 11;
            this.label_date.Text = "Date";
            // 
            // dateTimePicker_from
            // 
            this.dateTimePicker_from.Location = new System.Drawing.Point(153, 62);
            this.dateTimePicker_from.Name = "dateTimePicker_from";
            this.dateTimePicker_from.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_from.TabIndex = 16;
            // 
            // dateTimePicker_to
            // 
            this.dateTimePicker_to.Location = new System.Drawing.Point(425, 61);
            this.dateTimePicker_to.Name = "dateTimePicker_to";
            this.dateTimePicker_to.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_to.TabIndex = 17;
            // 
            // SaleTableAdapter
            // 
            this.SaleTableAdapter.ClearBeforeFill = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Or";
            // 
            // saleReport
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
            this.Controls.Add(this.textBox_salesmanID);
            this.Controls.Add(this.label_salesmanID);
            this.Controls.Add(this.reportViewer1);
            this.Name = "saleReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "saleReport";
            this.Load += new System.EventHandler(this.saleReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SaleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Sale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label_salesmanID;
        private System.Windows.Forms.TextBox textBox_salesmanID;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.BindingSource SaleBindingSource;
        private DataSet_Sale DataSet_Sale;
        private DataSet_SaleTableAdapters.SaleTableAdapter SaleTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_date;
        private System.Windows.Forms.DateTimePicker dateTimePicker_from;
        private System.Windows.Forms.DateTimePicker dateTimePicker_to;
        private System.Windows.Forms.Label label2;
    }
}