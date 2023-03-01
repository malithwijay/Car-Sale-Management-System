
namespace Car_Sale
{
    partial class vehicleReport
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
            this.VehicleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet_Vehicle = new Car_Sale.DataSet_Vehicle();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label_price = new System.Windows.Forms.Label();
            this.label_to = new System.Windows.Forms.Label();
            this.textBox_min = new System.Windows.Forms.TextBox();
            this.textBox_max = new System.Windows.Forms.TextBox();
            this.button_search = new System.Windows.Forms.Button();
            this.VehicleTableAdapter = new Car_Sale.DataSet_VehicleTableAdapters.VehicleTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.VehicleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Vehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // VehicleBindingSource
            // 
            this.VehicleBindingSource.DataMember = "Vehicle";
            this.VehicleBindingSource.DataSource = this.DataSet_Vehicle;
            // 
            // DataSet_Vehicle
            // 
            this.DataSet_Vehicle.DataSetName = "DataSet_Vehicle";
            this.DataSet_Vehicle.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.VehicleBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Car_Sale.vehicleReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(27, 91);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(925, 490);
            this.reportViewer1.TabIndex = 0;
            // 
            // label_price
            // 
            this.label_price.AutoSize = true;
            this.label_price.Location = new System.Drawing.Point(78, 30);
            this.label_price.Name = "label_price";
            this.label_price.Size = new System.Drawing.Size(31, 13);
            this.label_price.TabIndex = 1;
            this.label_price.Text = "Price";
            // 
            // label_to
            // 
            this.label_to.AutoSize = true;
            this.label_to.Location = new System.Drawing.Point(255, 30);
            this.label_to.Name = "label_to";
            this.label_to.Size = new System.Drawing.Size(20, 13);
            this.label_to.TabIndex = 2;
            this.label_to.Text = "To";
            // 
            // textBox_min
            // 
            this.textBox_min.Location = new System.Drawing.Point(131, 27);
            this.textBox_min.Name = "textBox_min";
            this.textBox_min.Size = new System.Drawing.Size(100, 20);
            this.textBox_min.TabIndex = 3;
            // 
            // textBox_max
            // 
            this.textBox_max.Location = new System.Drawing.Point(301, 27);
            this.textBox_max.Name = "textBox_max";
            this.textBox_max.Size = new System.Drawing.Size(100, 20);
            this.textBox_max.TabIndex = 4;
            // 
            // button_search
            // 
            this.button_search.Location = new System.Drawing.Point(434, 25);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(75, 23);
            this.button_search.TabIndex = 5;
            this.button_search.Text = "Search";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button_search_Click);
            // 
            // VehicleTableAdapter
            // 
            this.VehicleTableAdapter.ClearBeforeFill = true;
            // 
            // vehicleReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.button_search);
            this.Controls.Add(this.textBox_max);
            this.Controls.Add(this.textBox_min);
            this.Controls.Add(this.label_to);
            this.Controls.Add(this.label_price);
            this.Controls.Add(this.reportViewer1);
            this.Name = "vehicleReport";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "vehicleReport";
            this.Load += new System.EventHandler(this.vehicleReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VehicleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet_Vehicle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource VehicleBindingSource;
        private DataSet_Vehicle DataSet_Vehicle;
        private DataSet_VehicleTableAdapters.VehicleTableAdapter VehicleTableAdapter;
        private System.Windows.Forms.Label label_price;
        private System.Windows.Forms.Label label_to;
        private System.Windows.Forms.TextBox textBox_min;
        private System.Windows.Forms.TextBox textBox_max;
        private System.Windows.Forms.Button button_search;
    }
}