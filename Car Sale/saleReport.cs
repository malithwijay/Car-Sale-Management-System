using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Sale
{
    public partial class saleReport : Form
    {
        public saleReport()
        {
            InitializeComponent();
        }

        private void saleReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet_Sale.Sale' table. You can move, or remove it, as needed.
            
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            try
            { 
            this.SaleTableAdapter.Fill(this.DataSet_Sale.Sale,textBox_salesmanID.Text, dateTimePicker_from.Value, dateTimePicker_to.Value);
            }

            catch(Exception)
            { }
            this.reportViewer1.RefreshReport();
        }

        
    }
}
